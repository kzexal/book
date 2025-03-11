using System;
using System.Windows.Forms;
using Npgsql;
using System.Data;

namespace book
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

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

        private void btnadd_Click(object sender, EventArgs e)
        {
            string ten_sach = textboxTenSach.Text;
            string tac_gia = textboxTacGia.Text;
            string the_loai = textboxTheLoai.Text;
            int nam_xuat_ban = int.Parse(textboxNamXuatBan.Text);
            string nha_xuat_ban = textboxNhaXuatBan.Text;
            int so_luong = int.Parse(textboxSoLuong.Text);
            DateTime thoi_gian = pickThoiGianNhap.Value;
            using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO tua_sach (ten_sach, tac_gia, the_loai, nam_xuat_ban, nha_xuat_ban, so_luong ,thoi_gian) VALUES (@ten, @tacgia, @theloai, @nam, @nxb, @sl ,@thoigian)";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ten", ten_sach);
                    cmd.Parameters.AddWithValue("@tacgia", tac_gia);
                    cmd.Parameters.AddWithValue("@theloai", the_loai);
                    cmd.Parameters.AddWithValue("@nam", nam_xuat_ban);
                    cmd.Parameters.AddWithValue("@nxb", nha_xuat_ban);
                    cmd.Parameters.AddWithValue("@sl", so_luong);
                    cmd.Parameters.AddWithValue("@thoigian", thoi_gian);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show($"Thêm sách '{textboxTenSach.Text}' thành công!");
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }
    }
}