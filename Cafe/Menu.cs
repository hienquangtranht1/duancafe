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

namespace Cafe
{
    public partial class Menu : Form
    {
        private readonly ACCOUNTService aCCOUNTService = new ACCOUNTService();
        private readonly TYPEACCOUNTService aTYPEACCOUNTService = new TYPEACCOUNTService();
        private string currentUsername;
        public Menu(string username) 
        {
            
            InitializeComponent();
            currentUsername = username; 
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }



        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void quảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var account = aCCOUNTService.GetCurrentUser(currentUsername); // Lấy thông tin tài khoản

            if (account == null)
            {
                MessageBox.Show("Không tìm thấy tài khoản. Vui lòng đăng nhập lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (account.IDTYPETK == 1) // Kiểm tra quyền admin
            {
                Admin adminForm = new Admin(currentUsername);
                this.Hide();
                adminForm.ShowDialog();
                this.Show();
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
