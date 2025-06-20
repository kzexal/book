﻿using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace book
{
    public partial class DaXoaDauSach : BaseForm
    {
        private readonly Action _refreshParent; // Delegate to refresh the parent form

        public DaXoaDauSach(Action refreshParent = null)
        {
            InitializeComponent();
            _refreshParent = refreshParent; // Store the refresh callback
            LoadDeletedDauSach();
        }

        private void LoadDeletedDauSach()
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
    ds.ngay_nhap,
    ds.ngay_xoa,  -- thêm dòng này để lấy ngày xoá
    ds.trang_thai
FROM Dau_Sach ds
JOIN Tua_Sach ts ON ds.id_tua_sach = ts.id_tua_sach
LEFT JOIN TuaSach_TacGia tgts ON tgts.id_tua_sach = ts.id_tua_sach
LEFT JOIN Tac_Gia tg ON tg.id_tac_gia = tgts.id_tac_gia
WHERE ds.trang_thai = FALSE
GROUP BY ds.id_dau_sach, ds.ma_dau_sach, ts.ten_sach, ts.the_loai,
         ts.nam_xuat_ban, ts.nha_xuat_ban, ds.ngay_nhap, ds.ngay_xoa, ds.trang_thai";

                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    datagridviewDauSachDaXoa.DataSource = dt;

                    // Đặt tiêu đề cột
                    datagridviewDauSachDaXoa.Columns["id_dau_sach"].HeaderText = "ID Đầu Sách";
                    datagridviewDauSachDaXoa.Columns["ma_dau_sach"].HeaderText = "Mã Đầu Sách";
                    datagridviewDauSachDaXoa.Columns["ten_sach"].HeaderText = "Tên Sách";
                    datagridviewDauSachDaXoa.Columns["the_loai"].HeaderText = "Thể Loại";
                    datagridviewDauSachDaXoa.Columns["tac_gia"].HeaderText = "Tác Giả";
                    datagridviewDauSachDaXoa.Columns["nam_xuat_ban"].HeaderText = "Năm Xuất Bản";
                    datagridviewDauSachDaXoa.Columns["nha_xuat_ban"].HeaderText = "Nhà Xuất Bản";
                    datagridviewDauSachDaXoa.Columns["ngay_nhap"].HeaderText = "Ngày Nhập";
                    datagridviewDauSachDaXoa.Columns["ngay_xoa"].HeaderText = "Ngày Xoá";
                    datagridviewDauSachDaXoa.Columns["trang_thai"].HeaderText = "Trạng Thái";
                    datagridviewDauSachDaXoa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (datagridviewDauSachDaXoa.SelectedRows.Count > 0)
            {
                int idDauSach = Convert.ToInt32(datagridviewDauSachDaXoa.SelectedRows[0].Cells["id_dau_sach"].Value);

                using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        UPDATE dau_sach
                        SET trang_thai = TRUE,
                            ngay_xoa = NULL
                        WHERE id_dau_sach = @idDauSach";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idDauSach", idDauSach);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Khôi phục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadDeletedDauSach();
                _refreshParent?.Invoke();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sách để khôi phục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}