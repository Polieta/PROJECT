
namespace Nhom6_QLShopThoiTrang.Forms
{
    partial class FormKhachHang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormKhachHang));
            this.txt_DiaChi = new System.Windows.Forms.TextBox();
            this.lbl_DiaChi = new System.Windows.Forms.Label();
            this.txt_TenKH = new System.Windows.Forms.TextBox();
            this.lbl_SoDienThoai = new System.Windows.Forms.Label();
            this.lbl_TenKhachHang = new System.Windows.Forms.Label();
            this.txt_MaKhachHang = new System.Windows.Forms.TextBox();
            this.lbl_MaKhachHang = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_SDT = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_KhachHang = new System.Windows.Forms.DataGridView();
            this.txt_TimTheoTenKH = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_BackSearch = new System.Windows.Forms.Button();
            this.btn_TimKiem = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Sua = new System.Windows.Forms.Button();
            this.btn_Huy = new System.Windows.Forms.Button();
            this.btn_Xoa = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_KhachHang)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_DiaChi
            // 
            this.txt_DiaChi.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt_DiaChi.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_DiaChi.Location = new System.Drawing.Point(884, 43);
            this.txt_DiaChi.Margin = new System.Windows.Forms.Padding(2);
            this.txt_DiaChi.Multiline = true;
            this.txt_DiaChi.Name = "txt_DiaChi";
            this.txt_DiaChi.Size = new System.Drawing.Size(275, 30);
            this.txt_DiaChi.TabIndex = 10;
            // 
            // lbl_DiaChi
            // 
            this.lbl_DiaChi.AccessibleDescription = "";
            this.lbl_DiaChi.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_DiaChi.AutoSize = true;
            this.lbl_DiaChi.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lbl_DiaChi.Location = new System.Drawing.Point(785, 46);
            this.lbl_DiaChi.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_DiaChi.Name = "lbl_DiaChi";
            this.lbl_DiaChi.Size = new System.Drawing.Size(91, 26);
            this.lbl_DiaChi.TabIndex = 7;
            this.lbl_DiaChi.Text = "Địa chỉ ";
            // 
            // txt_TenKH
            // 
            this.txt_TenKH.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt_TenKH.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_TenKH.Location = new System.Drawing.Point(339, 121);
            this.txt_TenKH.Margin = new System.Windows.Forms.Padding(2);
            this.txt_TenKH.Multiline = true;
            this.txt_TenKH.Name = "txt_TenKH";
            this.txt_TenKH.Size = new System.Drawing.Size(255, 30);
            this.txt_TenKH.TabIndex = 6;
            // 
            // lbl_SoDienThoai
            // 
            this.lbl_SoDienThoai.AccessibleDescription = "";
            this.lbl_SoDienThoai.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_SoDienThoai.AutoSize = true;
            this.lbl_SoDienThoai.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lbl_SoDienThoai.Location = new System.Drawing.Point(729, 125);
            this.lbl_SoDienThoai.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_SoDienThoai.Name = "lbl_SoDienThoai";
            this.lbl_SoDienThoai.Size = new System.Drawing.Size(151, 26);
            this.lbl_SoDienThoai.TabIndex = 5;
            this.lbl_SoDienThoai.Text = "Số điện thoại ";
            // 
            // lbl_TenKhachHang
            // 
            this.lbl_TenKhachHang.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_TenKhachHang.AutoSize = true;
            this.lbl_TenKhachHang.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lbl_TenKhachHang.Location = new System.Drawing.Point(156, 124);
            this.lbl_TenKhachHang.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_TenKhachHang.Name = "lbl_TenKhachHang";
            this.lbl_TenKhachHang.Size = new System.Drawing.Size(175, 26);
            this.lbl_TenKhachHang.TabIndex = 3;
            this.lbl_TenKhachHang.Text = "Tên khách hàng";
            // 
            // txt_MaKhachHang
            // 
            this.txt_MaKhachHang.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt_MaKhachHang.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MaKhachHang.ForeColor = System.Drawing.Color.Red;
            this.txt_MaKhachHang.Location = new System.Drawing.Point(339, 42);
            this.txt_MaKhachHang.Margin = new System.Windows.Forms.Padding(2);
            this.txt_MaKhachHang.Multiline = true;
            this.txt_MaKhachHang.Name = "txt_MaKhachHang";
            this.txt_MaKhachHang.Size = new System.Drawing.Size(255, 30);
            this.txt_MaKhachHang.TabIndex = 2;
            // 
            // lbl_MaKhachHang
            // 
            this.lbl_MaKhachHang.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_MaKhachHang.AutoSize = true;
            this.lbl_MaKhachHang.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lbl_MaKhachHang.Location = new System.Drawing.Point(164, 46);
            this.lbl_MaKhachHang.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_MaKhachHang.Name = "lbl_MaKhachHang";
            this.lbl_MaKhachHang.Size = new System.Drawing.Size(171, 26);
            this.lbl_MaKhachHang.TabIndex = 1;
            this.lbl_MaKhachHang.Text = "Mã khách hàng";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.SteelBlue;
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btn_Sua);
            this.groupBox1.Controls.Add(this.btn_Huy);
            this.groupBox1.Controls.Add(this.btn_Xoa);
            this.groupBox1.Controls.Add(this.txt_DiaChi);
            this.groupBox1.Controls.Add(this.lbl_DiaChi);
            this.groupBox1.Controls.Add(this.txt_TenKH);
            this.groupBox1.Controls.Add(this.lbl_SoDienThoai);
            this.groupBox1.Controls.Add(this.txt_SDT);
            this.groupBox1.Controls.Add(this.lbl_TenKhachHang);
            this.groupBox1.Controls.Add(this.txt_MaKhachHang);
            this.groupBox1.Controls.Add(this.lbl_MaKhachHang);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(1324, 827);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            // 
            // txt_SDT
            // 
            this.txt_SDT.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt_SDT.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SDT.Location = new System.Drawing.Point(884, 122);
            this.txt_SDT.Margin = new System.Windows.Forms.Padding(2);
            this.txt_SDT.Multiline = true;
            this.txt_SDT.Name = "txt_SDT";
            this.txt_SDT.Size = new System.Drawing.Size(275, 30);
            this.txt_SDT.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.btn_BackSearch);
            this.panel1.Controls.Add(this.btn_TimKiem);
            this.panel1.Controls.Add(this.txt_TimTheoTenKH);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 273);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1324, 554);
            this.panel1.TabIndex = 31;
            // 
            // dgv_KhachHang
            // 
            this.dgv_KhachHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_KhachHang.BackgroundColor = System.Drawing.Color.White;
            this.dgv_KhachHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_KhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_KhachHang.Location = new System.Drawing.Point(3, 31);
            this.dgv_KhachHang.Margin = new System.Windows.Forms.Padding(2);
            this.dgv_KhachHang.Name = "dgv_KhachHang";
            this.dgv_KhachHang.RowHeadersWidth = 51;
            this.dgv_KhachHang.RowTemplate.Height = 28;
            this.dgv_KhachHang.Size = new System.Drawing.Size(1316, 426);
            this.dgv_KhachHang.TabIndex = 30;
            this.dgv_KhachHang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_KhachHang_CellClick);
            this.dgv_KhachHang.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_KhachHang_CellContentClick);
            // 
            // txt_TimTheoTenKH
            // 
            this.txt_TimTheoTenKH.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt_TimTheoTenKH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_TimTheoTenKH.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_TimTheoTenKH.Location = new System.Drawing.Point(529, 22);
            this.txt_TimTheoTenKH.Margin = new System.Windows.Forms.Padding(2);
            this.txt_TimTheoTenKH.Multiline = true;
            this.txt_TimTheoTenKH.Name = "txt_TimTheoTenKH";
            this.txt_TimTheoTenKH.Size = new System.Drawing.Size(304, 46);
            this.txt_TimTheoTenKH.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "";
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(191, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(304, 31);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tên khách hàng cần tìm:";
            // 
            // btn_BackSearch
            // 
            this.btn_BackSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_BackSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_BackSearch.Image = ((System.Drawing.Image)(resources.GetObject("btn_BackSearch.Image")));
            this.btn_BackSearch.Location = new System.Drawing.Point(1038, 18);
            this.btn_BackSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btn_BackSearch.Name = "btn_BackSearch";
            this.btn_BackSearch.Size = new System.Drawing.Size(112, 54);
            this.btn_BackSearch.TabIndex = 31;
            this.btn_BackSearch.UseVisualStyleBackColor = true;
            this.btn_BackSearch.Click += new System.EventHandler(this.btn_BackSearch_Click);
            // 
            // btn_TimKiem
            // 
            this.btn_TimKiem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_TimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_TimKiem.Image = global::Nhom6_QLShopThoiTrang.Properties.Resources.icons8_search_50;
            this.btn_TimKiem.Location = new System.Drawing.Point(899, 18);
            this.btn_TimKiem.Margin = new System.Windows.Forms.Padding(2);
            this.btn_TimKiem.Name = "btn_TimKiem";
            this.btn_TimKiem.Size = new System.Drawing.Size(100, 55);
            this.btn_TimKiem.TabIndex = 31;
            this.btn_TimKiem.UseVisualStyleBackColor = true;
            this.btn_TimKiem.Click += new System.EventHandler(this.btn_TimKiem_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button1.Image = global::Nhom6_QLShopThoiTrang.Properties.Resources.icons8_add_35;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(732, 197);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 48);
            this.button1.TabIndex = 35;
            this.button1.Text = "Thêm";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Sua
            // 
            this.btn_Sua.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Sua.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Sua.ForeColor = System.Drawing.Color.Blue;
            this.btn_Sua.Image = global::Nhom6_QLShopThoiTrang.Properties.Resources.icons8_update_351;
            this.btn_Sua.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Sua.Location = new System.Drawing.Point(252, 197);
            this.btn_Sua.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Sua.Name = "btn_Sua";
            this.btn_Sua.Size = new System.Drawing.Size(124, 48);
            this.btn_Sua.TabIndex = 34;
            this.btn_Sua.Text = "Sửa";
            this.btn_Sua.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Sua.UseVisualStyleBackColor = true;
            this.btn_Sua.Click += new System.EventHandler(this.btn_Sua_Click);
            // 
            // btn_Huy
            // 
            this.btn_Huy.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Huy.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Huy.ForeColor = System.Drawing.Color.Red;
            this.btn_Huy.Image = global::Nhom6_QLShopThoiTrang.Properties.Resources.icons8_clear_35;
            this.btn_Huy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Huy.Location = new System.Drawing.Point(972, 197);
            this.btn_Huy.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Huy.Name = "btn_Huy";
            this.btn_Huy.Size = new System.Drawing.Size(124, 48);
            this.btn_Huy.TabIndex = 33;
            this.btn_Huy.Text = "Hủy";
            this.btn_Huy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Huy.UseVisualStyleBackColor = true;
            this.btn_Huy.Click += new System.EventHandler(this.btn_Huy_Click);
            // 
            // btn_Xoa
            // 
            this.btn_Xoa.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Xoa.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Xoa.ForeColor = System.Drawing.Color.Red;
            this.btn_Xoa.Image = global::Nhom6_QLShopThoiTrang.Properties.Resources.icons8_delete_35;
            this.btn_Xoa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Xoa.Location = new System.Drawing.Point(492, 197);
            this.btn_Xoa.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Xoa.Name = "btn_Xoa";
            this.btn_Xoa.Size = new System.Drawing.Size(124, 48);
            this.btn_Xoa.TabIndex = 33;
            this.btn_Xoa.Text = "Xóa ";
            this.btn_Xoa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Xoa.UseVisualStyleBackColor = true;
            this.btn_Xoa.Click += new System.EventHandler(this.btn_Xoa_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv_KhachHang);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1322, 460);
            this.groupBox2.TabIndex = 32;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh sách khách hàng";
            // 
            // FormKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1324, 827);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormKhachHang";
            this.Text = "FormKhachHang";
            this.Load += new System.EventHandler(this.FormKhachHang_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_KhachHang)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Sua;
        private System.Windows.Forms.TextBox txt_DiaChi;
        private System.Windows.Forms.Label lbl_DiaChi;
        private System.Windows.Forms.TextBox txt_TenKH;
        private System.Windows.Forms.Label lbl_SoDienThoai;
        private System.Windows.Forms.Label lbl_TenKhachHang;
        private System.Windows.Forms.TextBox txt_MaKhachHang;
        private System.Windows.Forms.Label lbl_MaKhachHang;
        private System.Windows.Forms.Button btn_Xoa;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_SDT;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv_KhachHang;
        private System.Windows.Forms.TextBox txt_TimTheoTenKH;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_TimKiem;
        private System.Windows.Forms.Button btn_Huy;
        private System.Windows.Forms.Button btn_BackSearch;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}