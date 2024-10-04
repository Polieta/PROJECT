using CrystalDecisions.ReportAppServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiaoDien_SanBong
{
    public partial class FormQuanLyHoaDon : Form
    {
        private int selectedMaHoaDon;
        private int maNV;
        string connectionString = "Data Source=POLIETTA\\SQLEXPRESS;Initial Catalog=QLSANBONG;Integrated Security=True";
        public int MaNV
        {
            get { return maNV; }
            set { maNV = value; }
        }
        private string userRole;
        public string UserRole
        {
            get { return userRole; }
            set { userRole = value; }
        }
        // Các phương thức và logic khác của FormTrangChu

        // Phương thức để thiết lập vai trò từ form đăng nhập
        public void SetRole(string role)
        {
            userRole = role;
        }
        public FormQuanLyHoaDon(string role) : this()
        {
            userRole = role;
        }
        // Phương thức để lấy thông tin vai trò từ form khác (nếu cần)
        public string GetRole()
        {
            return userRole;
        }
        public FormQuanLyHoaDon()
        {
            InitializeComponent();
        }
        private void LoadDataGridView()
        {
            int currentYear = DateTime.Now.Year;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Sử dụng SqlDataAdapter để lấy dữ liệu từ SQL Server và điền vào DataTable
                string selectQuery = "SELECT * FROM HoaDon WHERE YEAR(NgayTao) = @CurrentYear";
                SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@CurrentYear", currentYear);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Đặt DataTable làm nguồn dữ liệu cho DataGridView
                dataGridView1.DataSource = dataTable;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //// Mở FormDichVu_ThanhToan và truyền vai trò
            FormHoaDonThanhToan frmDV_TT = new FormHoaDonThanhToan();
            frmDV_TT.MaNV = MaNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.Location = this.Location;
            if (selectedMaHoaDon > 0)
            {
                FormHoaDonThanhToan formChiTietHoaDon = new FormHoaDonThanhToan();
                formChiTietHoaDon.SetMaHoaDon(selectedMaHoaDon);
                formChiTietHoaDon.Show();
            }

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
        private void btnDatSan_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormTrangChu frmDV_TT = new FormTrangChu();
            frmDV_TT.MaNV = MaNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.ShowDialog();
        }
        ThongTinDatSan thongTin = new ThongTinDatSan();
        private void btnNhanSan_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormNhanSan frmDV_TT = new FormNhanSan(thongTin);
            frmDV_TT.MaNV = MaNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.ShowDialog();
        }

        private void btnDV_TT_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormDichVu_ThanhToan frmDV_TT = new FormDichVu_ThanhToan();
            frmDV_TT.MaNV = MaNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.ShowDialog();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem vai trò của người dùng có phải là admin không
            if (userRole == "Admin")
            {

                this.Hide();
                FormBaoCaoDoanhThu formBCDT = new FormBaoCaoDoanhThu(userRole);
                formBCDT.MaNV = MaNV;
                formBCDT.SetRole(GetRole());
                formBCDT.Location = this.Location;
                formBCDT.ShowDialog();
            }
            else
            {
                // Nếu không phải admin, thông báo lỗi hoặc hiển thị tin nhắn phù hợp
                MessageBox.Show("Bạn không có quyền truy cập chức năng này.", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnQLNV_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem vai trò của người dùng có phải là admin không
            if (userRole == "Admin")
            {
                // Mở form quản lý nhân viên
                // Ví dụ: 
                this.Hide();
                FormQuanLyNhanVien formQLNV = new FormQuanLyNhanVien();
                formQLNV.MaNV = MaNV;
                formQLNV.SetRole(GetRole());
                formQLNV.Location = this.Location;
                formQLNV.ShowDialog();
            }
            else
            {
                // Nếu không phải admin, thông báo lỗi hoặc hiển thị tin nhắn phù hợp
                MessageBox.Show("Bạn không có quyền truy cập chức năng này.", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnQLSB_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormQuanLySanBong frmDV_TT = new FormQuanLySanBong();
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.ShowDialog();
        }

        private void btnQLKH_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem vai trò của người dùng có phải là admin không
            if (userRole == "Admin")
            {
                // Mở form quản lý nhân viên
                // Ví dụ: 
                this.Hide();
                FormQuanLyKhachHang formQLNV = new FormQuanLyKhachHang(userRole);
                formQLNV.MaNV = MaNV;
                formQLNV.SetRole(GetRole());
                formQLNV.Location = this.Location;
                formQLNV.ShowDialog();
            }
            else
            {
                // Nếu không phải admin, thông báo lỗi hoặc hiển thị tin nhắn phù hợp
                MessageBox.Show("Bạn không có quyền truy cập chức năng này.", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private void FormQuanLyHoaDon_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                cboMaHD.Text = row.Cells["MaHoaDon"].Value.ToString();
                // Lấy giá trị từ dòng được chọn và điền vào các control
                txtTT.Text = row.Cells["MaTrangThaiHoaDon"].Value.ToString();
                txtDonGia.Text = row.Cells["GiaSan"].Value.ToString();
                txtGiamGia.Text = row.Cells["GiamGia"].Value.ToString();
                txtThanhtien.Text = row.Cells["TongTien"].Value.ToString();
                mskNgayTao.Text = row.Cells["NgayTao"].Value.ToString();

                string connectionString = "Data Source=DESKTOP-8HIB0JN\\SQLEXPRESS;Initial Catalog=QLSANBONG;Integrated Security = true;";
                int maNhanSan = int.Parse(row.Cells["MaNhanSan"].Value.ToString());
                int maNhanVien = int.Parse(row.Cells["MaNV"].Value.ToString());
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string selectTenSanBongQuery = "SELECT LoaiSan.Ten " +
                                                  "FROM NhanSan " +
                                                  "INNER JOIN SanBong ON NhanSan.MaSanBong = SanBong.MaSanBong " +
                                                  "INNER JOIN LoaiSan ON SanBong.MaLoaiSan = LoaiSan.MaLoaiSan " +
                                                  "WHERE NhanSan.MaNhanSan = @MaNhanSan";

                    SqlCommand cmdSelectTenSanBong = new SqlCommand(selectTenSanBongQuery, conn);
                    cmdSelectTenSanBong.Parameters.AddWithValue("@MaNhanSan", maNhanSan);

                    object tenSanBong = cmdSelectTenSanBong.ExecuteScalar();

                    txtTenSB.Text = tenSanBong.ToString();
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                    selectedMaHoaDon = Convert.ToInt32(selectedRow.Cells["MaHoaDon"].Value);

                }
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string selectTenSanBongQuery = "SELECT NhanVien.TenNV " +
                                                  "FROM NhanVien " +                                                  
                                                  "WHERE NhanVien.MaNV = @MaNV";

                    SqlCommand cmdSelectTenNV = new SqlCommand(selectTenSanBongQuery, conn);
                    cmdSelectTenNV.Parameters.AddWithValue("@MaNV", maNhanSan);

                    object tenNhanVien = cmdSelectTenNV.ExecuteScalar();

                    txtTenNV.Text = tenNhanVien.ToString();

                }

                if (DateTime.TryParse(row.Cells["NgayTao"].Value.ToString(), out DateTime ngaySinh))
                {
                    // Gán giá trị vào MaskedTextBox
                    mskNgayTao.Text = ngaySinh.ToString("yyyy/MM/dd");
                }  
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maHoaDon= txtTimKiem.Text.Trim();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Sử dụng SqlDataAdapter để lấy dữ liệu từ SQL Server và điền vào DataTable với điều kiện tìm kiếm
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM hoaDon WHERE MaHoaDon LIKE N'%{maHoaDon}%'", connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Đặt DataTable làm nguồn dữ liệu cho DataGridView
                dataGridView1.DataSource = dataTable;
            }
        }
    
        private void cboLoaiTimKiem_SelectedIndexChanged(object sender, EventArgs e)
        {
            string luaChon = cboLoc.SelectedItem.ToString();

            // Hiển thị hoặc ẩn ô nhập liệu tùy theo lựa chọn
            txtLocTheoThangNam.Visible = (luaChon == "Lọc theo tháng" || luaChon == "Lọc theo năm");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string luaChon = cboLoc.SelectedItem.ToString();
            string locTheoThangNam = txtLocTheoThangNam.Text.Trim();

            // Thực hiện lọc dựa trên lựa chọn và giá trị nhập liệu
            switch (luaChon)
            {
                case "Lọc theo tháng":
                    if (int.TryParse(locTheoThangNam, out int thang))
                    {
                        // Thực hiện lọc theo tháng
                        TimKiemTheoThang(thang);
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập số tháng hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                case "Lọc theo năm":
                    if (int.TryParse(locTheoThangNam, out int nam))
                    {
                        // Thực hiện lọc theo năm
                        TimKiemTheoNam(nam);
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập số năm hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                default:
                    MessageBox.Show("Lựa chọn không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
        private void TimKiemTheoThang(int thang)
        {
            // Thực hiện lọc theo tháng và cập nhật DataGridView
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string selectQuery = "SELECT * FROM HoaDon WHERE MONTH(NgayTao) = @Thang";
                SqlCommand cmd = new SqlCommand(selectQuery, conn);
                cmd.Parameters.AddWithValue("@Thang", thang);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }

        private void TimKiemTheoNam(int nam)
        {
            // Thực hiện lọc theo năm và cập nhật DataGridView
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string selectQuery = "SELECT * FROM HoaDon WHERE YEAR(NgayTao) = @Nam";
                SqlCommand cmd = new SqlCommand(selectQuery, conn);
                cmd.Parameters.AddWithValue("@Nam", nam);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }

        private void txtLocTheoThangNam_TextChanged(object sender, EventArgs e)
        {
            string locTheoThangNam = txtLocTheoThangNam.Text.Trim();
            string luaChon = cboLoc.SelectedItem?.ToString();

            // Kiểm tra có đang chọn "Lọc theo tháng" hay không
            if (luaChon == "Lọc theo tháng")
            {
                // Kiểm tra độ dài và không cho nhập quá 2 kí tự
                if (locTheoThangNam.Length > 2)
                {
                    MessageBox.Show("Vui lòng nhập không quá 2 chữ số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Bạn có thể xóa kí tự vừa nhập để người dùng nhập lại
                    txtLocTheoThangNam.Text = locTheoThangNam.Substring(0, locTheoThangNam.Length - 1);
                    return;
                }

                // Kiểm tra chỉ cho phép nhập số và không nhập kí tự đặc biệt
                if (!int.TryParse(locTheoThangNam, out int thang) || thang <= 0 || thang > 12)
                {
                    MessageBox.Show("Vui lòng nhập số tháng hợp lệ (1-12).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (locTheoThangNam.Length > 0)
                    {
                        txtLocTheoThangNam.Text = locTheoThangNam.Substring(0, locTheoThangNam.Length - 1);
                    }
                    return;
                }
            }
            else if (luaChon == "Lọc theo năm")
            {
                // Kiểm tra chỉ cho phép nhập số và không nhập kí tự đặc biệt
                if (!int.TryParse(locTheoThangNam, out int nam) || nam <= 0)
                {
                    MessageBox.Show("Vui lòng nhập số năm hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Bạn có thể xóa kí tự vừa nhập để người dùng nhập lại
                    if (locTheoThangNam.Length > 0)
                    {
                        txtLocTheoThangNam.Text = locTheoThangNam.Substring(0, locTheoThangNam.Length - 1);
                    }
                    return;
                }
            }
            else
            {
                // Nếu không chọn cụ thể "Lọc theo tháng" hoặc "Lọc theo năm", không thực hiện kiểm tra chi tiết
            }
        }
    }
}
