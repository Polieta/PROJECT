using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Nhom6_QLShopThoiTrang.Forms
{
    public partial class FormKhachHang : Form
    {
        //connect
        string ConnStr = Properties.Settings.Default.connStr;

        //Main
        public FormKhachHang()
        {
            InitializeComponent();
        }

        //Form load
        private void FormKhachHang_Load(object sender, EventArgs e)
        {
            LoadDataTo_DGV();
            dgv_KhachHang.AllowUserToAddRows = false;
        }


        //Sự kiện cho button xóa
        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (txt_MaKhachHang.Text == "" || txt_TenKH.Text == "" || txt_SDT.Text == "" || txt_DiaChi.Text == "")
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                //kiểm tra xem mã khách hàng có tồn tại không
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    string query = "SELECT * FROM KHACHHANG WHERE MaKH = @MaKH";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaKH", txt_MaKhachHang.Text);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Mã khách hàng không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                //xac nhan xoa
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
                using (SqlConnection connection = new SqlConnection(ConnStr))
                {
                    connection.Open();

                    // Gọi stored procedure
                    SqlCommand command = new SqlCommand("DeleteKhachHang", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    command.Parameters.AddWithValue("@MaKH", txt_MaKhachHang.Text); // Giá trị MaKH cần xóa

                    // Thực thi stored procedure
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataTo_DGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Sự kiện cho Button sửa
        private void btn_Sua_Click(object sender, EventArgs e)
        {
            if (txt_TenKH.Text == "" || txt_SDT.Text == "" || txt_DiaChi.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                //kiêm tra xem mã khách hàng có tồn tại không
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    string query = "SELECT * FROM KHACHHANG WHERE MaKH = @MaKH";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaKH", txt_MaKhachHang.Text);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Mã khách hàng không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                using (SqlConnection connection = new SqlConnection(ConnStr))
                {
                    connection.Open();

                    // Gọi stored procedure
                    SqlCommand command = new SqlCommand("UpdateKhachHang", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Thêm các tham số cho stored procedure
                    command.Parameters.AddWithValue("@MaKH", txt_MaKhachHang.Text); // Giá trị MaKH cần cập nhật
                    command.Parameters.AddWithValue("@TenKH", txt_TenKH.Text);
                    command.Parameters.AddWithValue("@SoDienThoai", txt_SDT.Text); // Số điện thoại
                    command.Parameters.AddWithValue("@DiaChi", txt_DiaChi.Text);
                    command.Parameters.AddWithValue("@TongTienMuaHang", 0); // Tổng tiền mua hàng

                    // Thực thi stored procedure
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataTo_DGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Sự kiện cho button Click
        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            if (txt_TimTheoTenKH.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng cần tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnStr))
                {
                    connection.Open();

                    // Gọi stored procedure
                    SqlCommand command = new SqlCommand("SearchKhachHang", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    command.Parameters.AddWithValue("@TenKH", txt_TimTheoTenKH.Text.Trim());

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgv_KhachHang.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        //Sự kiện cho button BackSearch nút trở lại khi tìm kiếm
        private void btn_BackSearch_Click(object sender, EventArgs e)
        {
            LoadDataTo_DGV();
            txt_TimTheoTenKH.Text = string.Empty;
        }

        //Sự kiện cho button hủy
        private void btn_Huy_Click(object sender, EventArgs e)
        {
            txt_MaKhachHang.Text = "";
            txt_TenKH.Text = "";
            txt_SDT.Text = "";
            txt_DiaChi.Text = "";
            txt_TimTheoTenKH.Text = "";
            LoadDataTo_DGV();
        }

        //Load dữ liệu lên datagridview
        private void LoadDataTo_DGV()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnStr))
                {
                    connection.Open();

                    // Gọi stored procedure
                    SqlCommand command = new SqlCommand("GetKhachHangData", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgv_KhachHang.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data KHÁCH HÀNG: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        //Hàm cập nhật thông tin khách hàng
        private void Update_Info_Customer()
        {
            // Kiểm tra xem có dòng được chọn không
            if (dgv_KhachHang.SelectedRows.Count > 0)
            {
                // Lấy dữ liệu từ các ô của dòng được chọn
                string maKH = txt_MaKhachHang.Text;
                string tenKH = txt_TenKH.Text;
                string soDienThoai = txt_SDT.Text;
                string diaChi = txt_DiaChi.Text;

                try
                {
                    using (SqlConnection conn = new SqlConnection(ConnStr))
                    {
                        conn.Open();

                        // Sử dụng câu lệnh UPDATE để cập nhật thông tin khách hàng
                        string query = "UPDATE KHACHHANG SET TenKH = @TenKH, SoDienThoai = @SoDienThoai, DiaChi = @DiaChi WHERE MaKH = @MaKH";
                        SqlCommand cmd = new SqlCommand(query, conn);

                        // Thêm các tham số để tránh SQL Injection và định giá trị
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                        cmd.Parameters.AddWithValue("@TenKH", tenKH);
                        cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                        cmd.Parameters.AddWithValue("@DiaChi", diaChi);

                        // Thực hiện câu lệnh UPDATE
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Sau khi cập nhật thành công, cần làm mới dữ liệu trên DataGridView
                            LoadDataTo_DGV();
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật thông tin không thành công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Hàm xóa khách hàng
        private void Delete_Customer()
        {
            // Kiểm tra xem có dòng được chọn không
            if (dgv_KhachHang.SelectedRows.Count > 0)
            {
                // Lấy mã khách hàng từ cột MaKH của dòng được chọn
                string maKH = dgv_KhachHang.SelectedRows[0].Cells["MaKH"].Value.ToString();

                try
                {
                    using (SqlConnection conn = new SqlConnection(ConnStr))
                    {
                        conn.Open();

                        // Sử dụng câu lệnh DELETE để xóa thông tin khách hàng
                        string query = "DELETE FROM KHACHHANG WHERE MaKH = @MaKH";
                        SqlCommand cmd = new SqlCommand(query, conn);

                        // Thêm tham số để tránh SQL Injection và định giá trị
                        cmd.Parameters.AddWithValue("@MaKH", maKH);

                        // Thực hiện câu lệnh DELETE
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa thông tin thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Sau khi xóa thành công, cần làm mới dữ liệu trên DataGridView
                            LoadDataTo_DGV();
                        }
                        else
                        {
                            MessageBox.Show("Xóa thông tin không thành công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgv_KhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Lấy dữ liệu từ datagridview đổ lên textbox
            int i;
            i = dgv_KhachHang.CurrentRow.Index;
            txt_MaKhachHang.Text = dgv_KhachHang.Rows[i].Cells[0].Value.ToString();
            txt_TenKH.Text = dgv_KhachHang.Rows[i].Cells[1].Value.ToString();
            txt_SDT.Text = dgv_KhachHang.Rows[i].Cells[2].Value.ToString();
            txt_DiaChi.Text = dgv_KhachHang.Rows[i].Cells[3].Value.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_TenKH.Text == "" || txt_SDT.Text == "" || txt_DiaChi.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    string query = "SELECT * FROM KHACHHANG WHERE MaKH = @MaKH";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaKH", txt_MaKhachHang.Text);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        MessageBox.Show("Mã khách hàng đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                using (SqlConnection connection = new SqlConnection(ConnStr))
                {
                    connection.Open();

                    // Gọi stored procedure
                    SqlCommand command = new SqlCommand("InsertKhachHang", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Thêm các tham số cho stored procedure
                    command.Parameters.AddWithValue("@TenKH", txt_TenKH.Text);
                    command.Parameters.AddWithValue("@SoDienThoai", txt_SDT.Text); // Số điện thoại
                    command.Parameters.AddWithValue("@DiaChi", txt_DiaChi.Text);
                    command.Parameters.AddWithValue("@TongTienMuaHang", 0); // Tổng tiền mua hàng

                    // Thực thi stored procedure
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataTo_DGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dgv_KhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
