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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GiaoDien_SanBong
{
    public partial class FormNhanSan : Form
    {
        SqlConnection conn;
        DataSet ds;
        SqlDataAdapter adapt;
        DataColumn[] key = new DataColumn[1];

        private ThongTinDatSan thongTinDatSan;

        private int maNV;

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

        public void SetRole(string role)
        {
            userRole = role;
        }


        public string GetRole()
        {
            return userRole;
        }

        public FormNhanSan(ThongTinDatSan thongTin)
        {
            InitializeComponent();
            string sqlconnect = "Data Source=ANHQUAN\\SQLEXPRESS;Initial Catalog=QLSANBONG;User ID=sa;Password=123;";
            conn = new SqlConnection(sqlconnect);
            ds = new DataSet();

            thongTinDatSan = thongTin;
            // Hiển thị thông tin lên các control
            textBox2.Text = thongTin.HoTen;
            if (thongTin.CCCD == 0)
                textBox3.Text = string.Empty;
            else
                textBox3.Text = thongTin.CCCD.ToString();
            textBox4.Text = thongTin.TenSan;
            textBox5.Text = thongTin.TenLoaiSan;
            if (thongTin.NgayNhan == DateTime.MinValue)
                textBox6.Text = string.Empty;
            else
                textBox6.Text = thongTin.NgayNhan.ToString("yyyy-MM-dd");

            if (thongTin.NgayTra == DateTime.MinValue)
                textBox7.Text = string.Empty;
            else
                textBox7.Text = thongTin.NgayTra.ToString("yyyy-MM-dd");

            if (thongTin.SoLuongNguoiToiDa == 0)
                textBox8.Text = string.Empty;
            else
                textBox8.Text = thongTin.SoLuongNguoiToiDa.ToString();

            if (thongTin.Gia == 0)
                textBox9.Text = string.Empty;
            else
                textBox9.Text = thongTin.Gia.ToString();

            comboBox1.SelectedItem = thongTin.maLoaisan;
            comboBox2.SelectedItem = thongTin.maSanbong;

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

        private void FormNhanSan_Load(object sender, EventArgs e)
        {
            btnNhanSan.BackColor = Color.Orange;
            LoadDataToDataGridView_nhansan();
            LoadLoaiSanComboBox();
            LoadSanBongComboBox();
            dataGridView1.CellClick += dataGridView1_CellClick;

        }
        

        private void LoadDataToDataGridView_nhansan()
        {
            // Lấy ngày hiện tại
            DateTime currentDate = DateTime.Now;

            // Format ngày theo định dạng yyyy-MM-dd để sử dụng trong câu truy vấn SQL
            string formattedDate = currentDate.ToString("yyyy-MM-dd");

            // Sử dụng using để giải phóng tài nguyên
            //using (SqlDataAdapter phieuDatSanAdapter = new SqlDataAdapter("SELECT PDS.MaDatSan AS 'Mã Đặt Sân', KH.TenKH AS 'Họ và Tên', KH.MaThe AS 'CMND', LS.Ten AS 'Tên loại Sân', SB.Ten AS 'Loại Sân', PDS.NgayNhan AS 'Ngày Nhận', PDS.NgayTra AS 'Ngày Trả' " +
            //" FROM PhieuDatSan PDS " +
            //" INNER JOIN KhachHang KH ON PDS.MaKH = KH.MaKH " +
            //" INNER JOIN LoaiSan LS ON PDS.MaLoaiSan = LS.MaLoaiSan " +
            //" INNER JOIN SanBong SB ON PDS.MaSanBong = SB.MaSanBong and PDS.MaLoaiSan = SB.MaLoaiSan " +
            //" WHERE CONVERT(DATE, PDS.NgayNhan) = '" + formattedDate + "'", conn))
            //{
            //    DataTable phieuDatSanTable = new DataTable();
            //    phieuDatSanAdapter.Fill(phieuDatSanTable);

            //    // Kiểm tra xem có cột nào trong DataTable hay không trước khi đặt DataSource
            //    if (phieuDatSanTable.Columns.Count > 0)
            //    {
            //        dataGridView1.DataSource = phieuDatSanTable;
            //    }
            //    else
            //    {
            //        // Hiển thị thông báo hoặc xử lý tương ứng khi không có dữ liệu
            //    }
            //}



            using (SqlDataAdapter phieuNhanSanAdapter = new SqlDataAdapter("SELECT NS.MaNhanSan as N'Mã Nhận Sân', PDS.MaDatSan, KH.TenKH, KH.MaThe, LS.Ten AS 'TenLoaiSan', SB.Ten AS 'TenSan', PDS.NgayNhan, PDS.NgayTra, LS.SoLuongNguoiDa, LS.Gia, PDS.MaLoaiSan, PDS.MaSanBong " +
                            "FROM NhanSan NS " +
                            "INNER JOIN ChiTietNhanSan CTNS ON CTNS.MaNhanSan = NS.MaNhanSan " +
                            "INNER JOIN KhachHang KH ON KH.MaKH = CTNS.MaKH " +
                            "INNER JOIN PhieuDatSan PDS ON PDS.MaKH = CTNS.MaKH " +
                            "INNER JOIN LoaiSan LS ON PDS.MaLoaiSan = LS.MaLoaiSan " +
                            "INNER JOIN SanBong SB ON PDS.MaSanBong = SB.MaSanBong AND PDS.MaLoaiSan = SB.MaLoaiSan " +
                            "INNER JOIN HoaDon HD ON NS.MaNhanSan = HD.MaNhanSan " +
                            "WHERE CONVERT(DATE, PDS.NgayNhan) = CONVERT(DATE, GETDATE()) AND SB.MaTrangThaisan = 2 AND HD.MaTrangThaiHoaDon = 1", conn))          
                {
                

                DataTable phieuNhanSanTable = new DataTable();
                phieuNhanSanAdapter.Fill(phieuNhanSanTable);

                // Kiểm tra xem có cột nào trong DataTable hay không trước khi đặt DataSource
                if (phieuNhanSanTable.Columns.Count > 0)
                {
                    dataGridView1.DataSource = phieuNhanSanTable;
                }
                else
                {
                    // Hiển thị thông báo hoặc xử lý tương ứng khi không có dữ liệu
                }
            }

        }



        private void btnDV_TT_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormDichVu_ThanhToan frmDV_TT = new FormDichVu_ThanhToan();
            frmDV_TT.MaNV = maNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.Location = this.Location;
            frmDV_TT.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormQuanLySanBong frmDV_TT = new FormQuanLySanBong();
            frmDV_TT.MaNV = maNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.Location = this.Location;
            frmDV_TT.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormQuanLyDichVu frmDV_TT = new FormQuanLyDichVu();
            frmDV_TT.MaNV = maNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.Location = this.Location;
            frmDV_TT.ShowDialog();
        }

        private void btnNhanSan_Click(object sender, EventArgs e)
        {

        }

        private void btnThongKeDoanhThu_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem vai trò của người dùng có phải là admin không
            if (userRole == "Admin")
            {

                this.Hide();
                FormBaoCaoDoanhThu formBCDT = new FormBaoCaoDoanhThu(userRole);
                formBCDT.MaNV = maNV;
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
                formQLNV.MaNV = maNV;
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

        private void btnQLKH_Click(object sender, EventArgs e)
        {

            // Kiểm tra xem vai trò của người dùng có phải là admin không
            if (userRole == "Admin")
            {
                // Mở form quản lý nhân viên
                // Ví dụ: 
                this.Hide();
                FormQuanLyKhachHang formQLNV = new FormQuanLyKhachHang(userRole);
                formQLNV.MaNV = maNV;
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

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                conn.Open();
                SqlCommand getMaxId_PDS_Cmd = new SqlCommand("SELECT MAX(MaNhanSan) FROM NhanSan", conn);
                object maxId_PDS_Obj = getMaxId_PDS_Cmd.ExecuteScalar();
                int maxId_PDS = (maxId_PDS_Obj == DBNull.Value) ? 0 : Convert.ToInt32(maxId_PDS_Obj);
                //thông tin phiếu đặt sân
                int manhansan = maxId_PDS + 1;
                // Thêm thông tin vào bảng Nhận Sân
                string insertNhanSanQuery = "INSERT INTO NhanSan(MaNhansan,MaDatSan, MaSanBong) VALUES (@MaNhanSan,@MaDatSan, @MaSanBong)";
                SqlCommand cmdInsertNhanSan = new SqlCommand(insertNhanSanQuery, conn);
                cmdInsertNhanSan.Parameters.AddWithValue("@MaNhanSan", manhansan);
                cmdInsertNhanSan.Parameters.AddWithValue("@MaDatSan", textBox1.Text); // Sử dụng MaDatSan từ thongTinDatSan
                cmdInsertNhanSan.Parameters.AddWithValue("@MaSanBong", comboBox1.SelectedValue);
                cmdInsertNhanSan.ExecuteNonQuery();


                SqlCommand getMaxId_CTNS_Cmd = new SqlCommand("SELECT MAX(MaChiTietNhanSan) FROM ChiTietNhanSan", conn);
                object maxId_CTNS_Obj = getMaxId_PDS_Cmd.ExecuteScalar();
                int maxId_CTNS = (maxId_CTNS_Obj == DBNull.Value) ? 0 : Convert.ToInt32(maxId_CTNS_Obj);

                string selectMaKhQuery = "Select MaKH from PhieuDatSan Where MaDatSan = '" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(selectMaKhQuery, conn);
                int kq = (int)cmd.ExecuteScalar();
                // Thêm thông tin vào bảng Chi Tiết Nhận Sân
                int chitietnhansan = maxId_CTNS + 1;
                string insertChiTietNhanSanQuery = "INSERT INTO ChiTietNhanSan(MaChiTietNhanSan,MaNhanSan, MaKH) VALUES (@MaChiTietNhanSan,@MaNhanSan, @MaKH)";
                SqlCommand cmdInsertChiTietNhanSan = new SqlCommand(insertChiTietNhanSanQuery, conn);
                cmdInsertChiTietNhanSan.Parameters.AddWithValue("@MaChiTietNhanSan", chitietnhansan);
                cmdInsertChiTietNhanSan.Parameters.AddWithValue("@MaNhanSan", manhansan);
                cmdInsertChiTietNhanSan.Parameters.AddWithValue("@MaKH", kq); // Sử dụng MaKH từ thongTinDatSan

                cmdInsertChiTietNhanSan.ExecuteNonQuery();

                MessageBox.Show("Thêm thông tin nhận sân thành công.");

                cmd = new SqlCommand("UPDATE SanBong SET MaTrangThaiSan = 2 WHERE MaSanBong = @MaSanBong", conn);
                cmd.Parameters.AddWithValue("@MaSanBong", comboBox1.SelectedValue);
                cmd.ExecuteNonQuery();

                string updateHoaDonQuery = "UPDATE HoaDon SET MaTrangThaiHoaDon = @MaTrangThaiHoaDon WHERE MaNhanSan = @MaNhanSan";
                SqlCommand cmdupdateHoaDonQuery = new SqlCommand(updateHoaDonQuery, conn);
                cmdupdateHoaDonQuery.Parameters.AddWithValue("@MaTrangThaiHoaDon", 1/*Mã trạng thái sân cần cập nhật*/);
                cmdupdateHoaDonQuery.Parameters.AddWithValue("@MaNhanSan", manhansan);
                cmdupdateHoaDonQuery.ExecuteNonQuery();
                conn.Close();
                try
                {
                    conn.Open();
                    SqlCommand getMaxId_HD_Cmd = new SqlCommand("SELECT MAX(MaHoaDon) FROM HoaDon", conn);
                    object maxId_HD_Obj = getMaxId_HD_Cmd.ExecuteScalar();
                    int maxId_HD = (maxId_HD_Obj == DBNull.Value) ? 0 : Convert.ToInt32(maxId_HD_Obj);
                    int mahd = maxId_HD + 1;
                    // Lấy thông tin mã loại sân từ bảng NhanSan
                    string selectMaLoaiSanQuery = "SELECT LoaiSan.MaLoaiSan " +
                                                  "FROM NhanSan " +
                                                  "INNER JOIN SanBong ON NhanSan.MaSanBong = SanBong.MaSanBong " +
                                                  "INNER JOIN LoaiSan ON SanBong.MaLoaiSan = LoaiSan.MaLoaiSan " +
                                                  "WHERE NhanSan.MaNhanSan = @MaNhanSan";

                    SqlCommand cmdSelectMaLoaiSan = new SqlCommand(selectMaLoaiSanQuery, conn);
                    cmdSelectMaLoaiSan.Parameters.AddWithValue("@MaNhanSan", manhansan); // Sử dụng MaNhanSan từ thông tin nhận sân

                    int maLoaiSan = Convert.ToInt32(cmdSelectMaLoaiSan.ExecuteScalar());

                    // Lấy giá sân từ bảng LoaiSan
                    string selectGiaSanQuery = "SELECT Gia FROM LoaiSan WHERE MaLoaiSan = @MaLoaiSan";

                    SqlCommand cmdSelectGiaSan = new SqlCommand(selectGiaSanQuery, conn);
                    cmdSelectGiaSan.Parameters.AddWithValue("@MaLoaiSan", maLoaiSan);

                    decimal giaSan = Convert.ToDecimal(cmdSelectGiaSan.ExecuteScalar());

                    // Thêm thông tin vào bảng HoaDon
                    string insertHoaDonQuery = "INSERT INTO HoaDon (MaHoaDon,NgayTao, GiaSan, GiaDichVu, TongTien, GiamGia, PhuPhi, MaNhanSan, MaTrangThaiHoaDon, MaNV) " +
                                               "VALUES (@MaHoaDon,@NgayTao, @GiaSan, @GiaDichVu, @TongTien, @GiamGia, @PhuPhi, @MaNhanSan, @MaTrangThaiHoaDon, @MaNV)";
                    int giadichvu = 0;
                    SqlCommand cmdInsertHoaDon = new SqlCommand(insertHoaDonQuery, conn);
                    cmdInsertHoaDon.Parameters.AddWithValue("@MaHoaDon", mahd);
                    cmdInsertHoaDon.Parameters.AddWithValue("@NgayTao", DateTime.Now);
                    cmdInsertHoaDon.Parameters.AddWithValue("@GiaSan", giaSan); // Thay thế bằng giá trị thực
                    cmdInsertHoaDon.Parameters.AddWithValue("@GiaDichVu", giadichvu); // Thay thế bằng giá trị thực
                    cmdInsertHoaDon.Parameters.AddWithValue("@TongTien", giaSan + giadichvu); // Thay thế bằng giá trị thực
                    cmdInsertHoaDon.Parameters.AddWithValue("@GiamGia", 0); // Thay thế bằng giá trị thực
                    cmdInsertHoaDon.Parameters.AddWithValue("@PhuPhi", 0); // Thay thế bằng giá trị thực
                    cmdInsertHoaDon.Parameters.AddWithValue("@MaNhanSan", manhansan); // Sử dụng MaNhanSan từ thông tin nhận sân
                    cmdInsertHoaDon.Parameters.AddWithValue("@MaTrangThaiHoaDon", 1); // Thay thế bằng giá trị thực
                    cmdInsertHoaDon.Parameters.AddWithValue("@MaNV", 1); // Thay thế bằng giá trị thực

                    cmdInsertHoaDon.ExecuteNonQuery();


                    SqlCommand getMaxId_CTHD_Cmd = new SqlCommand("SELECT MAX(MaCTHD) FROM ChiTietHoaDon", conn);
                    object maxId_CTHD_Obj = getMaxId_CTHD_Cmd.ExecuteScalar();
                    int maxId_CTHD = (maxId_CTHD_Obj == DBNull.Value) ? 0 : Convert.ToInt32(maxId_CTHD_Obj);
                    int macthd = maxId_CTHD + 1;
                    // Lấy ID của hóa đơn vừa thêm vào để sử dụng cho chi tiết hóa đơn                  
                    int maHoaDon = mahd;

                    // Thêm thông tin vào bảng ChiTietHoaDon
                    string insertChiTietHoaDonQuery = "INSERT INTO ChiTietHoaDon (MaCTHD,MaHoaDon, MaDV, SoLuong, TongTien) " +
                                                      "VALUES (@MaCTHD,@MaHoaDon, @MaDV, @SoLuong, @TongTien)";

                    SqlCommand cmdInsertChiTietHoaDon = new SqlCommand(insertChiTietHoaDonQuery, conn);
                    cmdInsertChiTietHoaDon.Parameters.AddWithValue("@MaCTHD", macthd);
                    cmdInsertChiTietHoaDon.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    cmdInsertChiTietHoaDon.Parameters.AddWithValue("@MaDV", DBNull.Value); // Thay thế bằng giá trị thực
                    cmdInsertChiTietHoaDon.Parameters.AddWithValue("@SoLuong", 0); // Thay thế bằng giá trị thực
                    cmdInsertChiTietHoaDon.Parameters.AddWithValue("@TongTien", 0); // Thay thế bằng giá trị thực

                    cmdInsertChiTietHoaDon.ExecuteNonQuery();

                    MessageBox.Show("Thêm hóa đơn và chi tiết hóa đơn thành công!");
                }
                catch (Exception et)
                {
                    MessageBox.Show("Lỗi: " + et.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                try
                {

                    conn.Open();
                    SqlCommand getMaxId_PDS_Cmd = new SqlCommand("SELECT MAX(MaNhanSan) FROM NhanSan", conn);
                    object maxId_PDS_Obj = getMaxId_PDS_Cmd.ExecuteScalar();
                    int maxId_PDS = (maxId_PDS_Obj == DBNull.Value) ? 0 : Convert.ToInt32(maxId_PDS_Obj);
                    //thông tin phiếu đặt sân
                    int manhansan = maxId_PDS + 1;



                    // Thêm thông tin vào bảng Nhận Sân
                    string insertNhanSanQuery = "INSERT INTO NhanSan(MaNhansan,MaDatSan, MaSanBong) VALUES (@MaNhanSan,@MaDatSan, @MaSanBong)";
                    SqlCommand cmdInsertNhanSan = new SqlCommand(insertNhanSanQuery, conn);
                    cmdInsertNhanSan.Parameters.AddWithValue("@MaNhanSan", manhansan);
                    cmdInsertNhanSan.Parameters.AddWithValue("@MaDatSan", thongTinDatSan.maDatsan); // Sử dụng MaDatSan từ thongTinDatSan
                    cmdInsertNhanSan.Parameters.AddWithValue("@MaSanBong", thongTinDatSan.maSanbong);
                    comboBox1.SelectedValue = thongTinDatSan.maLoaisan.ToString();
                    cmdInsertNhanSan.ExecuteNonQuery();


                    SqlCommand getMaxId_CTNS_Cmd = new SqlCommand("SELECT MAX(MaChiTietNhanSan) FROM ChiTietNhanSan", conn);
                    object maxId_CTNS_Obj = getMaxId_PDS_Cmd.ExecuteScalar();
                    int maxId_CTNS = (maxId_CTNS_Obj == DBNull.Value) ? 0 : Convert.ToInt32(maxId_CTNS_Obj);
                    // Thêm thông tin vào bảng Chi Tiết Nhận Sân
                    int chitietnhansan = maxId_CTNS + 1;
                    string insertChiTietNhanSanQuery = "INSERT INTO ChiTietNhanSan(MaChiTietNhanSan,MaNhanSan, MaKH) VALUES (@MaChiTietNhanSan,@MaNhanSan, @MaKH)";
                    SqlCommand cmdInsertChiTietNhanSan = new SqlCommand(insertChiTietNhanSanQuery, conn);
                    cmdInsertChiTietNhanSan.Parameters.AddWithValue("@MaChiTietNhanSan", chitietnhansan);
                    cmdInsertChiTietNhanSan.Parameters.AddWithValue("@MaNhanSan", manhansan);
                    cmdInsertChiTietNhanSan.Parameters.AddWithValue("@MaKH", thongTinDatSan.maKhachH); // Sử dụng MaKH từ thongTinDatSan

                    cmdInsertChiTietNhanSan.ExecuteNonQuery();

                    MessageBox.Show("Thêm thông tin nhận sân thành công.");
                    SqlCommand cmd = new SqlCommand("UPDATE SanBong SET MaTrangThaiSan = 2 WHERE MaSanBong = @MaSanBong", conn);
                    cmd.Parameters.AddWithValue("@MaSanBong", thongTinDatSan.maSanbong);
                    cmd.ExecuteNonQuery();



                    string updateHoaDonQuery = "UPDATE HoaDon SET MaTrangThaiHoaDon = @MaTrangThaiHoaDon WHERE MaNhanSan = @MaNhanSan";
                    SqlCommand cmdupdateHoaDonQuery = new SqlCommand(updateHoaDonQuery, conn);
                    cmdupdateHoaDonQuery.Parameters.AddWithValue("@MaTrangThaiHoaDon", 1/*Mã trạng thái sân cần cập nhật*/);
                    cmdupdateHoaDonQuery.Parameters.AddWithValue("@MaNhanSan", manhansan);
                    cmdupdateHoaDonQuery.ExecuteNonQuery();

                    conn.Close();
                    //them hoa don, chi tiet hoa don
                    try
                    {
                        conn.Open();
                        SqlCommand getMaxId_HD_Cmd = new SqlCommand("SELECT MAX(MaHoaDon) FROM HoaDon", conn);
                        object maxId_HD_Obj = getMaxId_HD_Cmd.ExecuteScalar();
                        int maxId_HD = (maxId_HD_Obj == DBNull.Value) ? 0 : Convert.ToInt32(maxId_HD_Obj);
                        int mahd = maxId_HD + 1;
                        // Lấy thông tin mã loại sân từ bảng NhanSan
                        string selectMaLoaiSanQuery = "SELECT LoaiSan.MaLoaiSan " +
                                                      "FROM NhanSan " +
                                                      "INNER JOIN SanBong ON NhanSan.MaSanBong = SanBong.MaSanBong " +
                                                      "INNER JOIN LoaiSan ON SanBong.MaLoaiSan = LoaiSan.MaLoaiSan " +
                                                      "WHERE NhanSan.MaNhanSan = @MaNhanSan";

                        SqlCommand cmdSelectMaLoaiSan = new SqlCommand(selectMaLoaiSanQuery, conn);
                        cmdSelectMaLoaiSan.Parameters.AddWithValue("@MaNhanSan", manhansan); // Sử dụng MaNhanSan từ thông tin nhận sân

                        int maLoaiSan = Convert.ToInt32(cmdSelectMaLoaiSan.ExecuteScalar());

                        // Lấy giá sân từ bảng LoaiSan
                        string selectGiaSanQuery = "SELECT Gia FROM LoaiSan WHERE MaLoaiSan = @MaLoaiSan";

                        SqlCommand cmdSelectGiaSan = new SqlCommand(selectGiaSanQuery, conn);
                        cmdSelectGiaSan.Parameters.AddWithValue("@MaLoaiSan", maLoaiSan);

                        decimal giaSan = Convert.ToDecimal(cmdSelectGiaSan.ExecuteScalar());

                        // Thêm thông tin vào bảng HoaDon
                        string insertHoaDonQuery = "INSERT INTO HoaDon (MaHoaDon,NgayTao, GiaSan, GiaDichVu, TongTien, GiamGia, PhuPhi, MaNhanSan, MaTrangThaiHoaDon, MaNV) " +
                                                   "VALUES (@MaHoaDon,@NgayTao, @GiaSan, @GiaDichVu, @TongTien, @GiamGia, @PhuPhi, @MaNhanSan, @MaTrangThaiHoaDon, @MaNV)";
                        int giadichvu = 0;
                        SqlCommand cmdInsertHoaDon = new SqlCommand(insertHoaDonQuery, conn);
                        cmdInsertHoaDon.Parameters.AddWithValue("@MaHoaDon", mahd);
                        cmdInsertHoaDon.Parameters.AddWithValue("@NgayTao", DateTime.Now);
                        cmdInsertHoaDon.Parameters.AddWithValue("@GiaSan", giaSan); // Thay thế bằng giá trị thực
                        cmdInsertHoaDon.Parameters.AddWithValue("@GiaDichVu", giadichvu); // Thay thế bằng giá trị thực
                        cmdInsertHoaDon.Parameters.AddWithValue("@TongTien", giaSan + giadichvu); // Thay thế bằng giá trị thực
                        cmdInsertHoaDon.Parameters.AddWithValue("@GiamGia", 0); // Thay thế bằng giá trị thực
                        cmdInsertHoaDon.Parameters.AddWithValue("@PhuPhi", 0); // Thay thế bằng giá trị thực
                        cmdInsertHoaDon.Parameters.AddWithValue("@MaNhanSan", manhansan); // Sử dụng MaNhanSan từ thông tin nhận sân
                        cmdInsertHoaDon.Parameters.AddWithValue("@MaTrangThaiHoaDon", 1); // Thay thế bằng giá trị thực
                        cmdInsertHoaDon.Parameters.AddWithValue("@MaNV", 1); // Thay thế bằng giá trị thực

                        cmdInsertHoaDon.ExecuteNonQuery();


                        SqlCommand getMaxId_CTHD_Cmd = new SqlCommand("SELECT MAX(MaCTHD) FROM ChiTietHoaDon", conn);
                        object maxId_CTHD_Obj = getMaxId_CTHD_Cmd.ExecuteScalar();
                        int maxId_CTHD = (maxId_CTHD_Obj == DBNull.Value) ? 0 : Convert.ToInt32(maxId_CTHD_Obj);
                        int macthd = maxId_CTHD + 1;
                        // Lấy ID của hóa đơn vừa thêm vào để sử dụng cho chi tiết hóa đơn                  
                        int maHoaDon = mahd;

                        // Thêm thông tin vào bảng ChiTietHoaDon
                        string insertChiTietHoaDonQuery = "INSERT INTO ChiTietHoaDon (MaCTHD,MaHoaDon, MaDV, SoLuong, TongTien) " +
                                                          "VALUES (@MaCTHD,@MaHoaDon, @MaDV, @SoLuong, @TongTien)";

                        SqlCommand cmdInsertChiTietHoaDon = new SqlCommand(insertChiTietHoaDonQuery, conn);
                        cmdInsertChiTietHoaDon.Parameters.AddWithValue("@MaCTHD", macthd);
                        cmdInsertChiTietHoaDon.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                        cmdInsertChiTietHoaDon.Parameters.AddWithValue("@MaDV", DBNull.Value); // Thay thế bằng giá trị thực
                        cmdInsertChiTietHoaDon.Parameters.AddWithValue("@SoLuong", 0); // Thay thế bằng giá trị thực
                        cmdInsertChiTietHoaDon.Parameters.AddWithValue("@TongTien", 0); // Thay thế bằng giá trị thực

                        cmdInsertChiTietHoaDon.ExecuteNonQuery();

                        MessageBox.Show("Thêm hóa đơn và chi tiết hóa đơn thành công!");
                    }
                    catch (Exception et)
                    {
                        MessageBox.Show("Lỗi: " + et.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }

                catch (Exception ex)
                {


                }
                finally
                {
                    conn.Close();
                }
            }



            // Sau khi thêm thành công, bạn có thể load lại dữ liệu lên DataGridView_nhansan
            LoadDataToDataGridView_nhansan();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                // Tìm kiếm thông tin khách hàng theo Mã Đặt Sân
                string selectQuery = "SELECT PDS.MaDatSan, KH.TenKH, KH.MaThe, LS.Ten AS 'TenLoaiSan', SB.Ten AS 'TenSan', PDS.NgayNhan, PDS.NgayTra, LS.SoLuongNguoiDa, LS.Gia,PDS.MaLoaiSan,PDS.MaSanBong " +
                                     "FROM PhieuDatSan PDS " +
                                     "INNER JOIN KhachHang KH ON PDS.MaKH = KH.MaKH " +
                                     "INNER JOIN LoaiSan LS ON PDS.MaLoaiSan = LS.MaLoaiSan " +
                                     "INNER JOIN SanBong SB ON PDS.MaSanBong = SB.MaSanBong and PDS.MaLoaiSan = SB.MaLoaiSan " +
                                     "WHERE PDS.MaDatSan = @MaDatSan";

                SqlCommand cmdSelect = new SqlCommand(selectQuery, conn);
                cmdSelect.Parameters.AddWithValue("@MaDatSan", textBox1.Text);

                SqlDataReader reader = cmdSelect.ExecuteReader();

                if (reader.Read())
                {
                    // Điền thông tin lên các ô TextBox
                    textBox2.Text = reader["TenKH"].ToString();
                    textBox3.Text = reader["MaThe"].ToString();
                    textBox4.Text = reader["TenSan"].ToString();
                    textBox5.Text = reader["TenLoaiSan"].ToString();

                    DateTime ngayNhan = (reader["NgayNhan"] != DBNull.Value) ? Convert.ToDateTime(reader["NgayNhan"]) : DateTime.MinValue;
                    textBox6.Text = (ngayNhan != DateTime.MinValue) ? ngayNhan.ToString("yyyy-MM-dd") : string.Empty;

                    DateTime ngayTra = (reader["NgayTra"] != DBNull.Value) ? Convert.ToDateTime(reader["NgayTra"]) : DateTime.MinValue;
                    textBox7.Text = (ngayTra != DateTime.MinValue) ? ngayTra.ToString("yyyy-MM-dd") : string.Empty;

                    int soLuongNguoiToiDa = (reader["SoLuongNguoiDa"] != DBNull.Value) ? Convert.ToInt32(reader["SoLuongNguoiDa"]) : 0;
                    textBox8.Text = (soLuongNguoiToiDa != 0) ? soLuongNguoiToiDa.ToString() : string.Empty;

                    decimal gia = (reader["Gia"] != DBNull.Value) ? Convert.ToDecimal(reader["Gia"]) : 0;
                    textBox9.Text = (gia != 0) ? gia.ToString() : string.Empty;

                    comboBox1.SelectedValue = reader["MaLoaiSan"].ToString();
                    comboBox2.SelectedValue = reader["MaSanBong"].ToString();
                    
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin khách hàng với Mã Đặt Sân này.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        private void LoadLoaiSanComboBox()
        {
            adapt = new SqlDataAdapter("select * from LoaiSan", conn);
            adapt.Fill(ds, "LoaiSan");
            comboBox1.DataSource = ds.Tables["LoaiSan"];
            comboBox1.DisplayMember = "Ten";
            comboBox1.ValueMember = "MaLoaiSan";
        }

        private void LoadSanBongComboBox()
        {
            adapt = new SqlDataAdapter("select * from SanBong", conn);
            adapt.Fill(ds, "SanBong");
            //comboBox1.DataSource = ds.Tables["SanBong"];
            //comboBox1.DisplayMember = "Ten";
            //comboBox1.ValueMember = "MaLoaiSan";
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int maLoaiSanChon;


            if (int.TryParse(comboBox1.SelectedValue.ToString(), out maLoaiSanChon))
            {
                DataRow[] sanBongRows = ds.Tables["SanBong"].Select($"MaLoaiSan = {maLoaiSanChon}");

                // Kiểm tra nếu không có dòng nào được lọc
                if (sanBongRows.Length == 0)
                {
                    // Hiển thị thông báo hoặc thực hiện các hành động khác nếu cần
                    return;
                }

                // Tạo một DataTable mới để chứa các dòng đã lọc
                DataTable filteredSanBongTable = ds.Tables["SanBong"].Clone();
                foreach (DataRow row in sanBongRows)
                {
                    filteredSanBongTable.ImportRow(row);
                }

                // Gán dữ liệu cho comboBox2
                comboBox2.DataSource = filteredSanBongTable;
                comboBox2.DisplayMember = "Ten";
                comboBox2.ValueMember = "MaSanBong";

            }
        }
        
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string maNhanSan = row.Cells["Mã Nhận Sân"].Value.ToString();

                // Gọi hàm hiển thị thông tin chi tiết dựa trên mã nhận sân
                DisplayDetailInfo(maNhanSan);
            }
        }

        private void DisplayDetailInfo(string maNhanSan)
        {
            try
            {
                conn.Open();
                //using (SqlDataAdapter phieuNhanSanAdapter = new SqlDataAdapter("SELECT NS.MaNhanSan AS 'Mã Nhận Sân', KH.TenKH AS 'Họ và Tên', KH.MaThe AS 'CMND', LS.Ten AS 'Tên loại Sân', SB.Ten AS 'Loại Sân', PDS.NgayNhan AS 'Ngày Nhận', PDS.NgayTra AS 'Ngày Trả' " +
                //" FROM NhanSan NS " +
                //" INNER JOIN ChiTietNhanSan CTNS ON CTNS.MaNhanSan = NS.MaNhanSan " +
                //" INNER JOIN KhachHang KH ON KH.MaKH = CTNS.MaKH " +
                //" INNER JOIN PhieuDatSan PDS ON PDS.MaKH = CTNS.MaKH " +
                //" INNER JOIN LoaiSan LS ON PDS.MaLoaiSan = LS.MaLoaiSan " +
                //" INNER JOIN SanBong SB ON PDS.MaSanBong = SB.MaSanBong and PDS.MaLoaiSan = SB.MaLoaiSan " +
                //" WHERE CONVERT(DATE, PDS.NgayNhan) = '" + formattedDate + "'", conn))

                    // Tìm kiếm thông tin khách hàng theo Mã Đặt Sân
                    string selectQuery = "SELECT NS.MaNhanSan,PDS.MaDatSan, KH.TenKH, KH.MaThe, LS.Ten AS 'TenLoaiSan', SB.Ten AS 'TenSan', PDS.NgayNhan, PDS.NgayTra, LS.SoLuongNguoiDa, LS.Gia,PDS.MaLoaiSan,PDS.MaSanBong " +
                                     " FROM NhanSan NS " +
                                    " INNER JOIN ChiTietNhanSan CTNS ON CTNS.MaNhanSan = NS.MaNhanSan " +
                                    " INNER JOIN KhachHang KH ON KH.MaKH = CTNS.MaKH " +
                                    " INNER JOIN PhieuDatSan PDS ON PDS.MaKH = CTNS.MaKH " +
                                    " INNER JOIN LoaiSan LS ON PDS.MaLoaiSan = LS.MaLoaiSan " +
                                    " INNER JOIN SanBong SB ON PDS.MaSanBong = SB.MaSanBong and PDS.MaLoaiSan = SB.MaLoaiSan " +
                                     "WHERE NS.MaNhanSan = @MaNhanSan";

                SqlCommand cmdSelect = new SqlCommand(selectQuery, conn);
                cmdSelect.Parameters.AddWithValue("@MaNhanSan", maNhanSan);

                SqlDataReader reader = cmdSelect.ExecuteReader();

                if (reader.Read())
                {
                    // Điền thông tin lên các ô TextBox và ComboBox
                   
                    textBox2.Text = reader["TenKH"].ToString();
                    textBox3.Text = reader["MaThe"].ToString();
                    textBox4.Text = reader["TenSan"].ToString();
                    textBox5.Text = reader["TenLoaiSan"].ToString();

                    DateTime ngayNhan = (reader["NgayNhan"] != DBNull.Value) ? Convert.ToDateTime(reader["NgayNhan"]) : DateTime.MinValue;
                    textBox6.Text = (ngayNhan != DateTime.MinValue) ? ngayNhan.ToString("yyyy-MM-dd") : string.Empty;

                    DateTime ngayTra = (reader["NgayTra"] != DBNull.Value) ? Convert.ToDateTime(reader["NgayTra"]) : DateTime.MinValue;
                    textBox7.Text = (ngayTra != DateTime.MinValue) ? ngayTra.ToString("yyyy-MM-dd") : string.Empty;

                    int soLuongNguoiToiDa = (reader["SoLuongNguoiDa"] != DBNull.Value) ? Convert.ToInt32(reader["SoLuongNguoiDa"]) : 0;
                    textBox8.Text = (soLuongNguoiToiDa != 0) ? soLuongNguoiToiDa.ToString() : string.Empty;

                    decimal gia = (reader["Gia"] != DBNull.Value) ? Convert.ToDecimal(reader["Gia"]) : 0;
                    textBox9.Text = (gia != 0) ? gia.ToString() : string.Empty;

                    comboBox1.SelectedValue = reader["MaLoaiSan"].ToString();
                    comboBox2.SelectedValue = reader["MaSanBong"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin khách hàng với Mã Đặt Sân này.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                // Lấy mã nhận sân từ DataGridView
                int maNhanSan = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Mã Nhận Sân"].Value);

                // Cập nhật trạng thái sân trong bảng SanBong
                string updateSanBongQuery = "UPDATE SanBong SET MaTrangThaiSan = @MaTrangThaiSan WHERE MaSanBong IN (SELECT MaSanBong FROM NhanSan WHERE MaNhanSan = @MaNhanSan)";
                SqlCommand cmdUpdateSanBong = new SqlCommand(updateSanBongQuery, conn);
                cmdUpdateSanBong.Parameters.AddWithValue("@MaTrangThaiSan", 1/*Mã trạng thái sân cần cập nhật*/);
                cmdUpdateSanBong.Parameters.AddWithValue("@MaNhanSan", maNhanSan);
                cmdUpdateSanBong.ExecuteNonQuery();

                string updateHoaDonQuery = "UPDATE HoaDon SET MaTrangThaiHoaDon = @MaTrangThaiHoaDon WHERE MaNhanSan = @MaNhanSan";
                SqlCommand cmdupdateHoaDonQuery = new SqlCommand(updateHoaDonQuery, conn);
                cmdupdateHoaDonQuery.Parameters.AddWithValue("@MaTrangThaiHoaDon", 2/*Mã trạng thái sân cần cập nhật*/);
                cmdupdateHoaDonQuery.Parameters.AddWithValue("@MaNhanSan", maNhanSan);
                cmdupdateHoaDonQuery.ExecuteNonQuery();

                //// Xóa dữ liệu liên quan trong các bảng ChiTietNhanSan và NhanSan
                //string deleteChiTietNhanSanQuery = "DELETE FROM ChiTietNhanSan WHERE MaNhanSan = @MaNhanSan";
                //SqlCommand cmdDeleteChiTietNhanSan = new SqlCommand(deleteChiTietNhanSanQuery, conn);
                //cmdDeleteChiTietNhanSan.Parameters.AddWithValue("@MaNhanSan", maNhanSan);
                //cmdDeleteChiTietNhanSan.ExecuteNonQuery();

                //string deleteNhanSanQuery = "DELETE FROM NhanSan WHERE MaNhanSan = @MaNhanSan";
                //SqlCommand cmdDeleteNhanSan = new SqlCommand(deleteNhanSanQuery, conn);
                //cmdDeleteNhanSan.Parameters.AddWithValue("@MaNhanSan", maNhanSan);
                //cmdDeleteNhanSan.ExecuteNonQuery();

                MessageBox.Show("Trả sân thành công.");
                //SqlCommand cmd = new SqlCommand("UPDATE SanBong SET MaTrangThaiSan = 1 WHERE MaSanBong = @MaSanBong", conn);
                //cmd.Parameters.AddWithValue("@MaSanBong", comboBox1.SelectedValue);
                //cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                conn.Close();
                // Sau khi trả sân thành công, bạn có thể reload lại dữ liệu trong DataGridView1
                LoadDataToDataGridView_nhansan();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            comboBox1.Select();
            comboBox2.Select();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();

        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
