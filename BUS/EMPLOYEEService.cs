using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class EMPLOYEEService
    {
        public List<EMPLOYEE> GetAll()
        {
            using (CAFEModel model = new CAFEModel())
            {
                return model.EMPLOYEE.ToList();
            }
        }

        // Thêm một nhân viên mới
        public (bool result, string message) Add(EMPLOYEE employee)
        {
            try
            {
                using (CAFEModel model = new CAFEModel())
                {
                    model.EMPLOYEE.Add(employee);
                    model.SaveChanges();
                }
                return (true, "Thêm nhân viên thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi thêm nhân viên: {ex.Message}");
            }
        }

        // Cập nhật thông tin nhân viên
        public (bool result, string message) Update(EMPLOYEE employee)
        {
            try
            {
                using (CAFEModel model = new CAFEModel())
                {
                    var existingEmployee = model.EMPLOYEE.Find(employee.IDEMPLOYEE);
                    if (existingEmployee == null)
                    {
                        return (false, "Nhân viên không tồn tại.");
                    }

                    // Cập nhật thông tin nhân viên
                    existingEmployee.NAME = employee.NAME;
                    existingEmployee.POSITION = employee.POSITION;
                    existingEmployee.SALARY = employee.SALARY;
                    existingEmployee.DATE_HIRE = employee.DATE_HIRE;
                    existingEmployee.AVATAREMPLOYEE = employee.AVATAREMPLOYEE;

                    model.SaveChanges();
                }
                return (true, "Cập nhật thông tin nhân viên thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi cập nhật nhân viên: {ex.Message}");
            }
        }

        // Xóa nhân viên theo ID
        public (bool result, string message) DeleteById(int id)
        {
            try
            {
                using (CAFEModel model = new CAFEModel())
                {
                    var employee = model.EMPLOYEE.Find(id);
                    if (employee == null)
                    {
                        return (false, "Nhân viên không tồn tại.");
                    }

                    model.EMPLOYEE.Remove(employee);
                    model.SaveChanges();
                }
                return (true, "Xóa nhân viên thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi xóa nhân viên: {ex.Message}");
            }
        }

        public List<EMPLOYEE> FindByName(string name)
        {
            using (CAFEModel model = new CAFEModel())
            {
                return model.EMPLOYEE
                            .Where(e => e.NAME.Contains(name)) // Adjust this condition as needed
                            .ToList();
            }
        }
        public EMPLOYEE FindBynvId(int id)
        {
            using (CAFEModel model = new CAFEModel())
            {
                return model.EMPLOYEE.Find(id);
            }
        }
    }
}
