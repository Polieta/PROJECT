namespace Nhom8
{
    partial class frm_TrangChu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_TrangChu));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.IDXeMay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenXe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HangSanXuat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DonGiaBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.adimToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbx_IdXeMay = new System.Windows.Forms.TextBox();
            this.tbx_DonGia = new System.Windows.Forms.TextBox();
            this.tbx_TenXe = new System.Windows.Forms.TextBox();
            this.tbx_Diachi = new System.Windows.Forms.TextBox();
            this.tbx_IdDonHang = new System.Windows.Forms.TextBox();
            this.tbx_IdKH = new System.Windows.Forms.TextBox();
            this.tbx_HoTenKH = new System.Windows.Forms.TextBox();
            this.tbx_SDT = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btn_Xoa = new System.Windows.Forms.Button();
            this.btn_Luu = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_TENDN = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(12, 95);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(805, 628);
            this.panel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDXeMay,
            this.TenXe,
            this.HangSanXuat,
            this.SoLuong,
            this.DonGiaBan});
            this.dataGridView1.Location = new System.Drawing.Point(3, 2);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(800, 618);
            this.dataGridView1.TabIndex = 0;
            // 
            // IDXeMay
            // 
            this.IDXeMay.DataPropertyName = "IDXeMay";
            this.IDXeMay.HeaderText = "ID xe máy";
            this.IDXeMay.MinimumWidth = 6;
            this.IDXeMay.Name = "IDXeMay";
            this.IDXeMay.ReadOnly = true;
            // 
            // TenXe
            // 
            this.TenXe.DataPropertyName = "TenXe";
            this.TenXe.HeaderText = "Tên Xe";
            this.TenXe.MinimumWidth = 6;
            this.TenXe.Name = "TenXe";
            this.TenXe.ReadOnly = true;
            this.TenXe.Width = 160;
            // 
            // HangSanXuat
            // 
            this.HangSanXuat.DataPropertyName = "HangSanXuat";
            this.HangSanXuat.HeaderText = "Hãng Sản Xuất";
            this.HangSanXuat.MinimumWidth = 6;
            this.HangSanXuat.Name = "HangSanXuat";
            this.HangSanXuat.ReadOnly = true;
            this.HangSanXuat.Width = 125;
            // 
            // SoLuong
            // 
            this.SoLuong.DataPropertyName = "SoLuong";
            this.SoLuong.HeaderText = "Số lượng tồn";
            this.SoLuong.MinimumWidth = 6;
            this.SoLuong.Name = "SoLuong";
            this.SoLuong.ReadOnly = true;
            // 
            // DonGiaBan
            // 
            this.DonGiaBan.DataPropertyName = "DonGiaBan";
            this.DonGiaBan.HeaderText = "Đơn giá bán";
            this.DonGiaBan.MinimumWidth = 6;
            this.DonGiaBan.Name = "DonGiaBan";
            this.DonGiaBan.ReadOnly = true;
            this.DonGiaBan.Width = 125;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adimToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1331, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // adimToolStripMenuItem
            // 
            this.adimToolStripMenuItem.Name = "adimToolStripMenuItem";
            this.adimToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.adimToolStripMenuItem.Text = "Quản lý";
            this.adimToolStripMenuItem.Click += new System.EventHandler(this.adimToolStripMenuItem_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tbx_IdXeMay);
            this.panel2.Controls.Add(this.tbx_DonGia);
            this.panel2.Controls.Add(this.tbx_TenXe);
            this.panel2.Controls.Add(this.tbx_Diachi);
            this.panel2.Controls.Add(this.tbx_IdDonHang);
            this.panel2.Controls.Add(this.tbx_IdKH);
            this.panel2.Controls.Add(this.tbx_HoTenKH);
            this.panel2.Controls.Add(this.tbx_SDT);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(823, 176);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(495, 547);
            this.panel2.TabIndex = 0;
            // 
            // tbx_IdXeMay
            // 
            this.tbx_IdXeMay.Location = new System.Drawing.Point(179, 282);
            this.tbx_IdXeMay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbx_IdXeMay.Name = "tbx_IdXeMay";
            this.tbx_IdXeMay.ReadOnly = true;
            this.tbx_IdXeMay.Size = new System.Drawing.Size(255, 22);
            this.tbx_IdXeMay.TabIndex = 3;
            // 
            // tbx_DonGia
            // 
            this.tbx_DonGia.Location = new System.Drawing.Point(179, 383);
            this.tbx_DonGia.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbx_DonGia.Name = "tbx_DonGia";
            this.tbx_DonGia.ReadOnly = true;
            this.tbx_DonGia.Size = new System.Drawing.Size(255, 22);
            this.tbx_DonGia.TabIndex = 3;
            // 
            // tbx_TenXe
            // 
            this.tbx_TenXe.Location = new System.Drawing.Point(179, 334);
            this.tbx_TenXe.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbx_TenXe.Name = "tbx_TenXe";
            this.tbx_TenXe.ReadOnly = true;
            this.tbx_TenXe.Size = new System.Drawing.Size(255, 22);
            this.tbx_TenXe.TabIndex = 3;
            // 
            // tbx_Diachi
            // 
            this.tbx_Diachi.Location = new System.Drawing.Point(179, 238);
            this.tbx_Diachi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbx_Diachi.Name = "tbx_Diachi";
            this.tbx_Diachi.Size = new System.Drawing.Size(255, 22);
            this.tbx_Diachi.TabIndex = 4;
            // 
            // tbx_IdDonHang
            // 
            this.tbx_IdDonHang.Location = new System.Drawing.Point(179, 62);
            this.tbx_IdDonHang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbx_IdDonHang.Name = "tbx_IdDonHang";
            this.tbx_IdDonHang.Size = new System.Drawing.Size(255, 22);
            this.tbx_IdDonHang.TabIndex = 0;
            this.tbx_IdDonHang.Leave += new System.EventHandler(this.tbx_IdDonHang_Leave);
            // 
            // tbx_IdKH
            // 
            this.tbx_IdKH.Location = new System.Drawing.Point(179, 105);
            this.tbx_IdKH.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbx_IdKH.Name = "tbx_IdKH";
            this.tbx_IdKH.Size = new System.Drawing.Size(255, 22);
            this.tbx_IdKH.TabIndex = 1;
            this.tbx_IdKH.Leave += new System.EventHandler(this.tbx_IdKH_Leave);
            // 
            // tbx_HoTenKH
            // 
            this.tbx_HoTenKH.Location = new System.Drawing.Point(179, 145);
            this.tbx_HoTenKH.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbx_HoTenKH.Name = "tbx_HoTenKH";
            this.tbx_HoTenKH.Size = new System.Drawing.Size(255, 22);
            this.tbx_HoTenKH.TabIndex = 2;
            // 
            // tbx_SDT
            // 
            this.tbx_SDT.Location = new System.Drawing.Point(179, 194);
            this.tbx_SDT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbx_SDT.Name = "tbx_SDT";
            this.tbx_SDT.Size = new System.Drawing.Size(255, 22);
            this.tbx_SDT.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(80, 384);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 20);
            this.label6.TabIndex = 4;
            this.label6.Text = "Đơn giá:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(80, 335);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 20);
            this.label9.TabIndex = 4;
            this.label9.Text = "Tên xe:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(56, 284);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 20);
            this.label8.TabIndex = 4;
            this.label8.Text = "ID Xe máy:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(77, 239);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Địa chỉ:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(27, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Số điện thoại:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(51, 146);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 20);
            this.label10.TabIndex = 1;
            this.label10.Text = "Họ và Tên:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "ID Đơn Hàng:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "ID Khách Hàng:";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btn_Xoa);
            this.panel4.Controls.Add(this.btn_Luu);
            this.panel4.Location = new System.Drawing.Point(823, 95);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(495, 75);
            this.panel4.TabIndex = 1;
            // 
            // btn_Xoa
            // 
            this.btn_Xoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Xoa.Image = global::Nhom8.Properties.Resources.Hopstarter_Button_Button_Delete_16;
            this.btn_Xoa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Xoa.Location = new System.Drawing.Point(225, 21);
            this.btn_Xoa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Xoa.Name = "btn_Xoa";
            this.btn_Xoa.Size = new System.Drawing.Size(249, 38);
            this.btn_Xoa.TabIndex = 1;
            this.btn_Xoa.Text = "Xóa hết thông tin";
            this.btn_Xoa.UseVisualStyleBackColor = true;
            this.btn_Xoa.Click += new System.EventHandler(this.btn_Xoa_Click);
            // 
            // btn_Luu
            // 
            this.btn_Luu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Luu.Image = global::Nhom8.Properties.Resources.Hopstarter_Button_Button_Add_16;
            this.btn_Luu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Luu.Location = new System.Drawing.Point(3, 21);
            this.btn_Luu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Luu.Name = "btn_Luu";
            this.btn_Luu.Size = new System.Drawing.Size(207, 38);
            this.btn_Luu.TabIndex = 0;
            this.btn_Luu.Text = "Tạo hóa đơn";
            this.btn_Luu.UseVisualStyleBackColor = true;
            this.btn_Luu.Click += new System.EventHandler(this.btn_Luu_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Crimson;
            this.label1.Location = new System.Drawing.Point(129, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(308, 29);
            this.label1.TabIndex = 5;
            this.label1.Text = "Thông tin các loại xe máy";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(541, 50);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(297, 22);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Crimson;
            this.label7.Location = new System.Drawing.Point(964, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(262, 29);
            this.label7.TabIndex = 5;
            this.label7.Text = "Thông tin khách hàng";
            // 
            // lbl_TENDN
            // 
            this.lbl_TENDN.AutoSize = true;
            this.lbl_TENDN.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TENDN.Location = new System.Drawing.Point(1023, -1);
            this.lbl_TENDN.Name = "lbl_TENDN";
            this.lbl_TENDN.Size = new System.Drawing.Size(143, 29);
            this.lbl_TENDN.TabIndex = 5;
            this.lbl_TENDN.Text = "XIN CHÀO:";
            // 
            // frm_TrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1331, 741);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.lbl_TENDN);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frm_TrangChu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trang chủ";
            this.Load += new System.EventHandler(this.frm_TrangChu_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem adimToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbx_Diachi;
        private System.Windows.Forms.TextBox tbx_SDT;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Xoa;
        private System.Windows.Forms.Button btn_Luu;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbx_TenXe;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbx_HoTenKH;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbl_TENDN;
        private System.Windows.Forms.TextBox tbx_IdKH;
        private System.Windows.Forms.TextBox tbx_IdXeMay;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbx_IdDonHang;
        private System.Windows.Forms.TextBox tbx_DonGia;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDXeMay;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenXe;
        private System.Windows.Forms.DataGridViewTextBoxColumn HangSanXuat;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn DonGiaBan;
    }
}