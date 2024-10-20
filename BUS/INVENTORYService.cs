using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    internal class INVENTORYService
    {
        public List<INVENTORY> GetAll()
        {
            CAFEModel model = new CAFEModel();
            return model.INVENTORY.ToList();
        }
    }
}
