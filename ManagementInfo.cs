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
using System.Xml.Linq;

namespace HMresourcemanagementsystem
{
    public partial class Quanly : Form
    {
        connection kn = new connection();
        SqlConnection connect;
        SqlCommand cmd;
        SqlDataAdapter adap;
        DataTable dt;
        SqlDataAdapter adap1;
        DataTable dt1;
        SqlCommandBuilder cmbd;
        BindingSource bindingSource = new BindingSource();

        public Quanly()
        {
            InitializeComponent();
            connect = kn.con;
        }

        void load_Quyen()
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }

                string sql = "SELECT PHANQUYEN FROM ACCOUNTS";
                adap = new SqlDataAdapter(sql, connect);
                dt = new DataTable();
                adap.Fill(dt);
                cbo_quyen.DataSource = dt;
                cbo_quyen.DisplayMember = "PHANQUYEN";
                cbo_quyen.ValueMember = "PHANQUYEN";
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi SQL: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khác: " + ex.Message);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }

        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                // Kiểm tra nếu email có chứa "@gmail.com"
                return addr.Address == email && email.EndsWith("@gmail.com");
            }
            catch
            {
                return false;
            }
        }

        // Method to validate password strength
        private bool IsValidPassword(string password)
        {
            return password.Length >= 6 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit);
        }

        void load_form()
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }

                string sql1 = "SELECT * FROM ACCOUNTS";
                adap1 = new SqlDataAdapter(sql1, connect);
                dt1 = new DataTable();
                adap1.Fill(dt1);
                bindingSource.DataSource = dt1;
                dgv_show.DataSource = bindingSource;

                if (dt1.Rows.Count > 0)
                {
                    txt_ten.Text = dgv_show.Rows[0].Cells["TENDANGNHAP"].Value.ToString();
                    txt_matkhau.Text = dgv_show.Rows[0].Cells["MATKHAU"].Value.ToString();
                    cbo_quyen.SelectedValue = dgv_show.Rows[0].Cells["PHANQUYEN"].Value.ToString();
                    txt_email.Text = dgv_show.Rows[0].Cells["EMAIL"].Value.ToString();
                }
                else
                {
                    txt_ten.Clear();
                    txt_matkhau.Clear();
                    txt_email.Clear();
                    cbo_quyen.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            DialogResult dia;
            dia = MessageBox.Show("Bạn Có Muốn Thoát?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dia == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void ManagementInfo_Load(object sender, EventArgs e)
        {
            btn_sua.Enabled = btn_xoa.Enabled = btn_luu.Enabled = btn_them.Enabled = false;
            txt_ten.Enabled = false;
            txt_matkhau.Enabled = false;
            txt_email.Enabled = false;
            cbo_quyen.Enabled = false;
            dgv_show.ReadOnly = true;
            dgv_show.AllowUserToAddRows = false;
            load_Quyen();
            load_form();
        }

        private void dgv_show_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex <= dgv_show.Rows.Count - 1)
            {
                int index = e.RowIndex;
                txt_ten.Text = dgv_show.Rows[index].Cells["TENDANGNHAP"].Value.ToString();
                txt_matkhau.Text = dgv_show.Rows[index].Cells["MATKHAU"].Value.ToString();
                cbo_quyen.SelectedValue = dgv_show.Rows[index].Cells["PHANQUYEN"].Value.ToString();
                txt_email.Text = dgv_show.Rows[index].Cells["EMAIL"].Value.ToString();
            }
            btn_sua.Enabled = btn_xoa.Enabled = true;
            btn_them.Enabled = false;
        }




        private void btn_them_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_ten.Text) || string.IsNullOrEmpty(txt_matkhau.Text) || cbo_quyen.SelectedValue == null || string.IsNullOrEmpty(txt_email.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!IsValidEmail(txt_email.Text))
                {
                    MessageBox.Show("Email không hợp lệ! Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!IsValidPassword(txt_matkhau.Text))
                {
                    MessageBox.Show("Mật khẩu không đủ mạnh! Cần ít nhất 6 ký tự, bao gồm chữ hoa, chữ thường và số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra xem TENDANGNHAP đã tồn tại hay chưa
                string checkSql = "SELECT COUNT(*) FROM ACCOUNTS WHERE TENDANGNHAP = @tendangnhap";
                cmd = new SqlCommand(checkSql, connect);
                cmd.Parameters.AddWithValue("@tendangnhap", txt_ten.Text);

                connect.Open();
                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Nếu tên đăng nhập không tồn tại, tiến hành thêm mới
                string sqlInsert = "INSERT INTO ACCOUNTS (TENDANGNHAP, MATKHAU, PHANQUYEN, EMAIL) " +
                                   "VALUES (@tendangnhap, @matkhau, @phanquyen, @email)";
                cmd = new SqlCommand(sqlInsert, connect);
                cmd.Parameters.AddWithValue("@tendangnhap", txt_ten.Text);
                cmd.Parameters.AddWithValue("@matkhau", txt_matkhau.Text);
                cmd.Parameters.AddWithValue("@phanquyen", cbo_quyen.SelectedValue);
                cmd.Parameters.AddWithValue("@email", txt_email.Text);
                cmd.ExecuteNonQuery();

                load_form();
                bindingSource.ResetBindings(false);
                btn_sua.Enabled = btn_xoa.Enabled = false;
                txt_ten.Clear();
                txt_matkhau.Clear();
                txt_email.Clear();
                MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dữ liệu: " + ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }


        private void btn_moi_Click(object sender, EventArgs e)
        {
            txt_ten.Enabled = true;
            txt_matkhau.Enabled = true;
            txt_email.Enabled = true;
            cbo_quyen.Enabled = true;
            btn_luu.Enabled = true;
            btn_them.Enabled = true;
            btn_luu.Enabled = false;
            btn_xoa.Enabled = btn_sua.Enabled = false;
            txt_ten.Clear();
            txt_matkhau.Clear();
            txt_email.Clear();
            txt_ten.Focus();
            dgv_show.ReadOnly = false;
            for (int i = 0; i < dgv_show.Rows.Count - 1; i++)
            {
                dgv_show.Rows[i].ReadOnly = true;
            }
            dgv_show.FirstDisplayedScrollingRowIndex = dgv_show.Rows.Count - 1;
            dgv_show.AllowUserToAddRows = true;
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }
                if (!IsValidEmail(txt_email.Text))
                {
                    MessageBox.Show("Email không hợp lệ! Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (!IsValidPassword(txt_matkhau.Text))
                {
                    MessageBox.Show("Mật khẩu không đủ mạnh! Cần ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường và số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Lấy tên đăng nhập từ ô hiện tại trong DataGridView
                string tendn = txt_ten.Text;

                string sqlUpdate = "UPDATE ACCOUNTS SET MATKHAU=@matkhau, PHANQUYEN=@phanquyen, EMAIL=@email WHERE TENDANGNHAP=@tendn";
                cmd = new SqlCommand(sqlUpdate, connect);
                cmd.Parameters.AddWithValue("@tendn", tendn);
                cmd.Parameters.AddWithValue("@matkhau", txt_matkhau.Text);
                cmd.Parameters.AddWithValue("@phanquyen", cbo_quyen.SelectedValue);
                cmd.Parameters.AddWithValue("@email", txt_email.Text);
                cmd.ExecuteNonQuery();

                // Tải lại dữ liệu
                load_form();
                bindingSource.ResetBindings(false);
                btn_sua.Enabled = btn_xoa.Enabled = false;
                txt_ten.Clear();
                txt_matkhau.Clear();
                txt_email.Clear();
                txt_email.Enabled = true;
                txt_matkhau.Enabled = true;
                btn_luu.Enabled = false;
                txt_ten.Enabled = txt_email.Enabled = txt_matkhau.Enabled = cbo_quyen.Enabled = false;
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (dgv_show.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    connect.Open();
                    try
                    {
                        foreach (DataGridViewRow r in dgv_show.SelectedRows)
                        {
                            string tendn = r.Cells["TENDANGNHAP"].Value.ToString();
                            string sql = "DELETE FROM ACCOUNTS WHERE TENDANGNHAP=@tendn";
                            cmd = new SqlCommand(sql, connect);
                            cmd.Parameters.AddWithValue("@tendn", tendn);
                            cmd.ExecuteNonQuery();
                        }
                        load_form();
                        bindingSource.ResetBindings(false);
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_ten.Clear();
                        txt_matkhau.Clear();
                        txt_email.Clear();
                        txt_ten.Enabled = txt_email.Enabled = txt_matkhau.Enabled = cbo_quyen.Enabled = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            btn_xoa.Enabled = btn_sua.Enabled = btn_luu.Enabled = false;

        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            txt_ten.Enabled = false;
            txt_matkhau.Focus();
            txt_matkhau.Enabled = true;
            txt_email.Enabled = true;
            cbo_quyen.Enabled = true;
            btn_luu.Enabled = true;
            btn_xoa.Enabled = false;
            dgv_show.ReadOnly = false;
            btn_xoa.Enabled = btn_sua.Enabled = false;
        }

        private void txt_ten_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép ký tự chữ, số và các phím điều khiển (như Backspace)
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho nhập ký tự này
            }
        }

        private void txt_matkhau_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép ký tự chữ, số và các phím điều khiển (như Backspace)
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho nhập ký tự này
            }
        }


    }

}
