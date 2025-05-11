using System;
using System.Windows.Forms;
using Npgsql;

namespace book
{
    public partial class ThemDauSach : BaseForm
    {
        public ThemDauSach()
        {
            InitializeComponent();
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

        private string GenerateMaDauSach(long idTuaSach)
        {
            using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Dau_Sach WHERE id_tua_sach = @idTuaSach";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idTuaSach", idTuaSach);
                    long count = (long)cmd.ExecuteScalar();
                    return $"TS{idTuaSach}{count + 1}"; // e.g., TS51 for the first Dau_Sach of Tua_Sach with ID 5
                }
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textboxTenSach.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sách để tìm ID tựa sách!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Get id_tua_sach by looking up ten_sach
                string tenSach = textboxTenSach.Text;
                long idTuaSach = GetIdTuaSachFromTenSach(tenSach);

                // Generate ma_dau_sach
                string maDauSach = GenerateMaDauSach(idTuaSach);

                // Get ngay_nhap from DateTimePicker
                DateTime ngayNhap = pickThoiGianNhap.Value;

                using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string queryDauSach = "INSERT INTO Dau_Sach (id_tua_sach, ma_dau_sach, trang_thai, ngay_nhap) " +
                                          "VALUES (@idTuaSach, @maDauSach, TRUE, @ngayNhap)";

                    using (NpgsqlCommand cmdDauSach = new NpgsqlCommand(queryDauSach, conn))
                    {
                        cmdDauSach.Parameters.AddWithValue("@idTuaSach", idTuaSach);
                        cmdDauSach.Parameters.AddWithValue("@maDauSach", maDauSach);
                        cmdDauSach.Parameters.AddWithValue("@ngayNhap", ngayNhap);
                        cmdDauSach.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Thêm đầu sách với mã '{maDauSach}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm đầu sách: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Keep empty event handlers that might be in the designer
        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}