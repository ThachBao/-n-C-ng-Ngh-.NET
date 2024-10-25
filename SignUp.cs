using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMresourcemanagementsystem.Group
{
    public partial class SignUp : Form
    {
        connection kn = new connection();
        SqlConnection connect;

        public SignUp()
        {
            InitializeComponent();
            connect = kn.con;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void eye1_MouseDown(object sender, MouseEventArgs e)
        {
            txt_mk.UseSystemPasswordChar = false;
        }

        private void eye1_MouseUp(object sender, MouseEventArgs e)
        {
            txt_mk.UseSystemPasswordChar = true;

        }

        private void eye2_MouseDown(object sender, MouseEventArgs e)
        {
            txt_nlmk.UseSystemPasswordChar = false;
        }

        private void eye2_MouseUp(object sender, MouseEventArgs e)
        {
            txt_nlmk.UseSystemPasswordChar = true;

        }

        private void link_login_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog(); // Hiển thị form đăng nhập
            this.Close(); // hiển thị form login ẩn form signup
        }

        private void btn_dangky_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_tendn.Text) ||
                    string.IsNullOrEmpty(txt_mk.Text) ||
                    string.IsNullOrEmpty(txt_nlmk.Text) ||
                    string.IsNullOrEmpty(txt_email.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra mật khẩu và xác nhận mật khẩu có khớp nhau không
                if (txt_mk.Text != txt_nlmk.Text)
                {
                    MessageBox.Show("Mật khẩu và xác nhận mật khẩu không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!txt_email.Text.Contains('@') || !txt_email.Text.EndsWith(".com"))
                {
                    MessageBox.Show("Email không hợp lệ! Vui lòng có @ và .com.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                connect.Open();

                // Kiểm tra xem tài khoản đã tồn tại chưa
                string checkSql = "SELECT COUNT(*) FROM ACCOUNTS WHERE TENDANGNHAP = @name OR EMAIL = @email";
                SqlCommand checkCommand = new SqlCommand(checkSql, connect);
                checkCommand.Parameters.AddWithValue("@name", txt_tendn.Text);
                checkCommand.Parameters.AddWithValue("@email", txt_email.Text);

                int exists = (int)checkCommand.ExecuteScalar();
                if (exists > 0)
                {
                    MessageBox.Show("Tài khoản hoặc email đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Thêm tài khoản mới vào cơ sở dữ liệu
                string sql = "INSERT INTO ACCOUNTS (TENDANGNHAP, MATKHAU, PHANQUYEN, EMAIL) VALUES (@nameDN, @password, @phanQuyen, @email )";
                SqlCommand command = new SqlCommand(sql, connect);
                command.Parameters.AddWithValue("@nameDN", txt_tendn.Text);
                command.Parameters.AddWithValue("@password", txt_mk.Text);
                command.Parameters.AddWithValue("@email", txt_email.Text);
                command.Parameters.AddWithValue("@phanQuyen", "USER"); // Mặc định PhanQuyen là user

                command.ExecuteNonQuery(); // Thực thi lệnh thêm

                MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); 
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

        private void SignUp_Load(object sender, EventArgs e)
        {
            txt_tendn.Focus();
        }
    }
}
