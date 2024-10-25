using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMresourcemanagementsystem
{
    public partial class FormMain : Form
    {
        private string role;
        private string tendangnhap;

        public FormMain(string PHANQUYEN, string ten)
        {
            InitializeComponent();
            role = PHANQUYEN;
            tendangnhap = ten;
            if (PHANQUYEN == "ADMIN")
            {
                btn_chamcong.Enabled = false;
                btn_luong.Enabled = false;
            }
            else if(PHANQUYEN == "QLCHAMCONG")
            {
                btn_qltaikhoan.Enabled = false;
                btn_nhanvien.Enabled = false;
                btn_phongban.Enabled = false;
                btn_bophan.Enabled = false;
                btn_daotao.Enabled = false;
                btn_duan.Enabled = false;
                btn_luong.Enabled = false;
            }    
            else
            {
                btn_qltaikhoan.Enabled = false;
                btn_nhanvien.Enabled = false;
                btn_phongban.Enabled = false;
                btn_bophan.Enabled = false;
                btn_daotao.Enabled = false;
                btn_duan.Enabled = false;
                btn_chamcong.Enabled = false;
            }
        }
        public bool checkMdiChildren(string formName)
        {
            if(this.MdiChildren.Length > 0)
            {
                Form[] frm = this.MdiChildren;
                for(int i=0; i < this.MdiChildren.Length; i++)
                {
                    if (frm[i].Name == formName)
                        return false;
                }    
            }    
            return true;
        }

        private void mn_changepass_Click(object sender, EventArgs e)
        {
            ChangePassword change = new ChangePassword();
            change.ShowDialog();
        }



        private void btn_dangxuat_Click(object sender, EventArgs e)
        {
            DialogResult dia;
            dia = MessageBox.Show("Bạn Có Muốn Đăng Xuất Khỏi Phần Mềm?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dia == DialogResult.Yes)
            {
                this.Hide();
                Login login = new Login();
                login.ShowDialog();
                this.Close();
            }

        }



        private void btn_close_Click(object sender, EventArgs e)
        {
            DialogResult dia;
            dia = MessageBox.Show("Bạn Có Muốn Thoát Khỏi Phần Mềm?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dia == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btn_qltaikhoan_Click(object sender, EventArgs e)
        {
            this.pnl_formchild.Controls.Clear();
            Quanly mana = new Quanly();
            mana.TopLevel = false;
            mana.Dock = DockStyle.Fill;
            pnl_formchild.Controls.Add(mana);
            mana.Show();
        }

        private void btn_info_Click(object sender, EventArgs e)
        {
            this.pnl_formchild.Controls.Clear();
            ShowInfoAdmin userInfoForm = new ShowInfoAdmin(tendangnhap);
            userInfoForm.TopLevel = false;
            userInfoForm.Dock = DockStyle.Fill;
            pnl_formchild.Controls.Add(userInfoForm);
            userInfoForm.Show();
        }
    }
}
