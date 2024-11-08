namespace HMresourcemanagementsystem.BoPhan
{
    partial class QuanLyBoPhan
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
            this.label5 = new System.Windows.Forms.Label();
            this.txt_maBoPhan = new System.Windows.Forms.TextBox();
            this.txt_tenBoPhan = new System.Windows.Forms.TextBox();
            this.txt_moTa = new System.Windows.Forms.TextBox();
            this.cb_phongBan = new System.Windows.Forms.ComboBox();
            this.bt_them = new System.Windows.Forms.Button();
            this.bt_xoa = new System.Windows.Forms.Button();
            this.bt_sua = new System.Windows.Forms.Button();
            this.bt_thoat = new System.Windows.Forms.Button();
            this.bt_luu = new System.Windows.Forms.Button();
            this.DGV_BoPhan = new System.Windows.Forms.DataGridView();
            this.bt_themPhongBan = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_BoPhan)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(403, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quản Lý Bộ Phận";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(102, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mã Bộ Phận";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(105, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tên Bộ Phận";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(461, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 22);
            this.label4.TabIndex = 3;
            this.label4.Text = "Mô Tả";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(466, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 22);
            this.label5.TabIndex = 4;
            this.label5.Text = "Phòng";
            // 
            // txt_maBoPhan
            // 
            this.txt_maBoPhan.Location = new System.Drawing.Point(245, 100);
            this.txt_maBoPhan.Name = "txt_maBoPhan";
            this.txt_maBoPhan.Size = new System.Drawing.Size(165, 22);
            this.txt_maBoPhan.TabIndex = 5;
            // 
            // txt_tenBoPhan
            // 
            this.txt_tenBoPhan.Location = new System.Drawing.Point(245, 178);
            this.txt_tenBoPhan.Name = "txt_tenBoPhan";
            this.txt_tenBoPhan.Size = new System.Drawing.Size(165, 22);
            this.txt_tenBoPhan.TabIndex = 6;
            // 
            // txt_moTa
            // 
            this.txt_moTa.Location = new System.Drawing.Point(579, 80);
            this.txt_moTa.Multiline = true;
            this.txt_moTa.Name = "txt_moTa";
            this.txt_moTa.Size = new System.Drawing.Size(239, 67);
            this.txt_moTa.TabIndex = 7;
            // 
            // cb_phongBan
            // 
            this.cb_phongBan.FormattingEnabled = true;
            this.cb_phongBan.Location = new System.Drawing.Point(579, 180);
            this.cb_phongBan.Name = "cb_phongBan";
            this.cb_phongBan.Size = new System.Drawing.Size(174, 24);
            this.cb_phongBan.TabIndex = 8;
            // 
            // bt_them
            // 
            this.bt_them.Location = new System.Drawing.Point(102, 229);
            this.bt_them.Name = "bt_them";
            this.bt_them.Size = new System.Drawing.Size(110, 52);
            this.bt_them.TabIndex = 9;
            this.bt_them.Text = "Thêm";
            this.bt_them.UseVisualStyleBackColor = true;
            this.bt_them.Click += new System.EventHandler(this.bt_them_Click);
            // 
            // bt_xoa
            // 
            this.bt_xoa.Location = new System.Drawing.Point(271, 229);
            this.bt_xoa.Name = "bt_xoa";
            this.bt_xoa.Size = new System.Drawing.Size(110, 52);
            this.bt_xoa.TabIndex = 10;
            this.bt_xoa.Text = "Xóa";
            this.bt_xoa.UseVisualStyleBackColor = true;
            this.bt_xoa.Click += new System.EventHandler(this.bt_xoa_Click);
            // 
            // bt_sua
            // 
            this.bt_sua.Location = new System.Drawing.Point(433, 229);
            this.bt_sua.Name = "bt_sua";
            this.bt_sua.Size = new System.Drawing.Size(110, 52);
            this.bt_sua.TabIndex = 11;
            this.bt_sua.Text = "Sửa";
            this.bt_sua.UseVisualStyleBackColor = true;
            // 
            // bt_thoat
            // 
            this.bt_thoat.Location = new System.Drawing.Point(748, 229);
            this.bt_thoat.Name = "bt_thoat";
            this.bt_thoat.Size = new System.Drawing.Size(110, 52);
            this.bt_thoat.TabIndex = 12;
            this.bt_thoat.Text = "Thoát";
            this.bt_thoat.UseVisualStyleBackColor = true;
            // 
            // bt_luu
            // 
            this.bt_luu.Location = new System.Drawing.Point(589, 229);
            this.bt_luu.Name = "bt_luu";
            this.bt_luu.Size = new System.Drawing.Size(114, 52);
            this.bt_luu.TabIndex = 13;
            this.bt_luu.Text = "Lưu";
            this.bt_luu.UseVisualStyleBackColor = true;
            this.bt_luu.Click += new System.EventHandler(this.bt_luu_Click);
            // 
            // DGV_BoPhan
            // 
            this.DGV_BoPhan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_BoPhan.Location = new System.Drawing.Point(69, 317);
            this.DGV_BoPhan.Name = "DGV_BoPhan";
            this.DGV_BoPhan.RowHeadersWidth = 51;
            this.DGV_BoPhan.RowTemplate.Height = 24;
            this.DGV_BoPhan.Size = new System.Drawing.Size(863, 271);
            this.DGV_BoPhan.TabIndex = 14;
            this.DGV_BoPhan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_BoPhan_CellClick);
            // 
            // bt_themPhongBan
            // 
            this.bt_themPhongBan.Location = new System.Drawing.Point(769, 170);
            this.bt_themPhongBan.Name = "bt_themPhongBan";
            this.bt_themPhongBan.Size = new System.Drawing.Size(133, 42);
            this.bt_themPhongBan.TabIndex = 15;
            this.bt_themPhongBan.Text = "Thêm Phòng Ban";
            this.bt_themPhongBan.UseVisualStyleBackColor = true;
            this.bt_themPhongBan.Click += new System.EventHandler(this.bt_themPhongBan_Click);
            // 
            // QuanLyBoPhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 694);
            this.Controls.Add(this.bt_themPhongBan);
            this.Controls.Add(this.DGV_BoPhan);
            this.Controls.Add(this.bt_luu);
            this.Controls.Add(this.bt_thoat);
            this.Controls.Add(this.bt_sua);
            this.Controls.Add(this.bt_xoa);
            this.Controls.Add(this.bt_them);
            this.Controls.Add(this.cb_phongBan);
            this.Controls.Add(this.txt_moTa);
            this.Controls.Add(this.txt_tenBoPhan);
            this.Controls.Add(this.txt_maBoPhan);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "QuanLyBoPhan";
            this.Text = "QuanLyBoPhan";
            this.Load += new System.EventHandler(this.QuanLyBoPhan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_BoPhan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_maBoPhan;
        private System.Windows.Forms.TextBox txt_tenBoPhan;
        private System.Windows.Forms.TextBox txt_moTa;
        private System.Windows.Forms.ComboBox cb_phongBan;
        private System.Windows.Forms.Button bt_them;
        private System.Windows.Forms.Button bt_xoa;
        private System.Windows.Forms.Button bt_sua;
        private System.Windows.Forms.Button bt_thoat;
        private System.Windows.Forms.Button bt_luu;
        private System.Windows.Forms.DataGridView DGV_BoPhan;
        private System.Windows.Forms.Button bt_themPhongBan;
    }
}