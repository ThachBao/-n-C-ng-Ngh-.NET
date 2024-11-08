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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(182, 111);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(814, 363);
            this.dataGridView1.TabIndex = 27;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // bt_xuatFileExcel
            // 
            this.bt_xuatFileExcel.Location = new System.Drawing.Point(53, 500);
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
            this.label1.Location = new System.Drawing.Point(371, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(434, 37);
            this.label1.TabIndex = 25;
            this.label1.Text = "Bảng Danh Sách Chấm Công ";
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
            // ListChamCong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 593);
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
    }
}