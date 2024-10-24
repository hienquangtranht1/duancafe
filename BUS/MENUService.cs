using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
        public  class MENUService
    {
        public List<MENU> GetAll()
        {
            CAFEModel model = new CAFEModel();
            return model.MENUs.ToList();
        }
        public int GetMaxID()
        {
            using (CAFEModel model = new CAFEModel())
            {
                
                return model.MENUs.Any() ? model.MENUs.Max(m => m.IDMENU) : 0;
            }
        }

        public (bool success, string message) Add(MENU menu)
        {
            try
            {
                using (CAFEModel model = new CAFEModel())
                {
                    model.MENUs.Add(menu);
                    model.SaveChanges();
                }
                return (true, "Thêm món thành công.");
            }
            catch (DbUpdateException dbEx)
            {
                var innerEx = dbEx.InnerException?.Message ?? dbEx.Message;
                return (false, $"Đã xảy ra lỗi khi cập nhật cơ sở dữ liệu: {innerEx}");
            }
            catch (Exception ex)
            {
                return (false, $"Đã xảy ra lỗi: {ex.Message}");
            }
        }
        public MENU FindById(int menuID)
        {
            using (CAFEModel model = new CAFEModel())
            {
               
                return model.MENUs.SingleOrDefault(m => m.IDMENU == menuID);
            }
        }
        public (bool success, string message) DeleteById(int menuID)
        {
            try
            {
                using (CAFEModel model = new CAFEModel())
                {
                    var menuItem = model.MENUs.SingleOrDefault(m => m.IDMENU == menuID);
                    if (menuItem != null)
                    {
                        model.MENUs.Remove(menuItem);
                        model.SaveChanges();
                        return (true, "Xóa món ăn thành công.");
                    }
                    else
                    {
                        return (false, "Không tìm thấy món ăn để xóa.");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, $"Đã xảy ra lỗi: {ex.Message}");
            }
        }
        public (bool success, string message) Update(MENU menu)
        {
            
            if (string.IsNullOrWhiteSpace(menu.NAME))
            {
                return (false, "Tên món ăn không được để trống.");
            }

            if (menu.PRICE <= 0)
            {
                return (false, "Giá món ăn phải lớn hơn 0.");
            }

            using (CAFEModel model = new CAFEModel())
            {
                
                var existingMenuItem = model.MENUs.SingleOrDefault(m => m.IDMENU == menu.IDMENU);
                if (existingMenuItem == null)
                {
                    return (false, "Không tìm thấy món ăn để cập nhật.");
                }

                
                existingMenuItem.NAME = menu.NAME;
                existingMenuItem.PRICE = menu.PRICE;
                existingMenuItem.IDTYPE = menu.IDTYPE;
                existingMenuItem.AVATARMENU = menu.AVATARMENU;

                try
                {
                    model.SaveChanges();
                }
                catch (DbUpdateException dbEx)
                {
                    var innerEx = dbEx.InnerException?.Message ?? dbEx.Message;
                    return (false, $"Đã xảy ra lỗi khi cập nhật cơ sở dữ liệu: {innerEx}");
                }
                catch (Exception ex)
                {
                    return (false, $"Đã xảy ra lỗi: {ex.Message}");
                }
            }
            return (true, "Cập nhật món ăn thành công.");
        }
    }
}
    
   

