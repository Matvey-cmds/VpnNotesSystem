using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpnNotesSystem
{
    internal class PostgresNoteRepository : INoteRepository
    {
        private readonly string _connectionString;
        public PostgresNoteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddNote(string username, string text)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(
                    "SELECT add_note(@username, @text)",
                    conn))
                {
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("text", text);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
