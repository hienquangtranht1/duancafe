using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class TABLECOFFEEService
    {
        public List<TABLECOFFEE> GetAll()
        {
            using (CAFEModel model = new CAFEModel())
            {
                return model.TABLECOFFEEs.ToList();
            }
        }
        public int GetMaxID()
        {
            using (CAFEModel model = new CAFEModel())
            {
                return model.TABLECOFFEEs.Any() ? model.TABLECOFFEEs.Max(t => t.IDTABLE) : 0;
            }
        }

        public (bool success, string message) Add(TABLECOFFEE table)
        {
            try
            {
                using (CAFEModel model = new CAFEModel())
                {
                    model.TABLECOFFEEs.Add(table);
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

        public TABLECOFFEE FindById(int tableId)
        {
            using (CAFEModel model = new CAFEModel())
            {
                return model.TABLECOFFEEs.SingleOrDefault(t => t.IDTABLE == tableId);
            }
        }

        public (bool success, string message) DeleteById(int tableId)
        {
            try
            {
                using (CAFEModel model = new CAFEModel())
                {
                    var tableToDelete = model.TABLECOFFEEs.SingleOrDefault(t => t.IDTABLE == tableId);
                    if (tableToDelete != null)
                    {
                        model.TABLECOFFEEs.Remove(tableToDelete);
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

        public (bool success, string message) Update(TABLECOFFEE table)
        {
            if (string.IsNullOrWhiteSpace(table.NAME))
            {
                return (false, "Tên bàn không được để trống.");
            }

            using (CAFEModel model = new CAFEModel())
            {
                var existingTable = model.TABLECOFFEEs.SingleOrDefault(t => t.IDTABLE == table.IDTABLE);
                if (existingTable == null)
                {
                    return (false, "Không tìm thấy bàn để cập nhật.");
                }

                existingTable.NAME = table.NAME;
                existingTable.STATUS = table.STATUS;

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
