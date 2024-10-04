using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Nhom8.DB;

namespace Nhom8
{
    public partial class frm_QuanLy : Form
    {
        SqlDataAdapter adapter = new SqlDataAdapter();
        SqlDataAdapter adapter2 = new SqlDataAdapter();
        DataTable table = new DataTable();
        KetNoi kn = new KetNoi();
        SqlConnection consql;
        DataSet ds_HoaDon;
        DataSet ds_NhanVien;
        DataSet ds_XeMay;
        DataSet ds_KhachHang;
        DataColumn[] key = new DataColumn[1];
        public frm_QuanLy()
        {
            InitializeComponent();
            consql = kn.connect;
            string strSelect = "select*from Orders";
            adapter = new SqlDataAdapter(strSelect, consql);
            ds_HoaDon = new DataSet();
            adapter.Fill(ds_HoaDon, "Orders");
            key[0] = ds_HoaDon.Tables["Orders"].Columns[0];
            ds_HoaDon.Tables["Orders"].PrimaryKey = key;

            string strSelect1 = "select*from Employees";
            adapter = new SqlDataAdapter(strSelect1, consql);
            ds_NhanVien = new DataSet();
            adapter.Fill(ds_NhanVien, "Employees");
            key[0] = ds_NhanVien.Tables["Employees"].Columns[0];
            ds_NhanVien.Tables["Employees"].PrimaryKey = key;

            string strSelect2 = "select*from Motorbikes";
            adapter = new SqlDataAdapter(strSelect2, consql);
            ds_XeMay = new DataSet();
            adapter.Fill(ds_XeMay, "Motorbikes");
            key[0] = ds_XeMay.Tables["Motorbikes"].Columns[0];
            ds_XeMay.Tables["Motorbikes"].PrimaryKey = key;

            string strSelect3 = "select*from Customers";
            adapter = new SqlDataAdapter(strSelect3, consql);
            ds_KhachHang = new DataSet();
            adapter.Fill(ds_KhachHang, "Customers");
            key[0] = ds_KhachHang.Tables["Customers"].Columns[0];
            ds_KhachHang.Tables["Customers"].PrimaryKey = key;

        }
        void load_gird()
        {
            dgv_TTHD.DataSource=ds_HoaDon.Tables[0];
            dgv_NhanVien.DataSource = ds_NhanVien.Tables[0];
            dgv_XeMay.DataSource = ds_XeMay.Tables[0];
            dgv_KhachHang.DataSource = ds_KhachHang.Tables[0];
        }
        void Databingding_KhachHang(DataTable pDT)
        {
                txt_idKhach.DataBindings.Clear();
                txt_HoTenKhach.DataBindings.Clear();
                txt_DiaChiKhach.DataBindings.Clear();
                txt_SDT_Khach.DataBindings.Clear();
                txt_idKhach.DataBindings.Add("Text", pDT, "IDKhachHang");
                txt_HoTenKhach.DataBindings.Add("Text", pDT, "HoVaTen");
                txt_DiaChiKhach.DataBindings.Add("Text", pDT, "DiaChi");
                txt_SDT_Khach.DataBindings.Add("Text", pDT, "SoDienThoai");
        }
        void Databingding_NhanVien(DataTable pNV)
        {

            txt_idNhanVien.DataBindings.Clear();
            txt_hoten.DataBindings.Clear();
            txt_gioitinh.DataBindings.Clear();
            txt_DiaChi.DataBindings.Clear();
            txt_SDT.DataBindings.Clear();
            //date_ngaySinh.DataBindings.Clear();
            txt_idNhanVien.DataBindings.Add("Text", pNV, "IDNhanVien");
            txt_hoten.DataBindings.Add("Text", pNV, "HoVaTen");
            txt_DiaChi.DataBindings.Add("Text", pNV, "DiaChi");
            txt_SDT.DataBindings.Add("Text", pNV, "SoDienThoai");
            txt_gioitinh.DataBindings.Add("Text", pNV, "GioiTinh");
            //date_ngaySinh.DataBindings.Add("Date", pNV, "NgaySinh");
        }
        void Databingding_XeMay(DataTable pNV)
        {
            txt_XeMay.DataBindings.Clear();
            txt_TenXe.DataBindings.Clear();
            txt_HangSanXuat.DataBindings.Clear();
            txt_SoLuong.DataBindings.Clear();
            txt_SDT.DataBindings.Clear();
            txt_XeMay.DataBindings.Add("Text", pNV, "IDXeMay");
            txt_TenXe.DataBindings.Add("Text", pNV, "TenXe");
            txt_HangSanXuat.DataBindings.Add("Text", pNV, "HangSanXuat");
            txt_SoLuong.DataBindings.Add("Text", pNV, "SoLuong");
            txt_DonGia.DataBindings.Add("Text", pNV, "DonGiaBan");
        }
        private void frm_QuanLy_Load(object sender, EventArgs e)
        {
            load_gird();
            btn_Them.Enabled = false;
      
                Databingding_KhachHang(ds_KhachHang.Tables["Customers"]);
                Databingding_NhanVien(ds_NhanVien.Tables["Employees"]);
                Databingding_XeMay(ds_XeMay.Tables["Motorbikes"]);
                ShowLSGD();
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            string timKiemValue = tbx_TimKiem.Text.Trim();
            if (tabControl5.SelectedIndex == 0)
            {
                if (!string.IsNullOrEmpty(timKiemValue))
                {
                    string strSelect = "SELECT * FROM Orders WHERE IDDonHang LIKE @timKiemValue";
                    SqlDataAdapter adapter = new SqlDataAdapter(strSelect, consql);
                    adapter.SelectCommand.Parameters.AddWithValue("@timKiemValue", "%" + timKiemValue + "%");
                    DataTable resultTable = new DataTable();
                    adapter.Fill(resultTable);
                    dgv_TTHD.DataSource = resultTable;
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập dữ liệu cần tìm kiếm!");
                }
            }
            else if (tabControl5.SelectedIndex == 1)
            {
                if (!string.IsNullOrEmpty(timKiemValue))
                {
                    string strSelect = "SELECT * FROM Employees WHERE HoVaTen LIKE @timKiemValue";
                    SqlDataAdapter adapter = new SqlDataAdapter(strSelect, consql);
                    adapter.SelectCommand.Parameters.AddWithValue("@timKiemValue", "%" + timKiemValue + "%");
                    DataTable resultTable = new DataTable();
                    adapter.Fill(resultTable);
                    dgv_NhanVien.DataSource = resultTable;
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập dữ liệu cần tìm kiếm!");
                }
            }
            else if (tabControl5.SelectedIndex == 2)
            {
                if (!string.IsNullOrEmpty(timKiemValue))
                {
                    string strSelect = "SELECT * FROM Motorbikes WHERE TenXe LIKE @timKiemValue";
                    SqlDataAdapter adapter = new SqlDataAdapter(strSelect, consql);
                    adapter.SelectCommand.Parameters.AddWithValue("@timKiemValue", "%" + timKiemValue + "%");
                    DataTable resultTable = new DataTable();
                    adapter.Fill(resultTable);
                    dgv_XeMay.DataSource = resultTable;
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập dữ liệu cần tìm kiếm!");
                }
            }
            else if (tabControl5.SelectedIndex == 3)
            {
                if (!string.IsNullOrEmpty(timKiemValue))
                {
                    string strSelect = "SELECT * FROM Customers WHERE HoVaTen LIKE @timKiemValue";
                    SqlDataAdapter adapter = new SqlDataAdapter(strSelect, consql);
                    adapter.SelectCommand.Parameters.AddWithValue("@timKiemValue", "%" + timKiemValue + "%");
                    DataTable resultTable = new DataTable();
                    adapter.Fill(resultTable);
                    dgv_KhachHang.DataSource = resultTable;
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập dữ liệu cần tìm kiếm!");
                }
            }
        }

        private void btn_CapNhat_Click(object sender, EventArgs e)
        {
            if (tabControl5.SelectedIndex == 1)
            {
                string strSelect = "select*from Employees";
                adapter = new SqlDataAdapter(strSelect, consql);
                DataRow update_new = ds_NhanVien.Tables["Employees"].Rows.Find(txt_idNhanVien.Text);
                if (update_new != null)
                {
                    update_new["HoVaTen"] = txt_hoten.Text;
                    update_new["GioiTinh"] = txt_gioitinh.Text;
                    update_new["SoDienThoai"] = txt_SDT.Text;
                    update_new["NgaySinh"] = date_ngaySinh.Value;
                    SqlCommandBuilder CB = new SqlCommandBuilder(adapter);
                    adapter.Update(ds_NhanVien, "Employees");
                    MessageBox.Show("Thành công!");
                    load_gird();
                }
               
            }
            else if (tabControl5.SelectedIndex == 2)
            {
                string strSelect = "select * from Motorbikes";
                adapter = new SqlDataAdapter(strSelect, consql);

                DataRow update_new = ds_XeMay.Tables["Motorbikes"].Rows.Find(txt_XeMay.Text);
                if (update_new != null)
                {
                    update_new["TenXe"] = txt_TenXe.Text;
                    update_new["HangSanXuat"] = txt_HangSanXuat.Text;
                    update_new["SoLuong"] = txt_SoLuong.Text;
                    update_new["DonGiaBan"] = txt_DonGia.Text;
                    SqlCommandBuilder CB = new SqlCommandBuilder(adapter);
                    adapter.Update(ds_XeMay, "Motorbikes");
                }
                MessageBox.Show("Thành công!");
            }
            else if (tabControl5.SelectedIndex == 3)
            {
                string strSelect = "select * from Customers";
                adapter = new SqlDataAdapter(strSelect, consql);

                DataRow update_new = ds_KhachHang.Tables["Customers"].Rows.Find(txt_idKhach.Text);
                if (update_new != null)
                {
                    update_new["HoVaTen"] = txt_HoTenKhach;
                    update_new["DiaChi"] = txt_DiaChiKhach.Text;
                    update_new["SoDienThoai"] = txt_SDT_Khach.Text;
                    SqlCommandBuilder CB = new SqlCommandBuilder(adapter);
                    adapter.Update(ds_KhachHang, "Customers");
                }
                MessageBox.Show("Thành công!");
            }
         
        }
        private void btn_Them_Click(object sender, EventArgs e)
        {
            if(tabControl5.SelectedIndex==1)
            {
                string strSelect1 = "select*from Employees";
                adapter = new SqlDataAdapter(strSelect1, consql);
                DataRow newrow = ds_NhanVien.Tables[0].NewRow();
                newrow["IDNhanVien"] = txt_idNhanVien.Text;
                newrow["HoVaTen"] = txt_hoten.Text;
                newrow["GioiTinh"] = txt_gioitinh.Text;
                newrow["DiaChi"] = txt_DiaChi.Text;
                newrow["SoDienThoai"] = txt_SDT.Text;
                newrow["NgaySinh"] = Convert.ToDateTime(date_ngaySinh.Text);
                ds_NhanVien.Tables[0].Rows.Add(newrow);
                // Cap nhat trong CSDL 
                SqlCommandBuilder CB = new SqlCommandBuilder(adapter);
                // Cap nhat trong dataSet 
                adapter.Update(ds_NhanVien, "Employees");
            }
            else if(tabControl5.SelectedIndex == 2)
            {

                string strSelect1 = "select*from Motorbikes";
                adapter = new SqlDataAdapter(strSelect1, consql);
                DataRow newrow = ds_XeMay.Tables[0].NewRow();
                newrow["IDXeMay"] = txt_XeMay.Text;
                newrow["TenXe"] = txt_TenXe.Text;
                newrow["HangSanXuat"] = txt_HangSanXuat.Text;
                newrow["SoLuong"] = txt_SoLuong.Text;
                newrow["DonGiaBan"] = txt_DonGia.Text;
                ds_XeMay.Tables[0].Rows.Add(newrow);
                // Cap nhat trong CSDL 
                SqlCommandBuilder CB = new SqlCommandBuilder(adapter);
                // Cap nhat trong dataSet 
                adapter.Update(ds_XeMay, "Motorbikes");
            }
            else if (tabControl5.SelectedIndex == 3)
            {
                string strSelect1 = "select*from Customers";
                adapter = new SqlDataAdapter(strSelect1, consql);
                DataRow newrow = ds_KhachHang.Tables[0].NewRow();
                newrow["IDKhachHang"] = txt_idKhach.Text;
                newrow["HoVaTen"] = txt_HoTenKhach.Text;
                newrow["DiaChi"] = txt_DiaChiKhach.Text;
                newrow["SoDienThoai"] = txt_SDT_Khach.Text; 
        
                ds_KhachHang.Tables[0].Rows.Add(newrow);
                // Cap nhat trong CSDL 
                SqlCommandBuilder CB = new SqlCommandBuilder(adapter);
                // Cap nhat trong dataSet 
                adapter.Update(ds_KhachHang, "Customers");
            }
            MessageBox.Show("Thành công!");
        }
        private void btn_Xoa_Click(object sender, EventArgs e)
        
        {
            if (tabControl5.SelectedIndex == 1)
            {
                string strSelect1 = "select*from Employees";
                adapter = new SqlDataAdapter(strSelect1, consql);
                for (int i = 0; i < dgv_NhanVien.Rows.Count; i++)
                {
                    if (dgv_NhanVien.Rows[i].Selected)
                    {
                        dgv_NhanVien.Rows.RemoveAt(i);
                        SqlCommandBuilder CB = new SqlCommandBuilder(adapter);
                        adapter.Update(ds_NhanVien, "Employees");
                        break;
                    }
                }
            }
            else if(tabControl5.SelectedIndex==2)
            {
                string strSelect1 = "select*from Motorbikes";
                adapter = new SqlDataAdapter(strSelect1, consql);
                for (int i = 0; i < dgv_XeMay.Rows.Count; i++)
                {
                    if (dgv_XeMay.Rows[i].Selected)
                    {
                        dgv_XeMay.Rows.RemoveAt(i);
                        SqlCommandBuilder CB = new SqlCommandBuilder(adapter);
                        adapter.Update(ds_XeMay, "Motorbikes");
                        break;
                    }
                }
            }
            else if (tabControl5.SelectedIndex == 3)
            {
                DataTable dt_Orders = new DataTable();
                //string strSelect1 = "select*from Customers";
                SqlDataAdapter da_Orders = new SqlDataAdapter("select * from Orders where IDKhachHang= '" + txt_idKhach.Text + "'", consql);
                da_Orders.Fill(dt_Orders);
                if(dt_Orders.Rows.Count>0)
                {
                    MessageBox.Show("Dữ liệu đang sử dụng không được xóa");
                    return;
                }
                else
                {
                    string strSelect1 = "select*from Customers";
                    adapter = new SqlDataAdapter(strSelect1, consql);
                    for (int i = 0; i < dgv_KhachHang.Rows.Count; i++)
                    {
                        if (dgv_KhachHang.Rows[i].Selected)
                        {
                            dgv_KhachHang.Rows.RemoveAt(i);
                            SqlCommandBuilder CB = new SqlCommandBuilder(adapter);
                            adapter.Update(ds_KhachHang, "Customers");
                            break;
                        }
                    }
                }
              
            }
           
        }

        private void btn_xoaTXT_Click(object sender, EventArgs e)
        {
            txt_DiaChi.Clear();
            txt_DiaChiKhach.Clear();
            txt_DonGia.Clear();
            txt_gioitinh.Clear();
            txt_HangSanXuat.Clear();
            txt_hoten.Clear();
            txt_HoTenKhach.Clear();
            txt_idKhach.Clear();
            txt_idNhanVien.Clear();
            txt_SDT.Clear();
            txt_SDT_Khach.Clear();
            txt_SoLuong.Clear();
            txt_TenXe.Clear();
            txt_XeMay.Clear();
            
        }
        private void frm_QuanLy_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát!?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                
                e.Cancel = true;
            }
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_MO_Click(object sender, EventArgs e)
        {
            btn_Them.Enabled = true;
        }

        DataClasses1DataContext db = new DataClasses1DataContext();
        private void ShowLSGD()
        {
            var result = from hd in db.Orders.Where(x => x.NgayDatHang == DateTime.Now)
                         join p in db.OrderDetails.GroupBy(t => t.IDDonHang)
                         on hd.IDDonHang equals p.First().IDDonHang
                         select new
                         {
                            ID = hd.IDDonHang,
                            IDKhach = hd.IDKhachHang,
                            Ngay = hd.NgayDatHang,
                            tongtien = hd.TongTien
                         };
            dgv_LSGD.DataSource = result;
        }

        private void btn_IN_Click(object sender, EventArgs e)
        {
            pddHoaDon.Document = pdHoaDon;
            pddHoaDon.ShowDialog();
        }

        private void pdHoaDon_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (tabControl5.SelectedIndex == 0)
            {
                Bitmap obj = new Bitmap(this.dgv_TTHD.Width, this.dgv_TTHD.Height);
                dgv_TTHD.DrawToBitmap(obj, new Rectangle(0, 0, this.dgv_TTHD.Width, this.dgv_TTHD.Height));
                e.Graphics.DrawImage(obj, 190, 90);
            } else if (tabControl5.SelectedIndex == 1)
            {
                Bitmap obj = new Bitmap(this.dgv_NhanVien.Width, this.dgv_NhanVien.Height);
                dgv_NhanVien.DrawToBitmap(obj, new Rectangle(0, 0, this.dgv_NhanVien.Width, this.dgv_NhanVien.Height));
                e.Graphics.DrawImage(obj, 190, 90);
            } else if (tabControl5.SelectedIndex == 2)
            {
                Bitmap obj = new Bitmap(this.dgv_XeMay.Width, this.dgv_XeMay.Height);
                dgv_XeMay.DrawToBitmap(obj, new Rectangle(0, 0, this.dgv_XeMay.Width, this.dgv_XeMay.Height));
                e.Graphics.DrawImage(obj, 190, 90);
            } else if (tabControl5.SelectedIndex == 3)
            {
                Bitmap obj = new Bitmap(this.dgv_KhachHang.Width, this.dgv_KhachHang.Height);
                dgv_KhachHang.DrawToBitmap(obj, new Rectangle(0, 0, this.dgv_KhachHang.Width, this.dgv_KhachHang.Height));
                e.Graphics.DrawImage(obj, 190, 90);
            }
            else if (tabControl5.SelectedIndex == 4)
            {
                Bitmap obj = new Bitmap(this.dgv_LSGD.Width, this.dgv_LSGD.Height);
                dgv_LSGD.DrawToBitmap(obj, new Rectangle(0, 0, this.dgv_LSGD.Width, this.dgv_LSGD.Height));
                e.Graphics.DrawImage(obj, 190, 90);
            }
        }

        private void dgv_TTHD_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell selectedCell = dgv_TTHD.CurrentCell;
            object cellValue = selectedCell.Value;
            if (cellValue != null)
            {
                var b = db.OrderDetails.Where(n => n.IDDonHang == cellValue);
                if (b!=null)
                    {
                        string productDetails = "Thông tin chi tiết sản phẩm:\n";
                        foreach (var orderDetail in b)
                        {
                            productDetails += ("ID Đơn hàng: '" + orderDetail.IDDonHang + "', ID xe máy: '" + orderDetail.IDXeMay + "', Số lượng: " + orderDetail.SoLuong + ", Giá: " + orderDetail.DonGia + "\n");
                        }
                        MessageBox.Show(productDetails);
                    }
                 else
                    {
                        MessageBox.Show("Không tìm thấy thông tin sản phẩm tương ứng.");
                    }
            }
            else
            {
                MessageBox.Show("Ô được chọn không chứa dữ liệu.");
            }
        }
   
    }
}
