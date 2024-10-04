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
using System.Runtime.CompilerServices;
using System.Collections;

namespace Nhom8
{
    public partial class frm_DonHang : Form
    {
        int flag = 0;
        string iddh, idkh, xemay, dongia;
        KetNoi kn = new KetNoi();
        SqlConnection consql;
        int tong = 0;
        int tongtien = 0;
        bool x = true;
        public frm_DonHang()
        {
            InitializeComponent();
            consql = kn.connect;
        }
        public frm_DonHang(string a, string b, string c, string d)
        {
            InitializeComponent();
            consql = kn.connect;
            iddh = a;
            idkh = b;
            xemay = c;
            dongia = d;
        }
        int sl = 1;
        private void frm_DonHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flag == 0)
            {
                string insertString2 = "update Orders set NgayDatHang = Default,TongTien='" + tongtien + "' where IDDonHang= '" + tbx_ID_DonHang.Text + "'";
                SqlCommand cmd2 = new SqlCommand(insertString2, consql);
                consql.Open();
                cmd2.ExecuteNonQuery();
                flag++;
                consql.Close();
            }
            consql.Close();
            if (MessageBox.Show("Trở lại màn hình chính!?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frm_DonHang_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            tbx_SL.Text = Convert.ToString(sl);
            tbx_ID_DonHang.Text = iddh;
            tbx_ID_KhachHang.Text = idkh;
            cbx_XeMay.SelectedValue = xemay;
        }

        private void btn_XacNhan_Click(object sender, EventArgs e)
        {
            if (x)
            {
                string insertString2 = "insert into Orders(IDDonHang,IDKhachHang) values ('" + tbx_ID_DonHang.Text + "','" + tbx_ID_KhachHang.Text + "')";
                SqlCommand cmd2 = new SqlCommand(insertString2, consql);
                consql.Open();
                cmd2.ExecuteNonQuery();
                consql.Close();
                x = false;
            }

            string insertString = "update Motorbikes set SoLuong = SoLuong -" + tbx_SL.Text + " where TenXE = '" + cbx_XeMay.Text + "'";
            SqlCommand cmd = new SqlCommand(insertString, consql);
            //mo ket noi
            consql.Open();
            cmd.ExecuteNonQuery();
            tong = Convert.ToInt32(tbx_SL.Text) * Convert.ToInt32(dongia);
            tongtien += tong;
            string insertString1 = "insert into OrderDetails values('" + tbx_ID_DonHang.Text + "',N'" + cbx_XeMay.SelectedValue.ToString() + "','" + tbx_SL.Text + "','" + dongia + "','" + tong + "')";
            SqlCommand cmd1 = new SqlCommand(insertString1, consql);
            cmd1.ExecuteNonQuery();
            MessageBox.Show("Thao tác thành công!");
            //dong ket noi
            consql.Close();
        }
        private void btn_Giam_Click(object sender, EventArgs e)
        {
            sl--;
            tbx_SL.Text = Convert.ToString(sl);
        }
        private void btn_Tang_Click(object sender, EventArgs e)
        {
            sl++;
            tbx_SL.Text = Convert.ToString(sl);
        }
        public void LoadComboBox()
        {
            DataTable dt = new DataTable();
            consql = kn.connect;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("Select * From Motorbikes", consql);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.ToString());
            }
            try
            {
                cbx_XeMay.DataSource = dt;
                cbx_XeMay.DisplayMember = "TenXe";
                cbx_XeMay.ValueMember = "IDXeMay";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi load dữ liệu!\n", ex.ToString());
            }
        }
    }
}
