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
    public partial class Deleted : BaseForm
    {
        public Deleted()
        {
            InitializeComponent();
            Hienthisachdaxoa();
        }

        private void Hienthisachdaxoa()
        {
            using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT id_tua_sach, ten_sach, the_loai, nam_xuat_ban, nha_xuat_ban, so_luong, trang_thai, thoi_gian FROM tua_sach WHERE trang_thai = FALSE";

                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    Danhsachdaxoa.DataSource = dt; // Hiển thị dữ liệu lên DataGridView
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
                    string query = "UPDATE tua_sach SET trang_thai = TRUE WHERE id_tua_sach = @idTuaSach";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idTuaSach", idTuaSach);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}