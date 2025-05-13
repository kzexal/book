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
                WHERE ds.trang_thai = TRUE
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
                    dataGridViewDauSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
            }
        }

        private void btnThemDauSach_Click(object sender, EventArgs e)
        {
            if (dataGridViewDauSach.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một đầu sách để sao chép.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow selectedRow = dataGridViewDauSach.SelectedRows[0];
            int idTuaSach = Convert.ToInt32(selectedRow.Cells["id_tua_sach"].Value);

            using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    string newMaDauSach = GenerateUniqueMaDauSach(idTuaSach, conn);

                    // 1. Thêm vào bảng Dau_Sach
                    string insertQuery = @"
                INSERT INTO Dau_Sach (ma_dau_sach, id_tua_sach, trang_thai, ngay_nhap)
                VALUES (@maDauSach, @idTuaSach, TRUE, CURRENT_DATE)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@maDauSach", newMaDauSach);
                        cmd.Parameters.AddWithValue("@idTuaSach", idTuaSach);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Cập nhật số lượng sách trong bảng Tua_Sach
                    string updateSoLuongQuery = @"
                UPDATE Tua_Sach
                SET so_luong = so_luong + 1
                WHERE id_tua_sach = @idTuaSach";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(updateSoLuongQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@idTuaSach", idTuaSach);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }
            MessageBox.Show("Thêm đầu sách thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadDauSachList(); // Refresh lại danh sách
        }

        private string GenerateUniqueMaDauSach(int idTuaSach, NpgsqlConnection conn)
        {
            int stt = 1;
            string ma;
            while (true)
            {
                ma = $"TS{idTuaSach}_{stt}";

                string queryCheck = "SELECT COUNT(*) FROM dau_sach WHERE ma_dau_sach = @ma";
                using (var cmd = new NpgsqlCommand(queryCheck, conn))
                {
                    cmd.Parameters.AddWithValue("@ma", ma);
                    long count = (long)cmd.ExecuteScalar();
                    if (count == 0) break; // Không trùng => dùng được
                }

                stt++; // Nếu trùng thì thử tiếp
            }
            return ma;
        }

        private void btnSuaDauSach_Click(object sender, EventArgs e)
        {
            dataGridViewDauSach.Columns["id_dau_sach"].ReadOnly = true;
            if (dataGridViewDauSach.SelectedRows.Count > 0)
            {
                int idDauSach = Convert.ToInt32(dataGridViewDauSach.SelectedRows[0].Cells["id_dau_sach"].Value);
                SuaDauSach formSua = new SuaDauSach(idDauSach, LoadDauSachList);
                formSua.ShowDialog();
            }
        }

        private void btnXoaDauSach_Click(object sender, EventArgs e)
        {
            if (dataGridViewDauSach.SelectedRows.Count > 0)
            {
                int idDauSach = Convert.ToInt32(dataGridViewDauSach.SelectedRows[0].Cells["id_dau_sach"].Value);

                using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE Dau_Sach SET trang_thai = FALSE WHERE id_dau_sach = @idDauSach";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idDauSach", idDauSach);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            LoadDauSachList();
        }

        private void btnDanhSachDaXoaDauSach_Click(object sender, EventArgs e)
        {
            DaXoaDauSach formDeleted = new DaXoaDauSach(LoadDauSachList);
            formDeleted.Show();
        }

        private void btnLamMoiDauSach_Click(object sender, EventArgs e)
        {
            LoadDauSachList();
        }
    }
}