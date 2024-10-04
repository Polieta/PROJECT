using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
namespace Nhom6_QLShopThoiTrang
{
    public partial class FormAdmin : Form
    {
        string ConnStr = Properties.Settings.Default.connStr;
        SqlConnection conn;

        //Fields
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;
        public FormAdmin()
        {
            InitializeComponent();
            random = new Random();
            btn_CloseChildForm.Visible = false;

        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {

        }


        //constructer
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
                    panel_TitleBar.BackColor = color;
                    panel_Logo.BackColor = ThemeColor.changeColorBrightness(color, -0.3);
                    btn_CloseChildForm.Visible = true;
                }
            }
        }

        private Dictionary<Button, Color> buttonOriginalColors = new Dictionary<Button, Color>();

        private void DisableButton()
        {
            foreach (Control previousBtn in panel_Menu.Controls)
            {
                if (previousBtn is Button)
                {
                    var button = (Button)previousBtn;

                    // Kiểm tra xem button đã lưu màu ban đầu chưa
                    if (buttonOriginalColors.ContainsKey(button))
                    {
                        button.BackColor = buttonOriginalColors[button];
                    }
                    else
                    {
                        buttonOriginalColors.Add(button, button.BackColor);
                    }

                    button.ForeColor = Color.Black;
                    button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            lbl_Title.Text = "QUẢN LÝ SẢN PHẨM";
        }
        private void btn_KhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormKhachHang(), sender);
            lbl_Title.Text = "QUẢN LÝ KHÁCH HÀNG";
        }

        private void btn_QLHoaDon_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormQLHoaDon(), sender);
            lbl_Title.Text = "QUẢN LÝ HÓA ĐƠN";
        }

        private void btn_DoanhThu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormDoanhThu(), sender);
            lbl_Title.Text = "QUẢN LÝ SẢN PHẨM";
        }
        private void btn_LapHoaDon_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormHoaDon(), sender);
            lbl_Title.Text = "LẬP HÓA ĐƠN";
        }
        private void btn_CloseChildForm_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
            Reset();
        }

        private void Reset()
        {
            DisableButton();
            lbl_Title.Text = "WELCOME";
            panel_TitleBar.BackColor = Color.Cyan;
            panel_Logo.BackColor = Color.Cyan;
            currentButton = null;
            btn_CloseChildForm.Visible = false;
        }

        private void btn_DangXuat_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có muốn đăng xuất?", "Đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                FormDangNhap frmLogin = new FormDangNhap();
                frmLogin.Show();
                this.Close();
            }
        }

        private void vbButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string ConnStr = "Data Source=NGUYEN-VAN-PHU\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(ConnStr))
                {
                    connection.Open();

                    // Đặt cơ sở dữ liệu vào chế độ RECOVERY FULL
                    using (SqlCommand alterCommand = new SqlCommand("USE QL_SHOPTHOITRANG ALTER DATABASE QL_SHOPTHOITRANG SET RECOVERY FULL", connection))
                    {
                        alterCommand.ExecuteNonQuery();
                    }

                    // Thực hiện backup cơ sở dữ liệu
                    string backupPath = "D:\\APP_QL_SHOPTHOITRANG_FULL.bak";
                    string backupCommandText = $"BACKUP DATABASE QL_SHOPTHOITRANG TO DISK = '{backupPath}'";

                    using (SqlCommand backupCommand = new SqlCommand(backupCommandText, connection))
                    {
                        backupCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Backup cơ sở dữ liệu QL_SHOPTHOITRANG thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void vbButton2_Click(object sender, EventArgs e)
        {
            try
            {
                string ConnStr = "Data Source=NGUYEN-VAN-PHU\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(ConnStr))
                {
                    connection.Open();

                    // Đặt tên cơ sở dữ liệu và đường dẫn tập tin sao lưu
                    string databaseName = "QL_SHOPTHOITRANG";
                    string backupPath = "D:\\APP_QL_SHOPTHOITRANG_FULL.bak";

                    // Kiểm tra xem cơ sở dữ liệu đã tồn tại hay chưa
                    string checkDatabaseExistenceQuery = $"SELECT 1 FROM sys.databases WHERE name = '{databaseName}'";
                    using (SqlCommand checkExistenceCommand = new SqlCommand(checkDatabaseExistenceQuery, connection))
                    {
                        bool databaseExists = Convert.ToBoolean(checkExistenceCommand.ExecuteScalar());

                        if (databaseExists)
                        {
                            // Đóng tất cả các kết nối đến cơ sở dữ liệu
                            string killConnectionsQuery = $@"
                    USE master;
                    ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                            using (SqlCommand killConnectionsCommand = new SqlCommand(killConnectionsQuery, connection))
                            {
                                killConnectionsCommand.ExecuteNonQuery();
                            }

                            // Phục hồi cơ sở dữ liệu từ tập tin sao lưu
                            string restoreQuery = $@"
                    RESTORE DATABASE [{databaseName}] 
                    FROM DISK = '{backupPath}'
                    WITH REPLACE;";
                            using (SqlCommand restoreCommand = new SqlCommand(restoreQuery, connection))
                            {
                                restoreCommand.ExecuteNonQuery();
                            }

                            // Đặt lại chế độ multi-user cho cơ sở dữ liệu
                            string setMultiUserQuery = $@"
                    USE master;
                    ALTER DATABASE [{databaseName}] SET MULTI_USER;";
                            using (SqlCommand setMultiUserCommand = new SqlCommand(setMultiUserQuery, connection))
                            {
                                setMultiUserCommand.ExecuteNonQuery();
                            }

                            MessageBox.Show($"Khôi phục cơ sở dữ liệu {databaseName} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"Cơ sở dữ liệu {databaseName} không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_DangXuat_Click_1(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có muốn đăng xuất?", "Đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                //tắt form admin không ẩn
                this.Close();
                FormDangNhap frmLogin = new FormDangNhap();
                frmLogin.maNVLogin = "";
                frmLogin.Show();
                this.Close();
            }
        }

    }
}
