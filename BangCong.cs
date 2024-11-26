using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
namespace HMresourcemanagementsystem.ChamCong
{
    public partial class BangCong : Form
    {
        connection kn = new connection();
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;
        SqlConnection connect;

        // Tạo danh sách các ngày lễ


        public BangCong()
        {
            InitializeComponent();
            connect = kn.con;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            LayDanhSachNgayLe();
            SaveHolidaysToDatabase();
        }
        private List<NgayLe> danhSachNgayLe = new List<NgayLe>();
        private void LayDanhSachNgayLe()
        {
            ChineseLunisolarCalendar lunarCalendar = new ChineseLunisolarCalendar();

            for (int nam = 2024; nam <= 2030; nam++)
            {
                // Tính ngày mùng 1 Tết Nguyên Đán
                DateTime tet = lunarCalendar.ToDateTime(nam, 1, 1, 0, 0, 0, 0);
                danhSachNgayLe.Add(new NgayLe(tet.Day, tet.Month, nam, "Tết Nguyên Đán (mùng 1 Tết)"));
                danhSachNgayLe.Add(new NgayLe(tet.Day + 1, tet.Month, nam, "Tết Nguyên Đán (mùng 2 Tết)"));
                danhSachNgayLe.Add(new NgayLe(tet.Day + 2, tet.Month, nam, "Tết Nguyên Đán (mùng 3 Tết)"));
                // Các ngày lễ khác trong năm
                danhSachNgayLe.Add(new NgayLe(30, 4, nam, "Ngày Giải phóng miền Nam"));
                danhSachNgayLe.Add(new NgayLe(1, 5, nam, "Ngày Quốc tế Lao động"));
                danhSachNgayLe.Add(new NgayLe(2, 9, nam, "Ngày Quốc khánh"));
                // Lễ Giáng Sinh
                danhSachNgayLe.Add(new NgayLe(25, 12, nam, "Lễ Giáng Sinh"));
            }
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra cột NGAY
            if (dataGridView1.Columns[e.ColumnIndex].Name == "NGAY")
            {
                try
                {
                    int ngay = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["NGAY"].Value);
                    int thang = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["THANG"].Value);
                    int nam = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["NAM"].Value);

                    // Kiểm tra xem năm, tháng, ngày có hợp lệ không
                    if (IsValidDate(nam, thang, ngay))
                    {
                        DateTime currentDate = new DateTime(nam, thang, ngay);

                        // Kiểm tra xem ngày có phải là ngày lễ không
                        if (danhSachNgayLe.Exists(n => n.Ngay == currentDate.Day && n.Thang == currentDate.Month && n.Nam == currentDate.Year))
                        {
                            e.CellStyle.BackColor = System.Drawing.Color.Red; // Tô màu đỏ cho ô ngày
                            e.CellStyle.ForeColor = System.Drawing.Color.White; // Tô màu chữ trắng

                            // Tô màu ô mô tả
                            int motaColumnIndex = dataGridView1.Columns["MOTA"].Index; // Lấy chỉ số cột MOTA
                            if (motaColumnIndex >= 0) // Kiểm tra cột tồn tại
                            {
                                dataGridView1.Rows[e.RowIndex].Cells[motaColumnIndex].Style.BackColor = System.Drawing.Color.Red; // Tô màu đỏ cho ô mô tả
                                dataGridView1.Rows[e.RowIndex].Cells[motaColumnIndex].Style.ForeColor = System.Drawing.Color.White; // Tô màu chữ trắng cho mô tả
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connect.Close();
                }

            }
        }
        private bool IsValidDate(int year, int month, int day)
        {
            try
            {
                DateTime dt = new DateTime(year, month, day);
                return true;
            }
            catch
            {
                return false;
            }
        }
        void timKiemTheoTen()
        {
            try
            {
                connect.Open();
                DateTime time = DateTime.Now;
                int thang = time.Month;
                string maNV = txt_maNV.Text;
                string tenNV = txt_tenNhanVien.Text;

                // Truy vấn đầu tiên
                string sql = @"SELECT BC.MANV, NV.HoTen, BC.THU, BC.NGAY, BC.THANG, BC.NAM, BC.TRANGTHAI, BC.MOTA
                       FROM BANGCONG BC 
                       JOIN NHANVIEN NV ON NV.MANV = BC.MANV
                       WHERE (BC.MANV = @MANV OR NV.HoTen = @TENNV) AND BC.THANG = @THANG
                       ORDER BY MANV ASC";

                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@MANV", maNV);
                    cmd.Parameters.AddWithValue("@TENNV", tenNV);
                    cmd.Parameters.AddWithValue("@THANG", thang);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dt = new DataTable();
                            dt.Load(dr);
                            dataGridView1.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Không có dữ liệu cho Mã hoặc Tên đã chọn.");
                        }
                    }
                }

                // Truy vấn thứ hai
                string sql2 = @"SELECT BC.MANV AS N'Mã Nhân Viên', BC.TONGNGAYCONG AS N'Tổng Ngày Công' 
                        FROM BANGCONG BC 
                        JOIN NHANVIEN NV ON NV.MANV = BC.MANV
                        WHERE (BC.MANV = @MANV OR NV.HoTen = @TENNV) AND BC.THANG = @THANG";

                using (SqlCommand cmd2 = new SqlCommand(sql2, connect))
                {
                    cmd2.Parameters.AddWithValue("@MANV", maNV);
                    cmd2.Parameters.AddWithValue("@TENNV", tenNV);
                    cmd2.Parameters.AddWithValue("@THANG", thang);

                    using (SqlDataReader dr2 = cmd2.ExecuteReader())
                    {
                        DTGVTongNgayCong.DataSource = null;
                        if (dr2.HasRows)
                        {
                            dt = new DataTable();
                            dt.Load(dr2);
                            DTGVTongNgayCong.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Không có dữ liệu cho Mã hoặc Tên đã chọn trong tổng ngày công.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }
        private void bt_timKiemTheoNgay_Click_1(object sender, EventArgs e)
        {
            kiemtra();
        }

        private void bt_timKiemTheoTen_Click(object sender, EventArgs e)
        {
            timKiemTheoTen();
        }
        private void quayLạiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChamCong.ListChamCong lstcc = new ChamCong.ListChamCong();
            lstcc.ShowDialog();

        }
        private void SaveHolidaysToDatabase()
        {

            connect.Open();
            try
            {
                foreach (var holiday in danhSachNgayLe)
                {
                    // Kiểm tra sự tồn tại của ngày lễ trong cơ sở dữ liệu
                    string checkSql = @"SELECT COUNT(*) FROM NgayLe 
                                    WHERE NGAY = @Ngay AND THANG = @Thang AND NAM =@Nam";

                    using (SqlCommand checkCmd = new SqlCommand(checkSql, connect))
                    {
                        checkCmd.Parameters.AddWithValue("@Ngay", holiday.Ngay);
                        checkCmd.Parameters.AddWithValue("@Thang", holiday.Thang);
                        checkCmd.Parameters.AddWithValue("@Nam", holiday.Nam);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count == 0) // Nếu chưa tồn tại
                        {
                            // Nếu chưa tồn tại, thêm ngày lễ mới
                            string insertSql = @"INSERT INTO NgayLe (NGAY, THANG, NAM , TENLE) 
                                             VALUES (@Ngay, @Thang,@Nam, @TenLe)";
                            using (SqlCommand insertCmd = new SqlCommand(insertSql, connect))
                            {
                                insertCmd.Parameters.AddWithValue("@Ngay", holiday.Ngay);
                                insertCmd.Parameters.AddWithValue("@Thang", holiday.Thang);
                                insertCmd.Parameters.AddWithValue("@Nam", holiday.Nam);
                                insertCmd.Parameters.AddWithValue("@TenLe", holiday.TenLe);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }
        private void load_datagridviewTongNgayCong()
        {
            connect.Open();
            DateTime time = DateTime.Now;
            int thang = time.Month;

            try
            {
                string sql = @" SELECT DISTINCT BC.MANV AS N'Mã Nhân Viên', BC.TONGNGAYCONG AS N'Tổng Ngày Công' 
                            FROM BANGCONG BC 
                           WHERE BC.THANG=@THANG
                            ";
                using (cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@THANG", thang);
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    DTGVTongNgayCong.DataSource = dt;
                }
                connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }
        private void load_datagridview(string maNhanVien, int thang)
        {
            connect.Open();
            try
            {
                dataGridView1.DataSource = null;
                string sql = @"SELECT BC.MANV, NV.HoTen, BC.THU, BC.NGAY, BC.THANG, BC.NAM, BC.TRANGTHAI,BC.MOTA 
                               FROM BANGCONG BC 
                               JOIN NHANVIEN NV ON NV.MANV = BC.MANV
                               WHERE BC.MANV=@maNhanVien AND BC.THANG =@THANG
                               ORDER BY BC.NGAY ASC, BC.THANG ASC, BC.NAM ASC";
                using (cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@maNhanVien", maNhanVien);
                    cmd.Parameters.AddWithValue("@THANG", thang);
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        private void BangCong_Load(object sender, EventArgs e)
        {
            load_datagridviewTongNgayCong();
            checkBox_ngay.Checked = true;
            checkBox_thang.Checked = true;
            checkBox_nam.Checked = true;
        }

        private void kiemtra()
        {
            string sql, sql2;
            DateTime selectedDate = DTP.Value;
            int ngay = selectedDate.Day;
            int thang = selectedDate.Month;
            int nam = selectedDate.Year;

            if (checkBox_ngay.Checked && checkBox_thang.Checked && checkBox_nam.Checked)
            {
                sql = @"SELECT DISTINCT BC.MANV, NV.HoTen, BC.THU, BC.NGAY, BC.THANG, BC.NAM, BC.TRANGTHAI, BC.MOTA 
                FROM BANGCONG BC
                JOIN NHANVIEN NV ON NV.MANV = BC.MANV
                WHERE BC.NGAY = @NGAY AND BC.THANG = @THANG AND BC.NAM = @NAM
                ORDER BY BC.MANV ASC";

                sql2 = @"SELECT DISTINCT BC.MANV AS N'Mã Nhân Viên', BC.TONGNGAYCONG AS N'Tổng Ngày Công' 
                 FROM BANGCONG BC 
                 JOIN NHANVIEN NV ON NV.MANV = BC.MANV
                 WHERE BC.NGAY = @NGAY AND BC.THANG = @THANG AND BC.NAM = @NAM
                ORDER BY BC.MANV ASC";

                Load_ngayThangNam(sql, sql2, ngay, thang, nam);
            }
            else if (checkBox_thang.Checked && checkBox_nam.Checked)
            {
                sql = @"SELECT DISTINCT BC.MANV, NV.HoTen, BC.THU, BC.NGAY, BC.THANG, BC.NAM, BC.TRANGTHAI, BC.MOTA 
                FROM BANGCONG BC
                JOIN NHANVIEN NV ON NV.MANV = BC.MANV
                WHERE BC.THANG = @THANG AND BC.NAM = @NAM
                ORDER BY BC.MANV, BC.NGAY ASC";

                sql2 = @"SELECT DISTINCT BC.MANV AS N'Mã Nhân Viên', BC.TONGNGAYCONG AS N'Tổng Ngày Công' 
                 FROM BANGCONG BC 
                 JOIN NHANVIEN NV ON NV.MANV = BC.MANV
                 WHERE BC.THANG = @THANG AND BC.NAM = @NAM
                 ORDER BY BC.MANV ASC";

                Load_ngayThangNam(sql, sql2, null, thang, nam);
            }
            else if (checkBox_nam.Checked)
            {
                sql = @"SELECT DISTINCT BC.MANV, NV.HoTen, BC.THU, BC.NGAY, BC.THANG, BC.NAM, BC.TRANGTHAI, BC.MOTA 
                FROM BANGCONG BC
                JOIN NHANVIEN NV ON NV.MANV = BC.MANV
                WHERE BC.NAM = @NAM
                ORDER BY BC.MANV ASC";

                sql2 = @"SELECT DISTINCT BC.MANV AS N'Mã Nhân Viên', BC.TONGNGAYCONG AS N'Tổng Ngày Công' 
                 FROM BANGCONG BC 
                 JOIN NHANVIEN NV ON NV.MANV = BC.MANV
                 WHERE BC.NAM = @NAM
                ORDER BY BC.MANV ASC";

                Load_ngayThangNam(sql, sql2, null, null, nam);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một điều kiện tìm kiếm.");
            }
        }

        private void Load_ngayThangNam(string sql, string sql2, int? ngay, int? thang, int nam)
        {
            connect.Open();
            dataGridView1.DataSource = null;
            DTGVTongNgayCong.DataSource = null;

            // Truy vấn đầu tiên cho dataGridView1
            using (cmd = new SqlCommand(sql, connect))
            {
                try
                {
                    if (ngay.HasValue)
                        cmd.Parameters.AddWithValue("@NGAY", ngay.Value);
                    if (thang.HasValue)
                        cmd.Parameters.AddWithValue("@THANG", thang.Value);

                    cmd.Parameters.AddWithValue("@NAM", nam);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dt = new DataTable();
                            dt.Load(dr);
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

            // Truy vấn thứ hai cho DTGVTongNgayCong
            using (cmd = new SqlCommand(sql2, connect))
            {
                try
                {
                    if (ngay.HasValue)
                        cmd.Parameters.AddWithValue("@NGAY", ngay.Value);
                    if (thang.HasValue)
                        cmd.Parameters.AddWithValue("@THANG", thang.Value);

                    cmd.Parameters.AddWithValue("@NAM", nam);
                    using (SqlDataReader dr2 = cmd.ExecuteReader())
                    {
                        if (dr2.HasRows)
                        {
                            dt = new DataTable();
                            dt.Load(dr2);
                            DTGVTongNgayCong.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Không có dữ liệu cho tổng ngày công.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    connect.Close();
                }
            }
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            connect.Open();
            connect.Close();
        }
        private void DTGVTongNgayCong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DateTime time = DateTime.Now;
            int thang = time.Month;
            // Kiểm tra nếu click vào cột "Mã Nhân Viên"
            if (e.ColumnIndex == 0 && e.RowIndex >= 0) // Cột mã nhân viên
            {
                string maNhanVien = DTGVTongNgayCong.Rows[e.RowIndex].Cells[0].Value.ToString();
                
                // Gọi phương thức để tải dữ liệu chấm công cho nhân viên này
                load_datagridview(maNhanVien, thang);
            }
        }

        private void bt_xuatExcel_Click(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            int thang = time.Month;

            // Truy vấn SQL cho dataGridView1
            string sql1 = @"SELECT BC.MANV, NV.HoTen, BC.THU, BC.NGAY, BC.THANG, BC.NAM, BC.TRANGTHAI, BC.MOTA 
                    FROM BANGCONG BC 
                    JOIN NHANVIEN NV ON NV.MANV = BC.MANV
                    WHERE BC.THANG = @thang
                    ORDER BY BC.NGAY ASC, BC.THANG ASC, BC.NAM ASC";

            // Truy vấn SQL cho DTGVTongNgayCong
            string sql2 = @"SELECT BC.MANV AS N'Mã Nhân Viên', BC.TONGNGAYCONG AS N'Tổng Ngày Công' 
                    FROM BANGCONG BC 
                    JOIN NHANVIEN NV ON NV.MANV = BC.MANV
                    WHERE BC.THANG = @thang";

            // Tạo đối tượng DataTables để lưu trữ dữ liệu từ database
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = new DataTable();

            // Kết nối đến database và lấy dữ liệu cho dataGridView1
            connect.Open();
            using (SqlCommand command = new SqlCommand(sql1, connect))
            {
                command.Parameters.AddWithValue("@thang", thang);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dataTable1);  // Đổ dữ liệu vào DataTable
            }

            // Kết nối đến database và lấy dữ liệu cho DTGVTongNgayCong
            using (SqlCommand command = new SqlCommand(sql2, connect))
            {
                command.Parameters.AddWithValue("@thang", thang);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dataTable2);  // Đổ dữ liệu vào DataTable
            }
            connect.Close(); // Đảm bảo đóng kết nối

            // Kiểm tra xem DataTable có dữ liệu không
            if (dataTable1.Rows.Count == 0 && dataTable2.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu nào được tìm thấy.");
                return;
            }

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);// lấy đường dẫn đến desktop
            string filePath = System.IO.Path.Combine(desktopPath, "DanhSachBangCong.xlsx");//lấy đường dẫn file
            bool fileExists = System.IO.File.Exists(filePath);//kiểm tra đường dẫn có hay chưa 

            // Tạo Workbook và Worksheet

            // Tạo Workbook và Worksheet
            using (var workbook = fileExists ? new XLWorkbook(filePath) : new XLWorkbook())
            {
                // Xuất dữ liệu từ dataTable1 (dataGridView1)
                var worksheet1 = workbook.Worksheets.Add("BangCong");
                worksheet1.Cell(1, 1).InsertTable(dataTable1);

                // Xuất dữ liệu từ dataTable2 (DTGVTongNgayCong)
                var worksheet2 = workbook.Worksheets.Add("TongNgayCong");
                worksheet2.Cell(1, 1).InsertTable(dataTable2);

                // Lưu file Excel
                workbook.SaveAs(filePath); // Thay thế đường dẫn phù hợp
            }

            MessageBox.Show("Xuất File Excel Thành Công ");
        }
    }
}
