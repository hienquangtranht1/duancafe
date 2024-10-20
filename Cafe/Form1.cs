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
        public Form1()
        {
            InitializeComponent();
        }

        private void btndn_Click(object sender, EventArgs e)
        {
            Menu khoa = new Menu();
            khoa.Show();
            khoa.FormClosed += (s, args) => this.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
