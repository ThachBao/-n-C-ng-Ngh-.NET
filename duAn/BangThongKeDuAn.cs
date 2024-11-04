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
using System.Text.RegularExpressions;
namespace HMresourcemanagementsystem.duAn
{
    public partial class BangThongKeDuAn : Form
    {
        connection kn = new connection();
        SqlConnection connection;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        //Khởi tạo idDuAn Để Lưu trữ mã dự án Từ bảng quản lý dự án
        public string idDuAn { get; set; }

        public BangThongKeDuAn()
        {
            InitializeComponent();
            connection = kn.con;
            cb_dauViec.SelectedIndexChanged += new EventHandler(cb_dauViec_SelectedIndexChanged);
            this.FormClosed += new FormClosedEventHandler(BangThongKeDuAn_FormClosed);
        }

        private void BangThongKeDuAn_Load(object sender, EventArgs e)
        {
            txt_maDuAn.Enabled = false;
            txt_maPhong.Enabled = false;
            txt_moTa.Enabled = false;
            load_dataGridView_Start(idDuAn);
            LoadData();
            load_combobox(txt_maDuAn.Text);

        }
        private void load_combobox(string maDuAn)
        {
            try
            {


                connection.Open();
                string sql = @"SELECT DVDA.TenDauViec,DVDA.MADAUVIEC
                            FROM DAUVIECDUAN DVDA
                            WHERE DVDA.MaDuAn=@maDuAn
                            ";
                using (cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@maDuAn", maDuAn);
                    cb_dauViec.Items.Clear();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        string tendauviec = dr["TenDauViec"].ToString();
                        string madauviec = dr["MADAUVIEC"].ToString();

                        // Thêm vào ComboBox
                        cb_dauViec.Items.Add(new
                        {
                            Value = madauviec,
                            Text = tendauviec
                        });

                        cb_dauViec.DisplayMember = "Text";
                        cb_dauViec.ValueMember = "Value";

                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex);
            }
            finally
            {
                connection.Close();
            }
        }
        private void load_dataGridView_Start(string maDuAn)
        {
            DGV_DAUVIEC.DataSource = null;

            // Xây dựng câu truy vấn SQL dựa trên điều kiện mã dự án
            string sql = @"
                    SELECT DVDA.MaDauViec AS N'Mã Đầu Việc', DVDA.TenDauViec AS N'Tên Đầu Việc', NV.HoTen AS N'Họ Tên Nhân Viên'
                    FROM DAUVIECDUAN DVDA
                    JOIN NHANVIEN NV ON NV.MaNV = DVDA.MaNV
                    WHERE DVDA.MaDuAn = @maDuAn
                ";

            try
            {
                connection.Open();

                using (cmd = new SqlCommand(sql, connection))
                {
                    // Chỉ thêm tham số khi có mã dự án
                    if (!string.IsNullOrEmpty(maDuAn))
                    {
                        cmd.Parameters.AddWithValue("@maDuAn", maDuAn);
                    }

                    using (da = new SqlDataAdapter(cmd))
                    {
                        dt = new DataTable();
                        da.Fill(dt);
                        DGV_DAUVIEC.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void LoadData()
        {

            // sử dụng dữ liệu id DU AN
            txt_maDuAn.Text = idDuAn.ToString();
            string maduan = txt_maDuAn.Text;
            connection.Open();
            string sql = @"SELECT DA.MOTA,DA.MAPHONG
                            FROM DUAN DA
                            WHERE DA.MADUAN =@maduan
                        ";
            try
            {
                using (cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@maduan", maduan);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txt_moTa.Text = dr["MoTa"].ToString();
                        txt_maPhong.Text = dr["MaPhong"].ToString();
                    }


                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        private void load_dataGridView(string MaDuAn,string maDauViec)
        {
            DGV_DAUVIEC.DataSource = null; // Đặt lại DataSource về null thay vì false

            string sql = @"
                    SELECT DVDA.MaDauViec AS N'Mã Đầu Việc Dự Án', 
                           DVDA.TenDauViec AS N'Tên Đầu Việc', 
                           NV.HoTen AS N'Tên Nhân Viên'
                    FROM DAUVIECDUAN DVDA
                    JOIN NHANVIEN NV ON NV.MaNV = DVDA.MaNV
                    WHERE DVDA.MaDuAn = @MaDuAn AND DVDA.MaDauViec=@maDauViec
                  ";

            // Kiểm tra nếu MaDuAn là null hoặc rỗng
            if (string.IsNullOrEmpty(MaDuAn))
            {
                MessageBox.Show("Mã dự án không hợp lệ.");
                return;
            }

            try
            {
                // Sử dụng using để đảm bảo đóng kết nối
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@MaDuAn", MaDuAn);
                    cmd.Parameters.AddWithValue("@maDauViec", maDauViec);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        DGV_DAUVIEC.DataSource = dt;
                    }

                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void cb_dauViec_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lấy giá trị đầu vào 
            string selectedValue = cb_dauViec.SelectedItem?.ToString();
            string maDauViec = string.Empty;
            string tenDauViec = string.Empty;
            if (!string.IsNullOrEmpty(selectedValue))
            {
                var match = Regex.Match(selectedValue, @"\{ Value = (.*?), Text = (.*?) \}");
                if (match.Success)
                {
                    maDauViec = match.Groups[1].Value; 
                    tenDauViec = match.Groups[2].Value; 
                }
                txt_maDuAn.Enabled = true;
                string maDuAn = txt_maDuAn.Text;
                txt_maDuAn.Enabled = false; 

                // Tải dữ liệu lên DataGridView với mã đầu việc dự án
                load_dataGridView(maDuAn, maDauViec);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đầu việc.");
            }

        }

        private void bt_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //load lại datagridview ở phần trước 
        private void BangThongKeDuAn_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Khi form này bị đóng
            QuanLyDuAn ql = (QuanLyDuAn)this.Owner;
            ql.ReloadDataGridView();
        }
    }
}


