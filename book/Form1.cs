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

        private void add_Click(object sender, EventArgs e)
        {
            string ten_sach = TenSach.Text;
            string tac_gia = TacGia.Text;
            string the_loai = TheLoai.Text;
            string nam_xuat_ban = NamXuatBan.Text;
            string nha_xuat_ban = NhaXuatBan.Text;
            string thoi_gian = ThoiGian.Text;
            using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO tua_sach (ten_sach, tac_gia, the_loai, nam_xuat_ban, nha_xuat_ban, thoi_gian) VALUES (@ten, @tacgia, @theloai, @nam, @nxb, @thoigian)";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ten", ten_sach);
                    cmd.Parameters.AddWithValue("@tacgia", tac_gia);
                    cmd.Parameters.AddWithValue("@theloai", the_loai);
                    cmd.Parameters.AddWithValue("@nam", int.Parse(nam_xuat_ban));
                    cmd.Parameters.AddWithValue("@nxb", nha_xuat_ban);
                    cmd.Parameters.AddWithValue("@thoigian", DateTime.ParseExact(thoi_gian, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Thêm sách thành công!");
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }
    }
}