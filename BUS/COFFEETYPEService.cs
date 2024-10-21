using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class COFFEETYPEService
    {
        public List<COFFEETYPE> GetAll()
        {
            using (CAFEModel model = new CAFEModel())
            {
                return model.COFFEETYPEs.ToList();
            }
        }
        public int GetMaxID()
        {
            using (CAFEModel model = new CAFEModel())
            {

                return model.COFFEETYPEs.Any() ? model.COFFEETYPEs.Max(t => t.IDTYPE) : 0;
            }
        }

        public (bool success, string message) Add(COFFEETYPE type)
        {
            try
            {
                using (CAFEModel model = new CAFEModel())
                {
                    model.COFFEETYPEs.Add(type);
                    model.SaveChanges();
                }
                return (true, "Thêm bàn thành công.");
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

        public COFFEETYPE FindById(int idtype)
        {
            using (CAFEModel model = new CAFEModel())
            {

                return model.COFFEETYPEs.SingleOrDefault(t => t.IDTYPE == idtype);
            }
        }

        public (bool success, string message) DeleteById(int idtype)
        {
            try
            {
                using (CAFEModel model = new CAFEModel())
                {
                    var typeToDelete = model.COFFEETYPEs.SingleOrDefault(t => t.IDTYPE == idtype);
                    if (typeToDelete != null)
                    {
                        model.COFFEETYPEs.Remove(typeToDelete);
                        model.SaveChanges();
                        return (true, "Xóa bàn thành công.");
                    }
                    else
                    {
                        return (false, "Không tìm thấy bàn để xóa.");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, $"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        public (bool success, string message) Update(COFFEETYPE type)
        {

            if (string.IsNullOrWhiteSpace(type.NAME))
            {
                return (false, "Tên bàn không được để trống.");
            }

            using (CAFEModel model = new CAFEModel())
            {

                var existingtype = model.COFFEETYPEs.SingleOrDefault(t => t.IDTYPE == type.IDTYPE);
                if (existingtype == null)
                {
                    return (false, "Không tìm thấy bàn để cập nhật.");
                }
                existingtype.NAME = type.NAME;
                existingtype.ORIGIN = type.ORIGIN;
                existingtype.NSX = type.NSX;

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
            return (true, "Cập nhật bàn thành công.");
        }
    }
}
