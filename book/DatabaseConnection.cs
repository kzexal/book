using System;
using Npgsql;

namespace book
{
    public class DatabaseConnection
    {
        private static string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1;Database=Book";

        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connectionString);
        }
    }
}