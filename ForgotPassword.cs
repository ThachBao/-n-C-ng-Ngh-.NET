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
    public partial class ForgotPassword : Form
    {
        connection kn = new connection();
        SqlConnection connect;
        public ForgotPassword()
        {
            InitializeComponent();
            connect =kn.con;
        }

        private void link_signup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            SignUp signUp = new SignUp();
            signUp.ShowDialog(); // Hiển thị form đăng ký
            this.Close(); // hiển thị form signup ẩn form login
        }

        private void link_login_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog(); // Hiển thị form đăng ký
            this.Close(); // hiển thị form signup ẩn form login
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_forgotpass_Click(object sender, EventArgs e)
        {
            //Kiểm tra thông tin tài khoản có giống với trong csdl không
            try
            {
                if (string.IsNullOrEmpty(txt_email.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;

                }

                connect.Open();

                string sql = "select MATKHAU from  ACCOUNTS where EMAIL = @email ";
                SqlCommand command = new SqlCommand(sql, connect);
                command.Parameters.AddWithValue("@email", txt_email.Text);

                var result = command.ExecuteScalar();

                if (result != null)
                {
                    string matKhau = (string)result; // Ép kiểu về string
                    MessageBox.Show($"Mật khẩu của bạn là: {matKhau}", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_email.Clear();
                    txt_email.Focus();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tài khoản với email này!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_email.Clear();
                    txt_email.Focus();
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
    }
}
