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
    public partial class FormThongTinCaNhan : Form
    {


        private int maNV;

        public int MaNV
        {
            get { return maNV; }
            set { maNV = value; }
        }
        public FormThongTinCaNhan()
        {
            InitializeComponent();
        }

        private void FormThongTinCaNhan_Load(object sender, EventArgs e)
        {
            txtMatKhau.Enabled = false;
            txtTenNV.Text = maNV.ToString();
            string connectionString = "Data Source=ANHQUAN\\SQLEXPRESS;Initial Catalog=QLSANBONG;User ID=sa;Password=123;";
            // Kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Viết mã SQL để truy vấn thông tin nhân viên và mật khẩu
                string query = "SELECT NhanVien.*, TaiKhoan.MatKhau FROM NhanVien LEFT JOIN TaiKhoan ON NhanVien.MaNV = TaiKhoan.MaNV WHERE NhanVien.MaNV = @MaNV";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thêm tham số MaNV vào truy vấn
                    command.Parameters.AddWithValue("@MaNV", maNV);

                    // Thực hiện truy vấn và đọc dữ liệu

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Kiểm tra xem có dữ liệu hay không
                        if (reader.Read())
                        {
                            // Hiển thị thông tin nhân viên lên giao diện
                            txtTenNV.Text = reader["TenNV"].ToString();
                            txtGioiTinh.Text = reader["GioiTinh"].ToString();

                            // Kiểm tra nếu cột "NGAYSINH" không phải là DBNull
                            if (reader["NGAYSINH"] != DBNull.Value)
                            {
                                mskNgaySinh.Text = ((DateTime)reader["NGAYSINH"]).ToString("yyyy/MM/dd");
                            }
                            else
                            {
                                mskNgaySinh.Text = string.Empty; // hoặc bạn có thể thiết lập mskNgaySinh.Text thành giá trị mặc định khác
                            }

                            txtDiaChi.Text = reader["DIACHI"].ToString();
                            txtSDT.Text = reader["SODIENTHOAI"].ToString();
                            txtCMND.Text = reader["SoCMND"].ToString();
                            txtMatKhau.Text = reader["MatKhau"].ToString();

                            // Các trường khác tương tự
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void btnCapNhatMK_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=MSI;Initial Catalog=QLSANBONG;User Id=taanhtuan193;Password=123;";
            // Lấy mật khẩu mới từ TextBox
            string matKhauMoi = txtMKmoi.Text;
            string matKhauMoiNhapLai = txtMKMoiNhapLai.Text;

            // Kiểm tra xem mật khẩu mới có trùng khớp không
            if (matKhauMoi != matKhauMoiNhapLai)
            {
                MessageBox.Show("Mật khẩu mới và nhập lại mật khẩu mới không trùng khớp. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Cập nhật mật khẩu trong bảng TaiKhoan
                string updateQuery = "UPDATE TaiKhoan SET MatKhau = @MatKhau WHERE MaNV = @MaNV";

                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                {
                    // Thêm tham số MaNV và MatKhau vào truy vấn
                    updateCommand.Parameters.AddWithValue("@MaNV", maNV);
                    updateCommand.Parameters.AddWithValue("@MatKhau", matKhauMoi);

                    // Thực hiện cập nhật
                    updateCommand.ExecuteNonQuery();

                    MessageBox.Show("Cập nhật mật khẩu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=MSI;Initial Catalog=QLSANBONG;User Id=taanhtuan193;Password=123;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Viết mã SQL để cập nhật thông tin nhân viên
                string query = "UPDATE NhanVien SET TenNV = @TenNV, GioiTinh = @GioiTinh, SODIENTHOAI = @SODIENTHOAI, SoCMND = @SoCMND, NGAYSINH= @NgaySinh, DIACHI = @DiaChi WHERE MaNV = @MaNV";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TenNV", txtTenNV.Text);
                    command.Parameters.AddWithValue("@GioiTinh", txtGioiTinh.Text);
                    command.Parameters.AddWithValue("@SODIENTHOAI", txtSDT.Text);
                    command.Parameters.AddWithValue("@SoCMND", txtCMND.Text);

                    // Chuyển đổi ngày từ mskNgaySinh.Text sang DateTime
                    DateTime ngaySinh;
                    if (DateTime.TryParse(mskNgaySinh.Text, out ngaySinh))
                    {
                        command.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                    }
                    else
                    {
                        // Xử lý trường hợp chuyển đổi thất bại (cung cấp giá trị mặc định hoặc hiển thị thông báo lỗi)
                        // Trong ví dụ này, sử dụng DateTime.MinValue làm giá trị mặc định
                        command.Parameters.AddWithValue("@NgaySinh", DateTime.MinValue);
                    }

                    command.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    command.Parameters.AddWithValue("@MaNV", maNV);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật thông tin thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không có dòng nào được cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void mskNgaySinh_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
