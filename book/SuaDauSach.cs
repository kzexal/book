using System;
using System.Windows.Forms;
using Npgsql;

namespace book
{
    public partial class SuaDauSach : BaseForm
    {
        private int idDauSach; // Lưu ID đầu sách để sửa
        private readonly Action _refreshParent; // Delegate to refresh the parent form

        public SuaDauSach(int idDauSach, Action refreshParent = null)
        {
            InitializeComponent();
            this.idDauSach = idDauSach;
            _refreshParent = refreshParent; // Store the refresh callback
            LoadDauSachInfo();
        }

        private void LoadDauSachInfo()
        {
            using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT ds.ma_dau_sach, ds.id_tua_sach, ds.ngay_nhap, ts.ten_sach
                    FROM Dau_Sach ds
                    JOIN Tua_Sach ts ON ds.id_tua_sach = ts.id_tua_sach
                    WHERE ds.id_dau_sach = @id";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idDauSach);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtboxIDDauSach.Text = reader["ma_dau_sach"].ToString();
                            textboxTenSach.Text = reader["ten_sach"].ToString(); // Display linked Tua_Sach name
                            pickThoiGianNhap.Value = reader.GetDateTime(reader.GetOrdinal("ngay_nhap"));
                        }
                    }
                }
            }
        }

        private long GetIdTuaSachFromTenSach(string tenSach)
        {
            using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT id_tua_sach FROM Tua_Sach WHERE ten_sach = @tenSach AND trang_thai = TRUE";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tenSach", tenSach);
                    object result = cmd.ExecuteScalar();
                    if (result == null)
                    {
                        throw new Exception($"Không tìm thấy tựa sách '{tenSach}' trong cơ sở dữ liệu!");
                    }
                    return (long)result;
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtboxIDDauSach.Text) || string.IsNullOrWhiteSpace(textboxTenSach.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ mã đầu sách và tên sách!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Verify or get id_tua_sach from ten_sach
                long idTuaSach = GetIdTuaSachFromTenSach(textboxTenSach.Text);

                // Get updated values
                string maDauSach = txtboxIDDauSach.Text;
                DateTime ngayNhap = pickThoiGianNhap.Value;

                using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    NpgsqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        string query = "UPDATE Dau_Sach SET ma_dau_sach = @ma, id_tua_sach = @idTuaSach, ngay_nhap = @ngayNhap WHERE id_dau_sach = @id";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", idDauSach);
                            cmd.Parameters.AddWithValue("@ma", maDauSach);
                            cmd.Parameters.AddWithValue("@idTuaSach", idTuaSach);
                            cmd.Parameters.AddWithValue("@ngayNhap", ngayNhap);
                            cmd.ExecuteNonQuery();
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
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xác thực dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Keep empty event handlers that might be in the designer
        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label9_Click(object sender, EventArgs e)
        {
        }

        private void btnThemTacGia_Click(object sender, EventArgs e)
        {
        }

        private void btnTacGiaChinh_Click(object sender, EventArgs e)
        {
        }

        private void btnTacGiaPhu_Click(object sender, EventArgs e)
        {
        }
    }
}