using HMresourcemanagementsystem.Group;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HMresourcemanagementsystem
{
    public partial class Login : Form
    {
        connection kn = new connection();
        SqlConnection connect;
        public static string loaitaikhoan = "";
        public Login()
        {
            InitializeComponent();
            connect = kn.con;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            DialogResult dia;
            dia = MessageBox.Show("Bạn Có Muốn Thoát Khỏi Phần Mềm?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(dia == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void txt_user_Click(object sender, EventArgs e)
        {
            txt_user.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = SystemColors.Control;
            txt_pass.BackColor = SystemColors.Control;
        }

        private void txt_pass_Click(object sender, EventArgs e)
        {
            txt_pass.BackColor = Color.White;
            panel4.BackColor = Color.White;
            txt_user.BackColor = SystemColors.Control;
            panel3.BackColor = SystemColors.Control;
        }
        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            txt_pass.UseSystemPasswordChar = false;
        }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            txt_pass.UseSystemPasswordChar = true;
        }

        private void link_signup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            SignUp signUp = new SignUp();
            signUp.ShowDialog(); // Hiển thị form đăng ký
            this.Close(); // hiển thị form signup ẩn form login
        }

        private void link_forgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            ForgotPassword forgot = new ForgotPassword();
            forgot.ShowDialog(); // Hiển thị form quên mật khẩu
            this.Close(); // hiển thị form quên mật khẩu ẩn form login
        }
        public void DangNhap()
        {
            //Kiểm tra thông tin tài khoản có giống với trong csdl không
            try
            {
                if (string.IsNullOrEmpty(txt_user.Text) || string.IsNullOrEmpty(txt_pass.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;

                }

                connect.Open();

                string sql = "select count(*) from ACCOUNTS where TENDANGNHAP = @name and MATKHAU = @password";
                SqlCommand command = new SqlCommand(sql, connect);
                command.Parameters.AddWithValue("@name", txt_user.Text);
                command.Parameters.AddWithValue("@password", txt_pass.Text);

                // Thực thi lệnh và lấy kết quả đếm
                int result = (int)command.ExecuteScalar();

                if (result > 0)
                {
                    MessageBox.Show("Đăng nhập thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    string sql1 = "select PHANQUYEN from ACCOUNTS where TENDANGNHAP = @name";
                    SqlCommand command1 = new SqlCommand(sql1, connect);
                    command1.Parameters.AddWithValue("@name", txt_user.Text);

                    loaitaikhoan = (string)command1.ExecuteScalar();
                    this.Hide();
                    FormMain hm = new FormMain(loaitaikhoan,txt_user.Text);
                    hm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }
        }
        private void btn_DN_Click(object sender, EventArgs e)
        {
            DangNhap();
        }

        private void txt_user_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Nếu phím nhấn là Enter
            {
                e.Handled = true; // Ngăn không cho sự kiện này tiếp tục
                e.SuppressKeyPress = true; // Ngăn không cho bấm Enter tiếp tục
                txt_pass.Focus();

            }
        }

        private void txt_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Nếu phím nhấn là Enter
            {
                e.Handled = true; // Ngăn không cho sự kiện này tiếp tục
                e.SuppressKeyPress = true; // Ngăn không cho bấm Enter tiếp tục

                // Gọi hàm đăng nhập
                DangNhap();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txt_user.Focus();
        }
    }
}
