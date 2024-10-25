﻿using System;
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
                        TreeNode phongBanNode = new TreeNode(tenPhongBan);
                        treeView_phongBan.Nodes.Add(phongBanNode);
                        // Lấy danh sách nhân viên cho phòng ban này
                        LoadNhanVienForPhongBan(phongBanNode);
                    }
                }
                connection.Close();

            }
            catch (Exception ex)
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
                using (cmd = new SqlCommand(sql, connection))
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
            finally
            {
                connection.Close();
            }
        }
        private void QuanLyPhongBan_Load(object sender, EventArgs e)
        {
            load_treeView_phongBan();
            load_combobox();
            treeView_phongBan.AfterSelect += new TreeViewEventHandler(treeView_phongBan_AfterSelect);
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
                cmd.Parameters.AddWithValue("@TenPhongBan", s);
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
                    using (cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@MAPHONGBAN", txt_maPhongBan.Text);
                        cmd.Parameters.AddWithValue("@TENPHONGBAN", txt_tenPhongBan.Text);

                        cmd.ExecuteNonQuery();

                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
                MessageBox.Show("Phòng Ban Đã Tồn Tại!");
            txt_tenPhongBan.Text = "";
            txt_tenPhongBan.Focus();

        }
        private void LoadNhanVienForPhongBan(TreeNode phongBanNode)
        {
            try
            {
                string sqlNhanVien = @"
                                            SELECT HSNV.HOTEN, HSNV.MANV
                                            FROM HOSONHANVIEN HSNV
                                            JOIN PHONGBAN PB ON PB.MAPHONG = HSNV.MAPHONG
                                            WHERE PB.TENPHONG = @VALUE";

                using (SqlCommand cmdNhanVien = new SqlCommand(sqlNhanVien, connection))
                {
                    cmdNhanVien.Parameters.AddWithValue("@VALUE", phongBanNode.Text);

                    using (SqlDataReader readerNhanVien = cmdNhanVien.ExecuteReader())
                    {
                        while (readerNhanVien.Read())
                        {
                            string hoTen = readerNhanVien["HOTEN"].ToString();
                            string maNV = readerNhanVien["MANV"].ToString();

                            // Tạo nút cho nhân viên và thêm vào nút phòng ban
                            //tag là thuộc tính của treeview cho phép bạn gán dữ liệu tùy ý vào nút mà không cần tạo thuộc tính mới.
                            TreeNode nhanVienNode = new TreeNode(hoTen) { Tag = maNV }; // cú pháp khởi tạo đối tượng
                            phongBanNode.Nodes.Add(nhanVienNode);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

        }

        private void bt_themNhanVien_Click(object sender, EventArgs e)
        {
            int index = -1;
            foreach (TreeNode node in treeView_phongBan.Nodes)
            {
                if (node.Text == cb_phongBan.Text)
                {
                    index = node.Index;
                    break;
                }
            }
            treeView_phongBan.Nodes[index].Nodes.Add("-" + txt_tenNhanVien.Text + "(-" + txt_maNhanVien.Text + "-)");
            treeView_phongBan.ExpandAll();//Mở Rộng TreeView
        }

        private void bt_xoaNhanVien_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn Có Chắc Chắn Muốn Xóa? ", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                if (treeView_phongBan.SelectedNode != null)
                {// Lấy tên phòng ban để sử dụng trong câu lệnh SQL
                    string tenPhongBan = treeView_phongBan.SelectedNode.Text;

                    // Xóa mục trong ComboBox (nếu cần)
                    cb_phongBan.Items.Remove(tenPhongBan);

                    // Xóa nút trong TreeView
                    treeView_phongBan.Nodes.Remove(treeView_phongBan.SelectedNode);

                    connection.Open();
                    string sql = "DELETE FROM PHONGBAN WHERE TENPHONG = @TENPHONG"; // Câu lệnh SQL để xóa phòng ban

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@TENPHONG", tenPhongBan); // Thêm tham số vào câu lệnh SQL

                        // Thực thi câu lệnh xóa
                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                    
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phòng ban để xóa.");
            }
        }

        private void treeView_phongBan_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Lấy nút được chọn từ tham số sự kiện
            TreeNode selectedNode = e.Node; // Đây là TreeNode, không phải TreeView

            // Kiểm tra xem nút có phải là nhân viên không
            if (selectedNode.Parent != null) // Nếu nút có cha, có thể là nhân viên
            {
                string tenNhanVien = selectedNode.Text; // Tên nhân viên

                
                    string maNV = selectedNode.Tag.ToString(); // Mã nhân viên (đã lưu trong Tag)

                    // Gọi hàm để hiển thị thông tin nhân viên
                    HienThiThongTinNhanVien(maNV);
                
                
            }
           

        }
        private void HienThiThongTinNhanVien(string maNV)
        {
            // Logic để lấy thông tin nhân viên từ cơ sở dữ liệu
            try
            {
                connection.Open();
                string sql = @"SELECT HSNV.MANV,HSNV.HOTEN,CV.MACHUCVU,CV.TENCHUCVU,PB.TENPHONG 
                                FROM HOSONHANVIEN HSNV
                                JOIN CHUCVU CV ON CV.MACHUCVU=HSNV.MACHUCVU
                                JOIN PHONGBAN PB ON PB.MAPHONG=HSNV.MAPHONG
                                WHERE MANV = @MANV";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@MANV", maNV);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string hoTen = reader["HOTEN"].ToString();
                        string manv = reader["MANV"].ToString();
                        string chucvu = reader["TENCHUCVU"].ToString();
                        string tenphong = reader["TENPHONG"].ToString();
                        // Hiển thị thông tin nhân viên
                        txt_maNhanVien.Text = maNV;
                        txt_tenNhanVien.Text = hoTen;
                        txt_chucVu.Text = chucvu;
                        cb_phongBan.Text = tenphong;
                        

                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin nhân viên.");
                    }
                }
                connection.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        
    }
}

