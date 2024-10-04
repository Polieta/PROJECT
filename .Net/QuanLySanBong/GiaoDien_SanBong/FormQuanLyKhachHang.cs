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
    public partial class FormQuanLyKhachHang : Form
    {
        private int maNV;
        
        SqlConnection conn;
        SqlDataAdapter adapter;
        DataSet ds;
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
        public FormQuanLyKhachHang(string role) : this()
        {
            userRole = role;
        }
        // Phương thức để lấy thông tin vai trò từ form khác (nếu cần)
        public string GetRole()
        {
            return userRole;
        }
        public FormQuanLyKhachHang()
        {
            InitializeComponent();
            SqlConnection sqlConnection = new SqlConnection("Data Source=ANHQUAN\\SQLEXPRESS;Initial Catalog=QLSANBONG;User ID=sa;Password=123;");
            conn = sqlConnection;
            adapter = new SqlDataAdapter();
            ds = new DataSet();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            adapter = new SqlDataAdapter("SELECT * FROM KHACHHANG", conn);

            ds.Clear();
            adapter.Fill(ds, "KHACHHANG");

            DataColumn[] col = new DataColumn[1];
            col[0] = ds.Tables["KHACHHANG"].Columns[0];
            ds.Tables["KHACHHANG"].PrimaryKey = col;
            DataTable dt = new DataTable();
            if (comboBox1.SelectedItem.ToString() == "Mã khách hàng")
            {
                string searchText = textBox1.Text;

                foreach (DataRow row in ds.Tables["KHACHHANG"].Rows)
                {
                    string maKH = row["MaKH"].ToString();
                    if (maKH.IndexOf(searchText) != -1)
                    {
                        dataGridView1.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString());
                    }
                    //else
                    //{
                    //    MessageBox.Show("Mã khách hàng không tồn tại", "Thông Báo");
                    //}
                }
            }
            else if (comboBox1.SelectedItem.ToString() == "Tên khách hàng")
            {
                string searchText = textBox1.Text;

                foreach (DataRow row in ds.Tables["KHACHHANG"].Rows)
                {
                    string tenKH = row["TenKH"].ToString();
                    if (tenKH.IndexOf(searchText) != -1)
                    {
                        dataGridView1.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString());
                    }
                    //else
                    //{
                    //    MessageBox.Show("Tên khách hàng không tồn tại", "Thông Báo");
                    //}
                }
            }
            else if (comboBox1.SelectedItem.ToString() == "Địa chỉ")
            {
                string searchText = textBox1.Text;

                foreach (DataRow row in ds.Tables["KHACHHANG"].Rows)
                {
                    string dcKH = row["Diachi"].ToString();
                    if (dcKH.IndexOf(searchText) != -1)
                    {
                        dataGridView1.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString());
                    }
                    //else
                    //{
                    //    MessageBox.Show("Địa chỉ khách hàng không tồn tại", "Thông Báo");
                    //}
                }
            }
            else if (comboBox1.SelectedItem.ToString() == "Giới tính")
            {
                string searchText = textBox1.Text;

                foreach (DataRow row in ds.Tables["KHACHHANG"].Rows)
                {
                    string gtKH = row["Gioitinh"].ToString();
                    if (gtKH.IndexOf(searchText) != -1)
                    {
                        dataGridView1.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString());
                    }
                    //else
                    //{
                    //    MessageBox.Show("Giới tính khách hàng không tồn tại", "Thông Báo");
                    //}
                }
            }
            else if (comboBox1.SelectedItem.ToString() == "Số điện thoại")
            {
                string searchText = textBox1.Text;

                foreach (DataRow row in ds.Tables["KHACHHANG"].Rows)
                {
                    string sdtKH = row["SDT"].ToString();
                    if (sdtKH.IndexOf(searchText) != -1)
                    {
                        dataGridView1.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString());
                    }
                    //else
                    //{
                    //    MessageBox.Show("Số điện thoại khách hàng không tồn tại", "Thông Báo");
                    //}
                }
            }
        }
        private void FormQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            btnQLKH.BackColor = Color.Orange;
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

        private void btnQLSB_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormQuanLySanBong frmDV_TT = new FormQuanLySanBong();
            frmDV_TT.MaNV = MaNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.ShowDialog();
        }


        private void btnQLKH_Click(object sender, EventArgs e)
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
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormBaoCaoDoanhThu frmDV_TT = new FormBaoCaoDoanhThu();
            frmDV_TT.MaNV = MaNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.ShowDialog();
        }

        private void btnQLNV_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormQuanLyNhanVien frmDV_TT = new FormQuanLyNhanVien();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            adapter = new SqlDataAdapter("SELECT * FROM KHACHHANG", conn);

            ds.Clear();
            adapter.Fill(ds, "KHACHHANG");

            DataColumn[] col = new DataColumn[1];
            col[0] = ds.Tables["KHACHHANG"].Columns[0];
            ds.Tables["KHACHHANG"].PrimaryKey = col;
            DataTable dt = new DataTable();
            if (comboBox1.SelectedItem.ToString() == "Mã khách hàng")
            {
                string searchText = textBox1.Text;

                foreach (DataRow row in ds.Tables["KHACHHANG"].Rows)
                {
                    string maKH = row["MaKH"].ToString();
                    if (maKH.IndexOf(searchText) != -1)
                    {
                        dataGridView1.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString());
                    }
                    //else
                    //{
                    //    MessageBox.Show("Mã khách hàng không tồn tại", "Thông Báo");
                    //}
                }
            }
            else if (comboBox1.SelectedItem.ToString() == "Tên khách hàng")
            {
                string searchText = textBox1.Text;

                foreach (DataRow row in ds.Tables["KHACHHANG"].Rows)
                {
                    string tenKH = row["TenKH"].ToString();
                    if (tenKH.IndexOf(searchText) != -1)
                    {
                        dataGridView1.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString());
                    }
                    //else
                    //{
                    //    MessageBox.Show("Tên khách hàng không tồn tại", "Thông Báo");
                    //}
                }
            }
            else if (comboBox1.SelectedItem.ToString() == "Địa chỉ")
            {
                string searchText = textBox1.Text;

                foreach (DataRow row in ds.Tables["KHACHHANG"].Rows)
                {
                    string dcKH = row["Diachi"].ToString();
                    if (dcKH.IndexOf(searchText) != -1)
                    {
                        dataGridView1.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString());
                    }
                    //else
                    //{
                    //    MessageBox.Show("Địa chỉ khách hàng không tồn tại", "Thông Báo");
                    //}
                }
            }
            else if (comboBox1.SelectedItem.ToString() == "Giới tính")
            {
                string searchText = textBox1.Text;

                foreach (DataRow row in ds.Tables["KHACHHANG"].Rows)
                {
                    string gtKH = row["Gioitinh"].ToString();
                    if (gtKH.IndexOf(searchText) != -1)
                    {
                        dataGridView1.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString());
                    }
                    //else
                    //{
                    //    MessageBox.Show("Giới tính khách hàng không tồn tại", "Thông Báo");
                    //}
                }
            }
            else if (comboBox1.SelectedItem.ToString() == "Số điện thoại")
            {
                string searchText = textBox1.Text;

                foreach (DataRow row in ds.Tables["KHACHHANG"].Rows)
                {
                    string sdtKH = row["SDT"].ToString();
                    if (sdtKH.IndexOf(searchText) != -1)
                    {
                        dataGridView1.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString());
                    }
                    //else
                    //{
                    //    MessageBox.Show("Số điện thoại khách hàng không tồn tại", "Thông Báo");
                    //}
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Control x = (Control)sender;
            if (x.Text.Length > 0 && !Char.IsDigit(x.Text[x.Text.Length - 1]))
            {
                this.errorProvider1.SetError(x, "Vui lòng nhập số");
            }
            else if (x.Text.Length > 10)
            {
                this.errorProvider1.SetError(x, "Bạn đã nhập vượt quá");

            }
            else
            {
                this.errorProvider1.Clear();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Control x = (Control)sender;
            if (x.Text.Length > 0 && !char.IsLetter(x.Text[x.Text.Length - 1]) && !char.IsWhiteSpace(x.Text[x.Text.Length - 1]))
            {
                this.errorProvider1.SetError(x, "Vui lòng nhập chữ");
            }
            else if (x.Text.Length > 13)
            {
                this.errorProvider1.SetError(x, "Bạn đã nhập vượt quá");

            }
            else
            {
                this.errorProvider1.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter("SELECT * FROM KHACHHANG", conn);

            ds.Clear();
            adapter.Fill(ds, "KHACHHANG");

            DataColumn[] col = new DataColumn[1];
            conn.Open();
            //
            string dateTimeString = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            string insertQuery = "INSERT INTO KHACHHANG (MaKH,TenKH,Mathe,NamSinh ,Diachi,SDT,GioiTinh) VALUES (@Value1, @Value2,@Value3, @Value4,@Value5, @Value6,@Value7)";
            SqlCommand command = new SqlCommand(insertQuery, conn);
            command.Parameters.AddWithValue("@Value1", (ds.Tables["KHACHHANG"].Rows.Count + 1).ToString("000"));
            command.Parameters.AddWithValue("@Value2", textBox3.Text);
            command.Parameters.AddWithValue("@Value3", ds.Tables["KHACHHANG"].Rows.Count.ToString("000"));
            command.Parameters.AddWithValue("@Value4", dateTimeString);
            command.Parameters.AddWithValue("@Value5", textBox4.Text);
            command.Parameters.AddWithValue("@Value6", textBox2.Text);
            command.Parameters.AddWithValue("@Value7", comboBox2.SelectedItem.ToString());
            command.ExecuteNonQuery();

            conn.Close();
            button1_Click(sender, e);
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            conn.Open();
            string dateTimeString = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            string updateQuery = "UPDATE KHACHHANG SET TenKH = @Value2,NAMSINH = @Value4, Diachi = @Value5, SDT = @Value6, Gioitinh = @Value7 WHERE MaKH = @Id";
            SqlCommand command = new SqlCommand(updateQuery, conn);
            //command.Parameters.AddWithValue("@Value1", value1);
            //command.Parameters.AddWithValue("@Value2", value2);
            //command.Parameters.AddWithValue("@Id", id);
            //command.Parameters.AddWithValue("@Value1", "KH" + (ds.Tables["KHACHHANG"].Rows.Count + 1).ToString("000"));
            command.Parameters.AddWithValue("@Value2", textBox3.Text);
            //command.Parameters.AddWithValue("@Value3", "MT0" + ds.Tables["KHACHHANG"].Rows.Count.ToString());
            command.Parameters.AddWithValue("@Value4", dateTimeString);
            command.Parameters.AddWithValue("@Value5", textBox4.Text);
            command.Parameters.AddWithValue("@Value6", textBox2.Text);
            command.Parameters.AddWithValue("@Value7", comboBox2.SelectedItem.ToString());
            //MessageBox.Show(dataGridView1.SelectedCells[0].Value.ToString().Trim());
            string s = dataGridView1.SelectedCells[0].Value.ToString().Trim();
            command.Parameters.AddWithValue("@Id", s);
            command.ExecuteNonQuery();

            conn.Close();
            button1_Click(sender, e);
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            conn.Open();
            string dateTimeString = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            string updateQuery = "UPDATE KHACHHANG SET TenKH = @Value2,NAMSINH = @Value4, Diachi = @Value5, SDT = @Value6, Gioitinh = @Value7 WHERE MaKH = @Id";
            SqlCommand command = new SqlCommand(updateQuery, conn);
            //command.Parameters.AddWithValue("@Value1", value1);
            //command.Parameters.AddWithValue("@Value2", value2);
            //command.Parameters.AddWithValue("@Id", id);
            //command.Parameters.AddWithValue("@Value1", "KH" + (ds.Tables["KHACHHANG"].Rows.Count + 1).ToString("000"));
            command.Parameters.AddWithValue("@Value2", textBox3.Text);
            //command.Parameters.AddWithValue("@Value3", "MT0" + ds.Tables["KHACHHANG"].Rows.Count.ToString());
            command.Parameters.AddWithValue("@Value4", dateTimeString);
            command.Parameters.AddWithValue("@Value5", textBox4.Text);
            command.Parameters.AddWithValue("@Value6", textBox2.Text);
            command.Parameters.AddWithValue("@Value7", comboBox2.SelectedItem.ToString());
            //MessageBox.Show(dataGridView1.SelectedCells[0].Value.ToString().Trim());
            string s = dataGridView1.SelectedCells[0].Value.ToString().Trim();
            command.Parameters.AddWithValue("@Id", s);
            command.ExecuteNonQuery();

            conn.Close();
            button1_Click(sender, e);
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["TenKH"].Value != null)
            {
                textBox3.Text = dataGridView1.CurrentRow.Cells["TenKH"].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                comboBox2.Text = dataGridView1.CurrentRow.Cells["GioiTinh"].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells["DiaChi"].Value.ToString();
                dateTimePicker1.Text = dataGridView1.CurrentRow.Cells["NamSinh"].Value.ToString();
            }
        }
    }
}
