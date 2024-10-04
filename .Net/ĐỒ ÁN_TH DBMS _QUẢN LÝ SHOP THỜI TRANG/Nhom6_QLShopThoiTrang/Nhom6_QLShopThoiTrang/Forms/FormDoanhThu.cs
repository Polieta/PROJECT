using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Nhom6_QLShopThoiTrang.Forms
{
    public partial class FormDoanhThu : Form
    {
        string ConnStr = Properties.Settings.Default.connStr;
        public FormDoanhThu()
        {
            InitializeComponent();
        }

        private void btn_HomNay_Click(object sender, System.EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            DateTime date = Convert.ToDateTime(currentDate.ToString("dd/MM/yyyy"));
            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                connection.Open();

                try
                {
                    // Tạo đối tượng SqlCommand để gọi Stored Procedure
                    using (SqlCommand command = new SqlCommand("GetDoanhThuDate", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@NgayDT", date);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            //kiểm tra xem có dữ liệu hay không
                            if (dataTable.Rows.Count == 0)
                            {
                                MessageBox.Show("Không có sản phẩm nào được bán ra trong ngày hôm nay !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            dataDoanhThu.DataSource = dataTable;
                            //Tổng doanh thu trong datagridview
                            int sum = 0;
                            for (int i = 0; i < dataDoanhThu.Rows.Count; ++i)
                            {
                                sum += Convert.ToInt32(dataDoanhThu.Rows[i].Cells[3].Value);
                            }
                            txt_TongDoanhThu.Text = sum.ToString();

                            txt_SoLuongDonHang.Text = dataDoanhThu.Rows.Count.ToString();

                            //tổng loi nhuan
                            int sumLN = 0;
                            for (int i = 0; i < dataDoanhThu.Rows.Count; ++i)
                            {
                                sumLN += Convert.ToInt32(dataDoanhThu.Rows[i].Cells[4].Value);
                            }
                            txt_TongLoiNhuan.Text = sumLN.ToString();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Tải doanh thu không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            DateTime dateS = dateTimePicker1.Value;
            DateTime dateE = dateTimePicker2.Value;
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
                    using (SqlCommand command = new SqlCommand("GetDoanhThuByDate", connection))
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
                                MessageBox.Show("Không có sản phẩm nào được bán ra trong ngày hôm nay !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            dataDoanhThu.DataSource = dataTable;
                            //Tổng doanh thu trong datagridview
                            int sum = 0;
                            for (int i = 0; i < dataDoanhThu.Rows.Count; ++i)
                            {
                                sum += Convert.ToInt32(dataDoanhThu.Rows[i].Cells[3].Value);
                            }
                            txt_TongDoanhThu.Text = sum.ToString();

                            txt_SoLuongDonHang.Text = dataDoanhThu.Rows.Count.ToString();

                            //tổng loi nhuan
                            int sumLN = 0;
                            for (int i = 0; i < dataDoanhThu.Rows.Count; ++i)
                            {
                                sumLN += Convert.ToInt32(dataDoanhThu.Rows[i].Cells[4].Value);
                            }
                            txt_TongLoiNhuan.Text = sumLN.ToString();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Tải doanh thu không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataDoanhThu.DataSource = null;
            txt_TongDoanhThu.Text = "";
            txt_SoLuongDonHang.Text = "";
            txt_TongLoiNhuan.Text = "";
        }
    }
}
