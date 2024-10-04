namespace GiaoDien_SanBong
{
    partial class FormThongTinCaNhan
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormThongTinCaNhan));
            this.imgList_TrangChu = new System.Windows.Forms.ImageList(this.components);
            this.mskNgaySinh = new System.Windows.Forms.MaskedTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtGioiTinh = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.txtCMND = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTenNV = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCapNhatMK = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMKMoiNhapLai = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMKmoi = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgList_TrangChu
            // 
            this.imgList_TrangChu.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList_TrangChu.ImageStream")));
            this.imgList_TrangChu.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList_TrangChu.Images.SetKeyName(0, "emoji_DatSan.png");
            this.imgList_TrangChu.Images.SetKeyName(1, "emoji_Bill.png");
            this.imgList_TrangChu.Images.SetKeyName(2, "emoji_Chart.png");
            this.imgList_TrangChu.Images.SetKeyName(3, "emoji_Check.png");
            this.imgList_TrangChu.Images.SetKeyName(4, "emoji_customer.png");
            this.imgList_TrangChu.Images.SetKeyName(5, "emoji_food.png");
            this.imgList_TrangChu.Images.SetKeyName(6, "emoji_football.png");
            this.imgList_TrangChu.Images.SetKeyName(7, "emoji_money.png");
            this.imgList_TrangChu.Images.SetKeyName(8, "emoji_rules.png");
            this.imgList_TrangChu.Images.SetKeyName(9, "emoji_User.png");
            this.imgList_TrangChu.Images.SetKeyName(10, "emoji_BillManage.png");
            this.imgList_TrangChu.Images.SetKeyName(11, "iccon-user.png");
            this.imgList_TrangChu.Images.SetKeyName(12, "icon-exit.png");
            this.imgList_TrangChu.Images.SetKeyName(13, "icon-info.png");
            this.imgList_TrangChu.Images.SetKeyName(14, "icon-menu.png");
            this.imgList_TrangChu.Images.SetKeyName(15, "icons8-user-100.png");
            // 
            // mskNgaySinh
            // 
            this.mskNgaySinh.Location = new System.Drawing.Point(38, 217);
            this.mskNgaySinh.Mask = "0000/00/00";
            this.mskNgaySinh.Name = "mskNgaySinh";
            this.mskNgaySinh.Size = new System.Drawing.Size(246, 22);
            this.mskNgaySinh.TabIndex = 22;
            this.mskNgaySinh.ValidatingType = typeof(System.DateTime);
            this.mskNgaySinh.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.mskNgaySinh_MaskInputRejected);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(41, 244);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 16);
            this.label19.TabIndex = 20;
            this.label19.Text = "Quê quán";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(38, 266);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(246, 22);
            this.txtDiaChi.TabIndex = 19;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(34, 195);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(67, 16);
            this.label22.TabIndex = 2;
            this.label22.Text = "Ngày sinh";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtGioiTinh);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.mskNgaySinh);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.btnCapNhat);
            this.groupBox2.Controls.Add(this.txtDiaChi);
            this.groupBox2.Controls.Add(this.txtCMND);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtSDT);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtTenNV);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(251, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(548, 421);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // txtGioiTinh
            // 
            this.txtGioiTinh.Location = new System.Drawing.Point(38, 67);
            this.txtGioiTinh.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtGioiTinh.Name = "txtGioiTinh";
            this.txtGioiTinh.Size = new System.Drawing.Size(246, 22);
            this.txtGioiTinh.TabIndex = 30;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.SlateGray;
            this.button3.ImageIndex = 15;
            this.button3.ImageList = this.imgList_TrangChu;
            this.button3.Location = new System.Drawing.Point(325, 67);
            this.button3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(189, 175);
            this.button3.TabIndex = 18;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.BackColor = System.Drawing.Color.MediumAquamarine;
            this.btnCapNhat.FlatAppearance.BorderSize = 0;
            this.btnCapNhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapNhat.ForeColor = System.Drawing.Color.White;
            this.btnCapNhat.Location = new System.Drawing.Point(289, 365);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(246, 38);
            this.btnCapNhat.TabIndex = 29;
            this.btnCapNhat.Text = "Lưu thay đổi";
            this.btnCapNhat.UseVisualStyleBackColor = false;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            // 
            // txtCMND
            // 
            this.txtCMND.Location = new System.Drawing.Point(38, 168);
            this.txtCMND.Name = "txtCMND";
            this.txtCMND.Size = new System.Drawing.Size(246, 22);
            this.txtCMND.TabIndex = 28;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 16);
            this.label5.TabIndex = 27;
            this.label5.Text = "Số chứng minh thư :";
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(38, 119);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(246, 22);
            this.txtSDT.TabIndex = 26;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(33, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 16);
            this.label8.TabIndex = 25;
            this.label8.Text = "Số điện thoại :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(34, 128);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(0, 16);
            this.label12.TabIndex = 18;
            // 
            // txtTenNV
            // 
            this.txtTenNV.Location = new System.Drawing.Point(325, 266);
            this.txtTenNV.Name = "txtTenNV";
            this.txtTenNV.Size = new System.Drawing.Size(189, 22);
            this.txtTenNV.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(34, 47);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 16);
            this.label13.TabIndex = 3;
            this.label13.Text = "Giới tính :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(374, 244);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(94, 16);
            this.label14.TabIndex = 2;
            this.label14.Text = "Tên nhân viên ";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label15.Location = new System.Drawing.Point(9, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(128, 16);
            this.label15.TabIndex = 0;
            this.label15.Text = "Thông tin nhân viên :";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCapNhatMK);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtMKMoiNhapLai);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtMKmoi);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtMatKhau);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(12, 68);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(233, 421);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            // 
            // btnCapNhatMK
            // 
            this.btnCapNhatMK.BackColor = System.Drawing.Color.MediumAquamarine;
            this.btnCapNhatMK.FlatAppearance.BorderSize = 0;
            this.btnCapNhatMK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhatMK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapNhatMK.ForeColor = System.Drawing.Color.White;
            this.btnCapNhatMK.Location = new System.Drawing.Point(30, 194);
            this.btnCapNhatMK.Name = "btnCapNhatMK";
            this.btnCapNhatMK.Size = new System.Drawing.Size(176, 23);
            this.btnCapNhatMK.TabIndex = 26;
            this.btnCapNhatMK.Text = "Lưu thay đổi";
            this.btnCapNhatMK.UseVisualStyleBackColor = false;
            this.btnCapNhatMK.Click += new System.EventHandler(this.btnCapNhatMK_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 16);
            this.label6.TabIndex = 25;
            this.label6.Text = "Xác nhận mật khẩu mới :";
            // 
            // txtMKMoiNhapLai
            // 
            this.txtMKMoiNhapLai.Location = new System.Drawing.Point(33, 150);
            this.txtMKMoiNhapLai.Name = "txtMKMoiNhapLai";
            this.txtMKMoiNhapLai.Size = new System.Drawing.Size(173, 22);
            this.txtMKMoiNhapLai.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 16);
            this.label2.TabIndex = 23;
            this.label2.Text = "Mật khẩu mới :";
            // 
            // txtMKmoi
            // 
            this.txtMKmoi.Location = new System.Drawing.Point(31, 96);
            this.txtMKmoi.Name = "txtMKmoi";
            this.txtMKmoi.Size = new System.Drawing.Size(173, 22);
            this.txtMKmoi.TabIndex = 22;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(36, 175);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(0, 16);
            this.label16.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 16);
            this.label9.TabIndex = 20;
            this.label9.Text = "Mật khẩu :";
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(30, 42);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Size = new System.Drawing.Size(173, 22);
            this.txtMatKhau.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 16);
            this.label3.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(9, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Tài khoản nhân viên ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(302, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 31);
            this.label1.TabIndex = 17;
            this.label1.Text = "Thông tin cá nhân ";
            // 
            // FormThongTinCaNhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkCyan;
            this.ClientSize = new System.Drawing.Size(823, 510);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Name = "FormThongTinCaNhan";
            this.Text = "Thông tin cá nhân";
            this.Load += new System.EventHandler(this.FormThongTinCaNhan_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imgList_TrangChu;
        private System.Windows.Forms.MaskedTextBox mskNgaySinh;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtCMND;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTenNV;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMKMoiNhapLai;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMKmoi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnCapNhatMK;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtGioiTinh;
    }
}