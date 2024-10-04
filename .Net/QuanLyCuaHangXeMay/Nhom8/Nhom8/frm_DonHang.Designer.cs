namespace Nhom8
{
    partial class frm_DonHang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_DonHang));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_Thoat = new System.Windows.Forms.Button();
            this.tbx_ID_DonHang = new System.Windows.Forms.TextBox();
            this.tbx_ID_KhachHang = new System.Windows.Forms.TextBox();
            this.btn_Tang = new System.Windows.Forms.Button();
            this.tbx_SL = new System.Windows.Forms.TextBox();
            this.btn_Giam = new System.Windows.Forms.Button();
            this.cbx_XeMay = new System.Windows.Forms.ComboBox();
            this.btn_XacNhan = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Crimson;
            this.label1.Location = new System.Drawing.Point(212, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "ĐƠN HÀNG";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(74, 136);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "ID Đơn Hàng:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(57, 198);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "ID Khách Hàng:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(87, 264);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 18);
            this.label4.TabIndex = 5;
            this.label4.Text = "ID Xe Máy:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(106, 326);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 18);
            this.label5.TabIndex = 7;
            this.label5.Text = "Số Lượng:";
            // 
            // btn_Thoat
            // 
            this.btn_Thoat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Thoat.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_Thoat.Image = global::Nhom8.Properties.Resources.Hopstarter_Rounded_Square_Button_Delete_16;
            this.btn_Thoat.Location = new System.Drawing.Point(365, 400);
            this.btn_Thoat.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Thoat.Name = "btn_Thoat";
            this.btn_Thoat.Size = new System.Drawing.Size(106, 49);
            this.btn_Thoat.TabIndex = 3;
            this.btn_Thoat.Text = "Thoát";
            this.btn_Thoat.UseVisualStyleBackColor = true;
            this.btn_Thoat.Click += new System.EventHandler(this.btn_Thoat_Click);
            // 
            // tbx_ID_DonHang
            // 
            this.tbx_ID_DonHang.Location = new System.Drawing.Point(254, 136);
            this.tbx_ID_DonHang.Name = "tbx_ID_DonHang";
            this.tbx_ID_DonHang.ReadOnly = true;
            this.tbx_ID_DonHang.Size = new System.Drawing.Size(168, 25);
            this.tbx_ID_DonHang.TabIndex = 0;
            // 
            // tbx_ID_KhachHang
            // 
            this.tbx_ID_KhachHang.Location = new System.Drawing.Point(254, 198);
            this.tbx_ID_KhachHang.Name = "tbx_ID_KhachHang";
            this.tbx_ID_KhachHang.ReadOnly = true;
            this.tbx_ID_KhachHang.Size = new System.Drawing.Size(168, 25);
            this.tbx_ID_KhachHang.TabIndex = 2;
            // 
            // btn_Tang
            // 
            this.btn_Tang.Location = new System.Drawing.Point(353, 326);
            this.btn_Tang.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Tang.Name = "btn_Tang";
            this.btn_Tang.Size = new System.Drawing.Size(24, 29);
            this.btn_Tang.TabIndex = 9;
            this.btn_Tang.Text = ">";
            this.btn_Tang.UseVisualStyleBackColor = true;
            this.btn_Tang.Click += new System.EventHandler(this.btn_Tang_Click);
            // 
            // tbx_SL
            // 
            this.tbx_SL.Enabled = false;
            this.tbx_SL.Location = new System.Drawing.Point(285, 326);
            this.tbx_SL.Name = "tbx_SL";
            this.tbx_SL.ReadOnly = true;
            this.tbx_SL.Size = new System.Drawing.Size(61, 25);
            this.tbx_SL.TabIndex = 1;
            // 
            // btn_Giam
            // 
            this.btn_Giam.Location = new System.Drawing.Point(254, 326);
            this.btn_Giam.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Giam.Name = "btn_Giam";
            this.btn_Giam.Size = new System.Drawing.Size(24, 29);
            this.btn_Giam.TabIndex = 8;
            this.btn_Giam.Text = "<";
            this.btn_Giam.UseVisualStyleBackColor = true;
            this.btn_Giam.Click += new System.EventHandler(this.btn_Giam_Click);
            // 
            // cbx_XeMay
            // 
            this.cbx_XeMay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_XeMay.FormattingEnabled = true;
            this.cbx_XeMay.Location = new System.Drawing.Point(254, 264);
            this.cbx_XeMay.Name = "cbx_XeMay";
            this.cbx_XeMay.Size = new System.Drawing.Size(168, 25);
            this.cbx_XeMay.TabIndex = 0;
            // 
            // btn_XacNhan
            // 
            this.btn_XacNhan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_XacNhan.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_XacNhan.Image = global::Nhom8.Properties.Resources.Custom_Icon_Design_Flatastic_9_Accept1;
            this.btn_XacNhan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_XacNhan.Location = new System.Drawing.Point(99, 400);
            this.btn_XacNhan.Margin = new System.Windows.Forms.Padding(4);
            this.btn_XacNhan.Name = "btn_XacNhan";
            this.btn_XacNhan.Size = new System.Drawing.Size(109, 49);
            this.btn_XacNhan.TabIndex = 2;
            this.btn_XacNhan.Text = "Xác Nhận";
            this.btn_XacNhan.UseVisualStyleBackColor = true;
            this.btn_XacNhan.Click += new System.EventHandler(this.btn_XacNhan_Click);
            // 
            // frm_DonHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 497);
            this.Controls.Add(this.cbx_XeMay);
            this.Controls.Add(this.tbx_SL);
            this.Controls.Add(this.tbx_ID_KhachHang);
            this.Controls.Add(this.tbx_ID_DonHang);
            this.Controls.Add(this.btn_Tang);
            this.Controls.Add(this.btn_Giam);
            this.Controls.Add(this.btn_Thoat);
            this.Controls.Add(this.btn_XacNhan);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frm_DonHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đơn hàng";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_DonHang_FormClosing);
            this.Load += new System.EventHandler(this.frm_DonHang_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_XacNhan;
        private System.Windows.Forms.Button btn_Thoat;
        private System.Windows.Forms.TextBox tbx_ID_DonHang;
        private System.Windows.Forms.TextBox tbx_ID_KhachHang;
        private System.Windows.Forms.Button btn_Tang;
        private System.Windows.Forms.TextBox tbx_SL;
        private System.Windows.Forms.Button btn_Giam;
        private System.Windows.Forms.ComboBox cbx_XeMay;
    }
}