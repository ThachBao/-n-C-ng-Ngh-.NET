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
    public partial class ChangePassword : Form
    {
        connection kn = new connection();
        SqlConnection connect;
        public ChangePassword()
        {
            InitializeComponent();
            connect = kn.con;
        }


        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_changepass_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_accid.Text) || string.IsNullOrEmpty(txt_passcu.Text) ||
                    string.IsNullOrEmpty(txt_passmoi.Text) || string.IsNullOrEmpty(txt_nhaplaipass.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;

                }

                connect.Open();

                string sql = "select count(*) from ACCOUNTS where TENDANGNHAP = '" + txt_accid.Text + "' and MATKHAU = '" + txt_passcu.Text + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connect);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (int.Parse(dt.Rows[0][0].ToString()) > 0)
                {
                    if (txt_passmoi.Text == txt_nhaplaipass.Text && txt_passmoi.Text.Length > 6)
                    {
                        string sql1 = "update ACCOUNTS set MATKHAU = N'" + txt_passmoi.Text + "' where TENDANGNHAP = '" + txt_accid.Text + "' and MATKHAU= '" + txt_passcu.Text + "'";
                        SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, connect);
                        DataTable dt1 = new DataTable();
                        adapter1.Fill(dt1);
                        adapter1.Update(dt1);

                        MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txt_accid.Clear();
                        txt_passcu.Clear();
                        txt_passmoi.Clear();
                        txt_nhaplaipass.Clear();
                        txt_accid.Focus();
                    }
                    else
                    {
                        if (txt_passmoi.Text != txt_nhaplaipass.Text)
                        {
                            error.SetError(txt_nhaplaipass, "Mật khẩu nhập lại chưa đúng!");
                        }
                        error.SetError(txt_passmoi, "Độ dài mật khẩu phải hơn 6 kí tự!");
                    }
                }
                else
                {
                    // Account or password incorrect
                    error.SetError(txt_accid, "Tài khoản không đúng!");
                    error.SetError(txt_passcu, "Mật khẩu cũ không đúng!");
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
