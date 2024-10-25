using BUS;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Input;
using System.IO;

namespace Cafe
{
    public partial class Menu : Form
    {
        private readonly ACCOUNTService aCCOUNTService = new ACCOUNTService();
        private readonly TYPEACCOUNTService aTYPEACCOUNTService = new TYPEACCOUNTService();
        private string currentUsername;
        private readonly TABLECOFFEEService table = new TABLECOFFEEService();
        private List<TABLECOFFEE> allTable;
        private readonly COFFEETYPEService cOFFEETYPEService = new COFFEETYPEService();
        private readonly MENUService mENUService = new MENUService();
        private readonly DISCOUNTService dISCOUNTService = new DISCOUNTService();
        private readonly BILLService bILLService = new BILLService();
        private readonly BILLINFOService bILLINFOService = new BILLINFOService();
        public Menu(string username)
        {

            InitializeComponent();
            currentUsername = username;
        }


        private void quảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var account = aCCOUNTService.GetCurrentUser(currentUsername);

            if (account == null)
            {
                MessageBox.Show("Không tìm thấy tài khoản. Vui lòng đăng nhập lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (account.IDTYPETK == 1)
            {
                Admin adminForm = new Admin(currentUsername);
                this.Hide();
                adminForm.ShowDialog();


            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào phần này.", "Truy cập bị từ chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                Application.Exit();
            }
            else
                return;
        }

        private void ddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            this.Hide();
            fm.ShowDialog();
            this.Show();
        }

    }
}