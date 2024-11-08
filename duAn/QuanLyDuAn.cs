using DocumentFormat.OpenXml.Office.Word;
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

using static System.Net.WebRequestMethods;

namespace HMresourcemanagementsystem.duAn
{
    public partial class QuanLyDuAn : Form
    {
        connection kn = new connection();
        SqlConnection connection;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        public QuanLyDuAn()
        {
            InitializeComponent();
            connection = kn.con;
            // Đăng ký sự kiện ValueChanged
            dateBatDau.ValueChanged += dateBatDau_ValueChanged;
            dateKetThuc.ValueChanged += dateKetThuc_ValueChanged;
            DTGView.CellClick += DTGView_CellClick; 
            
        }
        public void ReloadDataGridView()
        {
            load_dataGridView();
        }
        private void QuanLyDuAn_Load(object sender, EventArgs e)
        {
            load_dataGridView();
            txt_moTa.Enabled = false;
            txt_maPhong.Enabled = false;


        }
        void load_dataGridView()
        {
            try
            {
                connection.Open();
                string sql = @"SELECT DA.MaDuAn AS N'Mã Dự Án', DA.TenDuAn AS N'Tên Dự Án',DA.NgayBatDau AS N'Ngày Bắt Đầu',DA.NgayKetThuc AS N'Ngày Kết Thúc',DA.MoTa AS N'Mô Tả',DA.MaPhong AS N'Mã Phòng'
                                FROM DUAN DA
                        "
                ;


                cmd = new SqlCommand(sql, connection);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                DTGView.DataSource = dt;
               

                // Đảm bảo cột ID_CHAMCONG không hiển thị nếu không cần
                DTGView.Columns["Mã Dự Án"].Visible = false;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void kiemtraNgayThangNam()
        {
            DateTime ngaybatdau = dateBatDau.Value; // Lấy giá trị ngày bắt đầu
            DateTime ngayketthuc = dateKetThuc.Value; // Lấy giá trị ngày kết thúc

            string sql = @"SELECT DA.MaDuAn AS N'Mã Dự Án', DA.TenDuAn AS N'Tên Dự Án', DA.NgayBatDau AS N'Ngày Bắt Đầu', DA.NgayKetThuc AS N'Ngày Kết Thúc'
                    FROM DuAn DA
                    WHERE DA.NgayBatDau >= @ngaybatdau AND DA.NgayKetThuc <= @ngayketthuc"; // Sửa điều kiện để lấy các dự án trong khoảng thời gian

            load_dulieuNgayThangNam(sql, ngaybatdau, ngayketthuc);
        }

        private void load_dulieuNgayThangNam(string sql, DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            try
            {
                connection.Open(); // Mở kết nối
                DataTable dt = new DataTable(); // Tạo DataTable mới

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    // Thêm tham số cho câu lệnh SQL
                    cmd.Parameters.AddWithValue("@ngaybatdau", ngayBatDau);
                    cmd.Parameters.AddWithValue("@ngayketthuc", ngayKetThuc);

                    // Sử dụng SqlDataAdapter để điền dữ liệu vào DataTable
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt); // Điền dữ liệu từ câu lệnh SQL vào DataTable
                    }
                }

                // Liên kết DataTable với DataGridView
                DTGView.DataSource = dt;
                DTGView.Columns["Mã Dự Án"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message); // Hiển thị thông báo lỗi
            }
            finally
            {
                // Đóng kết nối nếu nó đang mở
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        //kiểm tra id dự án này có tồn tại hay không
        private bool kiemtra(string id)
        {
            connection.Open();
            try
            {
                string sql = @"SELECT COUNT(*)
                                FROM DUAN DA
                                WHERE DA.MADUAN =@id
                                ";
                using (cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0; // Nếu count >0 thì tồn tại là có id đó thì trả về true
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
        private void bt_timKiem_Click(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrWhiteSpace(txt_maDuAn.Text))
            {
                if (kiemtra(txt_maDuAn.Text))
                {
                    string idDuAn = txt_maDuAn.Text;
                    BangThongKeDuAn thongKeDuAn = new BangThongKeDuAn();
                    // Truyền tham chiếu form quản lý dự án vào form thống kê
                    thongKeDuAn.Owner = this;
                    thongKeDuAn.idDuAn = idDuAn; // Thiết lập giá trị ID
                    thongKeDuAn.Show();
                    txt_maDuAn.Clear();
                    
                }
                else
                {
                    MessageBox.Show("Id Bạn Nhập Không Tồn Tại", "Thông Báo");
                }

            }
            

            kiemtraNgayThangNam();
            

        }


        //ràng buộc ngày tháng năm
        private void dateBatDau_ValueChanged(object sender, EventArgs e)
        {
            // Nếu ngày bắt đầu được chọn, cập nhật ngày kết thúc
            dateKetThuc.MinDate = dateBatDau.Value; // Đặt ngày tối thiểu cho dateTimePicker2
        }

        private void dateKetThuc_ValueChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu ngày kết thúc nhỏ hơn ngày bắt đầu
            if (dateKetThuc.Value < dateBatDau.Value)
            {
                MessageBox.Show("Ngày kết thúc không được chọn trước ngày bắt đầu.");
                dateKetThuc.Value = dateBatDau.Value; // Đặt lại ngày kết thúc về ngày bắt đầu
            }

        }
        private void LoadData()
        {

            // sử dụng dữ liệu id DU AN
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
                    connection.Close();
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
        //CELL CLICK TRONG DATAGRIDVIEW
        private void DTGView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            // Kiểm tra nếu có hàng được chọn
            if (e.RowIndex >= 0) // Đảm bảo chỉ thực hiện nếu có hàng được chọn
            {
                // Lấy mã dự án từ cột đầu tiên (thay đổi chỉ số cột nếu cần)
                txt_maDuAn.Text = DTGView.Rows[e.RowIndex].Cells["Mã Dự Án"].Value.ToString();

                // Gọi hàm LoadData để lấy mô tả và mã phòng
                LoadData();
                txt_moTa.Enabled = true;
                txt_maPhong.Enabled = true;
            }
        }

        private void bt_sua_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem mã dự án có được nhập hay không
            if (string.IsNullOrWhiteSpace(txt_maDuAn.Text))
            {
                MessageBox.Show("Vui lòng chọn một dự án để sửa.");
                return;
            }

            // Lấy thông tin từ các ô văn bản
            string maDuAn = txt_maDuAn.Text;
            string moTa = txt_moTa.Text;
            string maPhong = txt_maPhong.Text;
            connection.Open();
            // Cập nhật cơ sở dữ liệu
            try
            {
                
                string sql = @"UPDATE DUAN 
                       SET MoTa = @moTa, MaPhong = @maPhong 
                       WHERE MaDuAn = @maDuAn"; // Câu lệnh SQL cập nhật

                using (cmd = new SqlCommand(sql, connection))
                {
                    // Thêm tham số vào câu lệnh SQL
                    cmd.Parameters.AddWithValue("@moTa", moTa);
                    cmd.Parameters.AddWithValue("@maPhong", maPhong);
                    cmd.Parameters.AddWithValue("@maDuAn", maDuAn);

                    int rowsAffected = cmd.ExecuteNonQuery(); // Thực hiện câu lệnh SQL
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật dự án thành công.");
                        connection.Close();
                        load_dataGridView(); // Tải lại dữ liệu vào DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dự án để cập nhật.");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
            finally
            {
                connection.Close(); // Đóng kết nối
            }
        }

        private void bt_exit_Click(object sender, EventArgs e)
        {
            //thoát của bảo :)))
        }

        private void bt_xoa_Click(object sender, EventArgs e)
        {
            // Lấy mã dự án từ ô văn bản
            string maDuAn = txt_maDuAn.Text;

            // Xác nhận trước khi xóa
            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa dự án này?",
                                                 "Xác nhận xóa!",
                                                 MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                connection.Open();
                try
                {
                    // Bắt đầu giao dịch
                    using (var transaction = connection.BeginTransaction())
                    {
                        // Xóa các bản ghi liên quan trong DAUVIECDUAN
                        string sqlDelete = @"DELETE FROM DAUVIECDUAN WHERE MaDuAn = @maDuAn";
                        using (SqlCommand cmd = new SqlCommand(sqlDelete, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@maDuAn", maDuAn);
                            cmd.ExecuteNonQuery();
                        }

                        // Xóa dự án
                        string sqlDeleteDuAn = @"DELETE FROM DuAn WHERE MaDuAn = @maDuAn";
                        using (SqlCommand cmd = new SqlCommand(sqlDeleteDuAn, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@maDuAn", maDuAn);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Xóa dự án thành công.");
                                
                                
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy dự án để xóa.");
                            }
                        }

                        // Commit giao dịch
                        transaction.Commit();
                        connection.Close();
                        load_dataGridView();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                }
                finally
                {
                    connection.Close(); // Đóng kết nối
                }
            }

        }
    }

    
}  
        



