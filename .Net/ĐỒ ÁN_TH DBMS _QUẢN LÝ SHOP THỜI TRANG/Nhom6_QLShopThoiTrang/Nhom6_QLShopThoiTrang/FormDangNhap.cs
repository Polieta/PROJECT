using System;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Nhom6_QLShopThoiTrang
{
    public partial class FormDangNhap : Form
    {
        public string maNVLogin = "";
        string ConnStr = Nhom6_QLShopThoiTrang.Properties.Settings.Default.connStr;
        //

        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void btn_DangNhap_Click(object sender, EventArgs e)
        {
            if (txt_TenDangNhap.Text == "" || txt_MatKhau.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin tài khoản và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                //Kết nối CSDL
                SqlConnection sqlConnection = new SqlConnection(ConnStr);
                sqlConnection.Open();
                //Truy vấn dữ liệu
                string sql = "SELECT * FROM Users WHERE Username = '" + txt_TenDangNhap.Text + "' AND Password = '" + txt_MatKhau.Text + "'";
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read() == true)
                {
                    MessageBox.Show("Đăng nhập thành công vào hệ thống quản lý bán hàng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (sqlDataReader["Role"].ToString() == "Admin")
                    {
                        maNVLogin = sqlDataReader["Username"].ToString();
                        FormAdmin formAdmin = new FormAdmin();
                        formAdmin.Show();
                        this.Hide();
                    }
                    else
                    {
                        maNVLogin = sqlDataReader["Username"].ToString();
                        FormNhanVien formNhanVien = new FormNhanVien();
                        formNhanVien.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Lỗi Đăng Nhập Vào Hệ Thống !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void txt_MatKhau_IconRightClick(object sender, EventArgs e)
        {
            if (txt_MatKhau.UseSystemPasswordChar == true)
            {
                txt_MatKhau.UseSystemPasswordChar = false;
                txt_MatKhau.IconRight = Properties.Resources.hidden;
            }
            else
            {
                txt_MatKhau.UseSystemPasswordChar = true;
                txt_MatKhau.IconRight = Properties.Resources.view__1_;
            }
        }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {
            txt_MatKhau.UseSystemPasswordChar = true;
        }
    }
}
