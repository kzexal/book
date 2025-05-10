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
    public partial class DauSach : BaseForm
    {
        public DauSach()
        {
            InitializeComponent();
            LoadDauSachList();
        }

        private void LoadDauSachList()
        {
            using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT
                ds.id_dau_sach,
                ds.ma_dau_sach,
                ds.id_tua_sach,
                ts.ten_sach,
                ts.the_loai,
                STRING_AGG(tg.ten_tac_gia, ', ') AS tac_gia,
                ts.nam_xuat_ban,
                ts.nha_xuat_ban,
                ds.trang_thai,
                ds.ngay_nhap
            FROM Dau_Sach ds
            JOIN Tua_Sach ts ON ds.id_tua_sach = ts.id_tua_sach
            LEFT JOIN TuaSach_TacGia tgts ON tgts.id_tua_sach = ts.id_tua_sach
            LEFT JOIN Tac_Gia tg ON tg.id_tac_gia = tgts.id_tac_gia
            GROUP BY ds.id_dau_sach, ds.ma_dau_sach, ds.id_tua_sach, ts.ten_sach, ts.the_loai,
                     ts.nam_xuat_ban, ts.nha_xuat_ban, ds.trang_thai, ds.ngay_nhap";

                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridViewDauSach.DataSource = dt;

                    dataGridViewDauSach.Columns["id_dau_sach"].HeaderText = "ID Đầu Sách";
                    dataGridViewDauSach.Columns["ma_dau_sach"].HeaderText = "Mã Đầu Sách";
                    dataGridViewDauSach.Columns["id_tua_sach"].HeaderText = "ID Tựa Sách";
                    dataGridViewDauSach.Columns["ten_sach"].HeaderText = "Tên Sách";
                    dataGridViewDauSach.Columns["the_loai"].HeaderText = "Thể Loại";
                    dataGridViewDauSach.Columns["tac_gia"].HeaderText = "Tác Giả";
                    dataGridViewDauSach.Columns["nam_xuat_ban"].HeaderText = "Năm Xuất Bản";
                    dataGridViewDauSach.Columns["nha_xuat_ban"].HeaderText = "Nhà Xuất Bản";
                    dataGridViewDauSach.Columns["trang_thai"].HeaderText = "Trạng Thái";
                    dataGridViewDauSach.Columns["ngay_nhap"].HeaderText = "Ngày Nhập";
                }
            }
        }

        private void btnThemDauSach_Click(object sender, EventArgs e)
        {
            try
            {
                long id_tua_sach = GetSelectedTuaSachId();
                if (id_tua_sach == -1)
                {
                    MessageBox.Show("Vui lòng chọn một tựa sách để thêm đầu sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string ma_dau_sach = $"TS{id_tua_sach}_{Guid.NewGuid().ToString("N").Substring(0, 8)}";

                using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Thêm đầu sách mới
                    string insertQuery = @"
                INSERT INTO Dau_Sach (id_tua_sach, ma_dau_sach, trang_thai, ngay_nhap)
                VALUES (@id_tua_sach, @ma_dau_sach, @trang_thai, @ngay_nhap)
                ON CONFLICT DO NOTHING";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_tua_sach", id_tua_sach);
                        cmd.Parameters.AddWithValue("@ma_dau_sach", ma_dau_sach);
                        cmd.Parameters.AddWithValue("@trang_thai", true);
                        cmd.Parameters.AddWithValue("@ngay_nhap", DateTime.Now);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            MessageBox.Show("Thêm đầu sách thất bại. Mã đầu sách có thể đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Cập nhật số lượng tựa sách
                    string updateQuery = @"
                UPDATE Tua_Sach 
                SET so_luong = so_luong + 1 
                WHERE id_tua_sach = @id_tua_sach";

                    using (NpgsqlCommand cmdUpdate = new NpgsqlCommand(updateQuery, conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@id_tua_sach", id_tua_sach);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    MessageBox.Show("Thêm đầu sách thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDauSachList();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show($"Lỗi khi thêm đầu sách: {ex.Message}", "Lỗi cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaDauSach_Click(object sender, EventArgs e)
        {
            if (dataGridViewDauSach.SelectedRows.Count == 0) return;

            var row = dataGridViewDauSach.SelectedRows[0];
            long id_dau_sach = (long)row.Cells["id_dau_sach"].Value;

            using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();

                string update = "UPDATE Dau_Sach SET ngay_nhap = CURRENT_DATE WHERE id_dau_sach = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(update, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id_dau_sach);
                    cmd.ExecuteNonQuery();
                }
            }

            LoadDauSachList();
        }

        private void btnXoaDauSach_Click(object sender, EventArgs e)
        {
            if (dataGridViewDauSach.SelectedRows.Count == 0) return;

            var row = dataGridViewDauSach.SelectedRows[0];
            long id_dau_sach = (long)row.Cells["id_dau_sach"].Value;
            string ma_dau_sach = (string)row.Cells["ma_dau_sach"].Value;

            using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();

                // Lấy id_tua_sach của đầu sách
                long id_tua_sach;
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT id_tua_sach FROM Dau_Sach WHERE id_dau_sach = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id_dau_sach);
                    id_tua_sach = (long)cmd.ExecuteScalar();
                }

                // Xoá mềm (cập nhật trạng thái)
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE Dau_Sach SET trang_thai = FALSE WHERE id_dau_sach = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id_dau_sach);
                    cmd.ExecuteNonQuery();
                }

                // Trừ số lượng
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE Tua_Sach SET so_luong = so_luong - 1 WHERE id_tua_sach = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id_tua_sach);
                    cmd.ExecuteNonQuery();
                }
            }

            LoadDauSachList();
        }

        private void btnDanhSachDaXoaDauSach_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT
                        ds.id_dau_sach,
                        ds.ma_dau_sach,
                        ts.ten_sach,
                        ts.the_loai,
                        STRING_AGG(tg.ten_tac_gia, ', ') AS tac_gia,
                        ts.nam_xuat_ban,
                        ts.nha_xuat_ban,
                        ds.trang_thai,
                        ds.ngay_nhap
                    FROM Dau_Sach ds
                    JOIN Tua_Sach ts ON ds.id_tua_sach = ts.id_tua_sach
                    LEFT JOIN TuaSach_TacGia tgts ON tgts.id_tua_sach = ts.id_tua_sach
                    LEFT JOIN Tac_Gia tg ON tg.id_tac_gia = tgts.id_tac_gia
                    WHERE ds.trang_thai = FALSE
                    GROUP BY ds.id_dau_sach, ds.ma_dau_sach, ts.ten_sach, ts.the_loai,
                             ts.nam_xuat_ban, ts.nha_xuat_ban, ds.trang_thai, ds.ngay_nhap";

                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewDauSach.DataSource = dt;
                }
            }
        }

        private void btnLamMoiDauSach_Click(object sender, EventArgs e)
        {
            LoadDauSachList();
        }

      
    }
}