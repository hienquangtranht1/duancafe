using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BILLINFOService
    {
        public List<BILLINFO> GetAll()
        {
            using (CAFEModel model = new CAFEModel())
            {
                return model.BILLINFOes.ToList();
            }
        }
        public (bool result, string message) Add(BILLINFO newBillInfo)
        {
            try
            {
                using (CAFEModel model = new CAFEModel())
                {
                    // Kiểm tra xem thông tin chi tiết hóa đơn đã tồn tại chưa (nếu cần)
                    var existingBillInfo = model.BILLINFOes
                        .FirstOrDefault(bi => bi.IDBILL == newBillInfo.IDBILL && bi.IDMENU == newBillInfo.IDMENU);

                    if (existingBillInfo != null)
                    {
                        return (false, "Thông tin hóa đơn này đã tồn tại.");
                    }

                    // Thêm mới thông tin chi tiết hóa đơn
                    model.BILLINFOes.Add(newBillInfo);
                    model.SaveChanges();
                    return (true, "Thêm chi tiết hóa đơn thành công.");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi thêm chi tiết hóa đơn: {ex.Message}");
            }
        }
        public int GenerateNewBillInfoId()
        {
            using (var model = new CAFEModel())
            {
                int maxBillInfoId = model.BILLINFOes.Any() ? model.BILLINFOes.Max(bi => bi.IDINFO) : 0;
                return maxBillInfoId + 1;
            }
        }
    }
}