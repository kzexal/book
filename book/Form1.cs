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
            ConnectToDatabase(); // Kết nối khi mở form
        }

        private void ConnectToDatabase()
        {
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1;Database=Book";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MessageBox.Show("Kết nối PostgreSQL thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}