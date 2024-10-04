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

namespace Nhom8
{

    public partial class frm_TrangChu : Form
    {
        DataSet ds_HoaDon;
        DataSet ds_KhachHang;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        string TenDN;
        KetNoi kn = new KetNoi();
        SqlConnection consql;
        DataSet ds_xemay_trangchu;
        DataColumn[] key = new DataColumn[1];
        void load_gird()
        {
            dataGridView1.DataSource = ds_xemay_trangchu.Tables[0];
            Databingding(ds_xemay_trangchu.Tables[0]);
        }
        private void frm_TrangChu_Load(object sender, EventArgs e)
        {
            lbl_TENDN.Text = "Xin chào: " + TenDN + "";
            load_gird();
        }
        public frm_TrangChu()
        {
            InitializeComponent();
            consql = kn.connect;
            string strSelect = "select*from Motorbikes";
            adapter = new SqlDataAdapter(strSelect, consql);
            ds_xemay_trangchu = new DataSet();
            adapter.Fill(ds_xemay_trangchu, "Motorbikes");
            key[0] = ds_xemay_trangchu.Tables["Motorbikes"].Columns[0];
            ds_xemay_trangchu.Tables["Motorbikes"].PrimaryKey = key;
        }
        void Databingding(DataTable pDT)
        {
            tbx_IdXeMay.DataBindings.Clear();
            tbx_TenXe.DataBindings.Clear();
            tbx_DonGia.DataBindings.Clear();
            tbx_IdXeMay.DataBindings.Add("Text", pDT, "IDXeMay");
            tbx_TenXe.DataBindings.Add("Text", pDT, "TenXe");
            tbx_DonGia.DataBindings.Add("Text", pDT, "DonGiaBan");
        }
        public frm_TrangChu(string tenDN)
        {
            InitializeComponent();
            consql = kn.connect;
            TenDN = tenDN;
            string strSelect = "select*from Motorbikes";
            adapter = new SqlDataAdapter(strSelect, consql);
            ds_xemay_trangchu = new DataSet();
            adapter.Fill(ds_xemay_trangchu, "Motorbikes");
            key[0] = ds_xemay_trangchu.Tables["Motorbikes"].Columns[0];
            ds_xemay_trangchu.Tables["Motorbikes"].PrimaryKey = key;
        }
        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            tbx_HoTenKH.Clear();
            tbx_SDT.Clear();
            tbx_Diachi.Clear();
        }
        private void btn_Luu_Click(object sender, EventArgs e)
        {
            try
            {
                if (consql.State == ConnectionState.Closed)
                {
                    consql.Open();
                }
                string insertString = "insert into Customers values('"+tbx_IdKH.Text+"',N'"+tbx_HoTenKH.Text+ "',N'" + tbx_Diachi.Text + "','" + tbx_SDT.Text + "')";
                SqlCommand cmd = new SqlCommand(insertString, consql);
                cmd.ExecuteNonQuery();
                frm_DonHang f = new frm_DonHang(tbx_IdDonHang.Text, tbx_IdKH.Text, tbx_IdXeMay.Text,tbx_DonGia.Text);
                this.Hide();
                f.ShowDialog();
                this.Show();
                load_gird();
                MessageBox.Show("Thêm hóa đơn thành công!");
                if (consql.State == ConnectionState.Open)
                {
                    consql.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể thêm!");
            }
            load_gird();
        }
        private void adimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_QuanLy f = new frm_QuanLy();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }


        private void tbx_IdDonHang_Leave(object sender, EventArgs e)
        {
            string strSelect = "select * from Orders";
            adapter = new SqlDataAdapter(strSelect, consql);
            ds_HoaDon = new DataSet();
            adapter.Fill(ds_HoaDon, "Orders");
            key[0] = ds_HoaDon.Tables["Orders"].Columns[0];
            ds_HoaDon.Tables["Orders"].PrimaryKey = key;
            DataRow check = ds_HoaDon.Tables["Orders"].Rows.Find(tbx_IdDonHang.Text);
            if (check != null)
            {
                MessageBox.Show("Dữ liệu đã tồn tại trong CSDL!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbx_IdDonHang.Clear();
                tbx_IdDonHang.Focus();
            }
        }

        private void tbx_IdKH_Leave(object sender, EventArgs e)
        {
            string strSelect = "select * from Customers";
            adapter = new SqlDataAdapter(strSelect, consql);
            ds_KhachHang = new DataSet();
            adapter.Fill(ds_KhachHang, "Customers");
            key[0] = ds_KhachHang.Tables["Customers"].Columns[0];
            ds_KhachHang.Tables["Customers"].PrimaryKey = key;
            DataRow check = ds_KhachHang.Tables["Customers"].Rows.Find(tbx_IdKH.Text);
            if (check != null)
            {
                MessageBox.Show("Dữ liệu đã tồn tại trong CSDL!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbx_IdKH.Clear();
                tbx_IdKH.Focus();
            }
        }

    }
}
