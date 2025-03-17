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
    public partial class Sua : BaseForm
    {
        private int idTuaSach; // Lưu ID sách để sửa

        public Sua(int idTuaSach)
        {
            InitializeComponent();
            this.idTuaSach = idTuaSach;
            LoadBookInfo();
        }

        private void LoadBookInfo()
        {
            using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT ten_sach, the_loai, nam_xuat_ban, nha_xuat_ban, so_luong, thoi_gian FROM Tua_Sach WHERE id_tua_sach = @id";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idTuaSach);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textboxIDTuasach.Text = idTuaSach.ToString();
                            textboxTenSach.Text = reader["ten_sach"].ToString();
                            textboxTheLoai.Text = reader["the_loai"].ToString();
                            textboxNamXuatBan.Text = reader["nam_xuat_ban"].ToString();
                            textboxNhaXuatBan.Text = reader["nha_xuat_ban"].ToString();
                            textboxSoLuong.Text = reader["so_luong"].ToString();
                            pickThoiGianNhap.Value = Convert.ToDateTime(reader["thoi_gian"]);
                        }
                    }
                }
            }
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = @"UPDATE Tua_Sach
                         SET ten_sach = @ten, the_loai = @theloai,
                             nam_xuat_ban = @nam, nha_xuat_ban = @nxb,
                             so_luong = @soluong, thoi_gian = @thoigian
                         WHERE id_tua_sach = @id";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idTuaSach);
                    cmd.Parameters.AddWithValue("@ten", textboxTenSach.Text);
                    cmd.Parameters.AddWithValue("@theloai", textboxTheLoai.Text);
                    cmd.Parameters.AddWithValue("@nam", int.Parse(textboxNamXuatBan.Text));
                    cmd.Parameters.AddWithValue("@nxb", textboxNhaXuatBan.Text);
                    cmd.Parameters.AddWithValue("@soluong", int.Parse(textboxSoLuong.Text));
                    cmd.Parameters.AddWithValue("@thoigian", pickThoiGianNhap.Value);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close(); // Đóng form sau khi cập nhật
        }


        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}