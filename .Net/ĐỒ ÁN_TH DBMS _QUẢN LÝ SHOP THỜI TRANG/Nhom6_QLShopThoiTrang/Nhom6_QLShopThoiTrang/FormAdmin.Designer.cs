
namespace Nhom6_QLShopThoiTrang
{
    partial class FormAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAdmin));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_DesktopPane = new System.Windows.Forms.Panel();
            this.panel_TitleBar = new System.Windows.Forms.Panel();
            this.btn_CloseChildForm = new System.Windows.Forms.Button();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.panel_Logo = new System.Windows.Forms.Panel();
            this.panel_Menu = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.vbButton2 = new Nhom6_QLShopThoiTrang.VBButton();
            this.vbButton1 = new Nhom6_QLShopThoiTrang.VBButton();
            this.btn_DoanhThu = new Nhom6_QLShopThoiTrang.VBButton();
            this.btn_QLHoaDon = new Nhom6_QLShopThoiTrang.VBButton();
            this.btn_LapHoaDon = new Nhom6_QLShopThoiTrang.VBButton();
            this.btn_KhachHang = new Nhom6_QLShopThoiTrang.VBButton();
            this.btn_SanPham = new Nhom6_QLShopThoiTrang.VBButton();
            this.btn_DangXuat = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel_DesktopPane.SuspendLayout();
            this.panel_TitleBar.SuspendLayout();
            this.panel_Logo.SuspendLayout();
            this.panel_Menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(406, 143);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(351, 55);
            this.label1.TabIndex = 1;
            this.label1.Text = "WELCOME TO";
            // 
            // panel_DesktopPane
            // 
            this.panel_DesktopPane.Controls.Add(this.panel_TitleBar);
            this.panel_DesktopPane.Controls.Add(this.label1);
            this.panel_DesktopPane.Controls.Add(this.pictureBox2);
            this.panel_DesktopPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_DesktopPane.Location = new System.Drawing.Point(200, 0);
            this.panel_DesktopPane.Margin = new System.Windows.Forms.Padding(2);
            this.panel_DesktopPane.Name = "panel_DesktopPane";
            this.panel_DesktopPane.Size = new System.Drawing.Size(1135, 681);
            this.panel_DesktopPane.TabIndex = 5;
            // 
            // panel_TitleBar
            // 
            this.panel_TitleBar.BackColor = System.Drawing.Color.Cyan;
            this.panel_TitleBar.Controls.Add(this.btn_CloseChildForm);
            this.panel_TitleBar.Controls.Add(this.lbl_Title);
            this.panel_TitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_TitleBar.Location = new System.Drawing.Point(0, 0);
            this.panel_TitleBar.Margin = new System.Windows.Forms.Padding(2);
            this.panel_TitleBar.Name = "panel_TitleBar";
            this.panel_TitleBar.Size = new System.Drawing.Size(1135, 87);
            this.panel_TitleBar.TabIndex = 2;
            // 
            // btn_CloseChildForm
            // 
            this.btn_CloseChildForm.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_CloseChildForm.FlatAppearance.BorderSize = 0;
            this.btn_CloseChildForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CloseChildForm.Font = new System.Drawing.Font("Showcard Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CloseChildForm.Location = new System.Drawing.Point(0, 0);
            this.btn_CloseChildForm.Margin = new System.Windows.Forms.Padding(2);
            this.btn_CloseChildForm.Name = "btn_CloseChildForm";
            this.btn_CloseChildForm.Size = new System.Drawing.Size(50, 87);
            this.btn_CloseChildForm.TabIndex = 1;
            this.btn_CloseChildForm.Text = "X";
            this.btn_CloseChildForm.UseVisualStyleBackColor = true;
            this.btn_CloseChildForm.Click += new System.EventHandler(this.btn_CloseChildForm_Click);
            // 
            // lbl_Title
            // 
            this.lbl_Title.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.Location = new System.Drawing.Point(497, 34);
            this.lbl_Title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(133, 26);
            this.lbl_Title.TabIndex = 0;
            this.lbl_Title.Text = "WELCOME";
            // 
            // panel_Logo
            // 
            this.panel_Logo.BackColor = System.Drawing.Color.Aqua;
            this.panel_Logo.Controls.Add(this.pictureBox1);
            this.panel_Logo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Logo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel_Logo.Location = new System.Drawing.Point(0, 0);
            this.panel_Logo.Margin = new System.Windows.Forms.Padding(2);
            this.panel_Logo.Name = "panel_Logo";
            this.panel_Logo.Size = new System.Drawing.Size(200, 141);
            this.panel_Logo.TabIndex = 0;
            // 
            // panel_Menu
            // 
            this.panel_Menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel_Menu.Controls.Add(this.vbButton2);
            this.panel_Menu.Controls.Add(this.vbButton1);
            this.panel_Menu.Controls.Add(this.btn_DoanhThu);
            this.panel_Menu.Controls.Add(this.btn_QLHoaDon);
            this.panel_Menu.Controls.Add(this.btn_LapHoaDon);
            this.panel_Menu.Controls.Add(this.btn_KhachHang);
            this.panel_Menu.Controls.Add(this.btn_SanPham);
            this.panel_Menu.Controls.Add(this.btn_DangXuat);
            this.panel_Menu.Controls.Add(this.panel_Logo);
            this.panel_Menu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Menu.Location = new System.Drawing.Point(0, 0);
            this.panel_Menu.Margin = new System.Windows.Forms.Padding(2);
            this.panel_Menu.Name = "panel_Menu";
            this.panel_Menu.Size = new System.Drawing.Size(200, 681);
            this.panel_Menu.TabIndex = 3;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(422, 312);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(319, 228);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // vbButton2
            // 
            this.vbButton2.BackColor = System.Drawing.Color.White;
            this.vbButton2.BackgroundColor = System.Drawing.Color.White;
            this.vbButton2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.vbButton2.BorderRadius = 20;
            this.vbButton2.BorderSize = 0;
            this.vbButton2.Dock = System.Windows.Forms.DockStyle.Top;
            this.vbButton2.FlatAppearance.BorderSize = 0;
            this.vbButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.vbButton2.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbButton2.ForeColor = System.Drawing.Color.Black;
            this.vbButton2.Image = global::Nhom6_QLShopThoiTrang.Properties.Resources.icons8_restore_35;
            this.vbButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.vbButton2.Location = new System.Drawing.Point(0, 483);
            this.vbButton2.Margin = new System.Windows.Forms.Padding(2);
            this.vbButton2.Name = "vbButton2";
            this.vbButton2.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.vbButton2.Size = new System.Drawing.Size(200, 57);
            this.vbButton2.TabIndex = 13;
            this.vbButton2.Text = "Khôi Phục";
            this.vbButton2.TextColor = System.Drawing.Color.Black;
            this.vbButton2.UseVisualStyleBackColor = false;
            this.vbButton2.Click += new System.EventHandler(this.vbButton2_Click);
            // 
            // vbButton1
            // 
            this.vbButton1.BackColor = System.Drawing.Color.White;
            this.vbButton1.BackgroundColor = System.Drawing.Color.White;
            this.vbButton1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.vbButton1.BorderRadius = 20;
            this.vbButton1.BorderSize = 0;
            this.vbButton1.Dock = System.Windows.Forms.DockStyle.Top;
            this.vbButton1.FlatAppearance.BorderSize = 0;
            this.vbButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.vbButton1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbButton1.ForeColor = System.Drawing.Color.Black;
            this.vbButton1.Image = global::Nhom6_QLShopThoiTrang.Properties.Resources.icons8_backup_35;
            this.vbButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.vbButton1.Location = new System.Drawing.Point(0, 426);
            this.vbButton1.Margin = new System.Windows.Forms.Padding(2);
            this.vbButton1.Name = "vbButton1";
            this.vbButton1.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.vbButton1.Size = new System.Drawing.Size(200, 57);
            this.vbButton1.TabIndex = 12;
            this.vbButton1.Text = "Backup";
            this.vbButton1.TextColor = System.Drawing.Color.Black;
            this.vbButton1.UseVisualStyleBackColor = false;
            this.vbButton1.Click += new System.EventHandler(this.vbButton1_Click);
            // 
            // btn_DoanhThu
            // 
            this.btn_DoanhThu.BackColor = System.Drawing.Color.White;
            this.btn_DoanhThu.BackgroundColor = System.Drawing.Color.White;
            this.btn_DoanhThu.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_DoanhThu.BorderRadius = 20;
            this.btn_DoanhThu.BorderSize = 0;
            this.btn_DoanhThu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_DoanhThu.FlatAppearance.BorderSize = 0;
            this.btn_DoanhThu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DoanhThu.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DoanhThu.ForeColor = System.Drawing.Color.Black;
            this.btn_DoanhThu.Image = ((System.Drawing.Image)(resources.GetObject("btn_DoanhThu.Image")));
            this.btn_DoanhThu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_DoanhThu.Location = new System.Drawing.Point(0, 369);
            this.btn_DoanhThu.Margin = new System.Windows.Forms.Padding(2);
            this.btn_DoanhThu.Name = "btn_DoanhThu";
            this.btn_DoanhThu.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.btn_DoanhThu.Size = new System.Drawing.Size(200, 57);
            this.btn_DoanhThu.TabIndex = 11;
            this.btn_DoanhThu.Text = "Doanh Thu";
            this.btn_DoanhThu.TextColor = System.Drawing.Color.Black;
            this.btn_DoanhThu.UseVisualStyleBackColor = false;
            this.btn_DoanhThu.Click += new System.EventHandler(this.btn_DoanhThu_Click);
            // 
            // btn_QLHoaDon
            // 
            this.btn_QLHoaDon.BackColor = System.Drawing.Color.White;
            this.btn_QLHoaDon.BackgroundColor = System.Drawing.Color.White;
            this.btn_QLHoaDon.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_QLHoaDon.BorderRadius = 20;
            this.btn_QLHoaDon.BorderSize = 0;
            this.btn_QLHoaDon.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_QLHoaDon.FlatAppearance.BorderSize = 0;
            this.btn_QLHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_QLHoaDon.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_QLHoaDon.ForeColor = System.Drawing.Color.Black;
            this.btn_QLHoaDon.Image = ((System.Drawing.Image)(resources.GetObject("btn_QLHoaDon.Image")));
            this.btn_QLHoaDon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_QLHoaDon.Location = new System.Drawing.Point(0, 312);
            this.btn_QLHoaDon.Margin = new System.Windows.Forms.Padding(2);
            this.btn_QLHoaDon.Name = "btn_QLHoaDon";
            this.btn_QLHoaDon.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.btn_QLHoaDon.Size = new System.Drawing.Size(200, 57);
            this.btn_QLHoaDon.TabIndex = 10;
            this.btn_QLHoaDon.Text = "QL Hóa Đơn";
            this.btn_QLHoaDon.TextColor = System.Drawing.Color.Black;
            this.btn_QLHoaDon.UseVisualStyleBackColor = false;
            this.btn_QLHoaDon.Click += new System.EventHandler(this.btn_QLHoaDon_Click);
            // 
            // btn_LapHoaDon
            // 
            this.btn_LapHoaDon.BackColor = System.Drawing.Color.White;
            this.btn_LapHoaDon.BackgroundColor = System.Drawing.Color.White;
            this.btn_LapHoaDon.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_LapHoaDon.BorderRadius = 20;
            this.btn_LapHoaDon.BorderSize = 0;
            this.btn_LapHoaDon.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_LapHoaDon.FlatAppearance.BorderSize = 0;
            this.btn_LapHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LapHoaDon.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LapHoaDon.ForeColor = System.Drawing.Color.Black;
            this.btn_LapHoaDon.Image = ((System.Drawing.Image)(resources.GetObject("btn_LapHoaDon.Image")));
            this.btn_LapHoaDon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_LapHoaDon.Location = new System.Drawing.Point(0, 255);
            this.btn_LapHoaDon.Margin = new System.Windows.Forms.Padding(2);
            this.btn_LapHoaDon.Name = "btn_LapHoaDon";
            this.btn_LapHoaDon.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.btn_LapHoaDon.Size = new System.Drawing.Size(200, 57);
            this.btn_LapHoaDon.TabIndex = 9;
            this.btn_LapHoaDon.Text = "Lập Hóa Đơn";
            this.btn_LapHoaDon.TextColor = System.Drawing.Color.Black;
            this.btn_LapHoaDon.UseVisualStyleBackColor = false;
            this.btn_LapHoaDon.Click += new System.EventHandler(this.btn_LapHoaDon_Click);
            // 
            // btn_KhachHang
            // 
            this.btn_KhachHang.BackColor = System.Drawing.Color.White;
            this.btn_KhachHang.BackgroundColor = System.Drawing.Color.White;
            this.btn_KhachHang.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_KhachHang.BorderRadius = 20;
            this.btn_KhachHang.BorderSize = 0;
            this.btn_KhachHang.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_KhachHang.FlatAppearance.BorderSize = 0;
            this.btn_KhachHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_KhachHang.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_KhachHang.ForeColor = System.Drawing.Color.Black;
            this.btn_KhachHang.Image = ((System.Drawing.Image)(resources.GetObject("btn_KhachHang.Image")));
            this.btn_KhachHang.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_KhachHang.Location = new System.Drawing.Point(0, 198);
            this.btn_KhachHang.Margin = new System.Windows.Forms.Padding(2);
            this.btn_KhachHang.Name = "btn_KhachHang";
            this.btn_KhachHang.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.btn_KhachHang.Size = new System.Drawing.Size(200, 57);
            this.btn_KhachHang.TabIndex = 8;
            this.btn_KhachHang.Text = "Khách hàng";
            this.btn_KhachHang.TextColor = System.Drawing.Color.Black;
            this.btn_KhachHang.UseVisualStyleBackColor = false;
            this.btn_KhachHang.Click += new System.EventHandler(this.btn_KhachHang_Click);
            // 
            // btn_SanPham
            // 
            this.btn_SanPham.BackColor = System.Drawing.Color.White;
            this.btn_SanPham.BackgroundColor = System.Drawing.Color.White;
            this.btn_SanPham.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_SanPham.BorderRadius = 20;
            this.btn_SanPham.BorderSize = 0;
            this.btn_SanPham.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_SanPham.FlatAppearance.BorderSize = 0;
            this.btn_SanPham.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SanPham.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SanPham.ForeColor = System.Drawing.Color.Black;
            this.btn_SanPham.Image = ((System.Drawing.Image)(resources.GetObject("btn_SanPham.Image")));
            this.btn_SanPham.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_SanPham.Location = new System.Drawing.Point(0, 141);
            this.btn_SanPham.Margin = new System.Windows.Forms.Padding(2);
            this.btn_SanPham.Name = "btn_SanPham";
            this.btn_SanPham.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.btn_SanPham.Size = new System.Drawing.Size(200, 57);
            this.btn_SanPham.TabIndex = 7;
            this.btn_SanPham.Text = " Sản phẩm";
            this.btn_SanPham.TextColor = System.Drawing.Color.Black;
            this.btn_SanPham.UseVisualStyleBackColor = false;
            this.btn_SanPham.Click += new System.EventHandler(this.btn_SanPham_Click);
            // 
            // btn_DangXuat
            // 
            this.btn_DangXuat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_DangXuat.FlatAppearance.BorderSize = 0;
            this.btn_DangXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DangXuat.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DangXuat.Image = ((System.Drawing.Image)(resources.GetObject("btn_DangXuat.Image")));
            this.btn_DangXuat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_DangXuat.Location = new System.Drawing.Point(0, 624);
            this.btn_DangXuat.Margin = new System.Windows.Forms.Padding(2);
            this.btn_DangXuat.Name = "btn_DangXuat";
            this.btn_DangXuat.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btn_DangXuat.Size = new System.Drawing.Size(200, 57);
            this.btn_DangXuat.TabIndex = 6;
            this.btn_DangXuat.Text = "   Đăng xuất";
            this.btn_DangXuat.UseVisualStyleBackColor = true;
            this.btn_DangXuat.Click += new System.EventHandler(this.btn_DangXuat_Click_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 141);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // FormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1335, 681);
            this.Controls.Add(this.panel_DesktopPane);
            this.Controls.Add(this.panel_Menu);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormAdmin";
            this.Text = "Trang Admin";
            this.Load += new System.EventHandler(this.FormAdmin_Load);
            this.panel_DesktopPane.ResumeLayout(false);
            this.panel_DesktopPane.PerformLayout();
            this.panel_TitleBar.ResumeLayout(false);
            this.panel_TitleBar.PerformLayout();
            this.panel_Logo.ResumeLayout(false);
            this.panel_Menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel_DesktopPane;
        private System.Windows.Forms.Panel panel_Logo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_DangXuat;
        private System.Windows.Forms.Panel panel_Menu;
        private System.Windows.Forms.Panel panel_TitleBar;
        private System.Windows.Forms.Button btn_CloseChildForm;
        private System.Windows.Forms.Label lbl_Title;
        private VBButton btn_SanPham;
        private VBButton btn_DoanhThu;
        private VBButton btn_QLHoaDon;
        private VBButton btn_LapHoaDon;
        private VBButton btn_KhachHang;
        private VBButton vbButton2;
        private VBButton vbButton1;
    }
}