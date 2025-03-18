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

                    // Gán nguồn dữ liệu
                    dataGridViewBooks.DataSource = dt;

                    // Đặt tiêu đề cột
                    dataGridViewBooks.Columns["id_tua_sach"].HeaderText = "ID";
                    dataGridViewBooks.Columns["ten_sach"].HeaderText = "Tên sách";
                    dataGridViewBooks.Columns["the_loai"].HeaderText = "Thể loại";
                    dataGridViewBooks.Columns["nam_xuat_ban"].HeaderText = "Năm xuất bản";
                    dataGridViewBooks.Columns["nha_xuat_ban"].HeaderText = "Nhà xuất bản";
                    dataGridViewBooks.Columns["so_luong"].HeaderText = "Số lượng";
                    dataGridViewBooks.Columns["trang_thai"].HeaderText = "Trạng thái";
                    dataGridViewBooks.Columns["thoi_gian"].HeaderText = "Thời Gian Nhập";
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them formAdd = new Them();
            formAdd.Show();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            dataGridViewBooks.Columns["id_tua_sach"].ReadOnly = true;
            if (dataGridViewBooks.SelectedRows.Count > 0)
            {
                int idTuaSach = Convert.ToInt32(dataGridViewBooks.SelectedRows[0].Cells["id_tua_sach"].Value);

                Sua formSua = new Sua(idTuaSach);
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
                    string query = "UPDATE tua_sach SET trang_thai = FALSE WHERE id_tua_sach = @idTuaSach";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idTuaSach", idTuaSach);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadBookList();
        }

        private void btnDanhsachdaxoa_Click(object sender, EventArgs e)
        {
            Deleted daxoa = new Deleted();
            daxoa.Show();
        }
    }
}