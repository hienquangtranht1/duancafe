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
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                var emptyTables = allTable.Where(t => t.STATUS == "KHÔNG CÓ KHÁCH").ToList();
                BindGridTable(emptyTables);
            }
            else
            {
                BindGridTable(allTable);
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
                txttensp.Text = dataGridViewRow.Cells[1].Value.ToString();
                selectedMSSP = dgvsv.Rows[e.RowIndex].Cells[0].Value.ToString();
                var selectedmenuidString = dataGridViewRow.Cells[0].Value.ToString();
                if (!string.IsNullOrEmpty(selectedmenuidString) && int.TryParse(selectedmenuidString, out int selectedmenuid))
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
            var selectedTableRow = dataGridView2.Rows.Cast<DataGridViewRow>().FirstOrDefault(row => (int)Convert.ChangeType(row.Cells[0].Value, typeof(int)) == soBan);

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

            string mssp = selectedMSSP;
            double discountPercentage = 0;
            if (!string.IsNullOrEmpty(txtkhuyenmai.Text) && txtkhuyenmai.Text != "Không có khuyến mãi")
            {
                discountPercentage = double.Parse(txtkhuyenmai.Text.TrimEnd('%')) / 100;
            }
            double totalPrice = giaSanPham * soLuong * (1 - discountPercentage);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == mssp)
                {
                    int currentQuantity = Convert.ToInt32(row.Cells[3].Value);
                    int newQuantity = currentQuantity + soLuong;

                    row.Cells[3].Value = newQuantity;
                    double newTotalPrice = giaSanPham * newQuantity * (1 - discountPercentage);
                    row.Cells[4].Value = newTotalPrice;
                    break;
                }
            }
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = mssp;
            dataGridView1.Rows[index].Cells[1].Value = tenSanPham;
            dataGridView1.Rows[index].Cells[2].Value = giaSanPham;
            dataGridView1.Rows[index].Cells[3].Value = soLuong;
            dataGridView1.Rows[index].Cells[4].Value = totalPrice;
            UpdateTotalPrice();
        }
        private void UpdateTotalPrice()
        {
            double grandTotal = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[4].Value != null)
                {
                    grandTotal += Convert.ToDouble(row.Cells[4].Value);
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
                string status = dataGridViewRow.Cells[2].Value.ToString(); // Trạng thái bàn

                if (status == "KHÔNG CÓ KHÁCH")
                {
                    selectedTableId = Convert.ToInt32(dataGridViewRow.Cells[0].Value); // Chuyển đổi ID bàn sang kiểu int
                }

                txtsb.Text = dataGridViewRow.Cells[0].Value.ToString();
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow dataGridViewRow = dataGridView1.Rows[e.RowIndex];
                txttensp.Text = dataGridViewRow.Cells[1].Value.ToString();
                txtgt.Text = dataGridViewRow.Cells[2].Value.ToString();
                txtsl.Text = dataGridViewRow.Cells[3].Value.ToString();
                txttn.Text = dataGridViewRow.Cells[4].Value.ToString();
            }
        }
        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        dataGridView1.Rows.Remove(row);
                    }
                    UpdateTotalPrice();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                selectedRow.Cells[1].Value = txttensp.Text;
                selectedRow.Cells[2].Value = decimal.Parse(txtgt.Text);
                selectedRow.Cells[3].Value = int.Parse(txtsl.Text);
                UpdateTotalPrice();

                MessageBox.Show("Sản phẩm đã được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private List<BILLINFO> selectedItems = new List<BILLINFO>();
        private void btntt_Click(object sender, EventArgs e)
        {
            if (selectedTableId > 0)
            {
                BILL newBill = new BILL
                {
                    IDBILL = GenerateBillId(),
                    IDTABLE = selectedTableId,
                    dateCheckIn = DateTime.Now,
                    dateCheckOut = DateTime.Now,
                    STATUS = 1,
                    //IDEMPLOYEE = GetCurrentEmployeeId(currentUsername) // Update with currentUsername parameter
                };

                bILLService.Add(newBill);

                // Loop through each row in dataGridView1 to add items to BILLINFO
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        // Retrieve menu ID, quantity, price and discount from dataGridView1
                        int menuId = Convert.ToInt32(row.Cells[0].Value);
                        float quantity = Convert.ToInt32(row.Cells[3].Value); // Số lượng
                        float price = Convert.ToSingle(row.Cells[4].Value);  
                        float discount = 0.0f; 
                        double count = price * quantity * (1 - discount);

                        // Create a new BILLINFO instance
                        BILLINFO billInfo = new BILLINFO
                        {
                            IDINFO = GenerateBillInfoId(),
                            IDBILL = newBill.IDBILL,
                            IDMENU = menuId,
                            COUNT = count 
                        };

                        // Save the BILLINFO to the database
                        bILLINFOService.Add(billInfo);
                    }
                }

                // Update the status of the selected table to 'Occupied'
                table.UpdateTableStatus(selectedTableId, "CÓ KHÁCH");
                var listtable = table.GetAll();
                BindGridTable(listtable);

                // Clear the dataGridView1 after processing the bill
                dataGridView1.Rows.Clear();
                UpdateTotalPrice();

                MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một bàn trước khi thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }
        private int GenerateBillId()
        {
            // Generate a unique ID for BILL, or use database auto-increment if supported
            return bILLService.GenerateNewBillId();
        }

        private int GenerateBillInfoId()
        {
            // Generate a unique ID for BILLINFO, or use database auto-increment if supported
            return bILLINFOService.GenerateNewBillInfoId();
        }

        
            private void btnranh_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int tableId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value);
                string status = "KHÔNG CÓ KHÁCH";
                TABLECOFFEEService tableService = new TABLECOFFEEService();
                var result = tableService.UpdateTableStatus(tableId, status);

                if (result.success)
                {
                    dataGridView2.SelectedRows[0].Cells[2].Value = status;
                }
                else
                {
                    MessageBox.Show(result.message);
                }
            }
        }
        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow dataGridViewRow = dataGridView1.Rows[e.RowIndex];
                txttensp.Text = dataGridViewRow.Cells[1].Value.ToString();
                txtgt.Text = dataGridViewRow.Cells[2].Value.ToString();
                txtsl.Text = dataGridViewRow.Cells[3].Value.ToString();
                txttn.Text = dataGridViewRow.Cells[4].Value.ToString();
            }
        }

    }
}
