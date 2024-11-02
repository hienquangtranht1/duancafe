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
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }



        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void ShowAvatar(int menuID)
        {
            string folderPath = Path.Combine(Application.StartupPath, "Images");
            var menuItem = mENUService.FindById(menuID);
            if (menuItem != null && !string.IsNullOrEmpty(menuItem.AVATARMENU))
            {
                string avatarFilePath = Path.Combine(folderPath, menuItem.AVATARMENU);
                if (File.Exists(avatarFilePath))
                {
                    pictureBox1.Image = Image.FromFile(avatarFilePath);
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
        }
        private void quảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var account = aCCOUNTService.GetCurrentUser(currentUsername); // Lấy thông tin tài khoản

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
                adminForm.FormClosed += (s, args) =>
                {
                    var listtable = table.GetAll();
                    BindGridTable(listtable);
                    var listtype = cOFFEETYPEService.GetAll();
                    FillCoffeeTypes();
                };

            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào phần này.", "Truy cập bị từ chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private void BindGridTable(List<TABLECOFFEE> table)
        {
            dataGridView2.Rows.Clear();
            var emptyTables = table.Where(t => t.STATUS == "KHÔNG CÓ KHÁCH").ToList();
            foreach (var tables in table)
            {
                int index = dataGridView2.Rows.Add();
                dataGridView2.Rows[index].Cells[0].Value = tables.IDTABLE;
                dataGridView2.Rows[index].Cells[1].Value = tables.NAME;
                dataGridView2.Rows[index].Cells[2].Value = tables.STATUS;
            }
        }
        private void Menu_Load(object sender, EventArgs e)
        {
            setGridViewStyle(dataGridView1);
            var listtable = table.GetAll();
            BindGridTable(listtable);
            allTable = table.GetAll();
            BindGridTable(allTable);
            FillCoffeeTypes();
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var allTables = table.GetAll();
            List<TABLECOFFEE> filteredTables;

            if (checkBox1.Checked)
            {
                filteredTables = allTables.Where(t => t.STATUS == "KHÔNG CÓ KHÁCH").ToList();
            }
            else
            {
                filteredTables = allTables;
            }

            BindGridTable(filteredTables);
        }
        public void setGridViewStyle(DataGridView dgview)
        {
            dgview.BorderStyle = BorderStyle.None;
            dgview.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgview.BackgroundColor = Color.White;
            dgview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void FillCoffeeTypes()
        {
            try
            {
                var coffeeTypes = cOFFEETYPEService.GetAll();
                cmbloaisp.DataSource = coffeeTypes;
                cmbloaisp.DisplayMember = "NAME";
                cmbloaisp.ValueMember = "IDTYPE";
                cmbloaisp.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách loại sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbloaisp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbloaisp.SelectedValue != null && int.TryParse(cmbloaisp.SelectedValue.ToString(), out int selectedTypeId))
            {
                FilterCoffee(selectedTypeId);
            }
        }
        private void FilterCoffee(int typeId)
        {
            try
            {
                var filteredtype = mENUService.GetAll().Where(MENU => MENU.IDTYPE == typeId).ToList();
                BindGridCoffee(filteredtype);
                txttensp.Clear();
                txtgt.Clear();
                txtsl.Clear();
                txtkhuyenmai.Clear();
                txttn.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc bàn theo loại sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BindGridCoffee(List<MENU> type)
        {
            dgvsv.Rows.Clear();
            foreach (var types in type)
            {
                int index = dgvsv.Rows.Add();
                dgvsv.Rows[index].Cells[0].Value = types.IDMENU;
                dgvsv.Rows[index].Cells[1].Value = types.NAME;
            }
        }
        private string selectedMSSP = "";

        private void dgvsv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow dataGridViewRow = dgvsv.Rows[e.RowIndex];
                int menuID = (int)dataGridViewRow.Cells[0].Value;
                ShowAvatar(menuID);
                txttensp.Text = dataGridViewRow.Cells[1].Value.ToString();
                selectedMSSP = dataGridViewRow.Cells[0].Value.ToString(); // Cập nhật mã sản phẩm

                if (int.TryParse(selectedMSSP, out int selectedmenuid))
                {
                    var menu = mENUService.FindById(selectedmenuid);
                    if (menu != null)
                    {
                        txtgt.Text = menu.PRICE.ToString();
                        int quantity = 1;
                        if (int.TryParse(txtsl.Text, out int parsedQuantity))
                        {
                            quantity = parsedQuantity;
                        }
                        var discountService = new DISCOUNTService();
                        var discounts = discountService.FindByMenuId(selectedmenuid);
                        double discountPercentage = 0;

                        if (discounts != null && discounts.Count > 0)
                        {
                            var discount = discounts.First();
                            discountPercentage = discount.DISCOUNT_PERCENTAGE / 100;
                            txtkhuyenmai.Text = discountPercentage.ToString("P0");
                        }
                        else
                        {
                            txtkhuyenmai.Text = "Không có khuyến mãi";
                        }
                        double menuPrice = (double)menu.PRICE;
                        double totalPrice;
                        if (discountPercentage > 0)
                        {
                            totalPrice = menuPrice * quantity * (1 - discountPercentage);
                        }
                        else
                        {
                            totalPrice = menuPrice * quantity;
                        }
                        txttn.Text = totalPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("vi-VN")) + " VND";
                    }
                }
            }
        }
        private void btnthem_Click(object sender, EventArgs e)
        {
            
            int soBan;
            if (!int.TryParse(txtsb.Text, out soBan) || soBan == 0)
            {
                MessageBox.Show("Vui lòng chọn một bàn để thêm sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedTableRow = dataGridView2.Rows.Cast<DataGridViewRow>()
                                     .FirstOrDefault(row => Convert.ToInt32(row.Cells[0].Value) == soBan);

            if (selectedTableRow != null)
            {
                string status = selectedTableRow.Cells[2].Value.ToString();
                if (status != "KHÔNG CÓ KHÁCH")
                {
                    MessageBox.Show("Bàn này hiện đang có khách. Vui lòng chọn bàn khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (string.IsNullOrEmpty(txttensp.Text) || string.IsNullOrEmpty(txtgt.Text) || string.IsNullOrEmpty(txtsl.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string tenSanPham = txttensp.Text;
            double giaSanPham;
            int soLuong;
            if (!double.TryParse(txtgt.Text, out giaSanPham))
            {
                MessageBox.Show("Giá sản phẩm không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtsl.Text, out soLuong))
            {
                MessageBox.Show("Số lượng không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            double discountPercentage = 0;
            if (!string.IsNullOrEmpty(txtkhuyenmai.Text) && txtkhuyenmai.Text != "Không có khuyến mãi")
            {
                discountPercentage = double.Parse(txtkhuyenmai.Text.TrimEnd('%')) / 100;
            }
            double totalPrice = giaSanPham * soLuong * (1 - discountPercentage);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == selectedMSSP)
                {
                    int currentQuantity = Convert.ToInt32(row.Cells[3].Value);
                    int newQuantity = currentQuantity + soLuong;
                    row.Cells[3].Value = newQuantity;

                    double newTotalPrice = giaSanPham * newQuantity * (1 - discountPercentage);
                    row.Cells[5].Value = newTotalPrice;
                    UpdateTotalPrice();
                    return;
                }
            }
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = selectedMSSP;
            dataGridView1.Rows[index].Cells[1].Value = tenSanPham;
            dataGridView1.Rows[index].Cells[2].Value = giaSanPham;
            dataGridView1.Rows[index].Cells[3].Value = soLuong;
            dataGridView1.Rows[index].Cells[4].Value = txtkhuyenmai.Text;
            dataGridView1.Rows[index].Cells[5].Value = totalPrice;
            UpdateTotalPrice();

            MessageBox.Show("Sản phẩm đã được thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void UpdateTotalPrice()
        {
            double grandTotal = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[5].Value != null)
                {
                    grandTotal += Convert.ToDouble(row.Cells[5].Value);
                }
            }
            txttt.Text = grandTotal.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("vi-VN")) + " VND";
        }
        private int selectedTableId = 0;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow dataGridViewRow = dataGridView2.Rows[e.RowIndex];
                string status = dataGridViewRow.Cells[2].Value.ToString(); 

                if (status == "KHÔNG CÓ KHÁCH")
                {
                    selectedTableId = Convert.ToInt32(dataGridViewRow.Cells[0].Value); 
                }

                txtsb.Text = dataGridViewRow.Cells[0].Value.ToString();
            }
        }
        
        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow hangDaChon = dataGridView1.SelectedRows[0];
                DialogResult xacNhan = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (xacNhan == DialogResult.Yes)
                {
                    dataGridView1.Rows.Remove(hangDaChon);
                    UpdateTotalPrice();
                    MessageBox.Show("Sản phẩm đã được xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow hangDaChon = dataGridView1.SelectedRows[0];
                hangDaChon.Cells[1].Value = txttensp.Text;
                hangDaChon.Cells[2].Value = double.Parse(txtgt.Text);
                hangDaChon.Cells[3].Value = int.Parse(txtsl.Text);
                double tiLeKhuyenMai = 0;
                if (!string.IsNullOrEmpty(txtkhuyenmai.Text) && txtkhuyenmai.Text != "Không có khuyến mãi")
                {
                    tiLeKhuyenMai = double.Parse(txtkhuyenmai.Text.TrimEnd('%')) / 100;
                }
                double tongTienMoi = (double)hangDaChon.Cells[2].Value * (int)hangDaChon.Cells[3].Value * (1 - tiLeKhuyenMai);
                hangDaChon.Cells[5].Value = tongTienMoi;

                UpdateTotalPrice();  

                MessageBox.Show("Thông tin sản phẩm đã được cập nhật thành công!", "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để cập nhật!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private void btntt_Click(object sender, EventArgs e)
        {

            if (selectedTableId > 0)
            {
                // Tạo hóa đơn mới
                BILL newBill = new BILL
                {
                    IDBILL = GenerateBillId(),
                    IDTABLE = selectedTableId,
                    dateCheckIn = DateTime.Now,
                    dateCheckOut = DateTime.Now,
                    STATUS = 1,
                };

                bILLService.Add(newBill);

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        int menuId = Convert.ToInt32(row.Cells[0].Value.ToString());
                        float quantity = Convert.ToInt32(row.Cells[3].Value);
                        float price = Convert.ToSingle(row.Cells[2].Value);
                        double count;
                        string discountText = row.Cells[4].Value?.ToString();
                        float discountPercentage = 0;

                        if (!string.IsNullOrEmpty(discountText) && discountText != "Không có khuyến mãi")
                        {
                            discountPercentage = float.Parse(discountText.TrimEnd('%')) / 100;
                            count = price * quantity * (1 - discountPercentage);
                        }
                        else
                        {
                            count = price * quantity;
                        }

                        BILLINFO billInfo = new BILLINFO
                        {
                            IDINFO = GenerateBillInfoId(),
                            IDBILL = newBill.IDBILL,
                            IDMENU = menuId,
                            COUNT = count,
                        };
                        bILLINFOService.Add(billInfo);
                    }
                }

                UpdateTotalPrice(); // Cập nhật tổng giá sau khi thêm hóa đơn

                // Cập nhật trạng thái bàn
                table.UpdateTableStatus(selectedTableId, "CÓ KHÁCH");
                var listtable = table.GetAll();
                BindGridTable(listtable);
                dataGridView1.Rows.Clear();

                MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Hiển thị WinForm Reportcs với IDBill của hóa đơn vừa tạo
                Reportcs reportForm = new Reportcs(newBill.IDBILL);
                reportForm.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một bàn trước khi thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private int GenerateBillId()
        {
            return bILLService.GenerateNewBillId();
        }

        private int GenerateBillInfoId()
        {
            return bILLINFOService.GenerateNewBillInfoId();
        }

        
        private void btnranh_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int tableId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value);
                string newStatus = "KHÔNG CÓ KHÁCH";
                TABLECOFFEEService tableService = new TABLECOFFEEService();
                var result = tableService.UpdateTableStatus(tableId, newStatus);
                MessageBox.Show(result.message);
                RefreshDataGridView();
            }
        }
        private void RefreshDataGridView()
        {
            TABLECOFFEEService tableService = new TABLECOFFEEService();
            var tables = tableService.GetAll();
            BindGridTable(tables);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 0) 
            {
                DataGridViewRow dataGridViewRow = dataGridView1.Rows[e.RowIndex];
                txttensp.Text = dataGridViewRow.Cells[1].Value.ToString();
                txtgt.Text = dataGridViewRow.Cells[2].Value.ToString();
                txtsl.Text = dataGridViewRow.Cells[3].Value.ToString();
                txtkhuyenmai.Text = dataGridViewRow.Cells[4].Value.ToString();
                txttt.Text = dataGridViewRow.Cells[5].Value.ToString();
            }
        }
    }
}
