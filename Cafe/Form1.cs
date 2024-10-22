﻿using BUS;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafe
{
    public partial class Form1 : Form
    {
        private readonly ACCOUNTService accountService = new ACCOUNTService();

        public Form1()
        {
            InitializeComponent();
            txttmk.UseSystemPasswordChar = true;
        }

        private void btndn_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txttdn.Text.Trim();
                string password = txttmk.Text.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<ACCOUNT> dn = accountService.FindById(username);
                if (dn != null && dn.Count > 0 && dn[0].PASSWORD == password)
                {

                    Menu khoa = new Menu();
                    khoa.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                Application.Exit();
            }
            else
                return;
        }
    }
}
