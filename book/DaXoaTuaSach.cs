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
    public partial class DaXoaTuaSach : BaseForm
    {
        private readonly Action _refreshParent; // Delegate to refresh the parent form

        public DaXoaTuaSach(Action refreshParent = null)
        {
            InitializeComponent();
            _refreshParent = refreshParent; // Store the refresh callback
            Hienthisachdaxoa();
        }

        private void Hienthisachdaxoa()
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
WHERE ts.trang_thai = FALSE
GROUP BY ts.id_tua_sach, ts.ten_sach, ts.the_loai, ts.nam_xuat_ban, ts.nha_xuat_ban, ts.so_luong, ts.trang_thai, ts.thoi_gian";

                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    Danhsachdaxoa.DataSource = dt; // Hiển thị dữ liệu lên DataGridView

                    // Đặt tiêu đề cột
                    Danhsachdaxoa.Columns["id_tua_sach"].HeaderText = "ID";
                    Danhsachdaxoa.Columns["ten_sach"].HeaderText = "Tên sách";
                    Danhsachdaxoa.Columns["the_loai"].HeaderText = "Thể loại";
                    Danhsachdaxoa.Columns["tac_gia"].HeaderText = "Tác giả";
                    Danhsachdaxoa.Columns["nam_xuat_ban"].HeaderText = "Năm xuất bản";
                    Danhsachdaxoa.Columns["nha_xuat_ban"].HeaderText = "Nhà xuất bản";
                    Danhsachdaxoa.Columns["so_luong"].HeaderText = "Số lượng";
                    Danhsachdaxoa.Columns["trang_thai"].HeaderText = "Trạng thái";
                    Danhsachdaxoa.Columns["thoi_gian"].HeaderText = "Thời gian nhập";
                }
            }
        }

        private void btnKhoiphuc_Click(object sender, EventArgs e)
        {
            if (Danhsachdaxoa.SelectedRows.Count > 0)
            {
                int idTuaSach = Convert.ToInt32(Danhsachdaxoa.SelectedRows[0].Cells["id_tua_sach"].Value);

                using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"
    UPDATE tua_sach SET trang_thai = TRUE WHERE id_tua_sach = @idTuaSach;
    UPDATE dau_sach SET trang_thai = TRUE WHERE id_tua_sach = @idTuaSach;
";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idTuaSach", idTuaSach);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Hiển thị thông báo khôi phục thành công
                MessageBox.Show("Khôi phục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Làm mới DataGridView trong Deleted form
                Hienthisachdaxoa();

                // Làm mới DataGridView trong begin form
                _refreshParent?.Invoke();
            }
            else
            {
                // Thông báo nếu không có dòng nào được chọn
                MessageBox.Show("Vui lòng chọn một sách để khôi phục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}