using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Nhom6_QLShopThoiTrang.Forms
{
    public partial class FormQLHoaDon : Form
    {
        string ConnStr = Nhom6_QLShopThoiTrang.Properties.Settings.Default.connStr;
        public FormQLHoaDon()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            DateTime date = Convert.ToDateTime(currentDate.ToString("dd/MM/yyyy"));
            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                connection.Open();

                try
                {
                    // Tạo đối tượng SqlCommand để gọi Stored Procedure
                    using (SqlCommand command = new SqlCommand("FilterHoaDonByNgayBan", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@NgayBan", date);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            //kiểm tra xem có dữ liệu hay không
                            if (dataTable.Rows.Count == 0)
                            {
                                MessageBox.Show("Không có hóa đơn nào trong ngày hôm này !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            dataGridView1.DataSource = dataTable;
                            //tính tổng tiền hóa đơn trong ngày
                            long sum = 0;
                            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                            {
                                sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);

                            }
                            txtTongTien.Text = sum.ToString();
                            txtTongSP.Text = dataGridView1.Rows.Count.ToString();
                            txtTongTien.ReadOnly = true;
                            txtTongSP.ReadOnly = true;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Lọc dữ liệu không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_XoaSanPham_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            txtTongSP.Text = "";
            txtTongTien.Text = "";
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtSDT.Text = "";
            txtTongTienKH.Text = "";
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtGT.Text = "";
            txtDC.Text = "";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime dateTime = dateTimePicker1.Value;
            DateTime date = Convert.ToDateTime(dateTime.ToString("dd/MM/yyyy"));
            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("FilterHoaDonByNgayBan", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@NgayBan", date);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            if (dataTable.Rows.Count == 0)
                            {
                                MessageBox.Show("Không có hóa đơn nào trong ngày này !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            dataGridView1.DataSource = dataTable;
                            long sum = 0;
                            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                            {
                                sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);

                            }
                            txtTongTien.Text = sum.ToString();
                            txtTongSP.Text = dataGridView1.Rows.Count.ToString();
                            txtTongTien.ReadOnly = true;
                            txtTongSP.ReadOnly = true;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Lọc dữ liệu không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime dateS = dateTimePicker2.Value;
            DateTime dateE = dateTimePicker3.Value;
            DateTime dateStart = Convert.ToDateTime(dateS.ToString("dd/MM/yyyy"));
            DateTime dateEnd = Convert.ToDateTime(dateE.ToString("dd/MM/yyyy"));

            if (dateStart > dateEnd)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("FilterHoaDonByThoiGian", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@NgayBatDau", dateStart);
                        command.Parameters.AddWithValue("@NgayKetThuc", dateEnd);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            if (dataTable.Rows.Count == 0)
                            {
                                MessageBox.Show("Không có hóa đơn nào trong khoảng thời gian này !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            dataGridView1.DataSource = dataTable;
                            long sum = 0;
                            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                            {
                                sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
                            }
                            txtTongTien.Text = sum.ToString();
                            txtTongSP.Text = dataGridView1.Rows.Count.ToString();
                            txtTongTien.ReadOnly = true;
                            txtTongSP.ReadOnly = true;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Lọc dữ liệu không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //lấy mã hóa đơn
            int index = e.RowIndex;
            if (index < 0)
            {
                return;
            }
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            string maHD = selectedRow.Cells[0].Value.ToString();
            //lấy thông tin khách hàng từ mã hóa đơn
            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("GetHangHoaByMaHoaDon", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@MaHoaDon", maHD);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            txtMaKH.Text = dataTable.Rows[0][0].ToString();
                            txtTenKH.Text = dataTable.Rows[0][1].ToString();
                            txtSDT.Text = dataTable.Rows[0][2].ToString();
                            txtTongTienKH.Text = dataTable.Rows[0][3].ToString();
                        }
                    }
                    using (SqlCommand command = new SqlCommand("GetNhanVienHD", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@MaHoaDon", maHD);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            txtMaNV.Text = dataTable.Rows[0][0].ToString();
                            txtTenNV.Text = dataTable.Rows[0][1].ToString();
                            txtGT.Text = dataTable.Rows[0][2].ToString();
                            txtDC.Text = dataTable.Rows[0][3].ToString();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Lấy dữ liệu khách hàng không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FormQLHoaDon_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
        }
    }
}
