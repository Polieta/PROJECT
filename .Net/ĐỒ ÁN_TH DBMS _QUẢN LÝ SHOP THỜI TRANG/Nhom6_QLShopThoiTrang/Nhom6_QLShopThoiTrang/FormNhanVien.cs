using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nhom6_QLShopThoiTrang
{
    public partial class FormNhanVien : Form
    {
        //Fields
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;

        public FormNhanVien()
        {
            InitializeComponent();
            random = new Random();
            btn_Close_ChildForm.Visible = false;
        }

        // -- CUSTOM MÀU SẮC START --//
        //Methods - chọn màu nền
        private Color selectThemeColor()
        {
            int index = random.Next(ThemeColor.colorList.Count);
            while (tempIndex == index)
            {
                index = random.Next(ThemeColor.colorList.Count);
            }
            tempIndex = index;
            string color = ThemeColor.colorList[index];
            return ColorTranslator.FromHtml(color);
        }

        //button kích hoạt
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = selectThemeColor();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    panel_TitleBarNV.BackColor = color;
                    panel_Logo.BackColor = ThemeColor.changeColorBrightness(color, -0.3);
                    btn_Close_ChildForm.Visible = true;
                }
            }
        }

        //button vô hiệu hóa
        private void DisableButton()
        {
            foreach (Control previousBtn in panel_Menu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(128, 255, 255);
                    previousBtn.ForeColor = Color.Black;
                    previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        // -- CUSTOM MÀU SẮC END --//
        //
        //-- MỞ FORM CON--//
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panel_DesktopPane.Controls.Add(childForm);
            this.panel_DesktopPane.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }
        private void btn_SanPham_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormSanPham(), sender);
            lbl_TitleNV.Text = "QUẢN LÝ SẢN PHẨM";
        }

        private void btn_KhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormKhachHang(), sender);
            lbl_TitleNV.Text = "QUẢN LÝ KHÁCH HÀNG";
        }

        private void btn_DangXuat_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có muốn đăng xuất?", "Đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                this.Close();
                FormDangNhap frmLogin = new FormDangNhap();
                frmLogin.maNVLogin = "";
                frmLogin.ShowDialog();
            }
        }

        private void btn_NhanVien_Click(object sender, EventArgs e)
        {

        }
        private void btn_HoaDon_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormHoaDon(), sender);
            lbl_TitleNV.Text = "LẬP HÓA ĐƠN";
        }
        private void btn_Close_ChildForm_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
            Reset();
        }
        private void Reset()
        {
            DisableButton();
            lbl_TitleNV.Text = "WELCOME";
            panel_TitleBar.BackColor = Color.Cyan;
            panel_Logo.BackColor = Color.Cyan;
            currentButton = null;
            btn_Close_ChildForm.Visible = false;
        }
    }
}
