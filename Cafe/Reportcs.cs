using BUS;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.Entities;


namespace Cafe
{
    public partial class Reportcs : Form
    {
        public int _idBill;

        public Reportcs()
        {
            InitializeComponent();
        }
        public Reportcs(int IDBILL)
        {
            InitializeComponent();
            _idBill = IDBILL;

        }
        private void Reportcs_Load(object sender, EventArgs e)
        {

            LoadReport(_idBill); 
        }
        
        private void LoadReport(int idBill)
        {

          
            var dshd = BILLService.Instance.GetAll();
            var dsMenu = MENUService.Instance.GetAll();

           
            var cthd = from hd in dshd
                       from billInfo in hd.BILLINFOes
                       join menu in dsMenu on billInfo.IDMENU equals menu.IDMENU
                       where hd.IDBILL.Equals(idBill) 
                       select new
                       {
                           hd.IDBILL,
                           NAME = menu.NAME,
                           COUNT = billInfo.COUNT,
                           PRICE = menu.PRICE,
                             billInfo.BILL.dateCheckIn,
                           billInfo.BILL.dateCheckOut,
                           hd.IDTABLE
                       };

            reportViewer1.LocalReport.ReportPath = "Report1.rdlc";
            var sourcecthd = new ReportDataSource("DataSet1", cthd.ToList());
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(sourcecthd);
            reportViewer1.RefreshReport();
        }
    }
}
