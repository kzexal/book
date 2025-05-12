using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace book
{
    public partial class SuaTuaSach : BaseForm
    {
        private int idTuaSach; // Lưu ID sách để sửa
        private readonly Action _refreshParent; // Delegate to refresh the parent form

        public SuaTuaSach(int idTuaSach, Action refreshParent = null)
        {
            InitializeComponent();
            this.idTuaSach = idTuaSach;
            _refreshParent = refreshParent; // Store the refresh callback
            LoadBookInfo();
            LoadAuthors();
        }

        private void LoadBookInfo()
        {
            using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT ten_sach, the_loai, nam_xuat_ban, nha_xuat_ban, so_luong, thoi_gian FROM Tua_Sach WHERE id_tua_sach = @id";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idTuaSach);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textboxIDTuasach.Text = idTuaSach.ToString();
                            textboxTenSach.Text = reader["ten_sach"].ToString();
                            textboxTheLoai.Text = reader["the_loai"].ToString();
                            textboxNamXuatBan.Text = reader["nam_xuat_ban"].ToString();
                            textboxNhaXuatBan.Text = reader["nha_xuat_ban"].ToString();
                            textboxSoLuong.Text = reader["so_luong"].ToString();
                            pickThoiGianNhap.Value = Convert.ToDateTime(reader["thoi_gian"]);
                        }
                    }
                }
            }
        }

        private void LoadAuthors()
        {
            using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = @"SELECT tg.ten_tac_gia, tgchinh.tac_gia_chinh
                                 FROM Tac_Gia tg
                                 JOIN TuaSach_TacGia tgchinh ON tg.id_tac_gia = tgchinh.id_tac_gia
                                 WHERE tgchinh.id_tua_sach = @id";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idTuaSach);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        listBox2.Items.Clear(); // Clear main authors first
                        listBox1.Items.Clear(); // Clear co-authors

                        while (reader.Read())
                        {
                            string tenTacGia = reader["ten_tac_gia"].ToString();
                            bool laTacGiaChinh = reader.GetBoolean("tac_gia_chinh");

                            if (laTacGiaChinh)
                                listBox2.Items.Add(tenTacGia); // Main authors in listBox2
                            else
                                listBox1.Items.Add(tenTacGia); // Co-authors in listBox1
                        }
                    }
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                NpgsqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Update Tua_Sach table
                    string query = @"UPDATE Tua_Sach
                             SET ten_sach = @ten, the_loai = @theloai,
                                 nam_xuat_ban = @nam, nha_xuat_ban = @nxb,
                                 so_luong = @soluong, thoi_gian = @thoigian
                             WHERE id_tua_sach = @id";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idTuaSach);
                        cmd.Parameters.AddWithValue("@ten", textboxTenSach.Text);
                        cmd.Parameters.AddWithValue("@theloai", textboxTheLoai.Text);
                        cmd.Parameters.AddWithValue("@nam", int.Parse(textboxNamXuatBan.Text));
                        cmd.Parameters.AddWithValue("@nxb", textboxNhaXuatBan.Text);
                        cmd.Parameters.AddWithValue("@soluong", int.Parse(textboxSoLuong.Text));
                        cmd.Parameters.AddWithValue("@thoigian", pickThoiGianNhap.Value);

                        cmd.ExecuteNonQuery();
                    }

                    // Delete existing author associations for this book
                    string deleteOld = "DELETE FROM TuaSach_TacGia WHERE id_tua_sach = @id";
                    using (var deleteCmd = new NpgsqlCommand(deleteOld, conn))
                    {
                        deleteCmd.Parameters.AddWithValue("@id", idTuaSach);
                        deleteCmd.ExecuteNonQuery();
                    }

                    // Insert new author associations from listBox1 (co-authors)
                    foreach (var item in listBox1.Items)
                    {
                        string tacGia = item.ToString();
                        AddOrUpdateAuthor(conn, tacGia, false); // false for co-author
                    }

                    // Insert new author associations from listBox2 (main authors)
                    foreach (var item in listBox2.Items)
                    {
                        string tacGia = item.ToString();
                        AddOrUpdateAuthor(conn, tacGia, true); // true for main author
                    }

                    transaction.Commit();
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh the parent form
                    _refreshParent?.Invoke();

                    this.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AddOrUpdateAuthor(NpgsqlConnection conn, string tenTacGia, bool isMainAuthor)
        {
            long idTacGia;

            // Check if author exists, get or insert ID
            string queryCheckExist = "SELECT id_tac_gia FROM Tac_Gia WHERE ten_tac_gia = @tenTacGia";
            using (NpgsqlCommand cmdCheckExist = new NpgsqlCommand(queryCheckExist, conn))
            {
                cmdCheckExist.Parameters.AddWithValue("@tenTacGia", tenTacGia);
                object result = cmdCheckExist.ExecuteScalar();

                if (result == null)
                {
                    string queryInsertTacGia = "INSERT INTO Tac_Gia (ten_tac_gia) VALUES (@tenTacGia) RETURNING id_tac_gia";
                    using (NpgsqlCommand cmdInsertTacGia = new NpgsqlCommand(queryInsertTacGia, conn))
                    {
                        cmdInsertTacGia.Parameters.AddWithValue("@tenTacGia", tenTacGia);
                        idTacGia = (long)cmdInsertTacGia.ExecuteScalar();
                    }
                }
                else
                {
                    idTacGia = (long)result;
                }
            }

            // Link author to book
            string queryLink = "INSERT INTO TuaSach_TacGia (id_tua_sach, id_tac_gia, tac_gia_chinh) VALUES (@idTuaSach, @idTacGia, @isMainAuthor)";
            using (NpgsqlCommand cmdLink = new NpgsqlCommand(queryLink, conn))
            {
                cmdLink.Parameters.AddWithValue("@idTuaSach", idTuaSach);
                cmdLink.Parameters.AddWithValue("@idTacGia", idTacGia);
                cmdLink.Parameters.AddWithValue("@isMainAuthor", isMainAuthor);
                cmdLink.ExecuteNonQuery();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void btnTacGiaChinh_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string selectedAuthor = listBox1.SelectedItem.ToString();
                if (!listBox2.Items.Contains(selectedAuthor))
                {
                    listBox2.Items.Add(selectedAuthor);
                    listBox1.Items.Remove(selectedAuthor);
                }
            }
        }

        private void btnTacGiaPhu_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                string selectedAuthor = listBox2.SelectedItem.ToString();
                listBox1.Items.Add(selectedAuthor);
                listBox2.Items.Remove(selectedAuthor);
            }
        }

        private void btnThemTacGia_Click(object sender, EventArgs e)
        {
            string tenTacGia = textboxTacGia.Text.Trim();  // Lấy tên tác giả từ textbox và loại bỏ khoảng trắng thừa

            if (!string.IsNullOrEmpty(tenTacGia))
            {
                // Kiểm tra xem tác giả đã có trong listBox1 chưa
                if (!listBox1.Items.Contains(tenTacGia))
                {
                    listBox1.Items.Add(tenTacGia);  // Thêm tác giả vào listBox1 (co-authors)
                    textboxTacGia.Clear();  // Làm sạch textbox sau khi thêm tác giả
                }
                else
                {
                    MessageBox.Show("Tác giả đã có trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập tên tác giả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
        }

        private void btnXoaTacGia_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng có chọn tác giả trong listBox1 hoặc listBox2 không
            if (listBox1.SelectedItem != null)
            {
                // Nếu có chọn tác giả trong listBox1 (co-authors), xoá tác giả đó
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
            else if (listBox2.SelectedItem != null)
            {
                // Nếu có chọn tác giả trong listBox2 (main authors), xoá tác giả đó
                listBox2.Items.Remove(listBox2.SelectedItem);
            }
            else
            {
                // Nếu không có tác giả nào được chọn, thông báo cho người dùng
                MessageBox.Show("Vui lòng chọn tác giả cần xoá.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}