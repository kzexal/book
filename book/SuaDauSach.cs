using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace book
{
    public partial class SuaDauSach : BaseForm
    {
        private int idDauSach;
        private readonly Action _refreshParent;

        public SuaDauSach(int idDauSach, Action refreshParent = null)
        {
            InitializeComponent();
            this.idDauSach = idDauSach;
            _refreshParent = refreshParent;
            LoadDauSachInfo();
            LoadAuthors();
        }

        private void LoadDauSachInfo()
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT ds.ma_dau_sach, ds.ngay_nhap, ts.ten_sach, ts.the_loai, ts.nam_xuat_ban, ts.nha_xuat_ban, ts.so_luong
                    FROM Dau_Sach ds
                    JOIN Tua_Sach ts ON ds.id_tua_sach = ts.id_tua_sach
                    WHERE ds.id_dau_sach = @id";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idDauSach);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBoxIDDauSach.Text = idDauSach.ToString();
                            textBoxTenDauSach.Text = reader["ten_sach"].ToString();
                            textBoxTheLoaiDauSach.Text = reader["the_loai"].ToString();
                            textBoxNamDauSach.Text = reader["nam_xuat_ban"].ToString();
                            textBoxNhaDauSach.Text = reader["nha_xuat_ban"].ToString();
                            textBoxSoLuongDauSach.Text = reader["so_luong"].ToString();
                            ThoiGianNhapDauSach.Value = Convert.ToDateTime(reader["ngay_nhap"]);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy đầu sách!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Close();
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
                string query = @"
            SELECT tg.ten_tac_gia, tgchinh.tac_gia_chinh
            FROM Dau_Sach ds
            JOIN TuaSach_TacGia tgchinh ON ds.id_tua_sach = tgchinh.id_tua_sach
            JOIN Tac_Gia tg ON tgchinh.id_tac_gia = tg.id_tac_gia
            WHERE ds.id_dau_sach = @id";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idDauSach);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        listBox3.Items.Clear(); // Main authors
                        listBox4.Items.Clear(); // Co-authors

                        while (reader.Read())
                        {
                            string tenTacGia = reader["ten_tac_gia"].ToString();
                            bool laTacGiaChinh = reader.GetBoolean(reader.GetOrdinal("tac_gia_chinh"));

                            if (laTacGiaChinh)
                                listBox3.Items.Add(tenTacGia); // Main authors
                            else
                                listBox4.Items.Add(tenTacGia); // Co-authors
                        }
                    }
                }
            }
        }

        private void btnLuuDauSach_Click(object sender, EventArgs e)
        {
            string tenSach = textBoxTenDauSach.Text.Trim();
            string theLoai = textBoxTheLoaiDauSach.Text.Trim();
            string namXuatBanStr = textBoxNamDauSach.Text.Trim();
            string nhaXuatBan = textBoxNhaDauSach.Text.Trim();
            DateTime ngayNhap = ThoiGianNhapDauSach.Value;

            try
            {
                using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        // Lấy id_tua_sach từ id_dau_sach
                        long idTuaSach;
                        using (var cmd = new NpgsqlCommand("SELECT id_tua_sach FROM Dau_Sach WHERE id_dau_sach = @id", conn))
                        {
                            cmd.Parameters.AddWithValue("@id", idDauSach);
                            var result = cmd.ExecuteScalar();
                            if (result == null)
                                throw new Exception("Không tìm thấy id_tua_sach từ đầu sách.");
                            idTuaSach = (long)result;
                        }

                        // Cập nhật bảng Dau_Sach
                        using (var cmd = new NpgsqlCommand(@"
                    UPDATE Dau_Sach
                    SET  ngay_nhap = @ngayNhap
                    WHERE id_dau_sach = @id", conn))
                        {
                            cmd.Parameters.AddWithValue("@ngayNhap", ngayNhap);
                            cmd.Parameters.AddWithValue("@id", idDauSach);
                            cmd.ExecuteNonQuery();
                        }

                        // Cập nhật bảng Tua_Sach
                        using (var cmd = new NpgsqlCommand(@"
                    UPDATE Tua_Sach
                    SET ten_sach = @ten, the_loai = @theloai, nam_xuat_ban = @nam,
                        nha_xuat_ban = @nxb
                    WHERE id_tua_sach = @idTuaSach", conn))
                        {
                            cmd.Parameters.AddWithValue("@ten", tenSach);
                            cmd.Parameters.AddWithValue("@theloai", theLoai);
                            cmd.Parameters.AddWithValue("@nam", int.Parse(namXuatBanStr));
                            cmd.Parameters.AddWithValue("@nxb", nhaXuatBan);
                            cmd.Parameters.AddWithValue("@idTuaSach", idTuaSach);
                            cmd.ExecuteNonQuery();
                        }

                        // Delete existing author associations for this book
                        string deleteOld = "DELETE FROM TuaSach_TacGia WHERE id_tua_sach = @id";
                        using (var deleteCmd = new NpgsqlCommand(deleteOld, conn))
                        {
                            deleteCmd.Parameters.AddWithValue("@id", idTuaSach);
                            deleteCmd.ExecuteNonQuery();

                            // Thêm lại tác giả phụ (listBox4) và chính (listBox3)
                            foreach (var item in listBox4.Items)
                            {
                                AddOrUpdateAuthor(conn, item.ToString(), false, idTuaSach); // phụ
                            }

                            foreach (var item in listBox3.Items)
                            {
                                AddOrUpdateAuthor(conn, item.ToString(), true, idTuaSach); // chính
                            }

                            transaction.Commit();
                            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _refreshParent?.Invoke();
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddOrUpdateAuthor(NpgsqlConnection conn, string tenTacGia, bool isMainAuthor, long idTuaSach)
        {
            long idTacGia;

            string checkQuery = "SELECT id_tac_gia FROM Tac_Gia WHERE ten_tac_gia = @ten";
            using (var cmd = new NpgsqlCommand(checkQuery, conn))
            {
                cmd.Parameters.AddWithValue("@ten", tenTacGia);
                var result = cmd.ExecuteScalar();

                if (result == null)
                {
                    string insertQuery = "INSERT INTO Tac_Gia (ten_tac_gia) VALUES (@ten) RETURNING id_tac_gia";
                    using (var insertCmd = new NpgsqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@ten", tenTacGia);
                        idTacGia = (long)insertCmd.ExecuteScalar();
                    }
                }
                else
                {
                    idTacGia = (long)result;
                }
            }

            string insertLink = "INSERT INTO TuaSach_TacGia (id_tua_sach, id_tac_gia, tac_gia_chinh) VALUES (@idTuaSach, @idTacGia, @isMain)";
            using (var cmd = new NpgsqlCommand(insertLink, conn))
            {
                cmd.Parameters.AddWithValue("@idTuaSach", idTuaSach);
                cmd.Parameters.AddWithValue("@idTacGia", idTacGia);
                cmd.Parameters.AddWithValue("@isMain", isMainAuthor);
                cmd.ExecuteNonQuery();
            }
        }

        private void btnXoaTacGiaDS_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedItem != null)
                listBox4.Items.Remove(listBox4.SelectedItem);
            else if (listBox3.SelectedItem != null)
                listBox3.Items.Remove(listBox3.SelectedItem);
            else
                MessageBox.Show("Vui lòng chọn tác giả cần xoá.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnThemTacGiaDS_Click(object sender, EventArgs e)
        {
            string tenTacGia = textBoxTacGiaDauSach.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenTacGia))
            {
                MessageBox.Show("Vui lòng nhập tên tác giả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listBox3.Items.Contains(tenTacGia) || listBox4.Items.Contains(tenTacGia))
            {
                MessageBox.Show("Tác giả đã có trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            listBox4.Items.Add(tenTacGia);
            textBoxTacGiaDauSach.Clear();
        }

        private void label16_Click(object sender, EventArgs e)
        {
        }

        private void btnChonTacGiaChinh_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedItem != null)
            {
                string selected = listBox4.SelectedItem.ToString();
                if (!listBox3.Items.Contains(selected))
                {
                    listBox3.Items.Add(selected);
                    listBox4.Items.Remove(selected);
                }
            }
        }

        private void btnChonTacGiaPhu_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedItem != null)
            {
                string selected = listBox3.SelectedItem.ToString();
                if (!listBox4.Items.Contains(selected))
                {
                    listBox4.Items.Add(selected);
                    listBox3.Items.Remove(selected);
                }
            }
        }

        private void SuaDauSach_Load(object sender, EventArgs e)
        {

        }
    }
}