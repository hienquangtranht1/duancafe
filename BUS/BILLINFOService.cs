using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BILLINFOService
    {
        public List<BILLINFO> GetAll()
        {
            CAFEModel model = new CAFEModel();
            return model.BILLINFO.ToList();
        }
        
    }
}
