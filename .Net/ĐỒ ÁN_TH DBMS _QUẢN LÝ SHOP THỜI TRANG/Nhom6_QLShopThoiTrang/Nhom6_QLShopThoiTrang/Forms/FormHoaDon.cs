using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Nhom6_QLShopThoiTrang.Forms
{
    public partial class FormHoaDon : Form
    {
        //connect
        string ConnStr = Properties.Settings.Default.connStr;


        private DataTable temporaryDataTable;
        public FormHoaDon()
        {
            InitializeComponent();

        }

        private void btnIDNN_Click(object sender, EventArgs e)
        {
            Random rd = new Random();
            int id = rd.Next(10000, 999999);
            txt_HD.Text = id.ToString();
            txt_HD.ForeColor = Color.Green;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Kiểm tra dữ liệu nhập vào mã hóa đơn, khách hàng, nhân viên.
            if (txt_HD.Text == "" || cbbKH.Text == "" || cbbNV.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                //Kiểm tra hóa đơn có tồn tại hay không
                try
                {
                    string storedProcedure = "GetHoaDonData";
                    using (SqlConnection connection = new SqlConnection(ConnStr))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(storedProcedure, connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MaHD", txt_HD.Text);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            MessageBox.Show("Hóa đơn đã tồn tại - Không thể thêm háo đơn này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Lỗi kết nối !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    //Thêm hóa đơn
                    string storedProcedure = "InsertHoaDon";
                    using (SqlConnection connection = new SqlConnection(ConnStr))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(storedProcedure, connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MaHD", txt_HD.Text);
                        command.Parameters.AddWithValue("@MaKH", cbbKH.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@MaNV", cbbNV.SelectedValue.ToString());

                        command.ExecuteNonQuery();
                        MessageBox.Show("Thêm hóa đơn thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtMAHD.Text = txt_HD.Text;
                        txt_HD.ReadOnly = true;
                        txtMAHD.ReadOnly = true;

                        string storedProcedureChiTietHoaDon = "GetChiTietHoaDonData";
                        SqlCommand commandChiTietHoaDon = new SqlCommand(storedProcedureChiTietHoaDon, connection);
                        commandChiTietHoaDon.CommandType = CommandType.StoredProcedure;
                        commandChiTietHoaDon.Parameters.AddWithValue("@MaHD", txtMAHD.Text);
                        SqlDataAdapter adapterChiTietHoaDon = new SqlDataAdapter(commandChiTietHoaDon);
                        DataTable dataTableChiTietHoaDon = new DataTable();
                        adapterChiTietHoaDon.Fill(dataTableChiTietHoaDon);
                        dataGridView1.DataSource = dataTableChiTietHoaDon;
                    }
                }
                catch
                {
                    MessageBox.Show("Thêm hóa đơn thất bại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            //kiêm tra dữ liệu nhập vào sản phẩm và số lượng sản phẩm
            if (cbbSP.Text == "" || txtSL.Text == "" || txtMAHD.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin chi tiết hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                string storedProcedureSanPham = "GetSanPhamSoLuongTon";
                using (SqlConnection connection = new SqlConnection(ConnStr))
                {
                    connection.Open();

                    SqlCommand commandSanPham = new SqlCommand(storedProcedureSanPham, connection);
                    commandSanPham.CommandType = CommandType.StoredProcedure;
                    commandSanPham.Parameters.AddWithValue("@MaSP", cbbSP.SelectedValue.ToString());
                    SqlDataAdapter adapterSanPham = new SqlDataAdapter(commandSanPham);
                    DataTable dataTableSanPham = new DataTable();
                    adapterSanPham.Fill(dataTableSanPham);
                    if (Convert.ToInt32(dataTableSanPham.Rows[0]["SoLuongTon"]) < Convert.ToInt32(txtSL.Text))
                    {
                        MessageBox.Show("Số lượng sản phẩm không đủ !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                try
                {
                    //Thêm chi tiết hóa đơn
                    string storedProcedure = "InsertChiTietHoaDon";
                    using (SqlConnection connection = new SqlConnection(ConnStr))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(storedProcedure, connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MaHD", txtMAHD.Text);
                        command.Parameters.AddWithValue("@MaSP", cbbSP.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@SoLuong", txtSL.Text);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Thêm chi tiết hóa đơn thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtMAHD.Text = txt_HD.Text;
                        txt_HD.ReadOnly = true;

                        //Load lại datagridview
                        string storedProcedureChiTietHoaDon = "GetChiTietHoaDonData";
                        SqlCommand commandChiTietHoaDon = new SqlCommand(storedProcedureChiTietHoaDon, connection);
                        commandChiTietHoaDon.CommandType = CommandType.StoredProcedure;
                        commandChiTietHoaDon.Parameters.AddWithValue("@MaHD", txtMAHD.Text);
                        SqlDataAdapter adapterChiTietHoaDon = new SqlDataAdapter(commandChiTietHoaDon);
                        DataTable dataTableChiTietHoaDon = new DataTable();
                        adapterChiTietHoaDon.Fill(dataTableChiTietHoaDon);
                        dataGridView1.DataSource = dataTableChiTietHoaDon;


                        //Load lại hóa đơn lấy tổng tiền hóa đơn và tổng số lượng sản phẩm gán vào textbox
                        string storedProcedureHoaDon = "GetHoaDonData";
                        SqlCommand commandHoaDon = new SqlCommand(storedProcedureHoaDon, connection);
                        commandHoaDon.CommandType = CommandType.StoredProcedure;
                        commandHoaDon.Parameters.AddWithValue("@MaHD", txtMAHD.Text);
                        SqlDataAdapter adapterHoaDon = new SqlDataAdapter(commandHoaDon);
                        DataTable dataTableHoaDon = new DataTable();
                        adapterHoaDon.Fill(dataTableHoaDon);
                        txtTongTienTT.Text = dataTableHoaDon.Rows[0]["TongTien"].ToString();
                        txtTongSL.Text = dataTableHoaDon.Rows[0]["TongSoLuong"].ToString();
                    }
                }
                catch
                {
                    MessageBox.Show("Thêm chi tiết hóa đơn thất bại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //kiêm tra dữ liệu nhập vào sản phẩm và số lượng sản phẩm
            if (cbbSP.Text == "" || txtSL.Text == "" || txtMAHD.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin chi tiết hóa đơn cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                try
                {
                    //Xóa chi tiết hóa đơn
                    string storedProcedure = "DeleteChiTietHoaDon";
                    using (SqlConnection connection = new SqlConnection(ConnStr))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(storedProcedure, connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MaHD", txtMAHD.Text);
                        command.Parameters.AddWithValue("@MaSP", cbbSP.SelectedValue.ToString());

                        command.ExecuteNonQuery();
                        MessageBox.Show("Xóa chi tiết hóa đơn thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtMAHD.Text = txt_HD.Text;
                        txt_HD.ReadOnly = true;

                        //Load lại datagridview
                        string storedProcedureChiTietHoaDon = "GetChiTietHoaDonData";
                        SqlCommand commandChiTietHoaDon = new SqlCommand(storedProcedureChiTietHoaDon, connection);
                        commandChiTietHoaDon.CommandType = CommandType.StoredProcedure;
                        commandChiTietHoaDon.Parameters.AddWithValue("@MaHD", txtMAHD.Text);
                        SqlDataAdapter adapterChiTietHoaDon = new SqlDataAdapter(commandChiTietHoaDon);
                        DataTable dataTableChiTietHoaDon = new DataTable();
                        adapterChiTietHoaDon.Fill(dataTableChiTietHoaDon);
                        dataGridView1.DataSource = dataTableChiTietHoaDon;

                        txt_HD.ReadOnly = true;
                        txtSL.Text = "";
                        cbbSP.Text = "";


                        //Load lại hóa đơn lấy tổng tiền hóa đơn và tổng số lượng sản phẩm gán vào textbox
                        string storedProcedureHoaDon = "GetHoaDonData";
                        SqlCommand commandHoaDon = new SqlCommand(storedProcedureHoaDon, connection);
                        commandHoaDon.CommandType = CommandType.StoredProcedure;
                        commandHoaDon.Parameters.AddWithValue("@MaHD", txtMAHD.Text);
                        SqlDataAdapter adapterHoaDon = new SqlDataAdapter(commandHoaDon);
                        DataTable dataTableHoaDon = new DataTable();
                        adapterHoaDon.Fill(dataTableHoaDon);
                        //kiểm tra hóa đơn có chi tiết hóa đơn hay không
                        if (dataTableHoaDon.Rows.Count == 0)
                        {
                            txtTongTienTT.Text = "";
                            txtTongSL.Text = "";
                        }
                        else
                        {
                            txtTongTienTT.Text = dataTableHoaDon.Rows[0]["TongTien"].ToString();
                            txtTongSL.Text = dataTableHoaDon.Rows[0]["TongSoLuong"].ToString();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Xóa chi tiết hóa đơn thất bại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //kiêm tra dữ liệu nhập vào sản phẩm và số lượng sản phẩm
            if (cbbSP.Text == "" || txtSL.Text == "" || txtMAHD.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin chi tiết hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                try
                {
                    //Sửa chi tiết hóa đơn
                    string storedProcedure = "UpdateChiTietHoaDon";
                    using (SqlConnection connection = new SqlConnection(ConnStr))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(storedProcedure, connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MaHD", txtMAHD.Text);
                        command.Parameters.AddWithValue("@MaSP", cbbSP.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@SoLuong", txtSL.Text);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Sửa chi tiết hóa đơn thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtMAHD.Text = txt_HD.Text;
                        txt_HD.ReadOnly = true;

                        //Load lại datagridview
                        string storedProcedureChiTietHoaDon = "GetChiTietHoaDonData";
                        SqlCommand commandChiTietHoaDon = new SqlCommand(storedProcedureChiTietHoaDon, connection);
                        commandChiTietHoaDon.CommandType = CommandType.StoredProcedure;
                        commandChiTietHoaDon.Parameters.AddWithValue("@MaHD", txtMAHD.Text);
                        SqlDataAdapter adapterChiTietHoaDon = new SqlDataAdapter(commandChiTietHoaDon);
                        DataTable dataTableChiTietHoaDon = new DataTable();
                        adapterChiTietHoaDon.Fill(dataTableChiTietHoaDon);
                        dataGridView1.DataSource = dataTableChiTietHoaDon;



                        //Load lại hóa đơn lấy tổng tiền hóa đơn và tổng số lượng sản phẩm gán vào textbox
                        string storedProcedureHoaDon = "GetHoaDonData";
                        SqlCommand commandHoaDon = new SqlCommand(storedProcedureHoaDon, connection);
                        commandHoaDon.CommandType = CommandType.StoredProcedure;
                        commandHoaDon.Parameters.AddWithValue("@MaHD", txtMAHD.Text);
                        SqlDataAdapter adapterHoaDon = new SqlDataAdapter(commandHoaDon);
                        DataTable dataTableHoaDon = new DataTable();
                        adapterHoaDon.Fill(dataTableHoaDon);
                        txtTongTienTT.Text = dataTableHoaDon.Rows[0]["TongTien"].ToString();
                        txtTongSL.Text = dataTableHoaDon.Rows[0]["TongSoLuong"].ToString();
                    }

                }
                catch
                {
                    MessageBox.Show("Sửa chi tiết hóa đơn thất bại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FormHoaDon_Load_1(object sender, EventArgs e)
        {


            string storedProcedureSanPhamHD = "GetSanPhamDataHD";
            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                connection.Open();

                SqlCommand commandSanPhamHD = new SqlCommand(storedProcedureSanPhamHD, connection);
                commandSanPhamHD.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapterSanPhamHD = new SqlDataAdapter(commandSanPhamHD);
                DataTable dataTableSanPham = new DataTable();
                adapterSanPhamHD.Fill(dataTableSanPham);

                cbbSP.DataSource = dataTableSanPham;
                cbbSP.DisplayMember = "TenSP"; // Column name for display
                cbbSP.ValueMember = "MaSP";    // Column name for value
            }



            string storedProcedureKhachHangHD = "GetKhachHangDataHD";
            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                connection.Open();

                SqlCommand commandKhachHangHD = new SqlCommand(storedProcedureKhachHangHD, connection);
                commandKhachHangHD.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapterKhachHangHD = new SqlDataAdapter(commandKhachHangHD);
                DataTable dataTableKhachHang = new DataTable();
                adapterKhachHangHD.Fill(dataTableKhachHang);

                cbbKH.DataSource = dataTableKhachHang;
                cbbKH.DisplayMember = "TenKH"; // Column name for display
                cbbKH.ValueMember = "MaKH";    // Column name for value
            }


            string storedProcedureNhanVienHD = "GetNhanVienDataHD";
            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                connection.Open();

                SqlCommand commandNhanVienHD = new SqlCommand(storedProcedureNhanVienHD, connection);
                commandNhanVienHD.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapterNhanVienHD = new SqlDataAdapter(commandNhanVienHD);
                DataTable dataTableNhanVien = new DataTable();
                adapterNhanVienHD.Fill(dataTableNhanVien);

                cbbNV.DataSource = dataTableNhanVien;
                cbbNV.DisplayMember = "HoTenNV"; // Column name for display
                cbbNV.ValueMember = "MaNV";    // Column name for value
            }

            txtTongTienTT.ReadOnly = true;
            txtTongSL.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void btnXOA_Click(object sender, EventArgs e)
        {
            //kiêm tra dữ liệu nhập vào mã hóa đơn
            if (txt_HD.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin hóa đơn cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                //kiểm tra hóa đơn có tồn tại hay không
                string storedProcedureHoaDon = "GetHoaDonData";
                using (SqlConnection connection = new SqlConnection(ConnStr))
                {
                    connection.Open();

                    SqlCommand commandHoaDon = new SqlCommand(storedProcedureHoaDon, connection);
                    commandHoaDon.CommandType = CommandType.StoredProcedure;
                    commandHoaDon.Parameters.AddWithValue("@MaHD", txt_HD.Text);
                    SqlDataAdapter adapterHoaDon = new SqlDataAdapter(commandHoaDon);
                    DataTable dataTableHoaDon = new DataTable();
                    adapterHoaDon.Fill(dataTableHoaDon);
                    if (dataTableHoaDon.Rows.Count == 0)
                    {
                        MessageBox.Show("Hóa đơn không tồn tại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                try
                {
                    //Xóa hóa đơn
                    string storedProcedure = "DeleteHoaDon";
                    using (SqlConnection connection = new SqlConnection(ConnStr))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(storedProcedure, connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MaHD", txt_HD.Text);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Xóa hóa đơn thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_HD.Text = "";
                        txtMAHD.Text = "";
                        txtTongTienTT.Text = "";
                        txtTongSL.Text = "";
                        txt_HD.ReadOnly = false;
                        txtMAHD.ReadOnly = false;
                        dataGridView1.DataSource = null;
                    }
                }
                catch
                {
                    MessageBox.Show("Xóa hóa đơn thất bại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txt_HD_TextChanged(object sender, EventArgs e)
        {
            //Kiêm tra mã hóa đơn có tồn tại hay không
            if (txt_HD.Text == "")
            {
                dataGridView1.DataSource = null;
                txtMAHD.Text = "";
                txtTongTienTT.Text = "";
                txtTongSL.Text = "";
                txt_HD.ReadOnly = false;
                txtMAHD.ReadOnly = false;
                return;
            }
            else
            {
                try
                {
                    //Load lại hóa đơn lấy tổng tiền hóa đơn và tổng số lượng sản phẩm gán vào textbox và datagridview
                    string storedProcedureHoaDon = "GetHoaDonData";
                    using (SqlConnection connection = new SqlConnection(ConnStr))
                    {
                        connection.Open();

                        SqlCommand commandHoaDon = new SqlCommand(storedProcedureHoaDon, connection);
                        commandHoaDon.CommandType = CommandType.StoredProcedure;
                        commandHoaDon.Parameters.AddWithValue("@MaHD", txt_HD.Text);
                        SqlDataAdapter adapterHoaDon = new SqlDataAdapter(commandHoaDon);
                        DataTable dataTableHoaDon = new DataTable();
                        adapterHoaDon.Fill(dataTableHoaDon);
                        txtMAHD.Text = txt_HD.Text;
                        txtTongTienTT.Text = dataTableHoaDon.Rows[0]["TongTien"].ToString();
                        txtTongSL.Text = dataTableHoaDon.Rows[0]["TongSoLuong"].ToString();

                        string storedProcedureChiTietHoaDon = "GetChiTietHoaDonData";
                        SqlCommand commandChiTietHoaDon = new SqlCommand(storedProcedureChiTietHoaDon, connection);
                        commandChiTietHoaDon.CommandType = CommandType.StoredProcedure;
                        commandChiTietHoaDon.Parameters.AddWithValue("@MaHD", txtMAHD.Text);
                        SqlDataAdapter adapterChiTietHoaDon = new SqlDataAdapter(commandChiTietHoaDon);
                        DataTable dataTableChiTietHoaDon = new DataTable();
                        adapterChiTietHoaDon.Fill(dataTableChiTietHoaDon);
                        dataGridView1.DataSource = dataTableChiTietHoaDon;

                        txtMAHD.Text = txt_HD.Text;
                        txtMAHD.ReadOnly = true;
                    }
                }
                catch
                {
                    dataGridView1.DataSource = null;
                    txtMAHD.Text = "";
                    txtTongTienTT.Text = "";
                    txtTongSL.Text = "";
                    txt_HD.ReadOnly = false;
                    txtMAHD.ReadOnly = false;
                    return;
                }
            }

        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //Lấy dữ liệu từ datagridview gán vào combobox sản phẩm và textbox số lượng sản phẩm
            if (dataGridView1.Rows.Count > 0)
            {
                txtMAHD.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                cbbSP.SelectedValue = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtSL.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //sửa hóa đơn
            if (txt_HD.Text == "" || cbbKH.Text == "" || cbbNV.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin hóa đơn cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                //kiêm tra hóa đơn có tồn tại hay không
                string storedProcedureHoaDon = "GetHoaDonData";
                using (SqlConnection connection = new SqlConnection(ConnStr))
                {
                    connection.Open();

                    SqlCommand commandHoaDon = new SqlCommand(storedProcedureHoaDon, connection);
                    commandHoaDon.CommandType = CommandType.StoredProcedure;
                    commandHoaDon.Parameters.AddWithValue("@MaHD", txt_HD.Text);
                    SqlDataAdapter adapterHoaDon = new SqlDataAdapter(commandHoaDon);
                    DataTable dataTableHoaDon = new DataTable();
                    adapterHoaDon.Fill(dataTableHoaDon);
                    if (dataTableHoaDon.Rows.Count == 0)
                    {
                        MessageBox.Show("Hóa đơn không tồn tại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                try
                {
                    //Sửa hóa đơn
                    string storedProcedure = "UpdateHoaDon";
                    using (SqlConnection connection = new SqlConnection(ConnStr))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(storedProcedure, connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MaHD", txt_HD.Text);
                        command.Parameters.AddWithValue("@MaKH", cbbKH.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@MaNV", cbbNV.SelectedValue.ToString());

                        command.ExecuteNonQuery();
                        MessageBox.Show("Sửa hóa đơn thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_HD.ReadOnly = true;
                        txtMAHD.ReadOnly = true;

                        //Load lại datagridview
                        string storedProcedureChiTietHoaDon = "GetChiTietHoaDonData";
                        SqlCommand commandChiTietHoaDon = new SqlCommand(storedProcedureChiTietHoaDon, connection);
                        commandChiTietHoaDon.CommandType = CommandType.StoredProcedure;
                        commandChiTietHoaDon.Parameters.AddWithValue("@MaHD", txtMAHD.Text);
                        SqlDataAdapter adapterChiTietHoaDon = new SqlDataAdapter(commandChiTietHoaDon);
                        DataTable dataTableChiTietHoaDon = new DataTable();
                        adapterChiTietHoaDon.Fill(dataTableChiTietHoaDon);
                        dataGridView1.DataSource = dataTableChiTietHoaDon;


                        //Load lại hóa đơn lấy tổng tiền hóa đơn và tổng số lượng sản phẩm gán vào textbox
                        string storedProcedureHoaDon1 = "GetHoaDonData";
                        SqlCommand commandHoaDon1 = new SqlCommand(storedProcedureHoaDon1, connection);
                        commandHoaDon1.CommandType = CommandType.StoredProcedure;
                        commandHoaDon1.Parameters.AddWithValue("@MaHD", txtMAHD.Text);
                        SqlDataAdapter adapterHoaDon1 = new SqlDataAdapter(commandHoaDon1);
                        DataTable dataTableHoaDon1 = new DataTable();
                        adapterHoaDon1.Fill(dataTableHoaDon1);
                        txtTongTienTT.Text = dataTableHoaDon1.Rows[0]["TongTien"].ToString();
                        txtTongSL.Text = dataTableHoaDon1.Rows[0]["TongSoLuong"].ToString();

                    }
                }
                catch
                {
                    MessageBox.Show("Sửa hóa đơn thất bại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
