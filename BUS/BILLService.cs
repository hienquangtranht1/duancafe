﻿using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BILLService
    {
        public List<BILL> GetAll()
        {
            CAFEModel model = new CAFEModel();
            return model.BILLs.ToList();
        }
        private static BILLService instance;
        public static BILLService Instance
        {
            get
            {
                if (instance == null) instance = new BILLService();
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

       

        public List<BILL> GetBillListByDate(DateTime checkIn, DateTime checkOut)
        {
            using (var model = new CAFEModel())
            {
           
                var billList = model.BILLs
                                    .Include(b => b.TABLECOFFEE)
                                    .Include(b => b.BILLINFOes.Select(bi => bi.MENU)) 
                                    .Where(b => b.dateCheckIn >= checkIn && b.dateCheckOut <= checkOut)
                                    .ToList();
                return billList;
            }
        }
    }

}

