namespace Nhom8
{
    partial class frm_dangnhap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_dangnhap));
            this.lbl_Tendangnhap = new System.Windows.Forms.Label();
            this.lbl_Matkhau = new System.Windows.Forms.Label();
            this.tbx_Tendangnhap = new System.Windows.Forms.TextBox();
            this.tbx_Matkhau = new System.Windows.Forms.TextBox();
            this.btn_Thoat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_DangNhap = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_Tendangnhap
            // 
            this.lbl_Tendangnhap.AutoSize = true;
            this.lbl_Tendangnhap.Font = new System.Drawing.Font("Microsoft YaHei UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Tendangnhap.Location = new System.Drawing.Point(58, 195);
            this.lbl_Tendangnhap.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_Tendangnhap.Name = "lbl_Tendangnhap";
            this.lbl_Tendangnhap.Size = new System.Drawing.Size(217, 37);
            this.lbl_Tendangnhap.TabIndex = 0;
            this.lbl_Tendangnhap.Text = "Tên tài khoản:";
            // 
            // lbl_Matkhau
            // 
            this.lbl_Matkhau.AutoSize = true;
            this.lbl_Matkhau.Font = new System.Drawing.Font("Microsoft YaHei UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Matkhau.Location = new System.Drawing.Point(58, 289);
            this.lbl_Matkhau.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_Matkhau.Name = "lbl_Matkhau";
            this.lbl_Matkhau.Size = new System.Drawing.Size(161, 37);
            this.lbl_Matkhau.TabIndex = 1;
            this.lbl_Matkhau.Text = "Mật khẩu:";
            // 
            // tbx_Tendangnhap
            // 
            this.tbx_Tendangnhap.Location = new System.Drawing.Point(332, 195);
            this.tbx_Tendangnhap.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tbx_Tendangnhap.Name = "tbx_Tendangnhap";
            this.tbx_Tendangnhap.Size = new System.Drawing.Size(364, 37);
            this.tbx_Tendangnhap.TabIndex = 0;
            // 
            // tbx_Matkhau
            // 
            this.tbx_Matkhau.Location = new System.Drawing.Point(332, 289);
            this.tbx_Matkhau.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tbx_Matkhau.Name = "tbx_Matkhau";
            this.tbx_Matkhau.Size = new System.Drawing.Size(364, 37);
            this.tbx_Matkhau.TabIndex = 1;
            this.tbx_Matkhau.UseSystemPasswordChar = true;
            // 
            // btn_Thoat
            // 
            this.btn_Thoat.Font = new System.Drawing.Font("Microsoft YaHei UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Thoat.Image = global::Nhom8.Properties.Resources.Hopstarter_Rounded_Square_Button_Delete_16;
            this.btn_Thoat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Thoat.Location = new System.Drawing.Point(543, 393);
            this.btn_Thoat.Name = "btn_Thoat";
            this.btn_Thoat.Size = new System.Drawing.Size(130, 43);
            this.btn_Thoat.TabIndex = 3;
            this.btn_Thoat.Text = "Thoát";
            this.btn_Thoat.UseVisualStyleBackColor = true;
            this.btn_Thoat.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MingLiU-ExtB", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(165, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(611, 43);
            this.label1.TabIndex = 4;
            this.label1.Text = "MỜI ĐĂNG NHẬP ĐỂ TIẾP TỤC";
            // 
            // btn_DangNhap
            // 
            this.btn_DangNhap.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_DangNhap.Font = new System.Drawing.Font("Microsoft YaHei UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DangNhap.Image = global::Nhom8.Properties.Resources.Icons8_Windows_8_User_Interface_Login_16;
            this.btn_DangNhap.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_DangNhap.Location = new System.Drawing.Point(173, 393);
            this.btn_DangNhap.Name = "btn_DangNhap";
            this.btn_DangNhap.Size = new System.Drawing.Size(175, 43);
            this.btn_DangNhap.TabIndex = 2;
            this.btn_DangNhap.Text = "Đăng nhập";
            this.btn_DangNhap.UseVisualStyleBackColor = true;
            this.btn_DangNhap.Click += new System.EventHandler(this.button1_Click);
            // 
            // frm_dangnhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(817, 478);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Thoat);
            this.Controls.Add(this.btn_DangNhap);
            this.Controls.Add(this.tbx_Matkhau);
            this.Controls.Add(this.tbx_Tendangnhap);
            this.Controls.Add(this.lbl_Matkhau);
            this.Controls.Add(this.lbl_Tendangnhap);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "frm_dangnhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_dangnhap_FormClosing);
            this.Load += new System.EventHandler(this.frm_dangnhap_Load);
            this.Enter += new System.EventHandler(this.frm_dangnhap_Enter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Tendangnhap;
        private System.Windows.Forms.Label lbl_Matkhau;
        private System.Windows.Forms.TextBox tbx_Tendangnhap;
        private System.Windows.Forms.TextBox tbx_Matkhau;
        private System.Windows.Forms.Button btn_DangNhap;
        private System.Windows.Forms.Button btn_Thoat;
        private System.Windows.Forms.Label label1;
    }
}

