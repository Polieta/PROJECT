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
    public partial class FormDichVu_ThanhToan : Form
    {
        SqlConnection conn;
        DataSet ds;
        SqlDataAdapter adapt;
        DataColumn[] key = new DataColumn[1];
        //Chuyển ROLES
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
        //
        private int maNV;

        public int MaNV
        {
            get { return maNV; }
            set { maNV = value; }
        }
        public FormDichVu_ThanhToan()
        {
            InitializeComponent();
            string sqlconnect = "Data Source=POLIETTA\SQLEXPRESS;Initial Catalog=QLSANBONG;Integrated Security=True";
            conn = new SqlConnection(sqlconnect);
            ds = new DataSet();
        }
       
        private void FormDichVu_ThanhToan_Load(object sender, EventArgs e)
        {
            btnDV_TT.BackColor = Color.Orange;
            LoadLoaidvComboBox1();
            LoaddvComboBox2();
            GanhTextChoButtons();
            loadformnhansan();
        }

        private void btnDatSan_Click(object sender, EventArgs e)
        {
            //Chuyen MaNV thanh cong
            this.Hide();
            FormTrangChu frmDV_TT = new FormTrangChu();
            this.Hide();    
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

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormQuanLySanBong frmDV_TT = new FormQuanLySanBong();
            frmDV_TT.MaNV = MaNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.Location = this.Location;
            frmDV_TT.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
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

        private void button6_Click(object sender, EventArgs e)
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

        private void button7_Click(object sender, EventArgs e)
        {// Kiểm tra xem vai trò của người dùng có phải là admin không
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

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormDichVu_ThanhToan frmDV_TT = new FormDichVu_ThanhToan();
            frmDV_TT.MaNV = MaNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.Location = this.Location;
            frmDV_TT.ShowDialog();
        }

        private void btnDV_TT_Click(object sender, EventArgs e)
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
        private void LoadLoaidvComboBox1()
        {
            adapt = new SqlDataAdapter("select * from LoaiDichVu", conn);
            adapt.Fill(ds, "LoaiDichVu");
            comboBox1.DataSource = ds.Tables["LoaiDichVu"];
            comboBox1.DisplayMember = "Ten";
            comboBox1.ValueMember = "MaLoaiDV";
        }

        private void LoaddvComboBox2()
        {
            adapt = new SqlDataAdapter("select * from DichVu", conn);
            adapt.Fill(ds, "DichVu");
            comboBox2.DataSource = ds.Tables["DichVu"];
            comboBox2.DisplayMember = "Ten";
            comboBox2.ValueMember = "MaDV";

        }
        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ds != null && ds.Tables["DichVu"] != null && comboBox1.SelectedItem != null)
            {
                //DataRowView selectedRow = (DataRowView)comboBox1.SelectedItem;
                //int maLoaiDVChon = Convert.ToInt32(selectedRow["MaLoaiDV"]);
                //DataView dvDichVu = new DataView(ds.Tables["DichVu"]);
                //dvDichVu.RowFilter = $"MaLoaiDV = {maLoaiDVChon}";

                //comboBox2.DataSource = dvDichVu.ToTable();
                //comboBox2.DisplayMember = "Ten";
                //comboBox2.ValueMember = "MaDV";


                //string slngtd = GetGiaThanhByMaLoaiDV(int.Parse(comboBox2.SelectedValue.ToString()));
                ////hiển thị số lượng người tối đa của sân bóng
                //textBox4.Text = slngtd;

                int maLoaiDV;


                if (int.TryParse(comboBox1.SelectedValue.ToString(), out maLoaiDV))
                {
                    DataRow[] sanBongRows = ds.Tables["DichVu"].Select($"MaLoaiDV = {maLoaiDV}");

                    // Kiểm tra nếu không có dòng nào được lọc
                    if (sanBongRows.Length == 0)
                    {
                        // Hiển thị thông báo hoặc thực hiện các hành động khác nếu cần
                        return;
                    }

                    // Tạo một DataTable mới để chứa các dòng đã lọc
                    DataTable filteredSanBongTable = ds.Tables["DichVu"].Clone();
                    foreach (DataRow row in sanBongRows)
                    {
                        filteredSanBongTable.ImportRow(row);
                    }

                    // Gán dữ liệu cho comboBox2
                    comboBox2.DataSource = filteredSanBongTable;
                    comboBox2.DisplayMember = "Ten";
                    comboBox2.ValueMember = "MaDV";
                    string slngtd = GetGiaThanhByMaLoaiDV(int.Parse(comboBox2.SelectedValue.ToString()));
                    //hiển thị số lượng người tối đa của sân bóng
                    textBox4.Text = slngtd;
                }
            }
        }
        private string GetGiaThanhByMaLoaiDV(int maDV)
        {
            string giaThanh = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Gia FROM DichVu WHERE MaDV = @MaDV", conn);
                cmd.Parameters.AddWithValue("@MaDV", maDV);
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
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(comboBox2.SelectedValue.ToString(), out int maDichVuChon))
            {
                // Lấy thông tin dịch vụ được chọn từ bảng DichVu
                DataRow[] dichVuRows = ds.Tables["DichVu"].Select($"MaDV = {maDichVuChon}");

                // Kiểm tra nếu có dịch vụ được chọn
                if (dichVuRows.Length > 0)
                {
                    // Lấy dòng đầu tiên từ kết quả lọc
                    DataRow selectedDichVuRow = dichVuRows[0];

                    // Lấy giá thành từ dòng đã lọc và hiển thị lên TextBox tương ứng
                    string giaThanh = selectedDichVuRow["Gia"].ToString();
                    textBox4.Text = giaThanh;
                }
            }
        }
        private bool isButtonClicked = false;
        public void loadformnhansan()
        {
            // Lấy dữ liệu từ SQL
            using (SqlDataAdapter phieuNhanSanAdapter = new SqlDataAdapter("SELECT NS.MaNhanSan as N'Mã Nhận Sân', PDS.MaDatSan, KH.TenKH, KH.MaThe, LS.Ten AS 'TenLoaiSan', SB.Ten AS 'TenSan', PDS.NgayNhan, PDS.NgayTra, LS.SoLuongNguoiDa, LS.Gia, PDS.MaLoaiSan, PDS.MaSanBong,HD.MaHoaDon " +
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
                int buttonCount = 6;
                DataTable loaiSanTable = GetLoaiSanFromDatabase();

                // Sắp xếp lại dữ liệu theo MaLoaiSan để đảm bảo thứ tự đúng
                loaiSanTable.DefaultView.Sort = "MaLoaiSan ASC";
                loaiSanTable = loaiSanTable.DefaultView.ToTable();

                // Kiểm tra từng button và dữ liệu tương ứng
                foreach (DataRow row in phieuNhanSanTable.Rows)
                {
                    int maLoaiSan = Convert.ToInt32(row["MaLoaiSan"]);
                    int maSanBong = Convert.ToInt32(row["MaSanBong"]);
                  

                    for (int i = 0; i < buttonCount; i++)
                    {

                        System.Windows.Forms.Button btn = (System.Windows.Forms.Button)this.Controls.Find($"button{i + 14}", true).FirstOrDefault();
                        System.Windows.Forms.Label lab = (System.Windows.Forms.Label)this.Controls.Find($"label{i + 16}", true).FirstOrDefault();
                        if (btn != null && i < loaiSanTable.Rows.Count)
                        {
                            // Đặt Tag của button là MaLoaiSan để sử dụng trong các xử lý sau này
                            if (maLoaiSan.ToString() == loaiSanTable.Rows[i]["MaLoaiSan"].ToString() && maSanBong.ToString() == loaiSanTable.Rows[i]["MaSanBong"].ToString())
                                btn.Enabled = true;
                                btn.Tag = Convert.ToInt32(row["MaHoaDon"]);

                        }
                    }
                }

            }
        }
        
        private void button14_Click(object sender, EventArgs e)
        {
            if (isButtonClicked)
            {
                // Nếu đã được click, quay về màu cũ
                button1.BackColor = Color.Pink;
            }
            else
            {
                // Nếu chưa được click, thay đổi màu sang hồng
                button1.Enabled = true;
            }

            // Đảo ngược trạng thái của biến cờ
            isButtonClicked = !isButtonClicked;
        }
        private void GanhTextChoButtons()
        {
            // Đoạn mã này giả sử bạn đã có các button từ button15 đến button19

            // Lấy dữ liệu loại sân từ CSDL hoặc sử dụng dữ liệu đã có
            DataTable loaiSanTable = GetLoaiSanFromDatabase();

            // Sắp xếp lại dữ liệu theo MaLoaiSan để đảm bảo thứ tự đúng
            loaiSanTable.DefaultView.Sort = "MaLoaiSan ASC";
            loaiSanTable = loaiSanTable.DefaultView.ToTable();

            // Số lượng button từ 15 đến 19
            int buttonCount = 6;

            for (int i = 0; i < buttonCount; i++)
            {
                               
                System.Windows.Forms.Button btn = (System.Windows.Forms.Button)this.Controls.Find($"button{i + 14}", true).FirstOrDefault();
                System.Windows.Forms.Label lab = (System.Windows.Forms.Label)this.Controls.Find($"label{i + 16}", true).FirstOrDefault();
                if (btn != null && i < loaiSanTable.Rows.Count)
                {
                    // Lấy tên sân từ dữ liệu loại sân
                    string tenSan = loaiSanTable.Rows[i]["Ten"].ToString();
                    lab.Text = loaiSanTable.Rows[i]["TenSan"].ToString();
                    // Gắn tên sân vào Text của button
                    btn.Text = tenSan;
                    
                    // Đặt Tag của button là MaLoaiSan để sử dụng trong các xử lý sau này
                    btn.Tag = loaiSanTable.Rows[i]["MaLoaiSan"].ToString();
                    lab.Tag = loaiSanTable.Rows[i]["MaSanBong"].ToString();
                }
            }
        }

        // Hàm để lấy dữ liệu từ CSDL (giả sử bạn đã có hàm này)
        private DataTable GetLoaiSanFromDatabase()
        {
            DataTable dataTable = new DataTable();

            try
            {
                conn.Open();

                string query = "SELECT LS.MaLoaiSan, LS.Ten, SB.Ten AS TenSan,SB.MaTrangThaiSan,SB.MaSanBong FROM LoaiSan LS " +
                               "LEFT JOIN SanBong SB ON LS.MaLoaiSan = SB.MaLoaiSan";

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dataTable;
        }


        // Sự kiện Load của Form


        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.Button)
            {
                System.Windows.Forms.Button clickedButton = (System.Windows.Forms.Button)sender;

                // Đổi màu của button thành LightSteelBlue
                clickedButton.BackColor = Color.LightSteelBlue;

                //Lấy thông tin từ Tag của button
                if (clickedButton.Tag != null)
                {
                    // Lấy thông tin từ các điều khiển trên form
                    string loaiDichVu = comboBox1.SelectedValue.ToString();
                    int giaDichVu = int.Parse(textBox4.Text);
                    int soLuong = (int)numericUpDown1.Value;
                    int tt = giaDichVu * soLuong;

                    try
                    {
                        conn.Open();

                        // Lấy mã hóa đơn từ Tag của button
                        int maHoaDon = Convert.ToInt32(clickedButton.Tag);

                        // Lấy mã chi tiết hóa đơn mới
                        SqlCommand getMaxIdCthdCmd = new SqlCommand("SELECT ISNULL(MAX(MaCTHD), 0) + 1 FROM ChiTietHoaDon", conn);
                        int maCthd = Convert.ToInt32(getMaxIdCthdCmd.ExecuteScalar());

                        // Thực hiện insert dữ liệu vào bảng ChiTietHoaDon
                        string query = $"INSERT INTO ChiTietHoaDon (MaCTHD, MaHoaDon, MaDV, SoLuong, TongTien) VALUES ({maCthd}, {maHoaDon}, {loaiDichVu}, {soLuong}, {tt})";
                        using (SqlCommand commandInsert = new SqlCommand(query, conn))
                        {
                            int rowsAffected = commandInsert.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Dữ liệu đã được thêm vào bảng ChiTietHoaDon.", "Thông Báo");
                            }
                            else
                            {
                                MessageBox.Show("Không thể thêm dữ liệu vào bảng ChiTietHoaDon.", "Lỗi");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
                    }
                    finally
                    {
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }


        private void button17_Click(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {

        }
    }
}
