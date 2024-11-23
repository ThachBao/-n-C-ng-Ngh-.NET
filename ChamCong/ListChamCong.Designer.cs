namespace HMresourcemanagementsystem.ChamCong
{
    partial class ListChamCong
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
            this.bt_xuatFileExcel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tùyChọnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bảngCôngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quayLạiTrangChủToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btn_exit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_maNhanVien = new System.Windows.Forms.TextBox();
            this.bt_xacNhan = new System.Windows.Forms.Button();
            this.Rb_vao = new System.Windows.Forms.RadioButton();
            this.Rb_ra = new System.Windows.Forms.RadioButton();
            this.bt_lamMoi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(25, 119);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(751, 363);
            this.dataGridView1.TabIndex = 27;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // bt_xuatFileExcel
            // 
            this.bt_xuatFileExcel.Location = new System.Drawing.Point(542, 503);
            this.bt_xuatFileExcel.Name = "bt_xuatFileExcel";
            this.bt_xuatFileExcel.Size = new System.Drawing.Size(283, 69);
            this.bt_xuatFileExcel.TabIndex = 26;
            this.bt_xuatFileExcel.Text = "Xuất Excel";
            this.bt_xuatFileExcel.UseVisualStyleBackColor = true;
            this.bt_xuatFileExcel.Click += new System.EventHandler(this.bt_xuatFileExcel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(307, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 37);
            this.label1.TabIndex = 25;
            this.label1.Text = "Chấm Công ";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tùyChọnToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1181, 30);
            this.menuStrip1.TabIndex = 28;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tùyChọnToolStripMenuItem
            // 
            this.tùyChọnToolStripMenuItem.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tùyChọnToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bảngCôngToolStripMenuItem,
            this.quayLạiTrangChủToolStripMenuItem});
            this.tùyChọnToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tùyChọnToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tùyChọnToolStripMenuItem.Name = "tùyChọnToolStripMenuItem";
            this.tùyChọnToolStripMenuItem.Size = new System.Drawing.Size(101, 26);
            this.tùyChọnToolStripMenuItem.Text = "Tùy Chọn";
            // 
            // bảngCôngToolStripMenuItem
            // 
            this.bảngCôngToolStripMenuItem.Name = "bảngCôngToolStripMenuItem";
            this.bảngCôngToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.bảngCôngToolStripMenuItem.Text = "Bảng Công";
            this.bảngCôngToolStripMenuItem.Click += new System.EventHandler(this.bảngCôngToolStripMenuItem_Click);
            // 
            // quayLạiTrangChủToolStripMenuItem
            // 
            this.quayLạiTrangChủToolStripMenuItem.Name = "quayLạiTrangChủToolStripMenuItem";
            this.quayLạiTrangChủToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.quayLạiTrangChủToolStripMenuItem.Text = "Quay Lại Trang Chủ";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(852, 503);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(283, 66);
            this.btn_exit.TabIndex = 29;
            this.btn_exit.Text = "Thoát";
            this.btn_exit.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(804, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 26);
            this.label2.TabIndex = 30;
            this.label2.Text = "Mã Nhân Viên";
            // 
            // txt_maNhanVien
            // 
            this.txt_maNhanVien.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_maNhanVien.Location = new System.Drawing.Point(833, 171);
            this.txt_maNhanVien.Name = "txt_maNhanVien";
            this.txt_maNhanVien.Size = new System.Drawing.Size(192, 34);
            this.txt_maNhanVien.TabIndex = 31;
            // 
            // bt_xacNhan
            // 
            this.bt_xacNhan.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_xacNhan.Location = new System.Drawing.Point(869, 277);
            this.bt_xacNhan.Name = "bt_xacNhan";
            this.bt_xacNhan.Size = new System.Drawing.Size(177, 44);
            this.bt_xacNhan.TabIndex = 32;
            this.bt_xacNhan.Text = "Xác Nhận";
            this.bt_xacNhan.UseVisualStyleBackColor = true;
            this.bt_xacNhan.Click += new System.EventHandler(this.bt_xacNhan_Click);
            // 
            // Rb_vao
            // 
            this.Rb_vao.AutoSize = true;
            this.Rb_vao.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rb_vao.Location = new System.Drawing.Point(830, 230);
            this.Rb_vao.Name = "Rb_vao";
            this.Rb_vao.Size = new System.Drawing.Size(71, 30);
            this.Rb_vao.TabIndex = 33;
            this.Rb_vao.TabStop = true;
            this.Rb_vao.Text = "Vào";
            this.Rb_vao.UseVisualStyleBackColor = true;
            this.Rb_vao.CheckedChanged += new System.EventHandler(this.Rb_vao_CheckedChanged);
            // 
            // Rb_ra
            // 
            this.Rb_ra.AutoSize = true;
            this.Rb_ra.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rb_ra.Location = new System.Drawing.Point(913, 230);
            this.Rb_ra.Name = "Rb_ra";
            this.Rb_ra.Size = new System.Drawing.Size(58, 30);
            this.Rb_ra.TabIndex = 34;
            this.Rb_ra.TabStop = true;
            this.Rb_ra.Text = "Ra";
            this.Rb_ra.UseVisualStyleBackColor = true;
            this.Rb_ra.CheckedChanged += new System.EventHandler(this.Rb_ra_CheckedChanged);
            // 
            // bt_lamMoi
            // 
            this.bt_lamMoi.Location = new System.Drawing.Point(68, 515);
            this.bt_lamMoi.Name = "bt_lamMoi";
            this.bt_lamMoi.Size = new System.Drawing.Size(155, 66);
            this.bt_lamMoi.TabIndex = 35;
            this.bt_lamMoi.Text = "Làm Mới";
            this.bt_lamMoi.UseVisualStyleBackColor = true;
            this.bt_lamMoi.Click += new System.EventHandler(this.bt_lamMoi_Click);
            // 
            // ListChamCong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 593);
            this.Controls.Add(this.bt_lamMoi);
            this.Controls.Add(this.Rb_ra);
            this.Controls.Add(this.Rb_vao);
            this.Controls.Add(this.bt_xacNhan);
            this.Controls.Add(this.txt_maNhanVien);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.bt_xuatFileExcel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ListChamCong";
            this.Text = "ListChamCong";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ListChamCong_FormClosing_1);
            this.Load += new System.EventHandler(this.ListChamCong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button bt_xuatFileExcel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tùyChọnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bảngCôngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quayLạiTrangChủToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_maNhanVien;
        private System.Windows.Forms.Button bt_xacNhan;
        private System.Windows.Forms.RadioButton Rb_vao;
        private System.Windows.Forms.RadioButton Rb_ra;
        private System.Windows.Forms.Button bt_lamMoi;
    }
}