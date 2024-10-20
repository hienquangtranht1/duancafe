using DAL.Entities;
using System;
using System.Collections.Generic;
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
    }
}
