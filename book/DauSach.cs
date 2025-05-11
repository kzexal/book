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
            ThemDauSach formAdd = new ThemDauSach();
            formAdd.ShowDialog(); // Use ShowDialog to wait for form closure
            LoadDauSachList(); // Refresh after adding
        }

        private void btnSuaDauSach_Click(object sender, EventArgs e)
        {
            dataGridViewDauSach.Columns["id_dau_sach"].ReadOnly = true;
            if (dataGridViewDauSach.SelectedRows.Count > 0)
            {
                int idDauSach = Convert.ToInt32(dataGridViewDauSach.SelectedRows[0].Cells["id_dau_sach"].Value);

                // Pass LoadDauSachList as the refresh callback
                SuaDauSach formSua = new SuaDauSach(idDauSach, LoadDauSachList);
                formSua.ShowDialog(); // Use ShowDialog to wait for form closure
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
            LoadDauSachList(); // Refresh after deletion
        }

        private void btnDanhSachDaXoaDauSach_Click(object sender, EventArgs e)
        {
            // Pass LoadDauSachList as the refresh callback
            DaXoaDauSach formDeleted = new DaXoaDauSach(LoadDauSachList);
            formDeleted.Show();
        }

        private void btnLamMoiDauSach_Click(object sender, EventArgs e)
        {
            LoadDauSachList();
        }
    }
}