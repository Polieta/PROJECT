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
    public partial class FormQuanLySanBong : Form
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

        public void SetRole(string role)
        {
            userRole = role;
        }


        public string GetRole()
        {
            return userRole;
        }
        public FormQuanLySanBong()
        {
            InitializeComponent();
            SqlConnection sqlConnection = new SqlConnection("Data Source=ANHQUAN\\SQLEXPRESS;Initial Catalog=QLSANBONG;User ID=sa;Password=123;");
            conn = sqlConnection;
            adapter = new SqlDataAdapter();
            ds = new DataSet();
            //comboBox2.SelectedIndex = 0;
            adapter = new SqlDataAdapter("SELECT Masanbong FROM Sanbong", conn);
            adapter.Fill(ds, "Sanbong");
            foreach (DataRow row in ds.Tables["Sanbong"].Rows)
            {

                comboBox1.Items.Add(row["Masanbong"].ToString());

            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;

            adapter = new SqlDataAdapter("SELECT sanbong.masanbong,sanbong.ten,loaisan.ten,trangthaisan.ten,loaisan.SoLuongNguoiDa,loaisan.gia FROM sanbong,trangthaisan,loaisan where sanbong.matrangthaisan=trangthaisan.matrangthaisan and sanbong.maloaisan=loaisan.maloaisan", conn);

            ds.Clear();
            adapter.Fill(ds, "sanbong");

            DataColumn[] col = new DataColumn[1];
            col[0] = ds.Tables["sanbong"].Columns[0];
            ds.Tables["sanbong"].PrimaryKey = col;
            DataTable dt = new DataTable();

            string searchText = textBox1.Text;
            string row3;
            string row2;
            foreach (DataRow row in ds.Tables["sanbong"].Rows)
            {
                string Ten = row["Ten"].ToString();
                string maDV = row["MaSanBong"].ToString();
                if (maDV.IndexOf(searchText) != -1 || Ten.IndexOf(searchText) != -1)
                {
                    if (row[2].ToString() == "1")
                    {
                        row2 = "Sân cỏ tự nhiên";
                    }
                    else if (row[2].ToString() == "2")
                    {
                        row2 = "Sân cỏ nhân tạo";
                    }
                    else
                    {
                        row2 = "Sân nhựa";
                    }
                    if (row[3].ToString() == "1")
                    {
                        row3 = "Đã đặt";
                    }
                    else if (row[3].ToString() == "2")
                    {
                        row3 = "Chưa đặt";
                    }
                    else
                    {
                        row3 = "Đang bảo trì";
                    }
                    dataGridView1.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString());
                }

            }
        }
        private void FormQuanLySanBong_Load(object sender, EventArgs e)
        {
            btnQLSB.BackColor = Color.Orange;
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

        private void btnDV_TT_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormDichVu_ThanhToan frmDV_TT = new FormDichVu_ThanhToan();
            frmDV_TT.MaNV = MaNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.Location = this.Location;
            frmDV_TT.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormQuanLyDichVu frmDV_TT = new FormQuanLyDichVu();
            frmDV_TT.MaNV = MaNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.Location = this.Location;
            frmDV_TT.ShowDialog();
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

        private void btnQLSB_Click(object sender, EventArgs e)
        {

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

        private void button9_Click(object sender, EventArgs e)
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
            adapter = new SqlDataAdapter("SELECT sanbong.masanbong,sanbong.ten,loaisan.ten,trangthaisan.ten,loaisan.SoLuongNguoiDa,loaisan.gia FROM sanbong,trangthaisan,loaisan where sanbong.matrangthaisan=trangthaisan.matrangthaisan and sanbong.maloaisan=loaisan.maloaisan", conn);

            ds.Clear();
            adapter.Fill(ds, "sanbong");

            DataColumn[] col = new DataColumn[1];
            col[0] = ds.Tables["sanbong"].Columns[0];
            ds.Tables["sanbong"].PrimaryKey = col;
            DataTable dt = new DataTable();

            string searchText = textBox1.Text;
            string row3;
            string row2;
            foreach (DataRow row in ds.Tables["sanbong"].Rows)
            {
                string Ten = row["Ten"].ToString();
                string maDV = row["MaSanBong"].ToString();
                if (maDV.IndexOf(searchText) != -1 || Ten.IndexOf(searchText) != -1)
                {
                    if (row[2].ToString() == "1")
                    {
                        row2 = "Sân cỏ tự nhiên";
                    }
                    else if (row[2].ToString() == "2")
                    {
                        row2 = "Sân cỏ nhân tạo";
                    }
                    else
                    {
                        row2 = "Sân nhựa";
                    }
                    if (row[3].ToString() == "1")
                    {
                        row3 = "Đã đặt";
                    }
                    else if (row[3].ToString() == "2")
                    {
                        row3 = "Chưa đặt";
                    }
                    else
                    {
                        row3 = "Đang bảo trì";
                    }
                    dataGridView1.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString());
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter("SELECT * FROM sanbong", conn);

            ds.Clear();
            adapter.Fill(ds, "sanbong");

            DataColumn[] col = new DataColumn[1];
            conn.Open();


            string insertQuery = "INSERT INTO sanbong (Masanbong,Ten,MaLoaisan,matrangthaisan) VALUES (@Value1, @Value2,@Value3, @Value4)";
            SqlCommand command = new SqlCommand(insertQuery, conn);
            command.Parameters.AddWithValue("@Value1", (ds.Tables["sanbong"].Rows.Count + 1).ToString());
            command.Parameters.AddWithValue("@Value2", textBox3.Text);
            if (comboBox3.SelectedItem.ToString() == "Sân cỏ tự nhiên")
            {
                command.Parameters.AddWithValue("@Value3", 1);

            }
            else if (comboBox3.SelectedItem.ToString() == "Sân cỏ nhân tạo")
            {
                command.Parameters.AddWithValue("@Value3", 2);

            }
            else
            {
                command.Parameters.AddWithValue("@Value3", 3);

            }
            if (comboBox2.SelectedItem.ToString() == "Đã đặt")
            {
                command.Parameters.AddWithValue("@Value4", 1);

            }
            else if (comboBox2.SelectedItem.ToString() == "Chưa đặt")
            {
                command.Parameters.AddWithValue("@Value4", 2);

            }
            else
            {
                command.Parameters.AddWithValue("@Value4", 3);

            }
            command.ExecuteNonQuery();
            conn.Close();
            button1_Click(sender, e);
            textBox1.Clear();
            textBox8.Clear();
            textBox7.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter("SELECT * FROM loaisan", conn);

            ds.Clear();
            adapter.Fill(ds, "loaisan");

            //DataColumn[] col1 = new DataColumn[1];
            conn.Open();


            string updateQuery = "Update  loaisan set gia=@Value2,soluongnguoida=@Value3 where maloaisan = @id";
            SqlCommand command = new SqlCommand(updateQuery, conn);

            command.Parameters.AddWithValue("@Value2", Convert.ToInt32(textBox7.Text));
            command.Parameters.AddWithValue("@Value3", Convert.ToInt32(textBox8.Text));
            if (comboBox3.SelectedItem.ToString() == "Sân cỏ tự nhiên")
            {
                command.Parameters.AddWithValue("@id", 1);

            }
            else if (comboBox3.SelectedItem.ToString() == "Sân cỏ nhân tạo")
            {
                command.Parameters.AddWithValue("@id", 2);

            }
            else
            {
                command.Parameters.AddWithValue("@id", 3);

            }
            command.ExecuteNonQuery();

            conn.Close();
            button1_Click(sender, e);
            textBox1.Clear();
            textBox8.Clear();
            textBox7.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter("SELECT * FROM loaisan", conn);

            ds.Clear();
            adapter.Fill(ds, "loaisan");

            //DataColumn[] col1 = new DataColumn[1];
            conn.Open();


            string updateQuery = "Update  sanbong set Ten=@Value2,maloaisan=@Value3,matrangthaisan =@Value4 where masanbong = @id";
            SqlCommand command = new SqlCommand(updateQuery, conn);

            command.Parameters.AddWithValue("@Value2", textBox3.Text);
            if (comboBox3.SelectedItem.ToString() == "Sân cỏ tự nhiên")
            {
                command.Parameters.AddWithValue("@Value3", "1");

            }
            else if (comboBox3.SelectedItem.ToString() == "Sân cỏ nhân tạo")
            {
                command.Parameters.AddWithValue("@Value3", "2");

            }
            else
            {
                command.Parameters.AddWithValue("@Value3", "3");

            }
            if (comboBox2.SelectedItem.ToString() == "Đã đặt")
            {
                command.Parameters.AddWithValue("@Value4", "1");

            }
            else if (comboBox2.SelectedItem.ToString() == "Chưa đặt")
            {
                command.Parameters.AddWithValue("@Value4", "2");

            }
            else
            {
                command.Parameters.AddWithValue("@Value4", "3");

            }
            string s = dataGridView1.SelectedCells[0].Value.ToString().Trim();
            command.Parameters.AddWithValue("@Id", s);
            command.ExecuteNonQuery();

            conn.Close();
            button1_Click(sender, e);
            textBox1.Clear();
            textBox8.Clear();
            textBox3.Clear();
            textBox7.Clear();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["tensan"].Value != null)
            {
                comboBox1.SelectedIndex = Convert.ToInt32(dataGridView1.CurrentRow.Cells["masan"].Value.ToString()) - 1;

                textBox3.Text = dataGridView1.CurrentRow.Cells["tensan"].Value.ToString();
                string loaisan = dataGridView1.CurrentRow.Cells["loaisan"].Value.ToString();
                if (loaisan == "Sân cỏ tự nhiên")
                {
                    comboBox3.SelectedIndex = 0;

                }
                else
                    if (loaisan == "Sân cỏ nhân tạo")
                {
                    comboBox3.SelectedIndex = 1;

                }
                else
                {
                    comboBox3.SelectedIndex = 2;

                }
                string trangthai = dataGridView1.CurrentRow.Cells["trangthai"].Value.ToString();
                if (trangthai == "Đã đặt")
                {
                    comboBox2.SelectedIndex = 0;

                }
                else
                    if (trangthai == "Chưa đặt")
                {
                    comboBox2.SelectedIndex = 1;

                }
                else
                {
                    comboBox2.SelectedIndex = 2;

                }
                textBox8.Text = dataGridView1.CurrentRow.Cells["songuoi"].Value.ToString();

                textBox7.Text = dataGridView1.CurrentRow.Cells["dongia"].Value.ToString();

            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            Control x = (Control)sender;
            if (x.Text.Length > 0 && !Char.IsDigit(x.Text[x.Text.Length - 1]))
            {
                this.errorProvider1.SetError(x, "Vui lòng nhập số");
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

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            Control x = (Control)sender;
            if (x.Text.Length > 0 && !Char.IsDigit(x.Text[x.Text.Length - 1]))
            {
                this.errorProvider1.SetError(x, "Vui lòng nhập số");
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

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
