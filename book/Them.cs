using Npgsql;

namespace book
{
    public partial class Them : BaseForm
    {
        public Them()
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

        private void ThemTacGiaVaoDatabase(NpgsqlConnection conn, long id_tua_sach, string tac_gia, bool tac_gia_chinh)
        {
            string queryTacGia = "INSERT INTO tac_gia (ten_tac_gia) VALUES (@tacgia) " +
                                 "ON CONFLICT (ten_tac_gia) DO NOTHING RETURNING id_tac_gia";
            long id_tac_gia;
            using (NpgsqlCommand cmdTacGia = new NpgsqlCommand(queryTacGia, conn))
            {
                cmdTacGia.Parameters.AddWithValue("@tacgia", tac_gia);
                object result = cmdTacGia.ExecuteScalar();

                if (result == null)
                {
                    string getIdTacGia = "SELECT id_tac_gia FROM tac_gia WHERE ten_tac_gia = @tacgia";
                    using (NpgsqlCommand cmdGetId = new NpgsqlCommand(getIdTacGia, conn))
                    {
                        cmdGetId.Parameters.AddWithValue("@tacgia", tac_gia);
                        id_tac_gia = Convert.ToInt64(cmdGetId.ExecuteScalar());
                    }
                }
                else
                {
                    id_tac_gia = (long)result;
                }
            }

            string queryLienKet = "INSERT INTO TuaSach_TacGia (id_tua_sach, id_tac_gia, tac_gia_chinh) " +
                                  "VALUES (@idSach, @idTacGia, @tacGiaChinh)";
            using (NpgsqlCommand cmdLienKet = new NpgsqlCommand(queryLienKet, conn))
            {
                cmdLienKet.Parameters.AddWithValue("@idSach", id_tua_sach);
                cmdLienKet.Parameters.AddWithValue("@idTacGia", id_tac_gia);
                cmdLienKet.Parameters.AddWithValue("@tacGiaChinh", tac_gia_chinh);
                cmdLienKet.ExecuteNonQuery();
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            string ten_sach = textboxTenSach.Text;
            string the_loai = textboxTheLoai.Text;
            int nam_xuat_ban = int.Parse(textboxNamXuatBan.Text);
            string nha_xuat_ban = textboxNhaXuatBan.Text;
            int so_luong = int.Parse(textboxSoLuong.Text);
            DateTime thoi_gian = pickThoiGianNhap.Value;

            using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                NpgsqlTransaction transaction = conn.BeginTransaction();

                string querySach = "INSERT INTO tua_sach (ten_sach, the_loai, nam_xuat_ban, nha_xuat_ban, so_luong, thoi_gian, trang_thai) " +
                                   "VALUES (@ten, @theloai, @nam, @nxb, @sl, @thoigian, TRUE) RETURNING id_tua_sach";
                long id_tua_sach;
                using (NpgsqlCommand cmdSach = new NpgsqlCommand(querySach, conn))
                {
                    cmdSach.Parameters.AddWithValue("@ten", ten_sach);
                    cmdSach.Parameters.AddWithValue("@theloai", the_loai);
                    cmdSach.Parameters.AddWithValue("@nam", nam_xuat_ban);
                    cmdSach.Parameters.AddWithValue("@nxb", nha_xuat_ban);
                    cmdSach.Parameters.AddWithValue("@sl", so_luong);
                    cmdSach.Parameters.AddWithValue("@thoigian", thoi_gian);
                    id_tua_sach = Convert.ToInt64(cmdSach.ExecuteScalar());
                }

                foreach (var item in listBox1.Items)
                {
                    string tac_gia = item.ToString();
                    ThemTacGiaVaoDatabase(conn, id_tua_sach, tac_gia, false);
                }

                // 3️⃣ Thêm tác giả chính từ listBox2 (tac_gia_chinh = true)
                foreach (var item in listBox2.Items)
                {
                    string tac_gia = item.ToString();
                    ThemTacGiaVaoDatabase(conn, id_tua_sach, tac_gia, true);
                }

                transaction.Commit();
                MessageBox.Show($"Thêm sách '{ten_sach}' thành công!");
            }
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

        private void btnThemTacGia_Click(object sender, EventArgs e)
        {
            string tacGiaMoi = textboxTacGia.Text.Trim();
            if (!string.IsNullOrEmpty(tacGiaMoi) && !listBox1.Items.Contains(tacGiaMoi))
            {
                listBox1.Items.Add(tacGiaMoi);
                textboxTacGia.Clear();
            }
        }

        private void btnChuyenSang_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string selectedAuthor = listBox1.SelectedItem.ToString();
                if (!listBox2.Items.Contains(selectedAuthor))
                {
                    listBox2.Items.Add(selectedAuthor);
                    listBox1.Items.Remove(selectedAuthor);
                }
            }
        }

        private void btnChuyenVe_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                string selectedAuthor = listBox2.SelectedItem.ToString();
                listBox1.Items.Add(selectedAuthor);
                listBox2.Items.Remove(selectedAuthor);
            }
        }
    }
}