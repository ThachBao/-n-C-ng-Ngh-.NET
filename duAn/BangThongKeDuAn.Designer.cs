namespace HMresourcemanagementsystem.duAn
{
    partial class BangThongKeDuAn
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
            this.bt_exit = new System.Windows.Forms.Button();
            this.txt_maDuAn = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_dauViec = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_maPhong = new System.Windows.Forms.TextBox();
            this.txt_moTa = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.DGV_DAUVIEC = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_DAUVIEC)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(417, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(271, 33);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bảng Thống Kê Dự Án";
            // 
            // bt_exit
            // 
            this.bt_exit.Location = new System.Drawing.Point(934, 573);
            this.bt_exit.Name = "bt_exit";
            this.bt_exit.Size = new System.Drawing.Size(171, 63);
            this.bt_exit.TabIndex = 2;
            this.bt_exit.Text = "Quay Lại";
            this.bt_exit.UseVisualStyleBackColor = true;
            this.bt_exit.Click += new System.EventHandler(this.bt_exit_Click);
            // 
            // txt_maDuAn
            // 
            this.txt_maDuAn.Location = new System.Drawing.Point(251, 122);
            this.txt_maDuAn.Name = "txt_maDuAn";
            this.txt_maDuAn.Size = new System.Drawing.Size(141, 22);
            this.txt_maDuAn.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(152, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 19);
            this.label2.TabIndex = 8;
            this.label2.Text = "Mã Dự Án";
            // 
            // cb_dauViec
            // 
            this.cb_dauViec.FormattingEnabled = true;
            this.cb_dauViec.Location = new System.Drawing.Point(251, 200);
            this.cb_dauViec.Name = "cb_dauViec";
            this.cb_dauViec.Size = new System.Drawing.Size(177, 24);
            this.cb_dauViec.TabIndex = 10;
            this.cb_dauViec.SelectedIndexChanged += new System.EventHandler(this.cb_dauViec_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(131, 201);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 19);
            this.label3.TabIndex = 11;
            this.label3.Text = "Mục đầu việc";
            // 
            // txt_maPhong
            // 
            this.txt_maPhong.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_maPhong.Location = new System.Drawing.Point(823, 221);
            this.txt_maPhong.Name = "txt_maPhong";
            this.txt_maPhong.Size = new System.Drawing.Size(124, 27);
            this.txt_maPhong.TabIndex = 15;
            // 
            // txt_moTa
            // 
            this.txt_moTa.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_moTa.Location = new System.Drawing.Point(823, 109);
            this.txt_moTa.Multiline = true;
            this.txt_moTa.Name = "txt_moTa";
            this.txt_moTa.Size = new System.Drawing.Size(251, 93);
            this.txt_moTa.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(710, 221);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 19);
            this.label6.TabIndex = 13;
            this.label6.Text = "Mã Phòng";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(722, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 19);
            this.label5.TabIndex = 12;
            this.label5.Text = "Mô Tả";
            // 
            // DGV_DAUVIEC
            // 
            this.DGV_DAUVIEC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_DAUVIEC.Location = new System.Drawing.Point(214, 282);
            this.DGV_DAUVIEC.Name = "DGV_DAUVIEC";
            this.DGV_DAUVIEC.RowHeadersWidth = 51;
            this.DGV_DAUVIEC.RowTemplate.Height = 24;
            this.DGV_DAUVIEC.Size = new System.Drawing.Size(751, 269);
            this.DGV_DAUVIEC.TabIndex = 16;
            // 
            // BangThongKeDuAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 668);
            this.Controls.Add(this.DGV_DAUVIEC);
            this.Controls.Add(this.txt_maPhong);
            this.Controls.Add(this.txt_moTa);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cb_dauViec);
            this.Controls.Add(this.txt_maDuAn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bt_exit);
            this.Controls.Add(this.label1);
            this.Name = "BangThongKeDuAn";
            this.Text = "BangThongKeDuAn";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BangThongKeDuAn_FormClosed);
            this.Load += new System.EventHandler(this.BangThongKeDuAn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_DAUVIEC)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_exit;
        private System.Windows.Forms.TextBox txt_maDuAn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_dauViec;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_maPhong;
        private System.Windows.Forms.TextBox txt_moTa;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView DGV_DAUVIEC;
    }
}