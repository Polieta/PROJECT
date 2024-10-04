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
    public partial class frm_dangnhap : Form
    {
        KetNoi kn = new KetNoi();
        SqlConnection consql;
        public frm_dangnhap()
        {
            InitializeComponent();
            consql = kn.connect;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frm_dangnhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn thoát!?","Thông báo",MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tentk = tbx_Tendangnhap.Text;
            string mk = tbx_Matkhau.Text;
            if (tentk.Trim() == "") { MessageBox.Show("Vui lòng không được bỏ trống!"); }
            else if (mk.Trim() == "") { MessageBox.Show("Vui lòng nhập mật khẩu!"); }
            else
            {
                string query = "select * from TaiKhoan where TaiKhoan = '" + tbx_Tendangnhap.Text + "' and MatKhau = '" + tbx_Matkhau.Text + "'";
               var modify = new DS_TaiKhoan();
               if (modify.TAIKHOAN(query).Count != 0)
               {
                   MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   frm_TrangChu f = new frm_TrangChu(tbx_Tendangnhap.Text);
                   this.Hide();
                   f.ShowDialog();
                   this.Show();
               }else
               {
                   MessageBox.Show("Tên tài khoản hoặc mật khẩu không chính xác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   tbx_Matkhau.Clear();
                   tbx_Tendangnhap.Focus();
               }
            }
        }
        private void frm_dangnhap_Load(object sender, EventArgs e)
        {

        }


        private void frm_dangnhap_Enter(object sender, EventArgs e)
        {
            string tentk = tbx_Tendangnhap.Text;
            string mk = tbx_Matkhau.Text;
            if (tentk.Trim() == "") { MessageBox.Show("Vui lòng không được bỏ trống!"); }
            else if (mk.Trim() == "") { MessageBox.Show("Vui lòng nhập mật khẩu!"); }
            else
            {
                string query = "select * from TaiKhoan where TaiKhoan = '" + tbx_Tendangnhap.Text + "' and MatKhau = '" + tbx_Matkhau.Text + "'";
                var modify = new DS_TaiKhoan();
                if (modify.TAIKHOAN(query).Count != 0)
                {
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frm_TrangChu f = new frm_TrangChu(tbx_Tendangnhap.Text);
                    this.Hide();
                    f.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Tên tài khoản hoặc mật khẩu không chính xác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbx_Matkhau.Clear();
                    tbx_Tendangnhap.Focus();
                }
            }
        }
    }
}
