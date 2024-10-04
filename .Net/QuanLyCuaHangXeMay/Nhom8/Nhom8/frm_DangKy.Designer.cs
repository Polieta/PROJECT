namespace Nhom8
{
    partial class frm_DangKy
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbx_TenTK = new System.Windows.Forms.TextBox();
            this.tbx_Mk = new System.Windows.Forms.TextBox();
            this.tbx_NhapMK = new System.Windows.Forms.TextBox();
            this.btn_Thoat = new System.Windows.Forms.Button();
            this.btn_Luu = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbx_IdTK = new System.Windows.Forms.TextBox();
            this.cbx_IdNV = new System.Windows.Forms.ComboBox();
            this.btn_DangNhap = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(228, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(421, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đăng ký thêm tài khoản nhân viên";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(101, 241);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tên tài khoản:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(101, 316);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mật khẩu:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(101, 392);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(201, 24);
            this.label4.TabIndex = 4;
            this.label4.Text = "Nhập lại mật khẩu:";
            // 
            // tbx_TenTK
            // 
            this.tbx_TenTK.Location = new System.Drawing.Point(355, 233);
            this.tbx_TenTK.Name = "tbx_TenTK";
            this.tbx_TenTK.Size = new System.Drawing.Size(268, 32);
            this.tbx_TenTK.TabIndex = 1;
            // 
            // tbx_Mk
            // 
            this.tbx_Mk.Location = new System.Drawing.Point(355, 308);
            this.tbx_Mk.Name = "tbx_Mk";
            this.tbx_Mk.Size = new System.Drawing.Size(268, 32);
            this.tbx_Mk.TabIndex = 3;
            // 
            // tbx_NhapMK
            // 
            this.tbx_NhapMK.Location = new System.Drawing.Point(355, 384);
            this.tbx_NhapMK.Name = "tbx_NhapMK";
            this.tbx_NhapMK.Size = new System.Drawing.Size(268, 32);
            this.tbx_NhapMK.TabIndex = 5;
            this.tbx_NhapMK.Leave += new System.EventHandler(this.tbx_NhapMK_TextChanged);
            // 
            // btn_Thoat
            // 
            this.btn_Thoat.Location = new System.Drawing.Point(755, 462);
            this.btn_Thoat.Name = "btn_Thoat";
            this.btn_Thoat.Size = new System.Drawing.Size(122, 49);
            this.btn_Thoat.TabIndex = 9;
            this.btn_Thoat.Text = "THOÁT";
            this.btn_Thoat.UseVisualStyleBackColor = true;
            this.btn_Thoat.Click += new System.EventHandler(this.btn_Thoat_Click);
            // 
            // btn_Luu
            // 
            this.btn_Luu.Location = new System.Drawing.Point(590, 462);
            this.btn_Luu.Name = "btn_Luu";
            this.btn_Luu.Size = new System.Drawing.Size(122, 49);
            this.btn_Luu.TabIndex = 8;
            this.btn_Luu.Text = "LƯU";
            this.btn_Luu.UseVisualStyleBackColor = true;
            this.btn_Luu.Click += new System.EventHandler(this.btn_Luu_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(101, 171);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 24);
            this.label6.TabIndex = 0;
            this.label6.Text = "Id Nhân viên:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(101, 99);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(146, 24);
            this.label7.TabIndex = 0;
            this.label7.Text = "Id Tài Khoản:";
            // 
            // tbx_IdTK
            // 
            this.tbx_IdTK.Location = new System.Drawing.Point(355, 91);
            this.tbx_IdTK.Name = "tbx_IdTK";
            this.tbx_IdTK.Size = new System.Drawing.Size(268, 32);
            this.tbx_IdTK.TabIndex = 1;
            // 
            // cbx_IdNV
            // 
            this.cbx_IdNV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_IdNV.FormattingEnabled = true;
            this.cbx_IdNV.Location = new System.Drawing.Point(355, 163);
            this.cbx_IdNV.Name = "cbx_IdNV";
            this.cbx_IdNV.Size = new System.Drawing.Size(268, 32);
            this.cbx_IdNV.TabIndex = 10;
            this.cbx_IdNV.SelectedIndexChanged += new System.EventHandler(this.cbx_IdNV_SelectedIndexChanged);
            // 
            // btn_DangNhap
            // 
            this.btn_DangNhap.Location = new System.Drawing.Point(411, 452);
            this.btn_DangNhap.Name = "btn_DangNhap";
            this.btn_DangNhap.Size = new System.Drawing.Size(145, 69);
            this.btn_DangNhap.TabIndex = 8;
            this.btn_DangNhap.Text = "QUAY VỀ ĐĂNG NHẬP";
            this.btn_DangNhap.UseVisualStyleBackColor = true;
            this.btn_DangNhap.Click += new System.EventHandler(this.btn_DangNhap_Click);
            // 
            // frm_DangKy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 543);
            this.Controls.Add(this.cbx_IdNV);
            this.Controls.Add(this.btn_DangNhap);
            this.Controls.Add(this.btn_Luu);
            this.Controls.Add(this.btn_Thoat);
            this.Controls.Add(this.tbx_NhapMK);
            this.Controls.Add(this.tbx_Mk);
            this.Controls.Add(this.tbx_IdTK);
            this.Controls.Add(this.tbx_TenTK);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frm_DangKy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_DangKy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_DangKy_FormClosing);
            this.Load += new System.EventHandler(this.frm_DangKy_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbx_TenTK;
        private System.Windows.Forms.TextBox tbx_Mk;
        private System.Windows.Forms.TextBox tbx_NhapMK;
        private System.Windows.Forms.Button btn_Thoat;
        private System.Windows.Forms.Button btn_Luu;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbx_IdTK;
        private System.Windows.Forms.ComboBox cbx_IdNV;
        private System.Windows.Forms.Button btn_DangNhap;
    }
}