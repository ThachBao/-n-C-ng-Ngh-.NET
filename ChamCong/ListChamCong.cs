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
using ClosedXML.Excel;
using System.Data;
namespace HMresourcemanagementsystem.ChamCong
{
    public partial class ListChamCong : Form
    {
        connection kn = new connection();
        SqlConnection connection;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        public ListChamCong()
        {
            InitializeComponent();
            connection = kn.con;
        }
        void load_dataGridView()
        {
            dataGridView1.DataSource = null;
            try
            {
                string sql = @"SELECT CC.ID_CHAMCONG, CC.MANV AS N'Mã Nhân Viên', NV.HOTEN N'Tên Nhân Viên',CC.NGAYVAO AS N'Ngày Hiện Tại', CC.TRANGTHAIVAO AS N'CHECK IN VÀO', CC.TRANGTHAIRA AS N'CHECK IN RA'
                       FROM CHAMCONG CC
                       JOIN NHANVIEN NV ON NV.MANV = CC.MANV
                        WHERE CAST(CC.NGAYVAO AS DATE) = CAST(GETDATE() AS DATE)
                        ";

                connection.Open();
                cmd = new SqlCommand(sql, connection);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                connection.Close();

                // Đảm bảo cột ID_CHAMCONG không hiển thị nếu không cần
                dataGridView1.Columns["ID_CHAMCONG"].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ListChamCong_Load(object sender, EventArgs e)
        {
            load_dataGridView();
            dataGridView1.ReadOnly = false;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.CellClick += dataGridView1_CellClick;
        }


        private void bt_xuatFileExcel_Click(object sender, EventArgs e)
        {
            // Truy vấn SQL
            string sql = @"SELECT CC.MANV AS N'Mã Nhân Viên',NV.HOTEN N'Tên Nhân Viên',CC.NGAYVAO AS N'Ngày Hiện Tại', CC.TRANGTHAIVAO AS N'CHECK IN VÀO',CC.TRANGTHAIRA AS N'CHECK IN RA'
                               FROM CHAMCONG CC
                               JOIN  NHANVIEN NV ON NV.MANV = CC.MANV
                                ";

            // Tạo đối tượng DataTable để lưu trữ dữ liệu từ database
            DataTable dataTable = new DataTable();

            // Kết nối đến database và lấy dữ liệu
            connection.Open();
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);  // Đổ dữ liệu vào DataTable
            }


            // Tạo Workbook và Worksheet
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sheet1");

                // Đổ dữ liệu từ DataTable vào Worksheet
                worksheet.Cell(1, 1).InsertTable(dataTable);

                // Lưu file Excel
                workbook.SaveAs("~\\Desktop\\DanhSachChamCong.xlsx");
            }

            MessageBox.Show("Xuất File Excel Thành Công ");

        }


        private void ListChamCong_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            DialogResult re = MessageBox.Show("Bạn Có Chắc Chắn Muốn Thóat ", "Thông Báo", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question);
            if (re == DialogResult.No)
            {
                e.Cancel = true;

            }
        }

        private void bảngCôngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChamCong.BangCong bc = new ChamCong.BangCong();

            bc.ShowDialog();


        }






        void UpdateDatabase(string maNV, bool trangThaiVao, bool trangThaiRa, DateTime thoiGianVao)
        {
            string sql = @"UPDATE CHAMCONG
                   SET TRANGTHAIVAO = @TRANGTHAIVAO, TRANGTHAIRA = @TRANGTHAIRA
                   WHERE MANV = @MANV AND NGAYVAO = @THOIGIANVAO";

            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@TRANGTHAIVAO", !trangThaiVao);
                cmd.Parameters.AddWithValue("@TRANGTHAIRA", !trangThaiRa);
                cmd.Parameters.AddWithValue("@MANV", maNV);
                cmd.Parameters.AddWithValue("@THOIGIANVAO", thoiGianVao);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Đảm bảo không click vào tiêu đề hoặc ô trống
            if (e.RowIndex >= 0 && (e.ColumnIndex == dataGridView1.Columns["CHECK IN VÀO"].Index || e.ColumnIndex == dataGridView1.Columns["CHECK IN RA"].Index))
            {
                try
                {
                    // Lấy MANV và THOIGIANVAO từ dòng hiện tại
                    string maNV = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Mã Nhân Viên"].Value);
                    DateTime thoiGianVao = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["Ngày Hiện Tại"].Value);

                    // Kiểm tra trạng thái
                    bool trangThaiVao = dataGridView1.Rows[e.RowIndex].Cells["CHECK IN VÀO"].Value != DBNull.Value
                                        ? Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells["CHECK IN VÀO"].Value)
                                        : false;

                    bool trangThaiRa = dataGridView1.Rows[e.RowIndex].Cells["CHECK IN RA"].Value != DBNull.Value
                                       ? Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells["CHECK IN RA"].Value)
                                       : false;

                    // Đảo trạng thái và cập nhật lại cột
                    if (e.ColumnIndex == dataGridView1.Columns["CHECK IN VÀO"].Index)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells["CHECK IN VÀO"].Value = !trangThaiVao;
                    }
                    else if (e.ColumnIndex == dataGridView1.Columns["CHECK IN RA"].Index)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells["CHECK IN RA"].Value = !trangThaiRa;
                    }
                    // Gọi hàm để cập nhật cơ sở dữ liệu
                    UpdateDatabase(maNV, !trangThaiVao, !trangThaiRa, thoiGianVao);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }


            }
        }

        private void Rb_vao_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb_vao.Checked == true)
            {
                Rb_ra.Checked = false;
            }

        }

        private void Rb_ra_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb_ra.Checked == true)
            {
                Rb_vao.Checked = false;
            }
        }
        private bool kiemTraTrangThaiRa(string manv)
        {
            bool hasCheckedOut = false; // Biến để kiểm tra trạng thái CHECK OUT
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Kiểm tra mã nhân viên
                if (row.Cells["Mã Nhân Viên"].Value != null &&
                    row.Cells["Mã Nhân Viên"].Value.ToString().ToUpper() == manv.ToUpper()) // So sánh không phân biệt chữ hoa
                {

                    hasCheckedOut = row.Cells["CHECK IN RA"].Value != DBNull.Value &&
                                    Convert.ToBoolean(row.Cells["CHECK IN RA"].Value);

                    // Thoát vòng lặp sau khi tìm thấy nhân viên
                    break;
                }
            }
            return hasCheckedOut;
        }
        private bool kiemTraTrangThaiVao(string manv)
        {
            
                bool hasCheckedIn = false; // Biến để kiểm tra trạng thái CHECK IN
               

                // Lặp qua tất cả các dòng trong DataGridView
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Kiểm tra mã nhân viên
                    if (row.Cells["Mã Nhân Viên"].Value != null &&
                        row.Cells["Mã Nhân Viên"].Value.ToString().ToUpper() == manv.ToUpper()) // So sánh không phân biệt chữ hoa
                    {
                        hasCheckedIn = row.Cells["CHECK IN VÀO"].Value != DBNull.Value &&
                                       Convert.ToBoolean(row.Cells["CHECK IN VÀO"].Value);

                        
                        // Thoát vòng lặp sau khi tìm thấy nhân viên
                        break;
                    }
                }
            return hasCheckedIn;
            
        }
        private void bt_xacNhan_Click(object sender, EventArgs e)
        {
            //false là tích true là không tích 
            // Lấy mã nhân viên từ TextBox
            string maNV = txt_maNhanVien.Text.ToUpper();

            // Kiểm tra trạng thái của RadioButton
            if (Rb_vao.Checked)
            {
                if (kiemTraTrangThaiVao(maNV) != true)
                {
                    // Nếu Rb_vao được chọn, tìm dòng có mã nhân viên khớp
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["Mã Nhân Viên"].Value != null &&
                            row.Cells["Mã Nhân Viên"].Value.ToString() == maNV)
                        {
                            bool trangThaiVao = row.Cells["CHECK IN VÀO"].Value != DBNull.Value
                                                ? Convert.ToBoolean(row.Cells["CHECK IN VÀO"].Value)
                                                : false;//FALSE

                            // Cập nhật cơ sở dữ liệu
                            DateTime thoiGianVao = Convert.ToDateTime(row.Cells["Ngày Hiện Tại"].Value);
                            UpdateDatabase(maNV, trangThaiVao, !trangThaiVao, thoiGianVao);//mình đẻ phủ địch trong hàm update này nên buộc bool là phải false
                            break; // Thoát khỏi vòng lặp sau khi đã cập nhật
                        }
                    }
                }
                else
                    MessageBox.Show("Nhân Viên Đã Chấm Công Vào Rồi", "Thông Báo", MessageBoxButtons.OK);
            }
            else if (Rb_ra.Checked)
            {
                if (kiemTraTrangThaiRa(maNV) != true)
                {
                    // Nếu Rb_ra được chọn, tìm dòng có mã nhân viên khớp
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["Mã Nhân Viên"].Value != null &&
                            row.Cells["Mã Nhân Viên"].Value.ToString() == maNV)
                        {
                            bool trangThaiVao = row.Cells["CHECK IN VÀO"].Value != DBNull.Value
                                                ? !Convert.ToBoolean(row.Cells["CHECK IN VÀO"].Value)
                                                : true;

                            // Chỉ cho phép CHECK IN RA nếu đã CHECK IN VÀO
                            if (trangThaiVao != true)
                            {
                                bool trangThaiRa = row.Cells["CHECK IN RA"].Value != DBNull.Value
                                                   ? Convert.ToBoolean(row.Cells["CHECK IN RA"].Value)
                                                   : false;



                                // Cập nhật cơ sở dữ liệu
                                DateTime thoiGianVao = Convert.ToDateTime(row.Cells["Ngày Hiện Tại"].Value);
                                UpdateDatabase(maNV, trangThaiVao, trangThaiRa, thoiGianVao);
                            }
                            else
                            {
                                MessageBox.Show("Không thể CHECK IN RA vì nhân viên chưa CHECK IN VÀO.");
                            }
                            break; // Thoát khỏi vòng lặp sau khi đã cập nhật
                        }
                    }
                }
                else
                    MessageBox.Show("Nhân Viên Đã Chấm Công Ra Rồi", "Thông Báo", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một trạng thái vào hoặc ra.");
            }

            // Xóa mã nhân viên và tải lại dữ liệu
            txt_maNhanVien.Clear();
            load_dataGridView();
        }

        private void bt_lamMoi_Click(object sender, EventArgs e)
        {
            load_dataGridView();
        }
    }
}
