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
    public partial class FormHoaDonThanhToan : Form
    {
        private int maNV;
        private string userRole;
        private int Manv;
        public int MaNV
        {
            get { return maNV; }
            set { maNV = value; }
        }
        // Phương thức để thiết lập vai trò từ form đăng nhập
        public void SetRole(string role)
        {
            userRole = role;

        }
        public void SetMaNV(int maNV)
        {
            Manv = maNV;

        }
        // Phương thức để lấy thông tin vai trò từ form khác (nếu cần)
        public string GetRole()
        {
            return userRole;
        }
        public int GetManV()
        {
            return Manv;
        }
        public FormHoaDonThanhToan()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem vai trò của người dùng có phải là admin không
            if (userRole == "Admin")
            {
                // Mở form quản lý nhân viên
                // Ví dụ: 
                FormBaoCaoDoanhThu formQLNV = new FormBaoCaoDoanhThu(userRole);
                formQLNV.ShowDialog();
            }
            else
            {
                // Nếu không phải admin, thông báo lỗi hoặc hiển thị tin nhắn phù hợp
                MessageBox.Show("Bạn không có quyền truy cập chức năng này.", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void FormHoaDonThanhToan_Load(object sender, EventArgs e)
        {
            
        }

        private void btnDatSan_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormTrangChu formTC = new FormTrangChu();

            formTC.Location = this.Location;
            // Hiển thị Form2
            formTC.ShowDialog();
        }
        ThongTinDatSan thongTin = new ThongTinDatSan();
        private void btnNhanSan_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormNhanSan formNS = new FormNhanSan(thongTin);

            formNS.Location = this.Location;
            // Hiển thị Form2
            formNS.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormQuanLySanBong formQLSB = new FormQuanLySanBong();

            formQLSB.Location = this.Location;
            // Hiển thị Form2
            formQLSB.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem vai trò của người dùng có phải là admin không
            if (userRole == "Admin")
            {
                // Mở form quản lý nhân viên
                // Ví dụ: 
                FormQuanLyKhachHang formQLNV = new FormQuanLyKhachHang(userRole);
                formQLNV.ShowDialog();
            }
            else
            {
                // Nếu không phải admin, thông báo lỗi hoặc hiển thị tin nhắn phù hợp
                MessageBox.Show("Bạn không có quyền truy cập chức năng này.", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormQuanLyDichVu frmQLDV = new FormQuanLyDichVu();

            frmQLDV.Location = this.Location;
            // Hiển thị Form2
            frmQLDV.ShowDialog();
        }

        private void btnDV_TT_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormDichVu_ThanhToan formDV_TT = new FormDichVu_ThanhToan();

            formDV_TT.Location = this.Location;
            // Hiển thị Form2
            formDV_TT.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Đây là quản lý hóa đơn 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void SetMaHoaDon(int maHoaDon)
        {
            string connectionString = "Data Source=ANHQUAN\\SQLEXPRESS;Initial Catalog=QLSANBONG;User ID=sa;Password=123;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT HoaDon.GiaSan, HoaDon.GiaDichVu, HoaDon.TongTien, HoaDon. GiamGia, HoaDon.PhuPhi, HoaDon.MaHoaDon, NhanVien.TenNV, HoaDon.NgayTao," +
                                   " ChiTietNhanSan.MaKH,  KhachHang.TenKH,  KhachHang.MaThe," +
                                   " KhachHang.NamSinh, KhachHang.DiaChi, KhachHang.SDT, KhachHang.GioiTinh, KhachHang.QuocTich, " +
                                   " LoaiSan.Ten AS TenLoaiSan, LoaiSan.Gia AS GiaLoaiSan, LoaiSan.SoLuongNguoiDa AS SoLuongNguoiDa, " +
                                   " SanBong.Ten AS TenSanBong, " +
                                   " PhieuDatSan.NgayDat AS NgayDatSan, PhieuDatSan.NgayNhan, PhieuDatSan.NgayTra " +
                                   "FROM HoaDon " +
                                   "INNER JOIN NhanSan ON HoaDon.MaNhanSan = NhanSan.MaNhanSan " +
                                   "INNER JOIN ChiTietNhanSan ON NhanSan.MaNhanSan = ChiTietNhanSan.MaNhanSan " +
                                   "INNER JOIN KhachHang ON ChiTietNhanSan.MaKH = KhachHang.MaKH " +
                                   "INNER JOIN NhanVien ON HoaDon.MaNV = NhanVien.MaNV " +
                                   "INNER JOIN SanBong ON NhanSan.MaSanBong = SanBong.MaSanBong " +
                                   "INNER JOIN PhieuDatSan ON SanBong.MaSanBong = PhieuDatSan.MaSanBong " +
                                   "INNER JOIN LoaiSan ON SanBong.MaLoaiSan = LoaiSan.MaLoaiSan " +
                                   "WHERE HoaDon.MaHoaDon = @MaHoaDon"; 

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int mahd = int.Parse(reader["MaHoaDon"].ToString());
                                string tenNV = reader["TenNV"].ToString();
                                DateTime ngayLap = Convert.ToDateTime(reader["NgayTao"]);
                                double giaSan = double.Parse(reader["GiaSan"].ToString());
                                string phuThu = reader["PhuPhi"].ToString();
                                string giamGia = reader["GiamGia"].ToString();
                                string giaDV = reader["GiaDichVu"].ToString();
                                string tongTien = reader["TongTien"].ToString();
                                //Lay thong tin khach hang
                                int maKhachHang = int.Parse(reader["MaKH"].ToString());
                                string tenKhachHang = reader["TenKH"].ToString();
                                string diaChi = reader["DiaChi"].ToString();
                                string quocTich = reader["QuocTich"].ToString();
                                string SDT = reader["SDT"].ToString();
                                string maThe = reader["MaThe"].ToString();
                                string gioiTinh = reader["GioiTinh"].ToString();
                                //Lay thong tin loai san
                                string tenSan = reader["TenLoaiSan"].ToString();
                                double giaLoaiSan = double.Parse(reader["GiaLoaiSan"].ToString());
                                int soLuongNguoiDa = int.Parse(reader["SoLuongNguoiDa"].ToString());
                                //Lay thong tin phieu dat san
                                DateTime ngayDen = Convert.ToDateTime(reader["NgayDatSan"]);
                                DateTime ngayNhan = Convert.ToDateTime(reader["NgayNhan"]);
                                DateTime ngayTra = Convert.ToDateTime(reader["NgayTra"]);
                                //Lay thong tin san bong
                                string tenSanBong = reader["TenSanBong"].ToString();
                                //Tính thời gian đã đặt
                                TimeSpan thoiGianDat = ngayTra - ngayNhan;
                                int soGioDat = (int)thoiGianDat.TotalHours;
                                //Gán các thông tin cho label
                                label32.Text = mahd.ToString();
                                label33.Text = tenNV;
                                label34.Text = ngayLap.ToString("yyyy-MM-dd");
                                label13.Text = tenKhachHang;
                                label17.Text = diaChi;
                                label15.Text = SDT;
                                label18.Text = quocTich;
                                label14.Text = maThe;
                                label37.Text = giaSan.ToString();
                                label41.Text = phuThu;
                                label42.Text = giamGia;
                                label45.Text = giaDV;
                                label38.Text = tongTien;
                                label25.Text = tenSan;
                                label27.Text = giaLoaiSan.ToString();
                                label30.Text = soLuongNguoiDa.ToString();
                                label28.Text = ngayDen.ToString();
                                label29.Text = soGioDat.ToString();
                                label26.Text = tenSanBong;
                                label16.Text = gioiTinh;
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy hóa đơn có mã " + maHoaDon, "Thông báo",
                                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
