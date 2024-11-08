using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMresourcemanagementsystem.ChamCong
{
    public partial class BangCong : Form
    {
        connection kn = new connection();
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;
        SqlConnection connect;
        public BangCong()
        {
            InitializeComponent();
            connect = kn.con;
        }
        
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            connect.Open();
            
        }
        void load_datagridview()
        {
            connect.Open();

            try
            {
                string sql = @"SELECT BC.MANV, NV.HoTen, BC.THU,BC.NGAY,BC.THANG,BC.NAM,BC.TRANGTHAI
                         FROM BANGCONG BC 
                        JOIN NHANVIEN NV ON NV.MANV = BC.MANV
                        ORDER BY BC.NGAY ASC, BC.THANG ASC, BC.NAM ASC
                         ";
                cmd = new SqlCommand(sql, connect);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                // đổ dữ liệu lên datatable
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                connect.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        
        private void BangCong_Load(object sender, EventArgs e)
        {
            load_datagridview();

            checkBox_ngay.Checked = true;
            checkBox_thang.Checked = true;
            checkBox_nam.Checked = true;
        }
        private void kiemtra()
        {
            string sql;
            DateTime selectedDate = DTP.Value;
            //lấy giá trị ngày tháng năm 
            int ngay = selectedDate.Day;
            int thang = selectedDate.Month;
            int nam = selectedDate.Year;
            // Kiểm tra các checkbox và tạo câu truy vấn SQL tương ứng
            if (checkBox_ngay.Checked && checkBox_thang.Checked && checkBox_nam.Checked)
            {
                // Nếu tất cả checkbox được chọn (ngày, tháng, năm)
                sql = @"SELECT BC.MANV, NV.HoTen, BC.THU, BC.NGAY, BC.THANG, BC.NAM, BC.TRANGTHAI
                FROM BANGCONG BC
                JOIN NHANVIEN NV ON NV.MANV = BC.MANV
                WHERE BC.NGAY = @NGAY AND BC.THANG = @THANG AND BC.NAM = @NAM
                ORDER BY BC.MANV ASC";
                Load_ngayThangNam(sql, ngay, thang, nam);
            }
            else if (checkBox_thang.Checked && checkBox_nam.Checked)
            {
                // Nếu chỉ chọn tháng và năm (bỏ qua ngày)
                sql = @"SELECT BC.MANV, NV.HoTen, BC.THU, BC.NGAY, BC.THANG, BC.NAM, BC.TRANGTHAI
                FROM BANGCONG BC
                JOIN NHANVIEN NV ON NV.MANV = BC.MANV
                WHERE BC.THANG = @THANG AND BC.NAM = @NAM
                ORDER BY BC.MANV ASC";
                Load_ngayThangNam(sql, null, thang, nam); // Bỏ qua ngày
            }
            else if (checkBox_nam.Checked)
            {
                // Nếu chỉ chọn năm
                sql = @"SELECT BC.MANV, NV.HoTen, BC.THU, BC.NGAY, BC.THANG, BC.NAM, BC.TRANGTHAI
                FROM BANGCONG BC
                JOIN NHANVIEN NV ON NV.MANV = BC.MANV
                WHERE BC.NAM = @NAM
                ORDER BY BC.MANV ASC";
                Load_ngayThangNam(sql, null, null, nam); // Bỏ qua ngày và tháng
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một điều kiện tìm kiếm.");
            }
        }

        //Tìm kiếm nhân viên theo ngày tháng năm
        private void Load_ngayThangNam(string sql,int? ngay,int? thang,int nam)//giá trị ngày tháng có thể null
        {
            connect.Open();
            dataGridView1.DataSource = null;
            //lấy giá trị của datetimepicker
            
            
            using (cmd = new SqlCommand(sql, connect))
            {
                try
                {

                    // Chỉ thêm tham số khi có giá trị
                    if (ngay.HasValue)
                        cmd.Parameters.AddWithValue("@NGAY", ngay.Value);
                    if (thang.HasValue)
                        cmd.Parameters.AddWithValue("@THANG", thang.Value);

                    cmd.Parameters.AddWithValue("@NAM", nam);
                    using (SqlDataReader dr =cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            // Tải dữ liệu từ SqlDataReader vào DataTable
                            dt = new DataTable();
                            dt.Load(dr);

                            // Hiển thị dữ liệu lên DataGridView (giả sử bạn có DataGridView tên là 'dataGridView')
                            dataGridView1.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Không có dữ liệu cho ngày, tháng, năm đã chọn.");
                        }
                    }    

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            connect.Close();
        }

        

        void timKiemTheoTen()
        {
            try 
            { 
                connect.Open();
                string maNV = txt_maNV.Text;
                string tenNV = txt_tenNhanVien.Text;
                string sql = @"SELECT BC.MANV,NV.HoTen,BC.THU,BC.NGAY,BC.THANG,BC.NAM,BC.TRANGTHAI 
                                FROM BANGCONG BC 
                                JOIN NHANVIEN NV ON NV.MANV=BC.MANV
                                WHERE BC.MANV = @MANV OR NV.HoTen=@TENNV      
                                ORDER BY MANV ASC
                                ";
                using (cmd = new SqlCommand(sql, connect))
                {
                    try
                    {
                        //Thêm tham số mã nhân viên, tên nhân viên vào 
                        cmd.Parameters.AddWithValue("@MANV", maNV);
                        cmd.Parameters.AddWithValue("@TENNV", tenNV);
                        //SqlDataReader đọc kết quả từ sql
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            // kiểm tra sqldatareader có dữ liệu không 
                            if (dr.HasRows)
                            {
                                //tạo table lưu trữ dữ liệu sqldatareader
                                dt = new DataTable();
                                dt.Load(dr);
                                // Hiển thị dữ liệu lên DataGridView 
                                dataGridView1.DataSource = dt;

                            }
                            else
                            {
                                MessageBox.Show("Không có dữ liệu cho Mã hoặc Tên đã chọn.");
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                    connect.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void quayLạiToolStripMenuItem_Click(object sender, EventArgs e)
        {
                this.Hide();
                ChamCong.ListChamCong lstcc = new ChamCong.ListChamCong();
                lstcc.ShowDialog();
                
        }

        private void bt_timKiemTheoNgay_Click_1(object sender, EventArgs e)
        {
            kiemtra();
        }

        private void bt_timKiemTheoTen_Click(object sender, EventArgs e)
        {
            timKiemTheoTen();
        }

        
    }
}
