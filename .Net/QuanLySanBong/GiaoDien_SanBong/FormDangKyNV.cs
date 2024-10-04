using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace GiaoDien_SanBong
{
    public partial class FormDangKyNV : Form
    {
        SqlConnection conn;
        private string connectionString = "Data Source=ANHQUAN\\SQLEXPRESS;Initial Catalog=QLSANBONG;User ID=sa;Password=123;";
        public FormDangKyNV()
        {
            InitializeComponent();
        }

        private void btnThemNhanVien_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các TextBox trên form
            string tenNV = txtTenNV.Text;
            string gioiTinh = txtGioiTinh.Text;
            int soDienThoai = int.Parse(txtSDT.Text);
            long cmnd = long.Parse(txtCMND.Text);
            decimal luong = decimal.Parse(txtLuong.Text);
            string tenDangNhap = txtTaiKhoan.Text;
            string matKhau = txtMatKhau.Text;
            int maNV = SinhMaNhanVienTuDong();
            // Kiểm tra xem có ít nhất một mục được chọn trong ComboBox hay không
            if (cboQuyenTruyCap.SelectedIndex >= 0)
            {
                // Lấy giá trị đã chọn từ ComboBox
                string selectedQuyen = cboQuyenTruyCap.SelectedItem.ToString();

                // Lấy giá trị MaLoaiTk từ CSDL dựa trên giá trị đã chọn
                int maLoaiTk = LayMaLoaiTuTenQuyen(selectedQuyen);

                // Tiếp tục với quá trình thêm vào cơ sở dữ liệu
                string insertNhanVienQuery = "INSERT INTO NhanVien (MaNV, TenNV, GioiTinh, SODIENTHOAI, SoCMND, Luong) VALUES (@MaNV, @TenNV, @GioiTinh, @SoDienThoai, @CMND, @Luong)";
 

                if (string.IsNullOrEmpty(tenNV))
                {
                    MessageBox.Show("Bạn chưa nhập tên!");
                }
                else if(gioiTinh != "nam" || gioiTinh != "nữ")
                {
                    MessageBox.Show("Giới tính không hợp lệ. Vui lòng chỉ nhập 'Nam' hoặc 'Nữ'.");
                }    
                else if (txtMatKhau.Text != txtNhapLaiMatKhau.Text)
                {
                    MessageBox.Show("Mật khẩu nhập lại phải trùng khớp");
                }
                else if (string.IsNullOrEmpty(txtLuong.ToString()))
                {
                    txtLuong.Text = "0";
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand(insertNhanVienQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@MaNV", maNV);
                            cmd.Parameters.AddWithValue("@TenNV", tenNV);
                            cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                            cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                            cmd.Parameters.AddWithValue("@CMND", cmnd);
                            cmd.Parameters.AddWithValue("@Luong", luong);

                            cmd.ExecuteNonQuery();
                        }

                        // Thực hiện INSERT để thêm thông tin Tài Khoản với MaNV và MaLoaiTk
                        string insertTaiKhoanQuery = "INSERT INTO TaiKhoan (MaNV, MaLoaiTk, TenDangNhap, MatKhau) VALUES (@MaNV, @MaLoaiTk, @TenDangNhap, @MatKhau)";
                        using (SqlCommand cmd = new SqlCommand(insertTaiKhoanQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@MaNV", maNV);
                            cmd.Parameters.AddWithValue("@MaLoaiTk", maLoaiTk); // Sử dụng giá trị MaLoaiTk đã lấy được
                            cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                            cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Đã thêm thông tin Nhân Viên và Tài Khoản thành công!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn quyền truy cập!");
            }
        }
        private int LayMaLoaiTuTenQuyen(string tenQuyen)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectMaLoaiQuery = "SELECT MaLoaiTK FROM LoaiTaiKhoan WHERE TenLoai = @TenQuyen";
                using (SqlCommand cmd = new SqlCommand(selectMaLoaiQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@TenQuyen", tenQuyen);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
        private int SinhMaNhanVienTuDong()
        {
            int maNVTam = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Tìm mã Nhân Viên lớn nhất hiện có trong cơ sở dữ liệu
                string selectMaxMaNVQuery = "SELECT ISNULL(MAX(MaNV), 0) FROM NhanVien";

                using (SqlCommand cmd = new SqlCommand(selectMaxMaNVQuery, connection))
                {
                    maNVTam = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                }
            }

            return maNVTam;
        }
        private void LoadDataIntoComboBoxQuyenTruyCap()
        {
            // Xóa tất cả các mục hiện tại trong ComboBox
            cboQuyenTruyCap.Items.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Thực hiện truy vấn để lấy dữ liệu quyền truy cập từ cơ sở dữ liệu
                string selectQuyenQuery = "SELECT MaLoaiTk, TenLoai FROM LoaiTaiKhoan";

                using (SqlCommand cmd = new SqlCommand(selectQuyenQuery, connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Lấy dữ liệu từ cột TenQuyen trong dòng hiện tại
                        string tenQuyen = reader["TenLoai"].ToString();

                        // Thêm vào ComboBox
                        cboQuyenTruyCap.Items.Add(tenQuyen);
                    }

                    reader.Close();
                }
            }
        }

        private void FormDangKyNV_Load(object sender, EventArgs e)
        {
            LoadDataIntoComboBoxQuyenTruyCap();
        }
    }
}
