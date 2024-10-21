using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class DISCOUNTService
    {
        public List<DISCOUNT> GetAll()
        {
            CAFEModel model = new CAFEModel();
            return model.DISCOUNTs.ToList();
        }
        public (bool result, string message) Add(DISCOUNT dis)
        {
            try
            {
              
                CAFEModel model = new CAFEModel();
                model.DISCOUNTs.Add(dis);
                model.SaveChanges();
                return (true, "Thêm khuyến mãi thành công.");

            }
            catch (Exception ex)
            {
                return (false, $"Lỗi thêm khuyến mãi: {ex.Message}");
            }
        }
        public (bool result, string message) Update(DISCOUNT dis)
        {
            try
            {
                using (CAFEModel model = new CAFEModel())
                {
                    var exdiscount= model.DISCOUNTs.Find(dis.IDDIS);
                    if (exdiscount == null)
                    {
                        return (false, "Mã khuyến mãi không tồn tại.");
                    }

                    exdiscount.NAME = dis.NAME;
                    exdiscount.DISCOUNT_PERCENTAGE = dis.DISCOUNT_PERCENTAGE;
                    exdiscount.DATE_START = dis.DATE_START;
                    exdiscount.DATE_FINISH = dis.DATE_FINISH;
                    model.SaveChanges();
                }
                return (true, "Cập nhật thông tin khuyến mãi thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi cập nhật khuyến mãi: {ex.Message}");
            }
        }
        public (bool result, string message) DeleteById(int id)
        {
            try
            {
                using (CAFEModel model = new CAFEModel())
                {
                    var discounts = model.DISCOUNTs.Find(id);
                    if (discounts == null)
                    {
                        return (false, "Mã khuyến mãi không tồn tại.");
                    }

                    model.DISCOUNTs.Remove(discounts);
                    model.SaveChanges();
                }
                return (true, "Xóa khuyến mãi thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi xóa khuyến mãi: {ex.Message}");
            }
        }
        public List<DISCOUNT> FindByName(string name)
        {
            using (CAFEModel model = new CAFEModel())
            {
                return model.DISCOUNTs
                            .Where(e => e.NAME.Contains(name)) 
                            .ToList();
            }
        }
        public DISCOUNT FindBynvId(int id)
        {
            using (CAFEModel model = new CAFEModel())
            {
                return model.DISCOUNTs.Find(id);
            }
        }
    }
}
