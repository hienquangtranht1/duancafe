using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DAL.Entities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Cafe
{
    public partial class Admin : Form
    {
        private readonly MENUService menuService = new MENUService();
        private readonly COFFEETYPEService cOFFEETYPEService = new COFFEETYPEService();
        private readonly ACCOUNTService accountService = new ACCOUNTService();
        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            try
            {
                setGridViewStyle(dtgvFood);
                var listmenu = menuService.GetAll();
                var listcoffee = cOFFEETYPEService.GetAll();
                FillCoffeetypeCombobox(listcoffee);
                BindGrid(listmenu);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void setGridViewStyle(DataGridView dgview)
        {
            dgview.BorderStyle = BorderStyle.None;
            dgview.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgview.BackgroundColor = Color.White;
            dgview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void FillCoffeetypeCombobox(List<COFFEETYPE> listcoffee)
        {
            listcoffee.Insert(0, new COFFEETYPE());
            this.cboloaisp.DataSource = listcoffee;
            this.cboloaisp.DisplayMember = "NAME"; 
            this.cboloaisp.ValueMember = "IDTYPE";
        }
        private void FillAccount(List<ACCOUNT> listaccount)
        {
            listaccount.Insert(0, new ACCOUNT());
            this.cboloaitk.DataSource = listaccount;
            this.cboloaitk.DisplayMember = "NAME";
            this.cboloaitk.ValueMember = "IDTYPE";
        }
        private void BindGrid(List<MENU> listmenus)
        {
            dtgvFood.Rows.Clear();
            foreach (var item in listmenus)
            {
                int index = dtgvFood.Rows.Add();
                dtgvFood.Rows[index].Cells[0].Value = item.IDMENU;
                
                dtgvFood.Rows[index].Cells[2].Value = item.NAME;
                if (item.COFFEETYPE != null)
                {
                    dtgvFood.Rows[index].Cells[3].Value = item.COFFEETYPE.NAME;
                }
                dtgvFood.Rows[index].Cells[4].Value = item.PRICE;
              

            }
        }
    

        private void btnthemanhmenu_Click(object sender, EventArgs e)
        {

        }
    }
}
