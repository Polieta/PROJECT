using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiaoDien_SanBong
{
    public partial class FormQuanLyNhanVien : Form
    {
        SqlConnection conn;
        private int maNV;

        public int MaNV
        {
            get { return maNV; }
            set { maNV = value; }
        }
        private string userRole;
        string connectionString = "Data Source=ANHQUAN\\SQLEXPRESS;Initial Catalog=QLSANBONG;User ID=sa;Password=123;";
        public string UserRole
        {
            get { return userRole; }
            set { userRole = value; }
        }

        public void SetRole(string role)
        {
            userRole = role;
        }

        // Phương thức để lấy thông tin vai trò từ form khác (nếu cần)
        public string GetRole()
        {
            return userRole;
        }
        public FormQuanLyNhanVien()
        {
            InitializeComponent();
        }
        public FormQuanLyNhanVien(string role) : this()
        {
            userRole = role;
        }
        private void FormQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            btnQLNV.BackColor = Color.Orange;
            LoadDataGridView();
            LoadLoaiTaiKhoanComboBox();
            LoadDataGridView_TK();
        }
        private void LoadDataGridView()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Sử dụng SqlDataAdapter để lấy dữ liệu từ SQL Server và điền vào DataTable
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM NhanVien", connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Đặt DataTable làm nguồn dữ liệu cho DataGridView
                dataGridView1.DataSource = dataTable;
            }
        }
        private void LoadDataGridView_TK()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Sử dụng SqlDataAdapter để lấy dữ liệu từ SQL Server và điền vào DataTable
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TaiKhoan", connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Đặt DataTable làm nguồn dữ liệu cho DataGridView
                dataGridView2.DataSource = dataTable;
            }
        }
        private void btnDatSan_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormTrangChu frmDV_TT = new FormTrangChu();
            frmDV_TT.MaNV = MaNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.Location = this.Location;
            frmDV_TT.ShowDialog();
        }
        ThongTinDatSan thongTin = new ThongTinDatSan();
        private void btnNhanSan_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormNhanSan frmDV_TT = new FormNhanSan(thongTin);
            frmDV_TT.MaNV = MaNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.Location = this.Location;
            frmDV_TT.ShowDialog();
        }

        private void btnDV_TT_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormDichVu_ThanhToan frmDV_TT = new FormDichVu_ThanhToan();
            frmDV_TT.MaNV = MaNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.Location = this.Location;
            frmDV_TT.ShowDialog();
        }

        private void btnThongKeDT_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormBaoCaoDoanhThu frmDV_TT = new FormBaoCaoDoanhThu();
            frmDV_TT.MaNV = MaNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.Location = this.Location;
            frmDV_TT.ShowDialog();
        }

        private void btnQLSB_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormQuanLySanBong frmDV_TT = new FormQuanLySanBong();
            frmDV_TT.MaNV = MaNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.Location = this.Location;
            frmDV_TT.ShowDialog();
        }

        private void btnQLKH_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormQuanLyKhachHang frmDV_TT = new FormQuanLyKhachHang();
            frmDV_TT.MaNV = MaNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.Location = this.Location;
            frmDV_TT.ShowDialog();
        }

        private void btnQLHD_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem vai trò của người dùng có phải là admin không
            if (userRole == "Admin")
            {
                // Mở form quản lý nhân viên
                // Ví dụ: 
                this.Hide();
                FormQuanLyHoaDon formQLHD = new FormQuanLyHoaDon(userRole);
                formQLHD.MaNV = MaNV;
                formQLHD.SetRole(GetRole());
                formQLHD.Location = this.Location;
                formQLHD.ShowDialog();
            }
            else
            {
                // Nếu không phải admin, thông báo lỗi hoặc hiển thị tin nhắn phù hợp
                MessageBox.Show("Bạn không có quyền truy cập chức năng này.", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnQuyDinh_Click(object sender, EventArgs e)
        {

        }

        private void btnQLDV_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormQuanLyDichVu frmDV_TT = new FormQuanLyDichVu();
            frmDV_TT.MaNV = MaNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.Location = this.Location;
            frmDV_TT.ShowDialog();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tenNhanVien = txtNhanVien.Text.Trim();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Sử dụng SqlDataAdapter để lấy dữ liệu từ SQL Server và điền vào DataTable với điều kiện tìm kiếm
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM NhanVien WHERE TenNV LIKE N'%{tenNhanVien}%'", connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Đặt DataTable làm nguồn dữ liệu cho DataGridView
                dataGridView1.DataSource = dataTable;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Lấy giá trị từ dòng được chọn và điền vào các control
                txtTenNV.Text = row.Cells["TenNV"].Value.ToString();
                cboGioiTinh.Text = row.Cells["GioiTinh"].Value.ToString();
                txtSDT.Text = row.Cells["SODIENTHOAI"].Value.ToString();
                txtCMND.Text = row.Cells["SoCMND"].Value.ToString();
                txtLuong.Text = row.Cells["Luong"].Value.ToString();
                txtGhiChu.Text = row.Cells["GhiChu"].Value.ToString();
                txtDiaChi.Text = row.Cells["DIACHI"].Value.ToString();
                mskNgaySinh.Text = row.Cells["NGAYSINH"].Value.ToString();

                if (DateTime.TryParse(row.Cells["NGAYSINH"].Value.ToString(), out DateTime ngaySinh))
                {
                    // Gán giá trị vào MaskedTextBox
                    mskNgaySinh.Text = ngaySinh.ToString("yyyy/MM/dd");
                }
                // Load thông tin tài khoản
                LoadTaiKhoanInfo(Convert.ToInt32(row.Cells["MaNV"].Value));
            }
        }
        private void LoadTaiKhoanInfo(int maNV)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Sử dụng SqlDataAdapter để lấy thông tin tài khoản từ SQL Server và điền vào DataTable
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM TaiKhoan WHERE MaNV = {maNV}", connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Kiểm tra xem có thông tin tài khoản không
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];

                    // Điền thông tin tài khoản vào các control
                    txtTaiKhoan.Text = row["TenDangNhap"].ToString();
                    txtMatKhau.Text = row["MatKhau"].ToString();

                    // Load danh sách loại tài khoản vào ComboBox cboQuyenTruyCap
                    LoadLoaiTaiKhoanComboBox();
                    cboQuyenTruyCap.SelectedValue = Convert.ToInt32(row["MaLoaiTk"]);
                }
            }
        }
        private void LoadLoaiTaiKhoanComboBox()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Sử dụng SqlDataAdapter để lấy danh sách loại tài khoản từ SQL Server và điền vào DataTable
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM LoaiTaiKhoan", connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Đặt DataTable làm nguồn dữ liệu cho ComboBox cboQuyenTruyCap
                cboQuyenTruyCap.DataSource = dataTable;
                cboQuyenTruyCap.DisplayMember = "TenLoai";
                cboQuyenTruyCap.ValueMember = "MaLoaiTK";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                int maNV = Convert.ToInt32(row.Cells["MaNV"].Value);

                // Cập nhật thông tin nhân viên
                UpdateNhanVienInfo(maNV);

                // Cập nhật thông tin tài khoản
                UpdateTaiKhoanInfo(maNV);

                // Sau khi cập nhật, load lại dữ liệu
                LoadDataGridView();
                LoadDataGridView_TK();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void UpdateNhanVienInfo(int maNV)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Kiểm tra ngày sinh
                DateTime? ngaySinh = null; // Giá trị mặc định là null

                if (!string.IsNullOrEmpty(mskNgaySinh.Text))
                {
                    // Ngày sinh không rỗng, thực hiện chuyển đổi
                    if (DateTime.TryParse(mskNgaySinh.Text, out DateTime tempNgaySinh))
                    {
                        ngaySinh = tempNgaySinh;
                    }
                    else
                    {
                        // Ngày sinh rỗng, hiển thị thông báo và kiểm tra xem người dùng có muốn tiếp tục không
                        DialogResult result = MessageBox.Show("Ngày sinh bạn chưa nhập. Bạn muốn tiếp tục không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (result == DialogResult.No)
                        {
                            // Nếu người dùng không muốn tiếp tục, thoát khỏi phương thức và đặt giá trị ngày sinh là null
                            ngaySinh = null;
                        }
                        // Nếu người dùng chọn Yes, giữ ngày sinh là giá trị mặc định là null
                    }
                }

                // Viết mã SQL để cập nhật thông tin nhân viên
                string query = "UPDATE NhanVien SET TenNV = @TenNV, GioiTinh = @GioiTinh, SODIENTHOAI = @SODIENTHOAI, SoCMND = @SoCMND, Luong = @Luong, GhiChu = @GhiChu, NGAYSINH = @NgaySinh, DIACHI = @DiaChi WHERE MaNV = @MaNV";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TenNV", txtTenNV.Text);
                    command.Parameters.AddWithValue("@GioiTinh", cboGioiTinh.Text);
                    command.Parameters.AddWithValue("@SODIENTHOAI", txtSDT.Text);
                    command.Parameters.AddWithValue("@SoCMND", txtCMND.Text);
                    command.Parameters.AddWithValue("@Luong", txtLuong.Text);
                    command.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                    command.Parameters.AddWithValue("@NgaySinh", (object)ngaySinh ?? DBNull.Value); // Đặt giá trị null nếu ngày sinh là null

                    command.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    command.Parameters.AddWithValue("@MaNV", maNV);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void UpdateTaiKhoanInfo(int maNV)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Kiểm tra xem nhân viên có tài khoản chưa
                string checkQuery = "SELECT COUNT(*) FROM TaiKhoan WHERE MaNV = @MaNV";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@MaNV", maNV);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        // Nếu đã có tài khoản, thực hiện cập nhật
                        string updateQuery = "UPDATE TaiKhoan SET TenDangNhap = @TenDangNhap, MaLoaiTk = @MaLoaiTk WHERE MaNV = @MaNV";

                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@TenDangNhap", txtTaiKhoan.Text);
                            //updateCommand.Parameters.AddWithValue("@MatKhau", txtMatKhau.Text);
                            updateCommand.Parameters.AddWithValue("@MaLoaiTk", cboQuyenTruyCap.SelectedValue.ToString());
                            updateCommand.Parameters.AddWithValue("@MaNV", maNV);

                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Nếu chưa có tài khoản, thực hiện thêm mới
                        string insertQuery = "INSERT INTO TaiKhoan (MaNV, TenDangNhap, MatKhau, MaLoaiTk) VALUES (@MaNV, @TenDangNhap, @MatKhau, @MaLoaiTk)";

                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@MaNV", maNV);
                            insertCommand.Parameters.AddWithValue("@TenDangNhap", txtTaiKhoan.Text);
                            insertCommand.Parameters.AddWithValue("@MatKhau", txtMatKhau.Text);
                            insertCommand.Parameters.AddWithValue("@MaLoaiTk", cboQuyenTruyCap.SelectedValue);

                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            FormDangKyNV frmDK = new FormDangKyNV();
            frmDK.ShowDialog();
        }
        
        private void btnResetPassoword_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Lấy mã nhân viên từ dòng được chọn
                int maNV = Convert.ToInt32(selectedRow.Cells["MaNV"].Value);

                // Thực hiện đặt lại mật khẩu
                if (ResetPassword(maNV))
                {
                    MessageBox.Show("Mật khẩu đã được đặt lại thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGridView_TK();
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi khi đặt lại mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để đặt lại mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private bool ResetPassword(int maNV)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Viết mã SQL để đặt lại mật khẩu của nhân viên có mã nhân viên là maNV
                    string query = "UPDATE TaiKhoan SET MatKhau = @MatKhau WHERE MaNV = @MaNV";
                    string newPassword = txtMatKhau.Text; // Hàm này để tạo một mật khẩu ngẫu nhiên, bạn có thể thay thế bằng logic của mình

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MatKhau", newPassword);
                        command.Parameters.AddWithValue("@MaNV", maNV);

                        command.ExecuteNonQuery();

                        // Nếu bạn muốn trả về mật khẩu mới, bạn có thể sử dụng biến newPassword tại đây
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // Log hoặc xử lý lỗi theo cách của bạn
                Console.WriteLine("Lỗi: " + ex.Message);
                return false;
            }
        }

        private void btnXoaNhanVien_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Lấy mã nhân viên từ dòng được chọn
                int maNV = Convert.ToInt32(selectedRow.Cells["MaNV"].Value);

                // Hiển thị thông báo xác nhận xóa
                DialogResult result = MessageBox.Show("Bạn có muốn xóa nhân viên và tài khoản nhân viên không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Thực hiện xóa nhân viên và tài khoản nhân viên
                    if (XoaNhanVien(maNV))
                    {
                        MessageBox.Show("Nhân viên và tài khoản đã được xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Sau khi xóa thành công, bạn có thể cập nhật DataGridView để hiển thị dữ liệu mới
                        LoadDataGridView();
                        LoadDataGridView_TK();
                    }
                    else
                    {
                        MessageBox.Show("Đã xảy ra lỗi khi xóa nhân viên và tài khoản.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private bool XoaNhanVien(int maNV)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Viết mã SQL để xóa nhân viên có mã nhân viên là maNV
                    string queryNhanVien = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
                    string queryTaiKhoan = "DELETE FROM TaiKhoan WHERE MaNV = @MaNV";

                    using (SqlCommand commandNhanVien = new SqlCommand(queryNhanVien, connection))
                    using (SqlCommand commandTaiKhoan = new SqlCommand(queryTaiKhoan, connection))
                    {
                        commandNhanVien.Parameters.AddWithValue("@MaNV", maNV);
                        commandTaiKhoan.Parameters.AddWithValue("@MaNV", maNV);

                        // Thực hiện xóa trong cả hai bảng
                        commandTaiKhoan.ExecuteNonQuery();
                        commandNhanVien.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // Log hoặc xử lý lỗi theo cách của bạn
                Console.WriteLine("Lỗi: " + ex.Message);
                return false;
            }
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void btnQLNV_Click(object sender, EventArgs e)
        {

        }

        private void cboQuyenTruyCap_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            // Mở FormThongTinCaNhan và truyền thông tin người dùng
            FormThongTinCaNhan formThongTinCaNhan = new FormThongTinCaNhan();
            //formThongTinCaNhan.SetRole(GetRole());
            //formThongTinCaNhan.SetMaNV(GetManV());
            formThongTinCaNhan.MaNV = maNV;
            formThongTinCaNhan.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận trước khi đóng form hiện tại
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đóng form hiện tại?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Kiểm tra xem người dùng đã chọn Yes hay không
            if (result == DialogResult.Yes)
            {
                // Ẩn form hiện tại
                this.Hide();

                // Mở FormThongTinCaNhan và truyền thông tin người dùng
                FormDangNhap formThongTinCaNhan = new FormDangNhap();

                // Hiển thị form mới
                formThongTinCaNhan.ShowDialog();

                // Hiển thị lại form hiện tại khi form mới đóng
                this.Show();
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}

