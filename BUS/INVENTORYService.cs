using DAL.Entities;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BUS
{
    public class INVENTORYService
    {
        private readonly CAFEModel inventoryID;
        public List<INVENTORY> GetAll()
        {
            CAFEModel model = new CAFEModel();
            return model.INVENTORies.ToList();
        }
        public INVENTORYService()
        {
            inventoryID = new CAFEModel();
        }
        public INVENTORY GetById(int id)
        {
            return inventoryID.INVENTORies.FirstOrDefault(i => i.IDINVENTORY == id);
        }
        public (bool result, string message) Add(INVENTORY inventory)
        {
            try
            {
                using (CAFEModel model = new CAFEModel())
                {
                    model.INVENTORies.Add(inventory);
                    model.SaveChanges();
                }
                return (true, "Thêm hàng hóa thành công.");
            }
            catch (DbUpdateException dbEx)
            {
                return (false, $"Lỗi thêm hàng hóa: {dbEx.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi thêm hàng hóa: {ex.Message}");
            }
        }

        public List<INVENTORY> FindById(string username)
        {
            CAFEModel model = new CAFEModel();


            if (int.TryParse(username, out int idType))
            {
                return model.INVENTORies.Where(i => i.IDINVENTORY == idType).ToList();
            }
            else
            {

                return new List<INVENTORY>();
            }
        }


        public (bool result, string message) DeleteById(int idinventory)
        {
            try
            {
                using (CAFEModel model = new CAFEModel())
                {
                    var inventoryToDelete = model.INVENTORies.SingleOrDefault(i => i.IDINVENTORY == idinventory);
                    if (inventoryToDelete != null)
                    {
                        model.INVENTORies.Remove(inventoryToDelete);
                        model.SaveChanges();
                        return (true, "Xóa hàng hóa thành công.");
                    }
                    else
                    {
                        return (false, "Không tìm thấy hàng hóa để xóa.");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, $"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        public (bool, string) Update(INVENTORY inventory)
        {
            var existingInventory = inventoryID.INVENTORies.Find(inventory.IDINVENTORY);
            if (existingInventory != null)
            {
                existingInventory.IDTYPE = inventory.IDTYPE;
                existingInventory.QUANTITY = inventory.QUANTITY;
                existingInventory.DATE_RECEIVED = inventory.DATE_RECEIVED;
                existingInventory.DATE_EXPIRED = inventory.DATE_EXPIRED;
                inventoryID.SaveChanges();
                return (true, "Cập nhật thành công!");
            }
            return (false, "Không tìm thấy mặt hàng để cập nhật.");
        }
    }
}

