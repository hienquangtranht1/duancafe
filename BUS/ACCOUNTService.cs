using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class ACCOUNTService
    {
        public List<ACCOUNT> GetAll()
        {
            CAFEModel model = new CAFEModel();
            return model.ACCOUNT.ToList();
        }
    }
}
