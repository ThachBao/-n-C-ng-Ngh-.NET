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
            try
            {
                string sql = @"SELECT CC.ID_CHAMCONG, CC.MANV AS N'Mã Nhân Viên', NV.HOTEN N'Tên Nhân Viên',CC.NGAYVAO AS N'Ngày Hiện Tại', CC.TRANGTHAIVAO AS N'CHECK IN VÀO', CC.TRANGTHAIRA AS N'CHECK IN RA'
                       FROM CHAMCONG CC
                       JOIN HOSONHANVIEN NV ON NV.MANV = CC.MANV
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
                               JOIN  HOSONHANVIEN NV ON NV.MANV = CC.MANV
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
    }
}
