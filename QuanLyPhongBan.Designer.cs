namespace HMresourcemanagementsystem
{
    partial class QuanLyPhongBan
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
            this.treeView_phongBan = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_tenPhongBan = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bt_them = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_maNhanVien = new System.Windows.Forms.TextBox();
            this.txt_tenNhanVien = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_phongBan = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.bt_themNhanVien = new System.Windows.Forms.Button();
            this.bt_xoaNhanVien = new System.Windows.Forms.Button();
            this.bt_suaNhanVien = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_maPhongBan = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView_phongBan
            // 
            this.treeView_phongBan.Location = new System.Drawing.Point(6, 21);
            this.treeView_phongBan.Name = "treeView_phongBan";
            this.treeView_phongBan.Size = new System.Drawing.Size(350, 403);
            this.treeView_phongBan.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_maPhongBan);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txt_tenPhongBan);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.bt_them);
            this.groupBox1.Controls.Add(this.treeView_phongBan);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(25, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 610);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Phòng Ban";
            // 
            // txt_tenPhongBan
            // 
            this.txt_tenPhongBan.Location = new System.Drawing.Point(192, 494);
            this.txt_tenPhongBan.Multiline = true;
            this.txt_tenPhongBan.Name = "txt_tenPhongBan";
            this.txt_tenPhongBan.Size = new System.Drawing.Size(155, 35);
            this.txt_tenPhongBan.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 509);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tên Phòng Ban";
            // 
            // bt_them
            // 
            this.bt_them.Location = new System.Drawing.Point(217, 546);
            this.bt_them.Name = "bt_them";
            this.bt_them.Size = new System.Drawing.Size(130, 48);
            this.bt_them.TabIndex = 1;
            this.bt_them.Text = "Thêm";
            this.bt_them.UseVisualStyleBackColor = true;
            this.bt_them.Click += new System.EventHandler(this.bt_them_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(427, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(259, 37);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hồ Sơ Nhân Viên";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(410, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 26);
            this.label3.TabIndex = 3;
            this.label3.Text = "Mã Nhân Viên";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(410, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 26);
            this.label4.TabIndex = 4;
            this.label4.Text = "Tên Nhân Viên";
            // 
            // txt_maNhanVien
            // 
            this.txt_maNhanVien.Location = new System.Drawing.Point(597, 98);
            this.txt_maNhanVien.Multiline = true;
            this.txt_maNhanVien.Name = "txt_maNhanVien";
            this.txt_maNhanVien.Size = new System.Drawing.Size(193, 31);
            this.txt_maNhanVien.TabIndex = 5;
            // 
            // txt_tenNhanVien
            // 
            this.txt_tenNhanVien.Location = new System.Drawing.Point(597, 182);
            this.txt_tenNhanVien.Multiline = true;
            this.txt_tenNhanVien.Name = "txt_tenNhanVien";
            this.txt_tenNhanVien.Size = new System.Drawing.Size(193, 31);
            this.txt_tenNhanVien.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(410, 335);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 26);
            this.label5.TabIndex = 6;
            this.label5.Text = "Tên Phòng Ban";
            // 
            // cb_phongBan
            // 
            this.cb_phongBan.FormattingEnabled = true;
            this.cb_phongBan.Location = new System.Drawing.Point(597, 335);
            this.cb_phongBan.Name = "cb_phongBan";
            this.cb_phongBan.Size = new System.Drawing.Size(193, 24);
            this.cb_phongBan.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(410, 261);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 26);
            this.label6.TabIndex = 8;
            this.label6.Text = "Chức Vụ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(597, 258);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(193, 29);
            this.textBox1.TabIndex = 9;
            // 
            // bt_themNhanVien
            // 
            this.bt_themNhanVien.Location = new System.Drawing.Point(434, 428);
            this.bt_themNhanVien.Name = "bt_themNhanVien";
            this.bt_themNhanVien.Size = new System.Drawing.Size(145, 59);
            this.bt_themNhanVien.TabIndex = 10;
            this.bt_themNhanVien.Text = "Thêm";
            this.bt_themNhanVien.UseVisualStyleBackColor = true;
            // 
            // bt_xoaNhanVien
            // 
            this.bt_xoaNhanVien.Location = new System.Drawing.Point(626, 428);
            this.bt_xoaNhanVien.Name = "bt_xoaNhanVien";
            this.bt_xoaNhanVien.Size = new System.Drawing.Size(145, 59);
            this.bt_xoaNhanVien.TabIndex = 11;
            this.bt_xoaNhanVien.Text = "Xóa";
            this.bt_xoaNhanVien.UseVisualStyleBackColor = true;
            // 
            // bt_suaNhanVien
            // 
            this.bt_suaNhanVien.Location = new System.Drawing.Point(829, 428);
            this.bt_suaNhanVien.Name = "bt_suaNhanVien";
            this.bt_suaNhanVien.Size = new System.Drawing.Size(145, 59);
            this.bt_suaNhanVien.TabIndex = 12;
            this.bt_suaNhanVien.Text = "Sửa";
            this.bt_suaNhanVien.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(45, 455);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "Mã Phòng Ban";
            // 
            // txt_maPhongBan
            // 
            this.txt_maPhongBan.Location = new System.Drawing.Point(192, 440);
            this.txt_maPhongBan.Multiline = true;
            this.txt_maPhongBan.Name = "txt_maPhongBan";
            this.txt_maPhongBan.Size = new System.Drawing.Size(155, 35);
            this.txt_maPhongBan.TabIndex = 3;
            // 
            // QuanLyPhongBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 645);
            this.Controls.Add(this.bt_suaNhanVien);
            this.Controls.Add(this.bt_xoaNhanVien);
            this.Controls.Add(this.bt_themNhanVien);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cb_phongBan);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_tenNhanVien);
            this.Controls.Add(this.txt_maNhanVien);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Name = "QuanLyPhongBan";
            this.Text = "QuanLyPhongBan";
            this.Load += new System.EventHandler(this.QuanLyPhongBan_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView_phongBan;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_tenPhongBan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_them;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_maNhanVien;
        private System.Windows.Forms.TextBox txt_tenNhanVien;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_phongBan;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button bt_themNhanVien;
        private System.Windows.Forms.Button bt_xoaNhanVien;
        private System.Windows.Forms.Button bt_suaNhanVien;
        private System.Windows.Forms.TextBox txt_maPhongBan;
        private System.Windows.Forms.Label label7;
    }
}