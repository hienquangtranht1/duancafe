using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BILLService
    {
        public List<BILL> GetAll()
        {
            CAFEModel model = new CAFEModel();
            return model.BILLs.ToList();
        }
        private static BILLService instance;
        public static BILLService Instance
        {
            get
            {
                if (instance == null) instance = new BILLService();
                return instance;
            }
            private set
            {
                instance = value;
            }
        }



        public List<BILL> GetBillListByDate(DateTime checkIn, DateTime checkOut)
        {
            using (var model = new CAFEModel())
            {
                var billList = model.BILLs
                                    .Include(b => b.TABLECOFFEE)
                                    .Include(b => b.BILLINFOes.Select(bi => bi.MENU))
                                    .Where(b => DbFunctions.TruncateTime(b.dateCheckIn) >= DbFunctions.TruncateTime(checkIn)
                                             && DbFunctions.TruncateTime(b.dateCheckOut) <= DbFunctions.TruncateTime(checkOut))
                                    .ToList();
                return billList;
            }
        }
        public (bool result, string message) Add(BILL newBill)
        {
            try
            {
                CAFEModel model = new CAFEModel();
                model.BILLs.Add(newBill);
                model.SaveChanges();
                return (true, "Thêm hóa đơn thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi thêm hóa đơn: {ex.Message}");
            }
        }
        public int GenerateNewBillId()
        {
            using (var model = new CAFEModel())
            {
                int maxBillId = model.BILLs.Any() ? model.BILLs.Max(b => b.IDBILL) : 0;
                return maxBillId + 1;
            }
        }
        
    }

}

