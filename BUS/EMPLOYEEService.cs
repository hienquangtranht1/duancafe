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
                return model.EMPLOYEEs.ToList();
            }
        }

     
        public (bool result, string message) Add(EMPLOYEE employee)
        {
            try
            {
                using (CAFEModel model = new CAFEModel())
                {
                    model.EMPLOYEEs.Add(employee);
                    model.SaveChanges();
                }
                return (true, "Thêm nhân viên thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi thêm nhân viên: {ex.Message}");
            }
        }

        
        public (bool result, string message) Update(EMPLOYEE employee)
        {
            try
            {
                using (CAFEModel model = new CAFEModel())
                {
                    var existingEmployee = model.EMPLOYEEs.Find(employee.IDEMPLOYEE);
                    if (existingEmployee == null)
                    {
                        return (false, "Nhân viên không tồn tại.");
                    }

                    
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

       
        public (bool result, string message) DeleteById(int id)
        {
            try
            {
                using (CAFEModel model = new CAFEModel())
                {
                    var employee = model.EMPLOYEEs.Find(id);
                    if (employee == null)
                    {
                        return (false, "Nhân viên không tồn tại.");
                    }

                    model.EMPLOYEEs.Remove(employee);
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
                return model.EMPLOYEEs
                            .Where(e => e.NAME.Contains(name)) 
                            .ToList();
            }
        }
        public EMPLOYEE FindBynvId(int id)
        {
            using (CAFEModel model = new CAFEModel())
            {
                return model.EMPLOYEEs.Find(id);
            }
        }
    }
}
