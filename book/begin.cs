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
                string query = "SELECT id_tua_sach, ten_sach, the_loai, nam_xuat_ban, nha_xuat_ban, so_luong, trang_thai, thoi_gian FROM tua_sach";
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Định dạng cột thời gian
                    dt.Columns["id_tua_sach"].ColumnName = "ID"; // Đổi tên hiển thị nếu cần
                    dt.Columns["ten_sach"].ColumnName = "Tên sách"; // Đổi tên hiển thị nếu cần
                    dt.Columns["the_loai"].ColumnName = "Thể loại"; // Đổi tên hiển thị nếu cần
                    dt.Columns["nam_xuat_ban"].ColumnName = "Năm xuất bản"; // Đổi tên hiển thị nếu cần
                    dt.Columns["nha_xuat_ban"].ColumnName = "Nhà xuất bản"; // Đổi tên hiển thị nếu cần
                    dt.Columns["so_luong"].ColumnName = "Số lượng"; // Đổi tên hiển thị nếu cần
                    dt.Columns["trang_thai"].ColumnName = "Trạng thái"; // Đổi tên hiển thị nếu cần
                    dt.Columns["thoi_gian"].ColumnName = "Thời Gian Nhập"; // Đổi tên hiển thị nếu cần
                    dataGridViewBooks.DataSource = dt; // Hiển thị dữ liệu lên DataGridView
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them formAdd = new Them();
            formAdd.ShowDialog();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            dataGridViewBooks.Columns["id_tua_sach"].ReadOnly = true;
            if (dataGridViewBooks.SelectedRows.Count > 0)
            {
                // Lấy ID của sách được chọn
                int idTuaSach = Convert.ToInt32(dataGridViewBooks.SelectedRows[0].Cells["id_tua_sach"].Value);

                // Mở form sửa và truyền ID sách vào
                Sua formSua = new Sua(idTuaSach);
                formSua.ShowDialog();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Xoa formXoa = new Xoa();
            formXoa.ShowDialog();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadBookList();
        }
    }
}