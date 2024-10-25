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
using System.Xml.Linq;

namespace HMresourcemanagementsystem
{
    public partial class ShowInfoAdmin : Form
    {
        connection kn = new connection();
        SqlConnection connect;
        private string name;

        public ShowInfoAdmin(string nameid)
        {
            InitializeComponent();
            this.name = nameid;
            connect =kn.con;
            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            try
            {
                connect.Open();
                string sql = "SELECT * FROM ACCOUNTS WHERE TENDANGNHAP = @name"; // Thay đổi truy vấn nếu cần
                SqlCommand command = new SqlCommand(sql, connect);
                command.Parameters.AddWithValue("@name", name);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    txt_tendn.Text = reader["TENDANGNHAP"].ToString(); // Giả sử bạn có cột "Name"
                    txt_quyen.Text = reader["PHANQUYEN"].ToString(); // Giả sử bạn có cột "Email"
                    txt_email.Text = reader["Email"].ToString(); // Giả sử bạn có cột "PHANQUYEN"
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin người dùng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            DialogResult dia;
            dia = MessageBox.Show("Bạn Có Muốn Thoát?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dia == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}


