using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpnNotesSystem
{
    internal class PostgresUpdateRepository : IUpdateRepository
    {
        private readonly string _connectionString;

        public PostgresUpdateRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string GetLatestVersion()
        {
            using (var conn =
                new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                using (var cmd =
                    new NpgsqlCommand(
                        "SELECT get_latest_version()",
                        conn))
                {
                    return cmd.ExecuteScalar().ToString();
                }
            }
        }
    }
}
