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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace GiaoDien_SanBong
{

    public partial class FormTrangChu : Form
    {

        SqlConnection conn;
        DataSet ds;
        SqlDataAdapter adapt;
        DataColumn[] key = new DataColumn[1];
        private string userRole;
        private int Manv;
        private UserInfo userInfo;
        private int maLoaiTK;
        // Các phương thức và logic khác của FormTrangChu
        private int maNV;

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
        public FormTrangChu()
        {
            InitializeComponent();
            string sqlconnect = "Data Source=ANHQUAN\\SQLEXPRESS;Initial Catalog=QLSANBONG;User ID=sa;Password=123;";
            conn = new SqlConnection(sqlconnect);
            ds = new DataSet();
            this.userInfo = userInfo;
            this.maLoaiTK = maLoaiTK;
            //adapt = new SqlDataAdapter("select * from PhieuDatSan", conn);
            //adapt.Fill(ds, "PhieuDatSan");
            //key[0] = ds.Tables["PhieuDatSan"].Columns[0];
            //ds.Tables["PhieuDatSan"].PrimaryKey = key;
        }
        ThongTinDatSan thongTin = new ThongTinDatSan();
        private void btnNhanSan_Click(object sender, EventArgs e)
        {
            this.Hide();
            // Mở FormDichVu_ThanhToan và truyền vai trò
            FormNhanSan frmDV_TT = new FormNhanSan(thongTin);
            frmDV_TT.MaNV = maNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.Location = this.Location;
            frmDV_TT.ShowDialog();
        }

        private void btnDV_TT_Click(object sender, EventArgs e)
        {
            this.Hide();
            // Mở FormDichVu_ThanhToan và truyền vai trò
            FormDichVu_ThanhToan frmDV_TT = new FormDichVu_ThanhToan();
            frmDV_TT.MaNV = MaNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.Location = this.Location;
            frmDV_TT.ShowDialog();
        }
        DataTable originalDataTable;
        private void FormTrangChu_Load(object sender, EventArgs e)
        {
            // Đặt màu nền cho nút
            btnDatSan.BackColor = Color.Orange;

            // Load dữ liệu đặt sân vào DataGridView
            LoadDataToDataGridView();
            // Lưu trữ dữ liệu ban đầu từ DataGridView
            originalDataTable = (DataTable)dataGridView1.DataSource;



            // Load dữ liệu vào ComboBox LoaiSan
            LoadLoaiSanComboBox();

            // Load dữ liệu vào ComboBox SanBong
            LoadSanBongComboBox();

            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        private void LoadDataToDataGridView()
        {
            // Lấy ngày hiện tại
            DateTime currentDate = DateTime.Now;

            // Format ngày theo định dạng yyyy-MM-dd để sử dụng trong câu truy vấn SQL
            string formattedDate = currentDate.ToString("yyyy-MM-dd");

            // Sử dụng using để giải phóng tài nguyên
            using (SqlDataAdapter phieuDatSanAdapter = new SqlDataAdapter("SELECT PDS.MaDatSan AS 'Mã Đặt Sân', KH.TenKH AS 'Họ và Tên', KH.MaThe AS 'CMND', LS.Ten AS 'Tên loại Sân', SB.Ten AS 'Loại Sân', PDS.NgayNhan AS 'Ngày Nhận', PDS.NgayTra AS 'Ngày Trả' " +
            " FROM PhieuDatSan PDS " +
            " INNER JOIN KhachHang KH ON PDS.MaKH = KH.MaKH " +
            " INNER JOIN LoaiSan LS ON PDS.MaLoaiSan = LS.MaLoaiSan " +
            " INNER JOIN SanBong SB ON PDS.MaSanBong = SB.MaSanBong and PDS.MaLoaiSan = SB.MaLoaiSan " +
            " WHERE CONVERT(DATE, PDS.NgayDat) = '" + formattedDate + "' and sb.MaTrangThaiSan = 1", conn))
            {
                DataTable phieuDatSanTable = new DataTable();
                phieuDatSanAdapter.Fill(phieuDatSanTable);

                // Kiểm tra xem có cột nào trong DataTable hay không trước khi đặt DataSource
                if (phieuDatSanTable.Columns.Count > 0)
                {
                    dataGridView1.DataSource = phieuDatSanTable;
                }
                else
                {
                    // Hiển thị thông báo hoặc xử lý tương ứng khi không có dữ liệu
                }
            }
        }


        private void LoadLoaiSanComboBox()
        {
            adapt = new SqlDataAdapter("select * from LoaiSan", conn);
            adapt.Fill(ds, "LoaiSan");
            comboBox3.DataSource = ds.Tables["LoaiSan"];
            comboBox3.DisplayMember = "Ten";
            comboBox3.ValueMember = "MaLoaiSan";
        }

        private void LoadSanBongComboBox()
        {
            adapt = new SqlDataAdapter("select * from SanBong", conn);
            adapt.Fill(ds, "SanBong");
            //comboBox1.DataSource = ds.Tables["SanBong"];
            //comboBox1.DisplayMember = "Ten";
            //comboBox1.ValueMember = "MaLoaiSan";
        }


        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            // Mở FormDichVu_ThanhToan và truyền vai trò
            FormQuanLySanBong frmDV_TT = new FormQuanLySanBong();
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.Location = this.Location;
            frmDV_TT.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            // Mở FormDichVu_ThanhToan và truyền vai trò
            FormQuanLyDichVu frmDV_TT = new FormQuanLyDichVu();
            frmDV_TT.MaNV = MaNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.Location = this.Location;
            frmDV_TT.ShowDialog();
        }

        private void btnDatSan_Click(object sender, EventArgs e)
        {

        }

        private void btnThongKeDT_Click(object sender, EventArgs e)
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu ds là null hoặc không chứa bảng "SanBong"
                if (ds == null || !ds.Tables.Contains("SanBong") || comboBox3.SelectedValue == null)
                {
                    // Hiển thị thông báo hoặc thực hiện các hành động khác nếu cần
                    return;
                }

                int maLoaiSanChon;
                if (int.TryParse(comboBox3.SelectedValue.ToString(), out maLoaiSanChon))
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

                    // Gán dữ liệu cho comboBox1
                    comboBox1.DataSource = filteredSanBongTable;
                    comboBox1.DisplayMember = "Ten";
                    comboBox1.ValueMember = "MaSanBong";

                    //hiển thị mã loại của sân đó
                    textBox8.Text = maLoaiSanChon.ToString();

                    string slngtd = GetSoLuongNguoiDa(maLoaiSanChon);
                    //hiển thị số lượng người tối đa của sân bóng
                    textBox10.Text = slngtd;

                    string giaThanh = GetGiaThanhByMaLoaiSan(maLoaiSanChon);
                    // Hiển thị giá thành trong textBox11
                    textBox11.Text = giaThanh;
                }
            }
            catch (Exception ex)
            {
                // Xử lý exception theo nhu cầu của bạn
                //MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }


        private string GetGiaThanhByMaLoaiSan(int maLoaiSan)
        {
            string giaThanh = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Gia FROM LoaiSan WHERE MaLoaiSan = @MaLoaiSan", conn);
                cmd.Parameters.AddWithValue("@MaLoaiSan", maLoaiSan);
                giaThanh = cmd.ExecuteScalar()?.ToString();
            }
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi: " + ex.Message);
            //}
            finally
            {
                conn.Close();
            }
            return giaThanh;
        }
        private string GetSoLuongNguoiDa(int maLoaiSan)
        {
            string slngda = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT SoLuongNguoiDa FROM LoaiSan WHERE MaLoaiSan = @MaLoaiSan", conn);
                cmd.Parameters.AddWithValue("@MaLoaiSan", maLoaiSan);
                slngda = cmd.ExecuteScalar()?.ToString();
            }
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi: " + ex.Message);
            //}
            finally
            {
                conn.Close();
            }
            return slngda;
        }
        private string GetMaLoaiSan(int maLoaiSan)
        {
            string Maloaisan = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT MaLoaiSan FROM LoaiSan WHERE MaLoaiSan = @MaLoaiSan", conn);
                cmd.Parameters.AddWithValue("@MaLoaiSan", maLoaiSan);
                Maloaisan = cmd.ExecuteScalar()?.ToString();
            }
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi: " + ex.Message);
            //}
            finally
            {
                conn.Close();
            }
            return Maloaisan;
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void UpdateNgayTra()
        {
            // Kiểm tra xem maskedTextBox1 có giá trị không rỗng

            // Lấy giá trị từ maskedTextBox1
            string gioPhut = maskedTextBox1.Text;

            // Kiểm tra định dạng thời gian (HH:mm)
            if (DateTime.TryParseExact(gioPhut, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                // Lấy giá trị hiện tại của dateTimePicker2
                DateTime ngayTra = dateTimePicker2.Value;

                // Cộng thêm số giờ từ maskedTextBox1 vào ngày hiện tại
                ngayTra = ngayTra.AddHours(result.Hour).AddMinutes(result.Minute);

                // Đặt lại giá trị cho dateTimePicker2
                dateTimePicker2.Value = ngayTra;
            }


        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateNgayTra();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
               

                
                // Tăng giá trị mã khách hàng để có được mã mới
                conn.Open();
                SqlCommand getMaxIdCmd = new SqlCommand("SELECT MAX(MaKH) FROM KhachHang", conn);
                object maxIdObj = getMaxIdCmd.ExecuteScalar();
                int maxId = (maxIdObj == DBNull.Value) ? 0 : Convert.ToInt32(maxIdObj);

                //thông tin khách hàng
                int makh = maxId + 1;
                string hoten = textBox2.Text.Trim();
                long mathe_cccd = Convert.ToInt64(textBox3.Text.Trim());
                string diachi = textBox5.Text.Trim();
                int sdt = int.Parse(textBox4.Text.Trim());
                string gioitinh = textBox6.Text.Trim();
                string quoctich = textBox7.Text.Trim();
                DateTime namsinh = dateTimePicker3.Value;


                // Tăng giá trị mã phiếu đặt sân để có được mã mới
                SqlCommand getMaxId_PDS_Cmd = new SqlCommand("SELECT MAX(MaKH) FROM KhachHang", conn);
                object maxId_PDS_Obj = getMaxId_PDS_Cmd.ExecuteScalar();
                int maxId_PDS = (maxId_PDS_Obj == DBNull.Value) ? 0 : Convert.ToInt32(maxId_PDS_Obj);
                //thông tin phiếu đặt sân
                int maphieudatsan = maxId_PDS + 1;
                int maloaisan = Convert.ToInt32(textBox8.Text.Trim());
                int masanbong = Convert.ToInt32(comboBox1.SelectedValue);
                DateTime ngaydat = DateTime.Now;
                DateTime ngaynhan = dateTimePicker1.Value;
                DateTime ngaytra = dateTimePicker2.Value;
                string gioPhut = maskedTextBox1.Text;



                //string checkReservationQuery = "SELECT COUNT(*) FROM PhieuDatSan " +
                //              "WHERE MaSanBong = @MaSanBong " +
                //              "AND (@NgayNhan BETWEEN NgayNhan AND NgayTra OR @NgayTra BETWEEN NgayNhan AND NgayTra)";

                //using (SqlCommand cmdCheckReservation = new SqlCommand(checkReservationQuery, conn))
                //{
                //    cmdCheckReservation.Parameters.AddWithValue("@MaSanBong", comboBox1.SelectedValue);
                //    cmdCheckReservation.Parameters.AddWithValue("@NgayNhan", ngaynhan);
                //    cmdCheckReservation.Parameters.AddWithValue("@NgayTra", ngaytra);

                //    int reservationCount = (int)cmdCheckReservation.ExecuteScalar();

                //    if (reservationCount > 0)
                //    {
                //        // Hiển thị thông báo nếu đã có đặt sân cho thời gian đã chọn
                //        MessageBox.Show("Sân bóng đã được đặt cho thời gian này. Vui lòng chọn thời gian khác.");
                //        return;
                //    }
                //}

                string checkSanStatusQuery = "SELECT MaTrangThaiSan FROM SanBong WHERE MaSanBong = @MaSanBong";

                using (SqlCommand cmdCheckSanStatus = new SqlCommand(checkSanStatusQuery, conn))
                {
                    cmdCheckSanStatus.Parameters.AddWithValue("@MaSanBong", comboBox1.SelectedValue);

                    // Thực hiện truy vấn để lấy trạng thái của sân
                    int sanStatus = (int)cmdCheckSanStatus.ExecuteScalar();

                    // Kiểm tra trạng thái của sân
                    if (sanStatus == 1)
                    {
                        // Sân đang rãnh, tiếp tục kiểm tra giờ đặt
                        string checkReservationQuery = "SELECT COUNT(*) FROM PhieuDatSan " +
                                                      "WHERE MaSanBong = @MaSanBong " +
                                                      "AND (@NgayNhan BETWEEN NgayNhan AND NgayTra OR @NgayTra BETWEEN NgayNhan AND NgayTra)";

                        using (SqlCommand cmdCheckReservation = new SqlCommand(checkReservationQuery, conn))
                        {
                            cmdCheckReservation.Parameters.AddWithValue("@MaSanBong", comboBox1.SelectedValue);
                            cmdCheckReservation.Parameters.AddWithValue("@NgayNhan", ngaynhan);
                            cmdCheckReservation.Parameters.AddWithValue("@NgayTra", ngaytra);

                            int reservationCount = (int)cmdCheckReservation.ExecuteScalar();

                            if (reservationCount > 0)
                            {
                                // Hiển thị thông báo nếu đã có đặt sân cho thời gian đã chọn
                                MessageBox.Show("Sân bóng đã được đặt cho thời gian này. Vui lòng chọn thời gian khác.");
                                return;
                            }

                            // Thực hiện các hành động cần thiết khi có thể đặt sân
                            // ...
                        }
                    }
                    else
                    {
                        // Sân không rãnh, hiển thị thông báo hoặc thực hiện các hành động khác nếu cần
                        MessageBox.Show("Sân bóng không khả dụng cho thời gian này. Vui lòng chọn thời gian khác.");
                        return;
                    }
                }

                //// Kiểm tra trạng thái sân bóng
                //string checkSanBongStatusQuery = "SELECT MaTrangThaiSan FROM SanBong WHERE MaSanBong = @MaSanBong";

                //using (SqlCommand cmdCheckSanBongStatus = new SqlCommand(checkSanBongStatusQuery, conn))
                //{
                //    cmdCheckSanBongStatus.Parameters.AddWithValue("@MaSanBong", comboBox1.SelectedValue);

                //    object sanBongStatusObj = cmdCheckSanBongStatus.ExecuteScalar();

                //    if (sanBongStatusObj != DBNull.Value)
                //    {
                //        int sanBongStatus = Convert.ToInt32(sanBongStatusObj);

                //        if (sanBongStatus == 2)
                //        {
                //            // Hiển thị thông báo nếu sân bóng đã đặt
                //            MessageBox.Show("Sân bóng đã được đặt và không thể đặt thêm. Vui lòng chọn sân khác.");
                //            return;
                //        }
                //    }
                //}

                // Kiểm tra định dạng thời gian (HH:mm)
                if (DateTime.TryParseExact(gioPhut, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                {
                    // Cộng thêm số giờ từ maskedTextBox1 vào ngày hiện tại
                    ngaytra = ngaytra.AddHours(result.Hour).AddMinutes(result.Minute);

                    // Mở kết nối đến CSDL
                    //conn.Open();

                    // Thực hiện câu lệnh SQL để thêm thông tin vào CSDL
                    SqlCommand cmd = new SqlCommand("INSERT INTO KhachHang (MaKH, TenKH, MaThe, NamSinh, DiaChi, SDT, GIoiTinh, QuocTich) VALUES (@MaKH, @TenKH, @MaThe, @NamSinh, @DiaChi, @SDT, @GIoiTinh, @QuocTich)", conn);
                    cmd.Parameters.AddWithValue("@MaKH", makh);
                    cmd.Parameters.AddWithValue("@TenKH", hoten);
                    cmd.Parameters.AddWithValue("@MaThe", mathe_cccd);
                    cmd.Parameters.AddWithValue("@NamSinh", namsinh);
                    cmd.Parameters.AddWithValue("@DiaChi", diachi);
                    cmd.Parameters.AddWithValue("@SDT", sdt);
                    cmd.Parameters.AddWithValue("@GIoiTinh", gioitinh);
                    cmd.Parameters.AddWithValue("@QuocTich", quoctich);
                    cmd.ExecuteNonQuery();


                    cmd = new SqlCommand("INSERT INTO PhieuDatSan (MaDatSan, MaKH, MaLoaiSan, NgayDat, NgayNhan, NgayTra,MaSanBong) VALUES (@MaDatSan, @MaKH, @MaLoaiSan, @NgayDat, @NgayNhan, @NgayTra,@MaSanBong)", conn);
                    cmd.Parameters.AddWithValue("@MaDatSan", maphieudatsan);
                    cmd.Parameters.AddWithValue("@MaKH", makh);
                    cmd.Parameters.AddWithValue("@MaLoaiSan", maloaisan);
                    cmd.Parameters.AddWithValue("@NgayDat", ngaydat);
                    cmd.Parameters.AddWithValue("@NgayNhan", ngaynhan);
                    cmd.Parameters.AddWithValue("@NgayTra", ngaytra);
                    cmd.Parameters.AddWithValue("@MaSanBong", masanbong);
                    // Thực hiện câu lệnh SQL
                    cmd.ExecuteNonQuery();
                    // Đóng kết nối
                    conn.Close();
                    MessageBox.Show("Thêm thông tin thành công!");
                    // Hiển thị thông báo thành công hoặc cập nhật lại DataGridView, tùy thuộc vào yêu cầu của bạn.
                    if (checkBox1.Checked)
                    {
                        // Ẩn Form Trang Chủ
                        this.Hide();
                        ThongTinDatSan thongTin = new ThongTinDatSan
                        {
                            HoTen = textBox2.Text,
                            CCCD = Convert.ToInt64(textBox3.Text),
                            TenSan = comboBox3.SelectedValue.ToString(),
                            TenLoaiSan = comboBox1.SelectedValue.ToString(),
                            NgayNhan = dateTimePicker1.Value,
                            NgayTra = dateTimePicker2.Value,
                            SoLuongNguoiToiDa = Convert.ToInt32(textBox10.Text),
                            Gia = Convert.ToDecimal(textBox11.Text),
                            maDatsan = maphieudatsan,
                            maKhachH = makh,
                            maLoaisan = maloaisan,
                            maSanbong = masanbong,
                        };
                        // Tạo và hiển thị Form Nhận Sân
                        FormNhanSan formNS = new FormNhanSan(thongTin);
                        formNS.Location = this.Location;
                        formNS.ShowDialog();
                        LoadDataToDataGridView();
                    }
                    else
                    {
                        // Hiển thị thông báo thành công hoặc cập nhật lại DataGridView, tùy thuộc vào yêu cầu của bạn.
                        LoadDataToDataGridView();
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                // Đảm bảo kết nối được đóng sau mỗi lần sử dụng
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }
        private void UpdateDataGridView()
        {
            // Nếu TextBox rỗng, hiển thị toàn bộ dữ liệu
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                dataGridView1.DataSource = originalDataTable;
            }

        }
        private void TimKiemKhachHang()
        {
            // Lấy thông tin thẻ căn cước từ TextBox
            string maTheCanCuoc = textBox1.Text.Trim();

            // Tìm kiếm trong DataTable của DataGridView1
            DataTable dataTable = (DataTable)dataGridView1.DataSource;

            // Sử dụng Select để lọc dữ liệu
            DataRow[] ketQuaTimKiem = dataTable.Select($"CMND = '{maTheCanCuoc}'");

            // Hiển thị kết quả tìm kiếm trong DataGridView hoặc thông báo
            if (ketQuaTimKiem != null && ketQuaTimKiem.Length > 0)
            {
                // Tạo một DataTable mới để chứa kết quả tìm kiếm
                DataTable ketQuaTimKiemTable = dataTable.Clone();

                // Thêm các dòng tìm kiếm vào DataTable mới
                foreach (DataRow row in ketQuaTimKiem)
                {
                    ketQuaTimKiemTable.ImportRow(row);
                }

                // Hiển thị kết quả tìm kiếm trong DataGridView hoặc thông báo
                dataGridView1.DataSource = ketQuaTimKiemTable;
            }
            else
            {
                MessageBox.Show("Không tìm thấy khách hàng với thẻ căn cước này.");
            }
        }



        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            TimKiemKhachHang();

        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox10.Clear();
            textBox11.Clear();
            maskedTextBox1.Clear();
            comboBox1.Select();
            comboBox3.Select();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string maDatSan = row.Cells["Mã Đặt Sân"].Value.ToString();

                // Gọi hàm hiển thị thông tin chi tiết dựa trên mã nhận sân
                DisplayDetailInfo(maDatSan);
            }
        }
        private void DisplayDetailInfo(string maDatSan)
        {
            try
            {
                conn.Open();

                // Tìm kiếm thông tin khách hàng theo Mã Đặt Sân
                string selectQuery = "SELECT PDS.MaDatSan AS 'Mã Đặt Sân',KH.SDT,KH.GIoiTinh,KH.QuocTich,KH.DiaChi,KH.NamSinh,LS.SoLuongNguoiDa,LS.Gia,PDS.MaLoaiSan,PDS.MaSanBong, KH.TenKH AS 'Họ và Tên', KH.MaThe AS 'CMND', LS.Ten AS 'Tên loại Sân', SB.Ten AS 'Loại Sân', PDS.NgayNhan AS 'Ngày Nhận', PDS.NgayTra AS 'Ngày Trả' " +
                                     " FROM PhieuDatSan PDS " +
                                     " INNER JOIN KhachHang KH ON PDS.MaKH = KH.MaKH " +
                                     " INNER JOIN LoaiSan LS ON PDS.MaLoaiSan = LS.MaLoaiSan " +
                                     " INNER JOIN SanBong SB ON PDS.MaSanBong = SB.MaSanBong and PDS.MaLoaiSan = SB.MaLoaiSan " +
                                     " WHERE PDS.MaDatSan = @MaDatSan";

                using (SqlCommand cmdSelect = new SqlCommand(selectQuery, conn))
                {
                    cmdSelect.Parameters.AddWithValue("@MaDatSan", maDatSan);

                    using (SqlDataReader reader = cmdSelect.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Điền thông tin lên các ô TextBox và ComboBox
                            textBox2.Text = reader["Họ và Tên"].ToString();
                            textBox3.Text = reader["CMND"].ToString();
                            textBox4.Text = reader["SDT"].ToString();
                            textBox5.Text = reader["DiaChi"].ToString();
                            textBox6.Text = reader["GioiTinh"].ToString();
                            textBox7.Text = reader["QuocTich"].ToString();
                            //textBox8.Text = reader["MaLoaiSan"].ToString();

                            DateTime ngayNhan = (reader["Ngày Nhận"] != DBNull.Value) ? Convert.ToDateTime(reader["Ngày Nhận"]) : DateTime.MinValue;
                            dateTimePicker1.Value = ngayNhan;

                            DateTime ngayTra = (reader["Ngày Trả"] != DBNull.Value) ? Convert.ToDateTime(reader["Ngày Trả"]) : DateTime.MinValue;
                            dateTimePicker2.Value = ngayTra;

                            DateTime ngaySinh = (reader["NamSinh"] != DBNull.Value) ? Convert.ToDateTime(reader["NamSinh"]) : DateTime.MinValue;
                            dateTimePicker3.Value = ngaySinh;

                            int soLuongNguoiToiDa = (reader["SoLuongNguoiDa"] != DBNull.Value) ? Convert.ToInt32(reader["SoLuongNguoiDa"]) : 0;
                            textBox10.Text = (soLuongNguoiToiDa != 0) ? soLuongNguoiToiDa.ToString() : string.Empty;

                            decimal gia = (reader["Gia"] != DBNull.Value) ? Convert.ToDecimal(reader["Gia"]) : 0;
                            textBox11.Text = (gia != 0) ? gia.ToString() : string.Empty;

                            TimeSpan timeDifference = ngayTra - ngayNhan;
                            maskedTextBox1.Text = $"{timeDifference.TotalHours:00}:{timeDifference.Minutes:00}";

                            comboBox3.SelectedValue = reader["MaLoaiSan"].ToString();
                            comboBox1.SelectedValue = reader["MaSanBong"].ToString();
                            
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin khách hàng với Mã Đặt Sân này.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý exception theo nhu cầu của bạn
                //MessageBox.Show($"Lỗi: {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateDetailInfo(dataGridView1.CurrentRow.Cells["Mã Đặt Sân"].Value.ToString());

            // Refresh the DataGridView with updated data
            LoadDataToDataGridView();
        }
        private void UpdateDetailInfo(string maDatSan)
        {
            try
            {
                conn.Open();

                // Update the information in the database based on the changes in the controls
                string updateQuery = "UPDATE KhachHang SET TenKH = @TenKH, MaThe = @MaThe, SDT = @SDT, " +
                                     "DiaChi = @DiaChi, GioiTinh = @GioiTinh, QuocTich = @QuocTich, " +
                                     "NamSinh = @NamSinh " +
                                     "WHERE MaKH = (SELECT MaKH FROM PhieuDatSan WHERE MaDatSan = @MaDatSan)";

                using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn))
                {
                    cmdUpdate.Parameters.AddWithValue("@MaDatSan", maDatSan);
                    cmdUpdate.Parameters.AddWithValue("@TenKH", textBox2.Text);
                    cmdUpdate.Parameters.AddWithValue("@MaThe", textBox3.Text);
                    cmdUpdate.Parameters.AddWithValue("@SDT", textBox4.Text);
                    cmdUpdate.Parameters.AddWithValue("@DiaChi", textBox5.Text);
                    cmdUpdate.Parameters.AddWithValue("@GioiTinh", textBox6.Text);
                    cmdUpdate.Parameters.AddWithValue("@QuocTich", textBox7.Text);
                    cmdUpdate.Parameters.AddWithValue("@NamSinh", dateTimePicker3.Value);

                    // Execute the update query for KhachHang
                    cmdUpdate.ExecuteNonQuery();
                }

                // Update reservation information
                DateTime ngayNhan = dateTimePicker1.Value;
                DateTime ngayTra = dateTimePicker2.Value;
                int maLoaiSan = Convert.ToInt32(comboBox3.SelectedValue);
                int maSanBong = Convert.ToInt32(comboBox1.SelectedValue);

                string updateReservationQuery = "UPDATE PhieuDatSan SET MaLoaiSan = @MaLoaiSan, MaSanBong = @MaSanBong, " +
                                                "NgayNhan = @NgayNhan, NgayTra = @NgayTra " +
                                                "WHERE MaDatSan = @MaDatSan";

                using (SqlCommand cmdUpdateReservation = new SqlCommand(updateReservationQuery, conn))
                {
                    cmdUpdateReservation.Parameters.AddWithValue("@MaDatSan", maDatSan);
                    cmdUpdateReservation.Parameters.AddWithValue("@MaLoaiSan", maLoaiSan);
                    cmdUpdateReservation.Parameters.AddWithValue("@MaSanBong", maSanBong);
                    cmdUpdateReservation.Parameters.AddWithValue("@NgayNhan", ngayNhan);
                    cmdUpdateReservation.Parameters.AddWithValue("@NgayTra", ngayTra);

                    // Execute the update query for PhieuDatSan
                    cmdUpdateReservation.ExecuteNonQuery();
                }

                MessageBox.Show("Cập nhật thông tin khách hàng và phiếu đặt sân thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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
    }
    public class ThongTinDatSan
    {
        public string HoTen { get; set; }
        public long CCCD { get; set; }
        public string TenSan { get; set; }
        public string TenLoaiSan { get; set; }
        public DateTime NgayNhan { get; set; }
        public DateTime NgayTra { get; set; }
        public int SoLuongNguoiToiDa { get; set; }
        public decimal Gia { get; set; }
        public int maDatsan { get; set; }
        public int maKhachH { get; set; }
        public int maSanbong { get; set; }
        public int maLoaisan { get; set; }
    }

}
