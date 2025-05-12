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
                            txtboxIDDauSach.Text = idDauSach.ToString();
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

        private long GetIdTuaSachFromTenSach(string tenSach)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT id_tua_sach FROM Tua_Sach WHERE ten_sach = @tenSach AND trang_thai = TRUE";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tenSach", tenSach);
                    var result = cmd.ExecuteScalar();

                    if (result == null)
                        throw new Exception($"Không tìm thấy tựa sách '{tenSach}' trong cơ sở dữ liệu!");

                    return (long)result;
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maDauSach = txtboxIDDauSach.Text.Trim();
            string tenSach = textboxTenSach.Text.Trim();

            if (string.IsNullOrWhiteSpace(maDauSach) || string.IsNullOrWhiteSpace(tenSach))
            {
                MessageBox.Show("Vui lòng điền đầy đủ mã đầu sách và tên sách!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                long idTuaSach = GetIdTuaSachFromTenSach(tenSach);
                DateTime ngayNhap = pickThoiGianNhap.Value;

                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        string query = @"
                            UPDATE Dau_Sach
                            SET ma_dau_sach = @ma, id_tua_sach = @idTuaSach, ngay_nhap = @ngayNhap
                            WHERE id_dau_sach = @id";

                        using (var cmd = new NpgsqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ma", maDauSach);
                            cmd.Parameters.AddWithValue("@idTuaSach", idTuaSach);
                            cmd.Parameters.AddWithValue("@ngayNhap", ngayNhap);
                            cmd.Parameters.AddWithValue("@id", idDauSach);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _refreshParent?.Invoke();
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTacGiaChinh_Click(object sender, EventArgs e)
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

        private void btnTacGiaPhu_Click(object sender, EventArgs e)
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

        private void btnXoaTacGia_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedItem != null)
                listBox4.Items.Remove(listBox4.SelectedItem);
            else if (listBox3.SelectedItem != null)
                listBox3.Items.Remove(listBox3.SelectedItem);
            else
                MessageBox.Show("Vui lòng chọn tác giả cần xoá.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnThemTacGia_Click(object sender, EventArgs e)
        {
            string tenTacGia = textboxTacGia.Text.Trim();

            if (!string.IsNullOrEmpty(tenTacGia))
            {
                if (!listBox4.Items.Contains(tenTacGia))
                {
                    listBox4.Items.Add(tenTacGia);
                    textboxTacGia.Clear();
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

        private void label16_Click(object sender, EventArgs e)
        {
        }
    }
}