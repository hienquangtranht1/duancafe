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
        private string avartarFilePath = string.Empty;
        private readonly EMPLOYEEService employeeService = new EMPLOYEEService();
        private readonly DISCOUNTService discountService = new DISCOUNTService();

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

                var listEmployee = employeeService.GetAll();
                BindGridEmployees(listEmployee);

                var listdiscount = discountService.GetAll();
                BindGridDiscount(listdiscount);
                setGridViewStyle(dtgvdis);
                setGridViewStyle(dtgvnv);



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void BindGridDiscount(List<DISCOUNT> listdiscount)
        {
            dtgvdis.Rows.Clear();
            foreach (var item in listdiscount)
            {
                int index = dtgvdis.Rows.Add();
                dtgvdis.Rows[index].Cells[0].Value = item.IDDIS;
                dtgvdis.Rows[index].Cells[1].Value = item.NAME;
                dtgvdis.Rows[index].Cells[2].Value = item.DISCOUNT_PERCENTAGE;
                dtgvdis.Rows[index].Cells[3].Value = item.DATE_START;
                dtgvdis.Rows[index].Cells[4].Value = item.DATE_FINISH;
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
        private void BindGrid(List<MENU> listmenus)
        {
            dtgvFood.Rows.Clear();
            foreach (var item in listmenus)
            {
                int index = dtgvFood.Rows.Add();
                dtgvFood.Rows[index].Cells[0].Value = item.IDMENU;
                dtgvFood.Rows[index].Cells[1].Value = item.IDTYPE;
                dtgvFood.Rows[index].Cells[2].Value = item.NAME;
                if (item.COFFEETYPE != null)
                {
                    dtgvFood.Rows[index].Cells[3].Value = item.COFFEETYPE.NAME;
                }
                dtgvFood.Rows[index].Cells[4].Value = item.PRICE;
            }
        }

        private void BindGridEmployees(List<EMPLOYEE> employees)
        {
            dtgvnv.Rows.Clear();
            foreach (var employee in employees)
            {
                int index = dtgvnv.Rows.Add();
                dtgvnv.Rows[index].Cells[0].Value = employee.IDEMPLOYEE;
                dtgvnv.Rows[index].Cells[1].Value = employee.NAME;
                dtgvnv.Rows[index].Cells[2].Value = employee.POSITION;
                dtgvnv.Rows[index].Cells[3].Value = employee.SALARY;
                dtgvnv.Rows[index].Cells[4].Value = employee.DATE_HIRE?.ToString("dd/MM/yyyy");

            }
        }

        private void btnaddfood_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txttensp.Text) || string.IsNullOrEmpty(txtgiadoan.Text) || cboloaisp.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ các trường bắt buộc.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!float.TryParse(txtgiadoan.Text, out float price) || price <= 0)
                {
                    MessageBox.Show("Giá không hợp lệ. Vui lòng nhập số hợp lệ lớn hơn 0.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int newIDMenu = menuService.GetMaxID() + 1;

                MENU newMenuItem = new MENU
                {
                    IDMENU = newIDMenu,
                    NAME = txttensp.Text,
                    PRICE = price,
                    IDTYPE = (int)cboloaisp.SelectedValue,
                    AVATARMENU = avartarFilePath
                };

                var (result, message) = menuService.Add(newMenuItem);

                MessageBox.Show(message, result ? "Thành công" : "Lỗi", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (result)
                {
                    var listmenu = menuService.GetAll();
                    BindGrid(listmenu);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dtgvFood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dtgvFood.Rows[e.RowIndex];
                int menuID = (int)selectedRow.Cells[0].Value;
                ShowAvatar(menuID);

                txttensp.Text = selectedRow.Cells[2].Value.ToString();
                txtgiadoan.Text = selectedRow.Cells[4].Value.ToString();

                string tenLoai = selectedRow.Cells[3].Value.ToString();
                cboloaisp.Text = tenLoai;
            }
        }
        private void ShowAvatar(int menuID)
        {
            string folderPath = Path.Combine(Application.StartupPath, "Images");
            var menuItem = menuService.FindById(menuID);
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

        private string SaveAvatar(string sourceFilePath, int menuID)
        {
            try
            {
                string folderPath = Path.Combine(Application.StartupPath, "Images");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileExtension = Path.GetExtension(sourceFilePath);
                string targetFilePath = Path.Combine(folderPath, $"{menuID}{fileExtension}");
                if (!File.Exists(sourceFilePath))
                {
                    throw new FileNotFoundException($"Không tìm thấy file: {sourceFilePath}");
                }

                File.Copy(sourceFilePath, targetFilePath, true);
                return $"{menuID}{fileExtension}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving avatar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void btnthemanhmenu_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image File (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    avartarFilePath = openFileDialog.FileName;
                    pictureBox1.Image = Image.FromFile(avartarFilePath);

                    int menuID = menuService.GetMaxID() + 1;
                    string savedAvatarFileName = SaveAvatar(avartarFilePath, menuID);

                    if (savedAvatarFileName != null)
                    {
                        avartarFilePath = savedAvatarFileName;
                    }
                }
            }
        }

        private void btndeletefood_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgvFood.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dtgvFood.SelectedRows[0];
                    int menuID = (int)selectedRow.Cells[0].Value;

                    var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa món ăn này?",
                                                        "Xác nhận xóa",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question);
                    if (confirmResult == DialogResult.Yes)
                    {
                        var (result, message) = menuService.DeleteById(menuID);

                        MessageBox.Show(message, result ? "Thành công" : "Lỗi", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                        if (result)
                        {
                            var listmenu = menuService.GetAll();
                            BindGrid(listmenu);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một món ăn để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnfixffood_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgvFood.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dtgvFood.SelectedRows[0];
                    int menuID = (int)selectedRow.Cells[0].Value;


                    if (string.IsNullOrEmpty(txttensp.Text) || string.IsNullOrEmpty(txtgiadoan.Text) || cboloaisp.SelectedValue == null)
                    {
                        MessageBox.Show("Vui lòng điền đầy đủ các trường bắt buộc.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!float.TryParse(txtgiadoan.Text, out float price) || price <= 0)
                    {
                        MessageBox.Show("Giá không hợp lệ. Vui lòng nhập số hợp lệ lớn hơn 0.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    MENU updatedMenuItem = new MENU
                    {
                        IDMENU = menuID,
                        NAME = txttensp.Text,
                        PRICE = price,
                        IDTYPE = (int)cboloaisp.SelectedValue,
                        AVATARMENU = avartarFilePath
                    };

                    var (result, message) = menuService.Update(updatedMenuItem);

                    MessageBox.Show(message, result ? "Thành công" : "Lỗi", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                    if (result)
                    {
                        var listmenu = menuService.GetAll();
                        BindGrid(listmenu);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một món ăn để sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnfindfood_Click(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = txtfoodname.Text.Trim();

                var listMenu = menuService.GetAll();

                var filteredMenu = listMenu.Where(item =>
                    item.NAME.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

                BindGrid(filteredMenu);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnaddnhanvien_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtidnv.Text) ||
                    string.IsNullOrEmpty(txttennv.Text) ||
                    string.IsNullOrEmpty(txtchucvunv.Text) ||
                    string.IsNullOrEmpty(txtluongnv.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ các trường bắt buộc.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtluongnv.Text, out decimal salary) || salary < 0)
                {
                    MessageBox.Show("Lương không hợp lệ. Vui lòng nhập số hợp lệ không âm.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                EMPLOYEE newEmployee = new EMPLOYEE
                {
                    IDEMPLOYEE = int.Parse(txtidnv.Text.Trim()),
                    NAME = txttennv.Text.Trim(),
                    POSITION = txtchucvunv.Text.Trim(),
                    SALARY = (double?)salary,
                    DATE_HIRE = DateTime.Now,
                    AVATAREMPLOYEE = avartarFilePath
                };

                var (result, message) = employeeService.Add(newEmployee);

                MessageBox.Show(message, result ? "Thành công" : "Lỗi", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (result)
                {
                    var listEmployee = employeeService.GetAll();
                    BindGridEmployees(listEmployee);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnxoanv_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgvnv.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dtgvnv.SelectedRows[0];
                    int employeeID = (int)selectedRow.Cells[0].Value;

                    var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?",
                                                        "Xác nhận xóa",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question);
                    if (confirmResult == DialogResult.Yes)
                    {
                        var (result, message) = employeeService.DeleteById(employeeID);

                        MessageBox.Show(message, result ? "Thành công" : "Lỗi", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                        if (result)
                        {
                            var listEmployee = employeeService.GetAll();
                            BindGridEmployees(listEmployee);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một nhân viên để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnsuanv_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgvnv.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dtgvnv.SelectedRows[0];
                    int employeeID = (int)selectedRow.Cells[0].Value;

                    if (string.IsNullOrEmpty(txtidnv.Text) ||
                        string.IsNullOrEmpty(txttennv.Text) ||
                        string.IsNullOrEmpty(txtchucvunv.Text) ||
                        string.IsNullOrEmpty(txtluongnv.Text))
                    {
                        MessageBox.Show("Vui lòng điền đầy đủ các trường bắt buộc.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!decimal.TryParse(txtluongnv.Text, out decimal salary) || salary < 0)
                    {
                        MessageBox.Show("Lương không hợp lệ. Vui lòng nhập số hợp lệ không âm.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    EMPLOYEE updatedEmployee = new EMPLOYEE
                    {
                        IDEMPLOYEE = employeeID,
                        NAME = txttennv.Text.Trim(),
                        POSITION = txtchucvunv.Text.Trim(),
                        SALARY = (double?)salary,
                        DATE_HIRE = DateTime.Now,
                        AVATAREMPLOYEE = avartarFilePath
                    };

                    var (result, message) = employeeService.Update(updatedEmployee);

                    MessageBox.Show(message, result ? "Thành công" : "Lỗi", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                    if (result)
                    {
                        var listEmployee = employeeService.GetAll();
                        BindGridEmployees(listEmployee);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một nhân viên để sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btntimnv_Click(object sender, EventArgs e)
        {
            try
            {
                string searchName = txttimidnv.Text.Trim();

                if (string.IsNullOrEmpty(searchName))
                {
                    MessageBox.Show("Vui lòng nhập tên nhân viên để tìm kiếm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<EMPLOYEE> foundEmployees = employeeService.FindByName(searchName);

                if (foundEmployees != null && foundEmployees.Count > 0)
                {
                    BindGridEmployees(foundEmployees);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên nào với tên đã nhập.", "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtgvnv.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnthemanhnv_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image File (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    avartarFilePath = openFileDialog.FileName;
                    pictureBox2.Image = Image.FromFile(avartarFilePath);

                    if (int.TryParse(txtidnv.Text.Trim(), out int IDEMPLOYEE))
                    {
                        string savedAvatarFileName = SaveAvatarnv(avartarFilePath, IDEMPLOYEE);

                        if (savedAvatarFileName != null)
                        {
                            avartarFilePath = savedAvatarFileName;
                        }
                        else
                        {
                            MessageBox.Show("Không thể lưu avatar.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("ID nhân viên không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        private void ShowAvatarnv(int employeeID)
        {
            string folderPath = Path.Combine(Application.StartupPath, "Images");
            var employee = employeeService.FindBynvId(employeeID);
            if (employee != null && !string.IsNullOrEmpty(employee.AVATAREMPLOYEE))
            {
                string avatarFilePath = Path.Combine(folderPath, employee.AVATAREMPLOYEE);
                if (File.Exists(avatarFilePath))
                {
                    pictureBox2.Image = Image.FromFile(avatarFilePath);
                }
                else
                {
                    pictureBox2.Image = null;
                }
            }
        }

        private string SaveAvatarnv(string sourceFilePath, int employeeID)
        {
            try
            {
                string folderPath = Path.Combine(Application.StartupPath, "Images");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileExtension = Path.GetExtension(sourceFilePath);
                string targetFilePath = Path.Combine(folderPath, $"{employeeID}{fileExtension}");
                if (!File.Exists(sourceFilePath))
                {
                    throw new FileNotFoundException($"Không tìm thấy file: {sourceFilePath}");
                }

                File.Copy(sourceFilePath, targetFilePath, true);
                return $"{employeeID}{fileExtension}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu avatar: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void dtgvnv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dtgvnv.Rows[e.RowIndex];

                txtidnv.Text = selectedRow.Cells[0].Value?.ToString();
                txttennv.Text = selectedRow.Cells[1].Value?.ToString();
                txtchucvunv.Text = selectedRow.Cells[2].Value?.ToString();
                txtluongnv.Text = selectedRow.Cells[3].Value?.ToString();

                int employeeID = (int)selectedRow.Cells[0].Value;
                ShowAvatarnv(employeeID);
            }
        }

        private void btnaddkm_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtmakm.Text) ||
                   string.IsNullOrEmpty(txttenkm.Text) ||
                   string.IsNullOrEmpty(txtgiam.Text) ||
                   string.IsNullOrEmpty(dtpkdaystartkm.Text) ||
                   string.IsNullOrEmpty(dtpkdayendkm.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ các trường bắt buộc.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DISCOUNT newdis = new DISCOUNT
                {
                    IDDIS = int.Parse(txtmakm.Text),
                    NAME = txttenkm.Text,
                    DISCOUNT_PERCENTAGE = int.Parse(txtgiam.Text),
                    DATE_START = dtpkdaystartkm.Value,
                    DATE_FINISH = dtpkdayendkm.Value
                };
                var (result, message) = discountService.Add(newdis);
                MessageBox.Show(message, result ? "Thành công" : "Lỗi", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (result)
                {
                    var listdiscount = discountService.GetAll();
                    BindGridDiscount(listdiscount);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnsuakm_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtmakm.Text) ||
                   string.IsNullOrEmpty(txttenkm.Text) ||
                   string.IsNullOrEmpty(txtgiam.Text) ||
                   string.IsNullOrEmpty(dtpkdaystartkm.Text) ||
                   string.IsNullOrEmpty(dtpkdayendkm.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ các trường bắt buộc.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DISCOUNT newdis = new DISCOUNT
                {
                    IDDIS = int.Parse(txtmakm.Text),
                    NAME = txttenkm.Text,
                    DISCOUNT_PERCENTAGE = int.Parse(txtgiam.Text),
                    DATE_START = dtpkdaystartkm.Value,
                    DATE_FINISH = dtpkdayendkm.Value
                };
                var (result, message) = discountService.Update(newdis);
                MessageBox.Show(message, result ? "Thành công" : "Lỗi", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (result)
                {
                    var listdiscount = discountService.GetAll();
                    BindGridDiscount(listdiscount);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgcdis_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dtgvdis.Rows[e.RowIndex];
                txtmakm.Text = selectedRow.Cells[0].Value.ToString();
                txttenkm.Text = selectedRow.Cells[1].Value.ToString();
                txtgiam.Text = selectedRow.Cells[2].Value.ToString();
                dtpkdaystartkm.Text = selectedRow.Cells[3].Value.ToString();
                dtpkdayendkm.Text = selectedRow.Cells[4].Value.ToString();

            }
        }

        private void btnxoakm_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgvdis.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dtgvdis.SelectedRows[0];
                    int iddis = (int)selectedRow.Cells[0].Value;

                    var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?",
                                                        "Xác nhận xóa",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question);
                    if (confirmResult == DialogResult.Yes)
                    {
                        var (result, message) = discountService.DeleteById(iddis);

                        MessageBox.Show(message, result ? "Thành công" : "Lỗi", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                        if (result)
                        {
                            var listdiscount = discountService.GetAll();
                            BindGridDiscount(listdiscount);
                        }
                    }
                }
            
                 else
                {
                    MessageBox.Show("Vui lòng chọn một khuyến mãi để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btntimkm_Click(object sender, EventArgs e)
        {
            try
            {
                string searchName = txttenkm.Text.Trim();

                if (string.IsNullOrEmpty(searchName))
                {
                    MessageBox.Show("Vui lòng nhập tên khuyến mãi để tìm kiếm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<DISCOUNT> foundDiscount = discountService.FindByName(searchName);

                if (foundDiscount != null && foundDiscount.Count > 0)
                {
                    BindGridDiscount(foundDiscount);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tên khuyến mãi nào với tên đã nhập.", "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtgvdis.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    } 
}

