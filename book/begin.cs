﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using static System.Windows.Forms.DataFormats;

namespace book
{
    public partial class begin : BaseForm
    {
        public begin()
        {
            InitializeComponent();
            LoadBookList();
        }

        private void LoadBookList()
        {
            using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = @"
SELECT
    ts.id_tua_sach,
    ts.ten_sach,
    ts.the_loai,
    STRING_AGG(tg.ten_tac_gia, ', ') AS tac_gia,
    ts.nam_xuat_ban,
    ts.nha_xuat_ban,
    ts.so_luong,
    ts.trang_thai,
    ts.thoi_gian
FROM Tua_Sach ts
LEFT JOIN TuaSach_TacGia tgts ON tgts.id_tua_sach = ts.id_tua_sach
LEFT JOIN Tac_Gia tg ON tg.id_tac_gia = tgts.id_tac_gia
WHERE ts.trang_thai = TRUE
GROUP BY ts.id_tua_sach, ts.ten_sach, ts.the_loai, ts.nam_xuat_ban, ts.nha_xuat_ban, ts.so_luong, ts.trang_thai, ts.thoi_gian";

                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán nguồn dữ liệu
                    dataGridViewBooks.DataSource = dt;

                    // Đặt tiêu đề cột
                    dataGridViewBooks.Columns["id_tua_sach"].HeaderText = "ID";
                    dataGridViewBooks.Columns["ten_sach"].HeaderText = "Tên sách";
                    dataGridViewBooks.Columns["the_loai"].HeaderText = "Thể loại";
                    dataGridViewBooks.Columns["tac_gia"].HeaderText = "Tác giả";
                    dataGridViewBooks.Columns["nam_xuat_ban"].HeaderText = "Năm xuất bản";
                    dataGridViewBooks.Columns["nha_xuat_ban"].HeaderText = "Nhà xuất bản";
                    dataGridViewBooks.Columns["so_luong"].HeaderText = "Số lượng";
                    dataGridViewBooks.Columns["trang_thai"].HeaderText = "Trạng thái";
                    dataGridViewBooks.Columns["thoi_gian"].HeaderText = "Thời Gian Nhập";

                    dataGridViewBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ThemTuaSach formAdd = new ThemTuaSach(LoadBookList);
            formAdd.Show();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            dataGridViewBooks.Columns["id_tua_sach"].ReadOnly = true;
            if (dataGridViewBooks.SelectedRows.Count > 0)
            {
                int idTuaSach = Convert.ToInt32(dataGridViewBooks.SelectedRows[0].Cells["id_tua_sach"].Value);

                SuaTuaSach formSua = new SuaTuaSach(idTuaSach, LoadBookList);
                formSua.Show();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridViewBooks.SelectedRows.Count > 0)
            {
                int idTuaSach = Convert.ToInt32(dataGridViewBooks.SelectedRows[0].Cells["id_tua_sach"].Value);

                using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // 1. Cập nhật Tua_Sach: set trạng thái FALSE + ngày xóa
                    string queryUpdateTuaSach = @"
                UPDATE tua_sach
                SET trang_thai = FALSE,
                    ngay_xoa = CURRENT_DATE
                WHERE id_tua_sach = @idTuaSach";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(queryUpdateTuaSach, conn))
                    {
                        cmd.Parameters.AddWithValue("@idTuaSach", idTuaSach);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Cập nhật tất cả Đầu Sách liên quan: set trạng thái FALSE + ngày xóa
                    string queryUpdateDauSach = @"
                UPDATE dau_sach
                SET trang_thai = FALSE,
                    ngay_xoa = CURRENT_DATE
                WHERE id_tua_sach = @idTuaSach";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(queryUpdateDauSach, conn))
                    {
                        cmd.Parameters.AddWithValue("@idTuaSach", idTuaSach);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Load lại danh sách sau khi xóa
                LoadBookList();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadBookList();
        }

        private void btnDanhsachdaxoa_Click(object sender, EventArgs e)
        {
            // Pass LoadBookList as the refresh callback
            DaXoaTuaSach daxoa = new DaXoaTuaSach(LoadBookList);
            daxoa.Show();
        }

        private void btnDauSach_Click(object sender, EventArgs e)
        {
            DauSach formDauSach = new DauSach();
            formDauSach.Show();
        }

        private void begin_Load(object sender, EventArgs e)
        {

        }
    }
}