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
    public partial class frm_DangKy : Form
    {
        KetNoi kn = new KetNoi();
        SqlConnection consql;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public frm_DangKy()
        {
            InitializeComponent();
            consql = kn.connect;
        }


        private void frm_DangKy_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có muốn thoát", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
                e.Cancel = true;
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            try
            {
                if (consql.State == ConnectionState.Closed)
                {
                    consql.Open();
                }
                string insertString = "insert into TaiKhoan values('" + tbx_IdTK.Text + "','" + cbx_IdNV.Text + "',N'"+ tbx_TenTK.Text +"','"+ tbx_Mk.Text +"')";
                SqlCommand cmd = new SqlCommand(insertString, consql);
                cmd.ExecuteNonQuery();
                if (consql.State == ConnectionState.Open)
                {
                    consql.Close();
                }
                MessageBox.Show("Thêm tài khoản thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể thêm! Hãy nhập lại!");
            }
        }

        private void frm_DangKy_Load(object sender, EventArgs e)
        {
            btn_Luu.Enabled = false;
            consql.Open();
            string selectString = "select * from Employees";
            SqlCommand cmd = new SqlCommand(selectString, consql);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                cbx_IdNV.Items.Add(rd["IDNhanVien"].ToString());
            }
            rd.Close();
            consql.Close();
            cbx_IdNV.SelectedIndex = 0;
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbx_NhapMK_TextChanged(object sender, EventArgs e)
        {
            if (tbx_Mk.Text == tbx_NhapMK.Text)
            {
                btn_Luu.Enabled = true;
            }
            else
            {
                MessageBox.Show("Mật khẩu nhập lại không đúng!");
            }
        }

        private void btn_DangNhap_Click(object sender, EventArgs e)
        {
            frm_dangnhap f = new frm_dangnhap();
            this.Hide();
            f.ShowDialog();
        }

        private void cbx_IdNV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
