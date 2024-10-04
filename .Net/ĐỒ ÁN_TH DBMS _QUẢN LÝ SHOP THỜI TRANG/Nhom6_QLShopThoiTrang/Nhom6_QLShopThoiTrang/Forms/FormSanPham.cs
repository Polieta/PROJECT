using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO; //sử dụng lớp Path
using System.Windows.Forms;

namespace Nhom6_QLShopThoiTrang.Forms
{
    public partial class FormSanPham : Form
    {
        string ConnStr = Properties.Settings.Default.connStr;
        public FormSanPham()
        {
            InitializeComponent();
        }
        private void FormSanPham_Load(object sender, EventArgs e)
        {
            LoadDataToCombobox();
            LoadDataToDataGridView();
            DefaultData();
            dgv_SanPham.AllowUserToAddRows = false;
        }
        public void hideButton()
        {
            btn_Xoa.ForeColor = Color.DarkGray;
            btn_Xoa.BackColor = Color.DarkGray;
        }

        public void DefaultData()
        {
            txt_MaSP.Text = "SP00";
            txt_DonGia.Text = "100000";
            txt_MoTa.Text = "Không";
        }
        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_TenSP.Text) || string.IsNullOrEmpty(txt_DonGia.Text) ||
                    string.IsNullOrEmpty(txt_SoLuong.Text) || string.IsNullOrEmpty(txt_Sale.Text) ||
                    string.IsNullOrEmpty(txt_MauSac.Text) || string.IsNullOrEmpty(txt_MoTa.Text) ||
                    cb_LoaiSP.SelectedValue.ToString() == "" || cb_Size.SelectedValue.ToString() == "" || cb_XuatSu.SelectedValue.ToString() == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //kiểm tra xem có chọn sản phẩm để xóa hay không
            if (dgv_SanPham.SelectedRows.Count < 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                //Lấy mã sản phẩm từ cột MaSP của dòng được chọn
                string maSP = dgv_SanPham.CurrentRow.Cells[0].Value.ToString();

                // Prompt the user for confirmation
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này?", "Xóa",
                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Delete the row from the database
                    DeleteProduct(maSP);

                    //xóa các textbox và combobox
                    txt_MaSP.Text = string.Empty;
                    txt_TenSP.Text = string.Empty;
                    txt_DonGia.Text = string.Empty;
                    txt_SoLuong.Text = string.Empty;
                    cb_XuatSu.SelectedIndex = -1;
                    txt_Sale.Text = string.Empty;
                    cb_Size.SelectedIndex = -1;
                    txt_MauSac.Text = string.Empty;
                    txt_MoTa.Text = string.Empty;
                    cb_LoaiSP.SelectedIndex = -1;
                    pb_AnhBia.Image = null;

                    // Refresh the DataGridView
                    LoadDataToDataGridView();
                }
            }
        }

        private void btn_CapNhat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_TenSP.Text) || string.IsNullOrEmpty(txt_DonGia.Text) ||
                               string.IsNullOrEmpty(txt_SoLuong.Text) || string.IsNullOrEmpty(txt_Sale.Text) ||
                                              string.IsNullOrEmpty(txt_MauSac.Text) || string.IsNullOrEmpty(txt_MoTa.Text) ||
                                                             cb_LoaiSP.SelectedValue.ToString() == "" || cb_Size.SelectedValue.ToString() == "" || cb_XuatSu.SelectedValue.ToString() == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                // Kiểm tra giá nhập phải bé hơn giá bán
                float gia, sale, gianhap;
                if (!float.TryParse(txt_DonGia.Text, out gia) || !float.TryParse(txt_Sale.Text, out sale) || !float.TryParse(txtGiaNhap.Text, out gianhap))
                {
                    MessageBox.Show("Giá và Sale phải là số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                double gianhap1 = Convert.ToDouble(txtGiaNhap.Text);
                double gia1 = Convert.ToDouble(txt_DonGia.Text);

                if (gianhap1 > gia1)
                {
                    MessageBox.Show("Giá nhập phải bé hơn (<=) giá bán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (SqlConnection connection = new SqlConnection(ConnStr))
                {
                    connection.Open();

                    // Gọi stored procedure
                    SqlCommand command = new SqlCommand("UpdateSanPham", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Thêm các tham số cho stored procedure
                    command.Parameters.AddWithValue("@MaSP", txt_MaSP.Text);
                    command.Parameters.AddWithValue("@TenSP", txt_TenSP.Text);
                    command.Parameters.AddWithValue("@MaLoai", cb_LoaiSP.SelectedValue.ToString());
                    command.Parameters.AddWithValue("@MaNhaCungCap", cb_XuatSu.SelectedValue.ToString());
                    //Lấy hình ảnh từ PictureBox và chuyển thành mảng byte
                    byte[] imageBytes = null;
                    if (pb_AnhBia.Image != null)
                    {
                        // Chuyển hình ảnh thành mảng byte
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pb_AnhBia.Image.Save(ms, pb_AnhBia.Image.RawFormat);
                            imageBytes = ms.ToArray();
                        }
                    }
                    command.Parameters.AddWithValue("@HinhAnhSP", imageBytes);

                    string moTa = txt_MoTa.Text;

                    int soLuongTon;
                    if (!int.TryParse(txt_SoLuong.Text, out soLuongTon))
                    {
                        MessageBox.Show("Số lượng tồn phải là số nguyên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    command.Parameters.AddWithValue("@GiaNhap", gianhap1); // Giá trị Gia
                    command.Parameters.AddWithValue("@Gia", gia); // Giá trị Gia
                    command.Parameters.AddWithValue("@Sale", sale); // Giá trị Sale
                    command.Parameters.AddWithValue("@MoTa", txt_MoTa.Text);
                    command.Parameters.AddWithValue("@SoLuongTon", soLuongTon); // Giá trị SoLuongTon
                    command.Parameters.AddWithValue("@MAKT", cb_Size.SelectedValue.ToString());
                    command.Parameters.AddWithValue("@MauSac", txt_MauSac.Text);

                    // Thực thi stored procedure
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadDataToDataGridView();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void btn_huy_Click(object sender, EventArgs e)
        {
            pb_AnhBia.Image = null;
            txt_MaSP.Text = string.Empty;
            txt_TenSP.Text = string.Empty;
            txt_DonGia.Text = string.Empty;
            txt_SoLuong.Text = string.Empty;
            cb_XuatSu.SelectedIndex = -1;
            txt_Sale.Text = string.Empty;
            cb_Size.SelectedIndex = -1;
            txt_MauSac.Text = string.Empty;
            txt_MoTa.Text = string.Empty;
            cb_LoaiSP.SelectedIndex = -1;
            LoadDataToDataGridView();
        }

        private void btn_ChonAnh_Click(object sender, EventArgs e)
        {
            //Load ảnh từ file lên pictureBox
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFile.Title = "Chọn ảnh cho sản phẩm";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pb_AnhBia.Image = new Bitmap(openFile.FileName);
            }

        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem đã nhập đủ thông tin hay chưa
                if (string.IsNullOrEmpty(txt_TenSP.Text) || string.IsNullOrEmpty(txt_DonGia.Text) ||
                    string.IsNullOrEmpty(txt_SoLuong.Text) || string.IsNullOrEmpty(txt_Sale.Text) ||
                    string.IsNullOrEmpty(txt_MauSac.Text) || string.IsNullOrEmpty(txt_MoTa.Text) ||
                    cb_LoaiSP.SelectedValue.ToString() == "" || cb_Size.SelectedValue.ToString() == "" || cb_XuatSu.SelectedValue.ToString() == "")
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra giá nhập phải bé hơn giá bán
                float gia, sale, gianhap;
                if (!float.TryParse(txt_DonGia.Text, out gia) || !float.TryParse(txt_Sale.Text, out sale) || !float.TryParse(txtGiaNhap.Text, out gianhap))
                {
                    MessageBox.Show("Giá và Sale phải là số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                double gianhap1 = Convert.ToDouble(txtGiaNhap.Text);
                double gia1 = Convert.ToDouble(txt_DonGia.Text);

                if (gianhap1 > gia1)
                {
                    MessageBox.Show("Giá nhập phải bé hơn (<=) giá bán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra trùng mã sản phẩm
                using (SqlConnection connection = new SqlConnection(ConnStr))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM SANPHAM WHERE MaSP = @MaSP";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaSP", txt_MaSP.Text);

                    int result = (int)command.ExecuteScalar();
                    if (result > 0)
                    {
                        MessageBox.Show("Mã sản phẩm đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                string MaSP = txt_MaSP.Text;
                // Lấy thông tin sản phẩm từ các trường giao diện
                string tenSP = txt_TenSP.Text;
                string maLoai = cb_LoaiSP.SelectedValue.ToString();
                string maNhaCungCap = cb_XuatSu.SelectedValue.ToString();

                // Lấy hình ảnh từ PictureBox và chuyển thành mảng byte
                byte[] imageBytes = null;
                if (pb_AnhBia.Image != null)
                {
                    // Chuyển hình ảnh thành mảng byte
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pb_AnhBia.Image.Save(ms, pb_AnhBia.Image.RawFormat);
                        imageBytes = ms.ToArray();
                    }
                }

                string moTa = txt_MoTa.Text;

                int soLuongTon;
                if (!int.TryParse(txt_SoLuong.Text, out soLuongTon))
                {
                    MessageBox.Show("Số lượng tồn phải là số nguyên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maKT = cb_Size.SelectedValue.ToString();
                string mauSac = txt_MauSac.Text;

                using (SqlConnection connection = new SqlConnection(ConnStr))
                {
                    connection.Open();

                    // Gọi stored procedure để thêm sản phẩm
                    SqlCommand command = new SqlCommand("InsertProduct", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số vào stored procedure
                    command.Parameters.AddWithValue("@MaSP", MaSP);
                    command.Parameters.AddWithValue("@TenSP", tenSP);
                    command.Parameters.AddWithValue("@MaLoai", maLoai);
                    command.Parameters.AddWithValue("@MaNhaCungCap", maNhaCungCap);
                    command.Parameters.AddWithValue("@HinhAnhSP", imageBytes);
                    command.Parameters.AddWithValue("@GiaNhap", gianhap1);
                    command.Parameters.AddWithValue("@Gia", gia);
                    command.Parameters.AddWithValue("@Sale", sale);
                    command.Parameters.AddWithValue("@MoTa", moTa);
                    command.Parameters.AddWithValue("@SoLuongTon", soLuongTon);
                    command.Parameters.AddWithValue("@MAKT", maKT);
                    command.Parameters.AddWithValue("@MauSac", mauSac);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Thêm sản phẩm thành công");
                    LoadDataToDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void LoadDataToCombobox()
        {
            // Load data for ComboBox Size (cb_Size) from the stored procedure
            string storedProcedureName = "GetKichThuocData";
            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                connection.Open();

                SqlCommand commandSizes = new SqlCommand(storedProcedureName, connection);
                commandSizes.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(commandSizes);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Set up data binding for cb_Size
                cb_Size.DataSource = dataTable;
                cb_Size.DisplayMember = "Size"; // Column name for display
                cb_Size.ValueMember = "MaKT";   // Column name for value

                cb_LocSize.DataSource = dataTable;
                cb_LocSize.DisplayMember = "Size"; // Column name for display
                cb_LocSize.ValueMember = "MaKT";   // Column name for value
            }



            // Load data for ComboBox Loai (cb_Loai) from the stored procedure
            string storedProcedureLoai = "GetLoaiData";
            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                connection.Open();

                SqlCommand commandLoais = new SqlCommand(storedProcedureLoai, connection);
                commandLoais.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapterLoai = new SqlDataAdapter(commandLoais);
                DataTable dataTableLoai = new DataTable();
                adapterLoai.Fill(dataTableLoai);

                // Set up data binding for cb_LoaiSP
                cb_LoaiSP.DataSource = dataTableLoai;
                cb_LoaiSP.DisplayMember = "TenLoai"; // Column name for display
                cb_LoaiSP.ValueMember = "MaLoai";    // Column name for value

                cb_LocLoaiSP.DataSource = dataTableLoai;
                cb_LocLoaiSP.DisplayMember = "TenLoai"; // Column name for display
                cb_LocLoaiSP.ValueMember = "MaLoai";    // Column name for value
            }



            // Load data for ComboBox Xuất Sứ (cb_XuatSu) from the stored procedure
            string storedProcedureXuatSu = "GetNhaCungCapData";
            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                connection.Open();

                SqlCommand commandXuatSu = new SqlCommand(storedProcedureXuatSu, connection);
                commandXuatSu.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapterXuatSu = new SqlDataAdapter(commandXuatSu);
                DataTable dataTableXuatSu = new DataTable();
                adapterXuatSu.Fill(dataTableXuatSu);

                // Set up data binding for cb_XuatSu
                cb_XuatSu.DataSource = dataTableXuatSu;
                cb_XuatSu.DisplayMember = "TenNhaCungCap"; // Column name for display
                cb_XuatSu.ValueMember = "MaNhaCungCap";    // Column name for value
            }

        }

        private void LoadDataToDataGridView()
        {
            try
            {
                // Tạo một DataSet
                DataSet dataSet = new DataSet();

                // Kết nối và lấy dữ liệu từ cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(ConnStr))
                {
                    connection.Open();

                    // Gọi stored procedure
                    SqlCommand command = new SqlCommand("GetSanPhamData", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dataSet, "SANPHAM");
                }

                // Thiết lập nguồn dữ liệu cho DataGridView
                dgv_SanPham.DataSource = dataSet.Tables["SANPHAM"];

                // Kiểm tra xem cột HinhAnhSP có tồn tại hay không
                if (dataSet.Tables["SANPHAM"].Columns.Contains("HinhAnhSP"))
                {
                    int columnIndex = dataSet.Tables["SANPHAM"].Columns["HinhAnhSP"].Ordinal;

                    // Hiển thị hình ảnh trong cột HinhAnhSP nếu có
                    foreach (DataGridViewRow row in dgv_SanPham.Rows)
                    {
                        if (!row.IsNewRow && row.Cells[columnIndex].Value != DBNull.Value)
                        {
                            byte[] imageData = (byte[])row.Cells[columnIndex].Value;
                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                Image image = Image.FromStream(ms);
                                row.Cells["HinhAnhSP"].Value = image;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dgv_SanPham_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgv_SanPham.Columns[e.ColumnIndex].Name == "HinhAnhSP" && e.Value != null && e.Value is byte[])
            {
                byte[] imageData = (byte[])e.Value;
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    Image image = Image.FromStream(ms);
                    int desiredWidth = 70;
                    int desiredHeight = 70;

                    // Tạo bản sao thu nhỏ của ảnh gốc
                    Image resizedImage = new Bitmap(desiredWidth, desiredHeight);
                    using (Graphics g = Graphics.FromImage(resizedImage))
                    {
                        g.DrawImage(image, 0, 0, desiredWidth, desiredHeight);
                    }
                    e.Value = resizedImage;
                }
            }
        }
        private void DeleteProduct(string maSP)
        {
            try
            {
                string maSPToDelete = maSP.Trim();

                using (SqlConnection connection = new SqlConnection(ConnStr))
                {
                    connection.Open();

                    // Gọi stored procedure để thực hiện xóa sản phẩm
                    SqlCommand command = new SqlCommand("DeleteProduct", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MaSP", maSPToDelete);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Xóa sản phẩm thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_BackSearchSP_Click(object sender, EventArgs e)
        {
            LoadDataToDataGridView();
            txt_TimKiem.Text = "";
        }
        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            //kiêm tra xem có nhập thông tin tìm kiếm hay không
            if (string.IsNullOrEmpty(txt_TimKiem.Text))
            {
                MessageBox.Show("Vui lòng nhập thông tin tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                SearchProductByName(txt_TimKiem.Text.Trim());
                btn_BackSearchSP.Visible = true;
            }
        }

        private void SearchProductByName(string productName)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(ConnStr))
                {
                    connection.Open();

                    // Gọi function để thực hiện tìm kiếm
                    SqlCommand command = new SqlCommand("SELECT * FROM dbo.SearchProductByName(@ProductName)", connection);
                    command.Parameters.AddWithValue("@ProductName", productName);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet, "SANPHAM");

                    // Hiển thị kết quả trong DataGridView
                    dgv_SanPham.DataSource = dataSet.Tables["SANPHAM"];

                    if (dataSet.Tables["SANPHAM"].Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy sản phẩm nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void FilterProducts()
        {
            try
            {
                // Get selected values from ComboBoxes
                string selectedLoai = cb_LocLoaiSP.SelectedValue?.ToString();
                if (cb_LocLoaiSP.Text == "")
                {
                    LoadDataToDataGridView();
                    return;
                }
                using (SqlConnection connection = new SqlConnection(ConnStr))
                {
                    connection.Open();

                    // Call the stored procedure to get filtered data
                    SqlCommand command = new SqlCommand("GetSanPhamByLoai", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameter for MaLoai
                    command.Parameters.AddWithValue("@MaLoai", selectedLoai);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet, "SANPHAM");

                    // Display the filtered results in the DataGridView
                    dgv_SanPham.DataSource = dataSet.Tables["SANPHAM"];

                    if (dataSet.Tables["SANPHAM"].Rows.Count == 0)
                    {
                        MessageBox.Show("Không có sản phẩm nào phù hợp với bộ lọc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void FilterSize()
        {
            try
            {
                // Get selected values from ComboBoxes
                string selectedSize = cb_LocSize.SelectedValue?.ToString();
                if (cb_LocSize.Text == "")
                {
                    LoadDataToDataGridView();
                    return;
                }

                using (SqlConnection connection = new SqlConnection(ConnStr))
                {
                    connection.Open();

                    // Call the stored procedure to get filtered data
                    SqlCommand command = new SqlCommand("GetSanPhamBySize", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameter for Size
                    command.Parameters.AddWithValue("@Size", selectedSize);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet, "SANPHAM");

                    // Display the filtered results in the DataGridView
                    dgv_SanPham.DataSource = dataSet.Tables["SANPHAM"];

                    if (dataSet.Tables["SANPHAM"].Rows.Count == 0)
                    {
                        MessageBox.Show("Không có sản phẩm nào phù hợp với bộ lọc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void dgv_SanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //điền thông tin sản phẩm vào các trường dữ liệu
            txt_MaSP.Text = dgv_SanPham.CurrentRow.Cells[0].Value.ToString();
            txt_TenSP.Text = dgv_SanPham.CurrentRow.Cells[1].Value.ToString();
            cb_LoaiSP.SelectedValue = dgv_SanPham.CurrentRow.Cells[2].Value.ToString();
            cb_XuatSu.SelectedValue = dgv_SanPham.CurrentRow.Cells[3].Value.ToString();
            txtGiaNhap.Text = dgv_SanPham.CurrentRow.Cells[5].Value.ToString();
            txt_DonGia.Text = dgv_SanPham.CurrentRow.Cells[6].Value.ToString();
            txt_Sale.Text = dgv_SanPham.CurrentRow.Cells[7].Value.ToString();
            txt_MoTa.Text = dgv_SanPham.CurrentRow.Cells[8].Value.ToString();
            txt_SoLuong.Text = dgv_SanPham.CurrentRow.Cells[9].Value.ToString();
            cb_Size.SelectedValue = dgv_SanPham.CurrentRow.Cells[10].Value.ToString();
            txt_MauSac.Text = dgv_SanPham.CurrentRow.Cells[11].Value.ToString();
            if (dgv_SanPham.CurrentRow.Cells[4].Value != DBNull.Value)
            {
                byte[] imageData = (byte[])dgv_SanPham.CurrentRow.Cells[4].Value;
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    Image image = Image.FromStream(ms);
                    pb_AnhBia.Image = image;
                }
            }
            else
            {
                pb_AnhBia.Image = null;
            }
        }


        private void cb_LocSize_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterSize();
        }

        private void cb_LocLoaiSP_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterProducts();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pb_AnhBia.Image = null;
        }
    }
}
