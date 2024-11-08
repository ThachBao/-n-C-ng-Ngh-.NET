namespace HMresourcemanagementsystem.ChamCong
{
    partial class BangCong
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DTP = new System.Windows.Forms.DateTimePicker();
            this.bt_timKiemTheoNgay = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tùyChọnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quayLạiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuChínhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_nam = new System.Windows.Forms.CheckBox();
            this.checkBox_thang = new System.Windows.Forms.CheckBox();
            this.checkBox_ngay = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_tenNhanVien = new System.Windows.Forms.TextBox();
            this.txt_maNV = new System.Windows.Forms.TextBox();
            this.bt_timKiemTheoTen = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_exit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 311);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1171, 455);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(507, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 34);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bảng Công";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Chọn Ngày";
            // 
            // DTP
            // 
            this.DTP.Location = new System.Drawing.Point(148, 36);
            this.DTP.Name = "DTP";
            this.DTP.Size = new System.Drawing.Size(195, 22);
            this.DTP.TabIndex = 4;
            // 
            // bt_timKiemTheoNgay
            // 
            this.bt_timKiemTheoNgay.Location = new System.Drawing.Point(405, 27);
            this.bt_timKiemTheoNgay.Name = "bt_timKiemTheoNgay";
            this.bt_timKiemTheoNgay.Size = new System.Drawing.Size(141, 44);
            this.bt_timKiemTheoNgay.TabIndex = 5;
            this.bt_timKiemTheoNgay.Text = "Tìm Kiếm";
            this.bt_timKiemTheoNgay.UseVisualStyleBackColor = true;
            this.bt_timKiemTheoNgay.Click += new System.EventHandler(this.bt_timKiemTheoNgay_Click_1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tùyChọnToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1351, 30);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tùyChọnToolStripMenuItem
            // 
            this.tùyChọnToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quayLạiToolStripMenuItem,
            this.menuChínhToolStripMenuItem});
            this.tùyChọnToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tùyChọnToolStripMenuItem.Name = "tùyChọnToolStripMenuItem";
            this.tùyChọnToolStripMenuItem.Size = new System.Drawing.Size(101, 26);
            this.tùyChọnToolStripMenuItem.Text = "Tùy Chọn";
            // 
            // quayLạiToolStripMenuItem
            // 
            this.quayLạiToolStripMenuItem.Name = "quayLạiToolStripMenuItem";
            this.quayLạiToolStripMenuItem.Size = new System.Drawing.Size(304, 26);
            this.quayLạiToolStripMenuItem.Text = "Quay Lại Danh Sách Chấm Công";
            this.quayLạiToolStripMenuItem.Click += new System.EventHandler(this.quayLạiToolStripMenuItem_Click);
            // 
            // menuChínhToolStripMenuItem
            // 
            this.menuChínhToolStripMenuItem.Name = "menuChínhToolStripMenuItem";
            this.menuChínhToolStripMenuItem.Size = new System.Drawing.Size(304, 26);
            this.menuChínhToolStripMenuItem.Text = "Quay Lại Trang Chủ";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_nam);
            this.groupBox1.Controls.Add(this.checkBox_thang);
            this.groupBox1.Controls.Add(this.checkBox_ngay);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.DTP);
            this.groupBox1.Controls.Add(this.bt_timKiemTheoNgay);
            this.groupBox1.Location = new System.Drawing.Point(34, 122);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 141);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tìm Kiếm Theo Ngày";
            // 
            // checkBox_nam
            // 
            this.checkBox_nam.AutoSize = true;
            this.checkBox_nam.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_nam.Location = new System.Drawing.Point(338, 98);
            this.checkBox_nam.Name = "checkBox_nam";
            this.checkBox_nam.Size = new System.Drawing.Size(66, 24);
            this.checkBox_nam.TabIndex = 8;
            this.checkBox_nam.Text = "Năm";
            this.checkBox_nam.UseVisualStyleBackColor = true;
            // 
            // checkBox_thang
            // 
            this.checkBox_thang.AutoSize = true;
            this.checkBox_thang.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_thang.Location = new System.Drawing.Point(203, 98);
            this.checkBox_thang.Name = "checkBox_thang";
            this.checkBox_thang.Size = new System.Drawing.Size(77, 24);
            this.checkBox_thang.TabIndex = 7;
            this.checkBox_thang.Text = "Tháng";
            this.checkBox_thang.UseVisualStyleBackColor = true;
            // 
            // checkBox_ngay
            // 
            this.checkBox_ngay.AutoSize = true;
            this.checkBox_ngay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_ngay.Location = new System.Drawing.Point(60, 98);
            this.checkBox_ngay.Name = "checkBox_ngay";
            this.checkBox_ngay.Size = new System.Drawing.Size(69, 24);
            this.checkBox_ngay.TabIndex = 6;
            this.checkBox_ngay.Text = "Ngày";
            this.checkBox_ngay.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_tenNhanVien);
            this.groupBox2.Controls.Add(this.txt_maNV);
            this.groupBox2.Controls.Add(this.bt_timKiemTheoTen);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(649, 122);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(556, 141);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tìm Kiếm Theo Mã - Tên";
            // 
            // txt_tenNhanVien
            // 
            this.txt_tenNhanVien.Location = new System.Drawing.Point(167, 99);
            this.txt_tenNhanVien.Name = "txt_tenNhanVien";
            this.txt_tenNhanVien.Size = new System.Drawing.Size(161, 22);
            this.txt_tenNhanVien.TabIndex = 1;
            // 
            // txt_maNV
            // 
            this.txt_maNV.Location = new System.Drawing.Point(167, 42);
            this.txt_maNV.Name = "txt_maNV";
            this.txt_maNV.Size = new System.Drawing.Size(161, 22);
            this.txt_maNV.TabIndex = 1;
            // 
            // bt_timKiemTheoTen
            // 
            this.bt_timKiemTheoTen.Location = new System.Drawing.Point(389, 56);
            this.bt_timKiemTheoTen.Name = "bt_timKiemTheoTen";
            this.bt_timKiemTheoTen.Size = new System.Drawing.Size(141, 44);
            this.bt_timKiemTheoTen.TabIndex = 5;
            this.bt_timKiemTheoTen.Text = "Tìm Kiếm";
            this.bt_timKiemTheoTen.UseVisualStyleBackColor = true;
            this.bt_timKiemTheoTen.Click += new System.EventHandler(this.bt_timKiemTheoTen_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Nhập Tên";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Nhập Mã Nhân Viên";
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(888, 794);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(251, 72);
            this.btn_exit.TabIndex = 10;
            this.btn_exit.Text = "Thoát";
            this.btn_exit.UseVisualStyleBackColor = true;
            // 
            // BangCong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 904);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BangCong";
            this.Text = "bangCongcs";
            this.Load += new System.EventHandler(this.BangCong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DTP;
        private System.Windows.Forms.Button bt_timKiemTheoNgay;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tùyChọnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quayLạiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuChínhToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_tenNhanVien;
        private System.Windows.Forms.TextBox txt_maNV;
        private System.Windows.Forms.Button bt_timKiemTheoTen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox_nam;
        private System.Windows.Forms.CheckBox checkBox_thang;
        private System.Windows.Forms.CheckBox checkBox_ngay;
        private System.Windows.Forms.Button btn_exit;
    }
}