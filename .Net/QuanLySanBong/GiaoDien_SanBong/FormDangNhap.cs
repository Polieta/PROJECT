using Microsoft.SqlServer.Server;
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
    public partial class FormDangNhap : Form
    {
        public FormDangNhap()
        {
            InitializeComponent();
        }
        private int maNV;

        public int MaNV
        {
            get { return maNV; }
            set { maNV = value; }
        }
        private void btn_HideShowPassword_Click(object sender, EventArgs e)
        {
            // Nếu TextBox đang ẩn mật khẩu (PasswordChar = '*')
            if (txtPassword.PasswordChar == '*')
            {
                // Hiện mật khẩu bằng cách đặt PasswordChar thành '\0'
                txtPassword.PasswordChar = '\0';                
            }
            else
            {
                // Ẩn mật khẩu bằng cách đặt PasswordChar thành '*'
                txtPassword.PasswordChar = '*';
            }
        }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            //string connectionString = "Data Source=ANHQUAN\\SQLEXPRESS;Initial Catalog=QLSANBONG;User ID=sa;Password=123;";
              string connectionString = " Data Source=POLIETTA\SQLEXPRESS;Initial Catalog=QLSANBONG;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT MaNV, MaLoaiTK FROM TaiKhoan WHERE TenDangNhap = @Username AND MatKhau = @Password";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int maLoaiTK = reader.GetInt32(1); // Index 1 corresponds to MaLoaiTK in the query

                            

                            this.Hide();
                            FormTrangChu formTC = new FormTrangChu();
                            FormThongTinCaNhan frmTN = new FormThongTinCaNhan();
                            FormDichVu_ThanhToan frmDVTT = new FormDichVu_ThanhToan();
                            ThongTinDatSan thongtin = new ThongTinDatSan();
                            FormNhanSan frmNS = new FormNhanSan(thongtin);
                            FormQuanLyDichVu frmQLDV = new FormQuanLyDichVu();
                            FormQuanLyKhachHang frmQLKH = new FormQuanLyKhachHang();
                            FormBaoCaoDoanhThu frmBCDT = new FormBaoCaoDoanhThu();
                            FormQuanLyNhanVien frmQLNV = new FormQuanLyNhanVien ();
                            FormQuanLyHoaDon frmQLHD = new FormQuanLyHoaDon();
                            if (maLoaiTK == 1)
                            {
                                // Lấy giá trị từ DataTable
                                MaNV = Convert.ToInt32(dt.Rows[0]["MaNV"]);
                                formTC.MaNV = MaNV;
                                frmTN.MaNV = MaNV;
                                frmDVTT.MaNV = MaNV;
                                frmNS.MaNV = MaNV;
                                frmQLDV.MaNV = MaNV;
                                frmQLKH.MaNV = MaNV;
                                frmBCDT.MaNV = MaNV;
                                frmQLNV.MaNV = MaNV;
                                frmQLHD.MaNV = MaNV;
                                // Sử dụng giá trị maNV theo nhu cầu của bạn
                                MessageBox.Show("MaNV: " + MaNV);

                                // Nếu là nhân viên
                                formTC.SetRole("Nhân viên");
                               
                                MessageBox.Show("Đăng nhập thành công! Bạn đang đăng nhập với quyền hạn là Nhân Viên.");
                            }
                            else if (maLoaiTK == 2)
                            {
                                // Nếu là admin
                                formTC.SetRole("Admin");
                                
                                MessageBox.Show("Đăng nhập thành công! Bạn đang đăng nhập với quyền hạn là Quản Lý.");
                                formTC.MaNV = Convert.ToInt32(dt.Rows[0]["MaNV"]);
                                frmTN.MaNV = Convert.ToInt32(dt.Rows[0]["MaNV"]);
                                frmDVTT.MaNV = Convert.ToInt32(dt.Rows[0]["MaNV"]);
                                frmNS.MaNV = Convert.ToInt32(dt.Rows[0]["MaNV"]);
                                frmQLDV.MaNV = Convert.ToInt32(dt.Rows[0]["MaNV"]);
                                frmBCDT.MaNV = Convert.ToInt32(dt.Rows[0]["MaNV"]);
                                frmQLNV.MaNV = Convert.ToInt32(dt.Rows[0]["MaNV"]);
                                frmQLHD.MaNV = Convert.ToInt32(dt.Rows[0]["MaNV"]);
                            }

                            //frmTN.MaNV = MaNV;
                            formTC.ShowDialog();
                            
                        }
                        else
                        {
                            MessageBox.Show("Đăng nhập thất bại. Vui lòng kiểm tra lại tên đăng nhập và mật khẩu.");
                        }
                    }
                }
            }
        }

      
        private void FormDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có chắc muốn đóng ứng dụng không ?","Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (r == DialogResult.No) 
            {
                e.Cancel = true;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormDangNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Khi nhấn phím "Enter", tương tự như click vào nút "Login"
                btnDangNhap.PerformClick();
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            
        }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {

        }
    }
}
