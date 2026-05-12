using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpnNotesSystem
{
    internal class PostgresLogRepository : ILogRepository
    {
        private readonly string _connectionString;
        public PostgresLogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddLog(string username, string action)
        {
            using (var conn =
                new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(
                    "SELECT add_log(@username, @action)",
                    conn))
                {
                    cmd.Parameters.AddWithValue(
                        "username", username);

                    cmd.Parameters.AddWithValue(
                        "action", action);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<Log> GetLogs()
        {
            var logs = new List<Log>();

            using (var conn =
                new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                using (var cmd =
                    new NpgsqlCommand(
                        "SELECT * FROM get_logs()",
                        conn))
                {
                    using (var reader =
                        cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(new Log
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Action = reader.GetString(2),
                                CreatedAt =
                                    reader.GetDateTime(3)
                            });
                        }
                    }
                }
            }

            return logs;
        }
    }
}
