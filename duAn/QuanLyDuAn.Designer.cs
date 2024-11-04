namespace HMresourcemanagementsystem.duAn
{
    partial class QuanLyDuAn
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_maDuAn = new System.Windows.Forms.TextBox();
            this.dateBatDau = new System.Windows.Forms.DateTimePicker();
            this.dateKetThuc = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.DTGView = new System.Windows.Forms.DataGridView();
            this.bt_timKiem = new System.Windows.Forms.Button();
            this.bt_sua = new System.Windows.Forms.Button();
            this.bt_xoa = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_maPhong = new System.Windows.Forms.TextBox();
            this.txt_moTa = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.bt_exit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DTGView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mã Dự Án";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "Ngày Bắt Đầu";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(20, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 19);
            this.label4.TabIndex = 4;
            this.label4.Text = "Ngày Kết Thúc";
            // 
            // txt_maDuAn
            // 
            this.txt_maDuAn.Location = new System.Drawing.Point(109, 64);
            this.txt_maDuAn.Name = "txt_maDuAn";
            this.txt_maDuAn.Size = new System.Drawing.Size(141, 22);
            this.txt_maDuAn.TabIndex = 7;
            // 
            // dateBatDau
            // 
            this.dateBatDau.Location = new System.Drawing.Point(154, 32);
            this.dateBatDau.Name = "dateBatDau";
            this.dateBatDau.Size = new System.Drawing.Size(200, 22);
            this.dateBatDau.TabIndex = 11;
            this.dateBatDau.ValueChanged += new System.EventHandler(this.dateBatDau_ValueChanged);
            // 
            // dateKetThuc
            // 
            this.dateKetThuc.Location = new System.Drawing.Point(154, 114);
            this.dateKetThuc.Name = "dateKetThuc";
            this.dateKetThuc.Size = new System.Drawing.Size(200, 22);
            this.dateKetThuc.TabIndex = 12;
            this.dateKetThuc.ValueChanged += new System.EventHandler(this.dateKetThuc_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(438, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(195, 33);
            this.label7.TabIndex = 13;
            this.label7.Text = "Quản Lý Dự Án";
            // 
            // DTGView
            // 
            this.DTGView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DTGView.Location = new System.Drawing.Point(147, 329);
            this.DTGView.Name = "DTGView";
            this.DTGView.RowHeadersWidth = 51;
            this.DTGView.RowTemplate.Height = 24;
            this.DTGView.Size = new System.Drawing.Size(813, 250);
            this.DTGView.TabIndex = 14;
            this.DTGView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DTGView_CellClick);
            // 
            // bt_timKiem
            // 
            this.bt_timKiem.Location = new System.Drawing.Point(175, 255);
            this.bt_timKiem.Name = "bt_timKiem";
            this.bt_timKiem.Size = new System.Drawing.Size(137, 50);
            this.bt_timKiem.TabIndex = 15;
            this.bt_timKiem.Text = "Tìm Kiếm";
            this.bt_timKiem.UseVisualStyleBackColor = true;
            this.bt_timKiem.Click += new System.EventHandler(this.bt_timKiem_Click);
            // 
            // bt_sua
            // 
            this.bt_sua.Location = new System.Drawing.Point(384, 255);
            this.bt_sua.Name = "bt_sua";
            this.bt_sua.Size = new System.Drawing.Size(137, 50);
            this.bt_sua.TabIndex = 15;
            this.bt_sua.Text = "Sửa";
            this.bt_sua.UseVisualStyleBackColor = true;
            this.bt_sua.Click += new System.EventHandler(this.bt_sua_Click);
            // 
            // bt_xoa
            // 
            this.bt_xoa.Location = new System.Drawing.Point(597, 255);
            this.bt_xoa.Name = "bt_xoa";
            this.bt_xoa.Size = new System.Drawing.Size(137, 50);
            this.bt_xoa.TabIndex = 15;
            this.bt_xoa.Text = "Xóa";
            this.bt_xoa.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_maDuAn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(27, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 136);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tìm Kiếm Theo Mã";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dateKetThuc);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.dateBatDau);
            this.groupBox2.Location = new System.Drawing.Point(339, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(395, 151);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tím Kiếm Theo Ngày Tháng Năm";
            // 
            // txt_maPhong
            // 
            this.txt_maPhong.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_maPhong.Location = new System.Drawing.Point(844, 179);
            this.txt_maPhong.Name = "txt_maPhong";
            this.txt_maPhong.Size = new System.Drawing.Size(124, 27);
            this.txt_maPhong.TabIndex = 21;
            // 
            // txt_moTa
            // 
            this.txt_moTa.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_moTa.Location = new System.Drawing.Point(844, 67);
            this.txt_moTa.Multiline = true;
            this.txt_moTa.Name = "txt_moTa";
            this.txt_moTa.Size = new System.Drawing.Size(251, 93);
            this.txt_moTa.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(756, 179);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 19);
            this.label6.TabIndex = 19;
            this.label6.Text = "Mã Phòng";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(767, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 19);
            this.label5.TabIndex = 18;
            this.label5.Text = "Mô Tả";
            // 
            // bt_exit
            // 
            this.bt_exit.Location = new System.Drawing.Point(802, 255);
            this.bt_exit.Name = "bt_exit";
            this.bt_exit.Size = new System.Drawing.Size(144, 50);
            this.bt_exit.TabIndex = 22;
            this.bt_exit.Text = "Thoát";
            this.bt_exit.UseVisualStyleBackColor = true;
            this.bt_exit.Click += new System.EventHandler(this.bt_exit_Click);
            // 
            // QuanLyDuAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 633);
            this.Controls.Add(this.bt_exit);
            this.Controls.Add(this.txt_maPhong);
            this.Controls.Add(this.txt_moTa);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bt_xoa);
            this.Controls.Add(this.bt_sua);
            this.Controls.Add(this.bt_timKiem);
            this.Controls.Add(this.DTGView);
            this.Controls.Add(this.label7);
            this.Name = "QuanLyDuAn";
            this.Text = " ";
            this.Load += new System.EventHandler(this.QuanLyDuAn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DTGView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_maDuAn;
        private System.Windows.Forms.DateTimePicker dateBatDau;
        private System.Windows.Forms.DateTimePicker dateKetThuc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView DTGView;
        private System.Windows.Forms.Button bt_timKiem;
        private System.Windows.Forms.Button bt_sua;
        private System.Windows.Forms.Button bt_xoa;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_maPhong;
        private System.Windows.Forms.TextBox txt_moTa;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bt_exit;
    }
}