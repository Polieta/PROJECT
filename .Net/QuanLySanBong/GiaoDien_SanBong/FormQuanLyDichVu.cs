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
    public partial class FormQuanLyDichVu : Form
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
        public FormQuanLyDichVu()
        {
            InitializeComponent();
            SqlConnection sqlConnection = new SqlConnection("Data Source=ANHQUAN\\SQLEXPRESS;Initial Catalog=QLSANBONG;User ID=sa;Password=123;");
            conn = sqlConnection;
            adapter = new SqlDataAdapter();
            ds = new DataSet();
            comboBox2.SelectedIndex = 0;
            adapter = new SqlDataAdapter("SELECT MaDV FROM Dichvu", conn);
            adapter.Fill(ds, "Dichvu");

            foreach (DataRow row in ds.Tables["Dichvu"].Rows)
            {

                comboBox1.Items.Add(row["MaDV"].ToString());

            }
            adapter = new SqlDataAdapter("SELECT * FROM Dichvu", conn);

            ds.Clear();
            adapter.Fill(ds, "Dichvu");

            DataColumn[] col = new DataColumn[1];
            col[0] = ds.Tables["Dichvu"].Columns[0];
            ds.Tables["Dichvu"].PrimaryKey = col;
            DataTable dt = new DataTable();

            string searchText = textBox1.Text;

            foreach (DataRow row in ds.Tables["Dichvu"].Rows)
            {
                string Ten = row["Ten"].ToString();
                string maDV = row["MaDV"].ToString();
                if (maDV.IndexOf(searchText) != -1 || Ten.IndexOf(searchText) != -1)
                {
                    if (row[2].ToString() == "1")
                    {
                        dataGridView1.Rows.Add(row[0].ToString(), row[1].ToString(), "Nước Uống", row[3].ToString());

                    }
                    else if (row[2].ToString() == "2")
                    {
                        dataGridView1.Rows.Add(row[0].ToString(), row[1].ToString(), "Thức Ăn", row[3].ToString());

                    }
                    else if (row[2].ToString() == "1")
                    {
                        dataGridView1.Rows.Add(row[0].ToString(), row[1].ToString(), "Thuê Đồ", row[3].ToString());

                    }
                    else
                    {
                        dataGridView1.Rows.Add(row[0].ToString(), row[1].ToString(), "Nước Uống", row[3].ToString());

                    }
                }
            }
        }

        private void FormQuanLyDichVu_Load(object sender, EventArgs e)
        {
            btnQLDV.BackColor = Color.Orange;
        }

        private void btnDatSan_Click(object sender, EventArgs e)
        {
            this.Hide();
            // Mở FormDichVu_ThanhToan và truyền vai trò
            FormTrangChu frmDV_TT = new FormTrangChu();
            frmDV_TT.MaNV = maNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.Location = this.Location;
            frmDV_TT.ShowDialog();
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
            frmDV_TT.MaNV = maNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.Location = this.Location;
            frmDV_TT.ShowDialog();
        }

        private void btnQLSB_Click(object sender, EventArgs e)
        {
            this.Hide();
            // Mở FormDichVu_ThanhToan và truyền vai trò
            FormQuanLySanBong frmDV_TT = new FormQuanLySanBong();
            frmDV_TT.MaNV = maNV;
            frmDV_TT.SetRole(GetRole());
            frmDV_TT.Location = this.Location;
            frmDV_TT.ShowDialog();
        }

        private void btnThongKeDoanhThu_Click(object sender, EventArgs e)
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

        private void btnQLDV_Click(object sender, EventArgs e)
        {
            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // Mở FormThongTinCaNhan và truyền thông tin người dùng
            FormThongTinCaNhan formThongTinCaNhan = new FormThongTinCaNhan();
            //formThongTinCaNhan.SetRole(GetRole());
            //formThongTinCaNhan.SetMaNV(GetManV());
            formThongTinCaNhan.MaNV = maNV;
            formThongTinCaNhan.Location = this.Location;
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
            adapter = new SqlDataAdapter("SELECT * FROM Dichvu", conn);

            ds.Clear();
            adapter.Fill(ds, "Dichvu");

            DataColumn[] col = new DataColumn[1];
            col[0] = ds.Tables["Dichvu"].Columns[0];
            ds.Tables["Dichvu"].PrimaryKey = col;
            DataTable dt = new DataTable();

            string searchText = textBox1.Text;

            foreach (DataRow row in ds.Tables["Dichvu"].Rows)
            {
                string Ten = row["Ten"].ToString();
                string maDV = row["MaDV"].ToString();
                if (maDV.IndexOf(searchText) != -1 || Ten.IndexOf(searchText) != -1)
                {
                    if (row[2].ToString() == "1")
                    {
                        dataGridView1.Rows.Add(row[0].ToString(), row[1].ToString(), "Nước Uống", row[3].ToString());

                    }
                    else if (row[2].ToString() == "2")
                    {
                        dataGridView1.Rows.Add(row[0].ToString(), row[1].ToString(), "Thức Ăn", row[3].ToString());

                    }
                    else if (row[2].ToString() == "1")
                    {
                        dataGridView1.Rows.Add(row[0].ToString(), row[1].ToString(), "Thuê Đồ", row[3].ToString());

                    }
                    else
                    {
                        dataGridView1.Rows.Add(row[0].ToString(), row[1].ToString(), "Nước Uống", row[3].ToString());

                    }
                }
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
            adapter = new SqlDataAdapter("SELECT * FROM Dichvu", conn);

            ds.Clear();
            adapter.Fill(ds, "Dichvu");

            DataColumn[] col = new DataColumn[1];
            conn.Open();


            string insertQuery = "INSERT INTO Dichvu (MaDV,Ten,MaLoaiDV,Gia) VALUES (@Value1, @Value2,@Value3, @Value4)";
            SqlCommand command = new SqlCommand(insertQuery, conn);
            command.Parameters.AddWithValue("@Value1", (ds.Tables["Dichvu"].Rows.Count + 1).ToString("000"));
            command.Parameters.AddWithValue("@Value2", textBox3.Text);
            if (comboBox2.SelectedItem.ToString() == "Nước Uống")
            {
                command.Parameters.AddWithValue("@Value3", "1");

            }
            else if (comboBox2.SelectedItem.ToString() == "Thức Ăn")
            {
                command.Parameters.AddWithValue("@Value3", "2");

            }
            else if (comboBox2.SelectedItem.ToString() == "Thuê Đồ")
            {
                command.Parameters.AddWithValue("@Value3", "3");

            }
            else
            {
                command.Parameters.AddWithValue("@Value3", "1");

            }
            command.Parameters.AddWithValue("@Value4", textBox2.Text);
            command.ExecuteNonQuery();

            conn.Close();
            button1_Click(sender, e);
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter("SELECT * FROM Dichvu", conn);

            ds.Clear();
            adapter.Fill(ds, "Dichvu");

            DataColumn[] col = new DataColumn[1];
            conn.Open();


            string updateQuery = "Update  Dichvu set Ten=@Value2,MaLoaiDV=@Value3,Gia =@Value4 where madv = @id";
            SqlCommand command = new SqlCommand(updateQuery, conn);

            command.Parameters.AddWithValue("@Value2", textBox3.Text);
            if (comboBox2.SelectedItem.ToString() == "Nước Uống")
            {
                command.Parameters.AddWithValue("@Value3", "1");

            }
            else if (comboBox2.SelectedItem.ToString() == "Thức Ăn")
            {
                command.Parameters.AddWithValue("@Value3", "2");

            }
            else if (comboBox2.SelectedItem.ToString() == "Thuê Đồ")
            {
                command.Parameters.AddWithValue("@Value3", "3");

            }
            else
            {
                command.Parameters.AddWithValue("@Value3", "1");

            }
            command.Parameters.AddWithValue("@Value4", textBox2.Text);
            string s = dataGridView1.SelectedCells[0].Value.ToString().Trim();
            command.Parameters.AddWithValue("@Id", s);
            command.ExecuteNonQuery();

            conn.Close();
            button1_Click(sender, e);
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["Ten"].Value != null)
            {
                textBox3.Text = dataGridView1.CurrentRow.Cells["Ten"].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells["Gia"].Value.ToString();
                comboBox2.Text = dataGridView1.CurrentRow.Cells["LoaiDV"].Value.ToString();
                comboBox1.Text = dataGridView1.CurrentRow.Cells["MaDV"].Value.ToString();


            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
