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
    public partial class QuanLyPhongBan : Form
    {
        connection kn = new connection();
        SqlConnection connection;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        public QuanLyPhongBan()
        {
            InitializeComponent();
            connection = kn.con;
        }
        void load_treeView_phongBan()
        {
            try
            {
                //kết nối database
                connection.Open();
                string sql = @" SELECT * 
                                FROM PHONGBAN PB    

                                ";
                using (cmd = new SqlCommand(sql, connection))
                {
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    // Xóa các nút hiện tại trong TreeView trước khi thêm mới
                    treeView_phongBan.Nodes.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        string tenPhongBan = row["TenPhong"].ToString();
                        treeView_phongBan.Nodes.Add(tenPhongBan);
                    } 
                }
                connection.Close();

            } 
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void load_combobox()
        {
            connection.Open();
            try
            {
                string sql = @"SELECT TENPHONG FROM PHONGBAN";
                using (cmd = new SqlCommand(sql,connection))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        cb_phongBan.Items.Clear();
                        while (dr.Read())
                        {
                            cb_phongBan.Items.Add(dr["TENPHONG"].ToString());
                        }    
                    }    
                }    
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void QuanLyPhongBan_Load(object sender, EventArgs e)
        {
            load_treeView_phongBan();
            load_combobox();
        }
        bool kiemTraTrungTenPhongBan(string s)
        {
            try
            {
                connection.Open();
                string sql = @"SELECT COUNT(*)
                            FROM PHONGBAN 
                            WHERE TenPhong=@TenPhongBan
                        ";

                cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@TenPhongBan",s);
                //Thực thi lệnh và lấy số lượng kết quả
                object result = cmd.ExecuteScalar();

                // Kiểm tra xem result có phải là null không
                if (result != null)
                {
                    int count = Convert.ToInt32(result);
                    return count > 0; // Trả về true nếu có ít nhất một phòng ban trùng tên
                }
                else
                {
                    return false; // Nếu không có giá trị trả về
                }
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false; // Trả về false nếu có lỗi
            }
            finally
            {
                // Đảm bảo đóng kết nối
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        private void bt_them_Click(object sender, EventArgs e)
        {
            if (!kiemTraTrungTenPhongBan(txt_tenPhongBan.Text))
            {
                
                treeView_phongBan.Nodes.Add(txt_tenPhongBan.Text);
                cb_phongBan.Items.Add(txt_tenPhongBan.Text);
                connection.Open();
                try
                {
                    string sql = @"INSERT INTO PHONGBAN (MaPhong,TenPhong) VALUES (@MAPHONGBAN,@TENPHONGBAN)";
                    using (cmd =new SqlCommand(sql,connection))
                    {
                        cmd.Parameters.AddWithValue("@MAPHONGBAN", txt_maPhongBan.Text);
                        cmd.Parameters.AddWithValue("@TENPHONGBAN", txt_tenPhongBan.Text);

                        cmd.ExecuteNonQuery();

                    }
                    connection.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Phòng Ban Đã Tồn Tại!");
            txt_tenPhongBan.Text = "";
            txt_tenPhongBan.Focus();

        }

        
    }
}
