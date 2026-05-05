using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
using System.Threading.Tasks;

namespace VpnNotesSystem
{
    internal class PostgresUserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public PostgresUserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public User GetByUsername(string username)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(
                    "SELECT * FROM get_user_by_username(@username)",
                    conn))
                {
                    cmd.Parameters.AddWithValue("username", username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                Username = reader.GetString(0),
                                Password = reader.GetString(1),
                                Role = reader.GetString(2),
                                IsBlocked = reader.GetBoolean(3)
                            };
                        }
                    }
                }
            }
            return null;
        }
        public void BlockUser(string username)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(
                    "SELECT block_user(@username)", conn))
                {
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
