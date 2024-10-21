using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    internal class DISCOUNTService
    {
        public List<DISCOUNT> GetAll()
        {
            CAFEModel model = new CAFEModel();
            return model.DISCOUNTs.ToList();
        }
    }
}
