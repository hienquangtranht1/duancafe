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


                setGridViewStyle(dtgvnv);
                var listEmployee = employeeService.GetAll();
                BindGridEmployees(listEmployee); // Gọi hàm để bind danh sách nhân viên


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
        private void BindGrid(List<MENU> listmenus)
        {
            dtgvFood.Rows.Clear();
            foreach (var item in listmenus)
            {
                int index = dtgvFood.Rows.Add();
                dtgvFood.Rows[index].Cells[0].Value = item.IDMENU; // IDMENU
                dtgvFood.Rows[index].Cells[1].Value = item.IDTYPE; // IDTYPE (Có thể không cần nếu chỉ hiển thị tên loại)
                dtgvFood.Rows[index].Cells[2].Value = item.NAME; // NAME
                if (item.COFFEETYPE != null)
                {
                    dtgvFood.Rows[index].Cells[3].Value = item.COFFEETYPE.NAME; // Tên loại cà phê
                }
                dtgvFood.Rows[index].Cells[4].Value = item.PRICE; // PRICE
            }
        }
        private void BindGridEmployees(List<EMPLOYEE> employees)
        {
            dtgvnv.Rows.Clear(); // Xóa các dòng hiện có
            foreach (var employee in employees)
            {
                int index = dtgvnv.Rows.Add();
                dtgvnv.Rows[index].Cells[0].Value = employee.IDEMPLOYEE; // IDEMPLOYEE
                dtgvnv.Rows[index].Cells[1].Value = employee.NAME; // NAME
                dtgvnv.Rows[index].Cells[2].Value = employee.POSITION; // POSITION
                dtgvnv.Rows[index].Cells[3].Value = employee.SALARY; // SALARY
                dtgvnv.Rows[index].Cells[4].Value = employee.DATE_HIRE?.ToString("dd/MM/yyyy"); // DATE_HIRE
      
            }
        }
        private void tpfood_Click(object sender, EventArgs e)
        {

        }

        private void btnaddfood_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra các trường cần thiết đã được điền chưa
                if (string.IsNullOrEmpty(txttensp.Text) || string.IsNullOrEmpty(txtgiadoan.Text) || cboloaisp.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ các trường bắt buộc.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra xem giá trị của "txtgiadoan" có phải là số hợp lệ hay không
                if (!float.TryParse(txtgiadoan.Text, out float price) || price <= 0)
                {
                    MessageBox.Show("Giá không hợp lệ. Vui lòng nhập số hợp lệ lớn hơn 0.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy IDMENU lớn nhất hiện có từ cơ sở dữ liệu
                int newIDMenu = menuService.GetMaxID() + 1;

                // Tạo đối tượng MENU mới
                MENU newMenuItem = new MENU
                {
                    IDMENU = newIDMenu,  // Gán ID mới không trùng lặp
                    NAME = txttensp.Text,
                    PRICE = price,
                    IDTYPE = (int)cboloaisp.SelectedValue,
                    AVATARMENU = avartarFilePath
                };

                // Gọi service để thêm món mới
                var (result, message) = menuService.Add(newMenuItem);

                // Hiển thị kết quả cho người dùng
                MessageBox.Show(message, result ? "Thành công" : "Lỗi", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                // Nếu thêm thành công, cập nhật lại danh sách món
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
                int menuID = (int)selectedRow.Cells[0].Value;  // Lấy ID của món ăn từ cột đầu tiên
                ShowAvatar(menuID);  // Hiển thị avatar của món ăn

                txttensp.Text = selectedRow.Cells[2].Value.ToString();  // Điền tên món ăn vào textbox
                txtgiadoan.Text = selectedRow.Cells[4].Value.ToString();  // Điền giá món ăn vào textbox

                // Lấy tên loại cà phê từ cột thích hợp và điền vào textbox
                // Giả sử tên loại cà phê nằm ở cột thứ 3 (có thể điều chỉnh nếu cần)
                string tenLoai = selectedRow.Cells[3].Value.ToString(); // Thay đổi chỉ số cột nếu cần
                cboloaisp.Text = tenLoai;  // Điền tên loại vào textbox
            }
        }
        private void ShowAvatar(int menuID)
        {
            string folderPath = Path.Combine(Application.StartupPath, "Images");
            var menuItem = menuService.FindById(menuID);  // Sử dụng dịch vụ MENU để tìm món ăn theo ID
            if (menuItem != null && !string.IsNullOrEmpty(menuItem.AVATARMENU))  // Kiểm tra nếu món có avatar
            {
                string avatarFilePath = Path.Combine(folderPath, menuItem.AVATARMENU);  // Đường dẫn tới avatar
                if (File.Exists(avatarFilePath))
                {
                    pictureBox1.Image = Image.FromFile(avatarFilePath);  // Hiển thị avatar trong PictureBox
                }
                else
                {
                    pictureBox1.Image = null;  // Nếu không tìm thấy file avatar, không hiển thị ảnh
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
                    Directory.CreateDirectory(folderPath);  // Tạo thư mục "Images" nếu chưa tồn tại
                }

                string fileExtension = Path.GetExtension(sourceFilePath);  // Lấy phần mở rộng của file ảnh
                string targetFilePath = Path.Combine(folderPath, $"{menuID}{fileExtension}");  // Đường dẫn lưu ảnh với tên dựa trên menuID
                if (!File.Exists(sourceFilePath))
                {
                    throw new FileNotFoundException($"Không tìm thấy file: {sourceFilePath}");
                }

                File.Copy(sourceFilePath, targetFilePath, true);  // Sao chép file ảnh đến thư mục, ghi đè nếu có
                return $"{menuID}{fileExtension}";  // Trả về tên file đã lưu
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving avatar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void dtgvFood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnthemanhmenu_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image File (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    avartarFilePath = openFileDialog.FileName;  // Lưu đường dẫn của file ảnh đã chọn
                    pictureBox1.Image = Image.FromFile(avartarFilePath);  // Hiển thị ảnh đã chọn lên PictureBox

                    // Lưu avatar vào thư mục và cập nhật avatar cho món ăn
                    int menuID = menuService.GetMaxID() + 1;  // Lấy ID mới nhất hoặc ID của món ăn đang chỉnh sửa
                    string savedAvatarFileName = SaveAvatar(avartarFilePath, menuID);

                    if (savedAvatarFileName != null)
                    {
                        avartarFilePath = savedAvatarFileName;  // Cập nhật đường dẫn avatar vào biến nếu lưu thành công
                    }
                }
            }
        }

        private void btndeletefood_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem người dùng đã chọn dòng nào trong DataGridView chưa
                if (dtgvFood.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dtgvFood.SelectedRows[0];
                    int menuID = (int)selectedRow.Cells[0].Value;  // Lấy ID của món ăn từ cột đầu tiên

                    // Hỏi người dùng xem có chắc chắn muốn xóa món này không
                    var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa món ăn này?",
                                                        "Xác nhận xóa",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question);
                    if (confirmResult == DialogResult.Yes)
                    {
                        // Gọi phương thức DeleteById để xóa món ăn
                        var (result, message) = menuService.DeleteById(menuID);

                        // Hiển thị thông báo cho người dùng
                        MessageBox.Show(message, result ? "Thành công" : "Lỗi", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                        // Nếu xóa thành công, cập nhật lại danh sách món
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
                // Kiểm tra xem người dùng đã chọn dòng nào trong DataGridView chưa
                if (dtgvFood.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dtgvFood.SelectedRows[0];
                    int menuID = (int)selectedRow.Cells[0].Value;  // Lấy ID của món ăn từ cột đầu tiên

                    // Kiểm tra các trường cần thiết đã được điền chưa
                    if (string.IsNullOrEmpty(txttensp.Text) || string.IsNullOrEmpty(txtgiadoan.Text) || cboloaisp.SelectedValue == null)
                    {
                        MessageBox.Show("Vui lòng điền đầy đủ các trường bắt buộc.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Kiểm tra xem giá trị của "txtgiadoan" có phải là số hợp lệ hay không
                    if (!float.TryParse(txtgiadoan.Text, out float price) || price <= 0)
                    {
                        MessageBox.Show("Giá không hợp lệ. Vui lòng nhập số hợp lệ lớn hơn 0.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Tạo đối tượng MENU mới với thông tin đã cập nhật
                    MENU updatedMenuItem = new MENU
                    {
                        IDMENU = menuID,
                        NAME = txttensp.Text,
                        PRICE = price,
                        IDTYPE = (int)cboloaisp.SelectedValue,
                        AVATARMENU = avartarFilePath  // Có thể để rỗng nếu không muốn cập nhật avatar
                    };

                    // Gọi service để cập nhật món ăn
                    var (result, message) = menuService.Update(updatedMenuItem);

                    // Hiển thị thông báo cho người dùng
                    MessageBox.Show(message, result ? "Thành công" : "Lỗi", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                    // Nếu cập nhật thành công, cập nhật lại danh sách món
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

        

        private void dtgvnv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tpnhanvien_Click(object sender, EventArgs e)
        {

        }

        private void btnfindfood_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy từ khóa tìm kiếm từ textbox
                string searchTerm = txtfoodname.Text.Trim();

                // Lấy danh sách tất cả các món ăn từ dịch vụ
                var listMenu = menuService.GetAll();

                // Lọc danh sách món ăn theo từ khóa tìm kiếm chỉ theo tên
                var filteredMenu = listMenu.Where(item =>
                    item.NAME.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

                // Hiển thị danh sách đã lọc trong DataGridView
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
                // Kiểm tra các trường cần thiết đã được điền chưa
                if (string.IsNullOrEmpty(txtidnv.Text) ||
                    string.IsNullOrEmpty(txttennv.Text) ||
                    string.IsNullOrEmpty(txtchucvunv.Text) ||
                    string.IsNullOrEmpty(txtluongnv.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ các trường bắt buộc.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra xem giá trị của "txtSalary" có phải là số hợp lệ hay không
                if (!decimal.TryParse(txtluongnv.Text, out decimal salary) || salary < 0)
                {
                    MessageBox.Show("Lương không hợp lệ. Vui lòng nhập số hợp lệ không âm.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo đối tượng EMPLOYEE mới
                EMPLOYEE newEmployee = new EMPLOYEE
                {
                    IDEMPLOYEE = int.Parse(txtidnv.Text.Trim()), // Gán ID nhân viên từ txtidnv
                    NAME = txttennv.Text.Trim(),
                    POSITION = txtchucvunv.Text.Trim(),
                    SALARY = (double?)salary,
                    DATE_HIRE = DateTime.Now,  // Gán ngày thuê hiện tại
                    AVATAREMPLOYEE = avartarFilePath  // Nếu có avatar, gán đường dẫn vào đây
                };

                // Gọi service để thêm nhân viên mới
                var (result, message) = employeeService.Add(newEmployee);

                // Hiển thị kết quả cho người dùng
                MessageBox.Show(message, result ? "Thành công" : "Lỗi", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                // Nếu thêm thành công, cập nhật lại danh sách nhân viên
                if (result)
                {
                    var listEmployee = employeeService.GetAll();
                    BindGridEmployees(listEmployee);  // Gọi hàm để bind danh sách nhân viên
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
                // Kiểm tra xem người dùng đã chọn dòng nào trong DataGridView chưa
                if (dtgvnv.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dtgvnv.SelectedRows[0];
                    int employeeID = (int)selectedRow.Cells[0].Value;  // Lấy ID của nhân viên từ cột đầu tiên

                    // Hỏi người dùng xem có chắc chắn muốn xóa nhân viên này không
                    var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?",
                                                        "Xác nhận xóa",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question);
                    if (confirmResult == DialogResult.Yes)
                    {
                        // Gọi phương thức DeleteById để xóa nhân viên
                        var (result, message) = employeeService.DeleteById(employeeID);

                        // Hiển thị thông báo cho người dùng
                        MessageBox.Show(message, result ? "Thành công" : "Lỗi", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                        // Nếu xóa thành công, cập nhật lại danh sách nhân viên
                        if (result)
                        {
                            var listEmployee = employeeService.GetAll();
                            BindGridEmployees(listEmployee);  // Gọi hàm để bind danh sách nhân viên
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
                // Check if a row is selected
                if (dtgvnv.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dtgvnv.SelectedRows[0];
                    int employeeID = (int)selectedRow.Cells[0].Value;  // Get the selected employee's ID

                    // Validate required fields
                    if (string.IsNullOrEmpty(txtidnv.Text) ||
                        string.IsNullOrEmpty(txttennv.Text) ||
                        string.IsNullOrEmpty(txtchucvunv.Text) ||
                        string.IsNullOrEmpty(txtluongnv.Text))
                    {
                        MessageBox.Show("Vui lòng điền đầy đủ các trường bắt buộc.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Validate salary input
                    if (!decimal.TryParse(txtluongnv.Text, out decimal salary) || salary < 0)
                    {
                        MessageBox.Show("Lương không hợp lệ. Vui lòng nhập số hợp lệ không âm.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Create the updated employee object
                    EMPLOYEE updatedEmployee = new EMPLOYEE
                    {
                        IDEMPLOYEE = employeeID,
                        NAME = txttennv.Text.Trim(),
                        POSITION = txtchucvunv.Text.Trim(),
                        SALARY = (double?)salary,
                        DATE_HIRE = DateTime.Now,  // Assuming you don't want to change the hire date
                        AVATAREMPLOYEE = avartarFilePath  // Update the avatar path if applicable
                    };

                    // Call the service to update the employee
                    var (result, message) = employeeService.Update(updatedEmployee);

                    // Show the result to the user
                    MessageBox.Show(message, result ? "Thành công" : "Lỗi", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                    // If the update was successful, refresh the employee list
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
                // Get the name from a text box (assuming you have a text box named txtSearchName)
                string searchName = txttimidnv.Text.Trim();

                // Validate the input
                if (string.IsNullOrEmpty(searchName))
                {
                    MessageBox.Show("Vui lòng nhập tên nhân viên để tìm kiếm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Call the service to find employees by name
                List<EMPLOYEE> foundEmployees = employeeService.FindByName(searchName);

                // Check if any employees were found
                if (foundEmployees != null && foundEmployees.Count > 0)
                {
                    BindGridEmployees(foundEmployees); // Bind the found employees to the DataGridView
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên nào với tên đã nhập.", "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtgvnv.Rows.Clear(); // Clear the DataGridView if no employees are found
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
                    avartarFilePath = openFileDialog.FileName;  // Lưu đường dẫn của file ảnh đã chọn
                    pictureBox2.Image = Image.FromFile(avartarFilePath);  // Hiển thị ảnh đã chọn lên PictureBox

                    // Lấy ID nhân viên từ TextBox (txtidnv)
                    if (int.TryParse(txtidnv.Text.Trim(), out int IDEMPLOYEE))
                    {
                        // Lưu avatar vào thư mục và cập nhật avatar cho nhân viên
                        string savedAvatarFileName = SaveAvatarnv(avartarFilePath, IDEMPLOYEE);

                        if (savedAvatarFileName != null)
                        {
                            avartarFilePath = savedAvatarFileName;  // Cập nhật đường dẫn avatar vào biến nếu lưu thành công
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
            var employee = employeeService.FindBynvId(employeeID);  // Sử dụng dịch vụ EMPLOYEEService để tìm nhân viên theo ID
            if (employee != null && !string.IsNullOrEmpty(employee.AVATAREMPLOYEE))  // Kiểm tra nếu nhân viên có avatar
            {
                string avatarFilePath = Path.Combine(folderPath, employee.AVATAREMPLOYEE);  // Đường dẫn tới avatar
                if (File.Exists(avatarFilePath))
                {
                    pictureBox2.Image = Image.FromFile(avatarFilePath);  // Hiển thị avatar trong PictureBox
                }
                else
                {
                    pictureBox2.Image = null;  // Nếu không tìm thấy file avatar, không hiển thị ảnh
                }
            }
        }

        private string SaveAvatarnv (string sourceFilePath, int employeeID)
        {
            try
            {
                string folderPath = Path.Combine(Application.StartupPath, "Images");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);  // Tạo thư mục "Images" nếu chưa tồn tại
                }

                string fileExtension = Path.GetExtension(sourceFilePath);  // Lấy phần mở rộng của file ảnh
                string targetFilePath = Path.Combine(folderPath, $"{employeeID}{fileExtension}");  // Đường dẫn lưu ảnh với tên dựa trên employeeID
                if (!File.Exists(sourceFilePath))
                {
                    throw new FileNotFoundException($"Không tìm thấy file: {sourceFilePath}");
                }

                File.Copy(sourceFilePath, targetFilePath, true);  // Sao chép file ảnh đến thư mục, ghi đè nếu có
                return $"{employeeID}{fileExtension}";  // Trả về tên file đã lưu
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu avatar: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void dtgvnv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure that the clicked cell is not a header cell
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dtgvnv.Rows[e.RowIndex];

                // Populate the text boxes with the selected employee's details
                txtidnv.Text = selectedRow.Cells[0].Value?.ToString(); // IDEMPLOYEE
                txttennv.Text = selectedRow.Cells[1].Value?.ToString(); // NAME
                txtchucvunv.Text = selectedRow.Cells[2].Value?.ToString(); // POSITION
                txtluongnv.Text = selectedRow.Cells[3].Value?.ToString(); // SALARY

                // Optionally show the avatar if necessary
                int employeeID = (int)selectedRow.Cells[0].Value; // Assuming the first column is IDEMPLOYEE
                ShowAvatarnv(employeeID);
            }
        }
    }
    }

