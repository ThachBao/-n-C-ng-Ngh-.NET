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
using static HMresourcemanagementsystem.BoPhan.QuanLyBoPhan;

namespace HMresourcemanagementsystem.BoPhan
{
    public partial class QuanLyBoPhan : Form
    {
        connection kn = new connection();
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;
        SqlConnection connect;
        public class PhongBan
        {
            public string MaPhong { get; set; }
            public string TenPhong { get; set; }

            public override string ToString()
            {
                return TenPhong; // Hiển thị tên phòng ban trong ComboBox
            }
        }
        public void ReloadDataGridView()
        {
            load_dataGridView();
        }
        public QuanLyBoPhan()
        {
            InitializeComponent();
            connect = kn.con;
            DGV_BoPhan.CellClick += DGV_BoPhan_CellClick;
        }
        private void load_comBoBox()
        {


            try
            {
                connect.Open();
                string sql = @"SELECT MaPhong, TenPhong FROM PhongBan";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dr); // Tải dữ liệu vào DataTable
                        cb_phongBan.DataSource = dt; // Thiết lập DataSource cho ComboBox
                        cb_phongBan.DisplayMember = "TenPhong"; // Hiển thị tên phòng
                        cb_phongBan.ValueMember = "MaPhong"; // Giá trị mã phòng
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }

        }
        private void load_dataGridView()
        {
            try
            {
                connect.Open();
                string sql = @" SELECT BP.MaBoPhan,PB.MaPhong,BP.MoTa,BP.TenBoPhan AS N'Tên Bộ Phận' , PB.TenPhong AS N'Tên Phòng'
                                FROM BoPhan_PhongBan BP_PB
                                JOIN PhongBan PB ON PB.MaPhong=BP_PB.MaPhong
                                JOIN BoPhan BP ON BP.MaBoPhan=BP_PB.MaBoPhan
                                ";
                cmd = new SqlCommand(sql, connect);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                DGV_BoPhan.DataSource = dt;
                DGV_BoPhan.Columns["MaBoPhan"].Visible = false;
                DGV_BoPhan.Columns["MaPhong"].Visible = false;
                DGV_BoPhan.Columns["MoTa"].Visible = false;
                connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex);

            }
            finally
            {
                connect.Close();
            }
        }

        private void QuanLyBoPhan_Load(object sender, EventArgs e)
        {
            txt_maBoPhan.Enabled = false;
            txt_tenBoPhan.Enabled = false;
            txt_moTa.Enabled = false;
            cb_phongBan.Enabled = false;
            bt_themPhongBan.Enabled = false;
            load_dataGridView();
            load_comBoBox();
        }

        private void DGV_BoPhan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng đã nhấp vào hàng hợp lệ
            if (e.RowIndex >= 0) // Hàng đầu tiên có chỉ số là 0
            {
                // Lấy hàng đã được chọn
                DataGridViewRow row = DGV_BoPhan.Rows[e.RowIndex];

                // Gán giá trị vào các TextBox
                txt_maBoPhan.Text = row.Cells["MaBoPhan"].Value.ToString();
                txt_tenBoPhan.Text = row.Cells["Tên Bộ Phận"].Value.ToString();
                txt_moTa.Text = row.Cells["MoTa"].Value.ToString();

                // Gọi phương thức để nạp dữ liệu phòng ban vào ComboBox
                load_comBoBox(); // Nạp dữ liệu phòng ban mỗi khi chọn một bộ phận

                // Lấy mã phòng ban từ hàng đã chọn
                string maPhong = row.Cells["MaPhong"].Value?.ToString(); // Sử dụng ? để tránh NullReferenceException

                // Chọn mã phòng ban tương ứng trong ComboBox
                if (!string.IsNullOrEmpty(maPhong))
                {
                    cb_phongBan.SelectedValue = maPhong; // Sử dụng SelectedValue để thiết lập chọn
                }

                // Vô hiệu hóa TextBox và ComboBox
                txt_maBoPhan.Enabled = false;
                txt_tenBoPhan.Enabled = false;
                txt_moTa.Enabled = false;
                cb_phongBan.Enabled = false;
            }
        }

        private void bt_them_Click(object sender, EventArgs e)
        {
            // Bật khả năng chỉnh sửa cho các TextBox và ComboBox
            txt_maBoPhan.Clear();
            txt_tenBoPhan.Clear();
            txt_moTa.Clear();
            DGV_BoPhan.CellClick -= DGV_BoPhan_CellClick;
            txt_maBoPhan.Enabled = true;
            txt_tenBoPhan.Enabled = true;
            txt_moTa.Enabled = true;
            cb_phongBan.Enabled = true;
            bt_themPhongBan.Enabled = true;
        }

        private void bt_luu_Click(object sender, EventArgs e)
        {

            try
            {
                // Lấy thông tin từ các điều khiển
                string maBoPhan = txt_maBoPhan.Text.Trim();
                string tenBoPhan = txt_tenBoPhan.Text.Trim();
                string moTa = txt_moTa.Text.Trim();
                string maPhong = cb_phongBan.SelectedValue?.ToString(); // Lấy mã phòng nếu có

                // Kiểm tra thông tin đầu vào
                if (string.IsNullOrEmpty(maBoPhan) || string.IsNullOrEmpty(tenBoPhan) || string.IsNullOrEmpty(moTa) || string.IsNullOrEmpty(maPhong))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                connect.Open();

                // Thêm bộ phận
                 if (InsertBoPhan(connect, maBoPhan, tenBoPhan, moTa))
                {
                    
                    
                    // Thêm liên kết giữa Bộ phận và Phòng ban
                    if (InsertBoPhanPhongBan(connect, maBoPhan, maPhong))
                    {
                        MessageBox.Show("Thêm bộ phận và liên kết thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    load_dataGridView();
                }

            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi SQL: " + sqlEx.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private bool InsertBoPhan(SqlConnection connect, string maBoPhan, string tenBoPhan, string moTa)
        {
            string sqlInsertBoPhan = @"INSERT INTO BoPhan (MaBoPhan, TenBoPhan, MoTa) VALUES (@maBoPhan, @tenBoPhan, @moTa)";
            using (SqlCommand cmd = new SqlCommand(sqlInsertBoPhan, connect))
            {
                cmd.Parameters.AddWithValue("@maBoPhan", maBoPhan);
                cmd.Parameters.AddWithValue("@tenBoPhan", tenBoPhan);
                cmd.Parameters.AddWithValue("@moTa", moTa);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0; // Trả về true nếu thêm thành công
            }
        }

        private bool InsertBoPhanPhongBan(SqlConnection connect, string maBoPhan, string maPhong)
        {
            // Kiểm tra sự tồn tại của maBoPhan
            if (!CheckMaBoPhanExists(connect, maBoPhan))
            {
                MessageBox.Show("Mã bộ phận không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra sự tồn tại của maPhong
            if (!CheckMaPhongExists(connect, maPhong))
            {
                MessageBox.Show("Mã phòng ban không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string sqlInsertBoPhanPhongBan = @"INSERT INTO BoPhan_PhongBan (MaBoPhan, MaPhong) VALUES (@maBoPhan, @maPhong)";
            using (SqlCommand cmd = new SqlCommand(sqlInsertBoPhanPhongBan, connect))
            {
                cmd.Parameters.AddWithValue("@maBoPhan", maBoPhan);
                cmd.Parameters.AddWithValue("@maPhong", maPhong);
                cmd.ExecuteNonQuery(); // Thực hiện thêm liên kết
            }

            return true; // Trả về true nếu thêm thành công
        }

        private bool CheckMaBoPhanExists(SqlConnection connect, string maBoPhan)
        {
            string sqlCheck = "SELECT COUNT(*) FROM BoPhan WHERE MaBoPhan = @maBoPhan";
            using (SqlCommand cmd = new SqlCommand(sqlCheck, connect))
            {
                cmd.Parameters.AddWithValue("@maBoPhan", maBoPhan);
                return (int)cmd.ExecuteScalar() > 0; // Trả về true nếu tồn tại
            }
        }

        private bool CheckMaPhongExists(SqlConnection connect, string maPhong)
        {
            string sqlCheck = "SELECT COUNT(*) FROM PhongBan WHERE MaPhong = @maPhong";
            using (SqlCommand cmd = new SqlCommand(sqlCheck, connect))
            {
                cmd.Parameters.AddWithValue("@maPhong", maPhong);
                return (int)cmd.ExecuteScalar() > 0; // Trả về true nếu tồn tại
            }
        }
        private void bt_themPhongBan_Click(object sender, EventArgs e)
        {
            // Tạo một đối tượng Quản Lý Phòng Ban
            QuanLyPhongBan qlPhongBan = new QuanLyPhongBan
            {
                Owner = this // Thiết lập Owner cho form Quản Lý Phòng Ban
            };

            // Đăng ký sự kiện FormClosed
            qlPhongBan.FormClosed += (s, args) =>
            {
                // Tải lại dữ liệu cho form Quản Lý Bộ Phận
                ReloadDataGridView(); // Gọi phương thức này để cập nhật dữ liệu
                this.Show(); // Hiển thị lại form Quản Lý Bộ Phận
            };

            qlPhongBan.Show(); // Hiển thị form Quản Lý Phòng Ban
            this.Hide(); // Ẩn form Quản Lý Bộ Phận
        }
    

        private void DeleteBoPhan(string maBoPhan)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa bộ phận này và tất cả các phòng ban liên kết không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                XoaBoPhanVaPhongBan(maBoPhan);
            }
        }
        private void XoaBoPhanVaPhongBan(string maBoPhan)
        {
            try
            {
                connect.Open();

                // Xóa tất cả phòng ban liên kết trước
                string sqlDeleteLinks = @"DELETE FROM BoPhan_PhongBan WHERE MaBoPhan = @maBoPhan";
                using (SqlCommand cmdLink = new SqlCommand(sqlDeleteLinks, connect))
                {
                    cmdLink.Parameters.AddWithValue("@maBoPhan", maBoPhan);
                    cmdLink.ExecuteNonQuery(); // Thực hiện xóa liên kết
                }

                // Xóa bộ phận
                string sqlDelete = @"DELETE FROM BoPhan WHERE MaBoPhan = @maBoPhan";
                using (SqlCommand cmd = new SqlCommand(sqlDelete, connect))
                {
                    cmd.Parameters.AddWithValue("@maBoPhan", maBoPhan);
                    int rowsAffected = cmd.ExecuteNonQuery(); // Thực hiện xóa bộ phận

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xóa bộ phận và các phòng ban liên kết thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không có bản ghi nào được xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                connect.Close();
            }

            // Cập nhật lại DataGridView nếu cần
            load_dataGridView(); // Gọi phương thức để nạp lại dữ liệu vào DataGridView
        }
        private void DeletePhongBan(string maPhong)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng ban này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                XoaPhongBan(maPhong);
            }
        }
        private void XoaPhongBan(string maPhong)
        {
            try
            {
                connect.Open();

                // Xóa phòng ban trong bảng BoPhan_PhongBan
                string sqlDeleteLink = @"DELETE FROM BoPhan_PhongBan WHERE MaPhong = @maPhong";
                using (SqlCommand cmdLink = new SqlCommand(sqlDeleteLink, connect))
                {
                    cmdLink.Parameters.AddWithValue("@maPhong", maPhong);
                    int rowsAffected = cmdLink.ExecuteNonQuery(); // Thực hiện xóa phòng ban

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xóa phòng ban thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không có bản ghi nào được xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                connect.Close();
            }

            // Cập nhật lại DataGridView nếu cần
            load_dataGridView(); // Gọi phương thức để nạp lại dữ liệu vào DataGridView
        }
        private void bt_xoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn hàng nào trong DataGridView chưa
            if (DGV_BoPhan.SelectedCells.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một bộ phận hoặc phòng ban để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy hàng đã chọn từ ô được chọn
            DataGridViewCell selectedCell = DGV_BoPhan.SelectedCells[0];
            DataGridViewRow selectedRow = DGV_BoPhan.Rows[selectedCell.RowIndex];

            // Kiểm tra nếu hàng được chọn là bộ phận
            if (selectedRow.Cells["Tên Bộ Phận"].Value != null)
            {
                DeleteBoPhan(selectedRow.Cells["MaBoPhan"].Value.ToString());
            }
            // Kiểm tra nếu hàng được chọn là phòng ban
            else if (selectedRow.Cells["Tên Phòng"].Value != null)
            {
                DeletePhongBan(selectedRow.Cells["Tên Phòng"].Value.ToString());
            }
        }
    }
}
