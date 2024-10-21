using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace BUS
{
    public class ACCOUNTService
    {
        public List<ACCOUNT> GetAll()
        {
            CAFEModel model = new CAFEModel();
            
                return model.ACCOUNTs.ToList(); 
            
        }

        public string GetMaxID()
        {
            using (CAFEModel model = new CAFEModel())
            {
                return model.ACCOUNTs.Any() ? model.ACCOUNTs.Max(m => m.USERNAME) : "0";
            }
        }

        public (bool success, string message) Add(ACCOUNT account)
        {
            try
            {
                using (CAFEModel model = new CAFEModel())
                {
                    model.ACCOUNTs.Add(account);
                    model.SaveChanges();
                }
                return (true, "Thêm tài khoản thành công.");
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

        public List < ACCOUNT> FindById(string username)
        {
            CAFEModel model = new CAFEModel();

            return model.ACCOUNTs.Where(e => e.USERNAME == username).ToList();

        }

        public (bool success, string message) DeleteById(string username)
        {
            try
            {
                using (CAFEModel model = new CAFEModel())
                {
                    var accountItem = model.ACCOUNTs.SingleOrDefault(m => m.USERNAME == username);
                    if (accountItem != null)
                    {
                        model.ACCOUNTs.Remove(accountItem);
                        model.SaveChanges();
                        return (true, "Xóa tài khoản thành công.");
                    }
                    else
                    {
                        return (false, "Không tìm thấy tài khoản để xóa.");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, $"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        public (bool success, string message) Update(ACCOUNT account)
        {
            if (string.IsNullOrWhiteSpace(account.PASSWORD))
            {
                return (false, "Mật khẩu không được để trống.");
            }

            using (CAFEModel model = new CAFEModel())
            {
                var existingAccountItem = model.ACCOUNTs.SingleOrDefault(m => m.USERNAME == account.USERNAME);
                if (existingAccountItem == null)
                {
                    return (false, "Không tìm thấy tài khoản để cập nhật.");
                }

                existingAccountItem.PASSWORD = account.PASSWORD;
                existingAccountItem.IDTYPETK = account.IDTYPETK;
                existingAccountItem.IDEMPLOYEE = account.IDEMPLOYEE;

                try
                {
                    model.SaveChanges();
                    return (true, "Cập nhật tài khoản thành công.");
                }
                catch (DbUpdateException dba)
                {
                    var innerEx = dba.InnerException?.Message ?? dba.Message;
                    return (false, $"Đã xảy ra lỗi khi cập nhật cơ sở dữ liệu: {innerEx}");
                }
                catch (Exception ex)
                {
                    return (false, $"Đã xảy ra lỗi: {ex.Message}");
                }
            }
        }
    }
}
