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
        public List<Note> GetUserNotes(string username)
        {
            var notes = new List<Note>();

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(
                    "SELECT * FROM get_notes_by_user(@username)", conn))
                {
                    cmd.Parameters.AddWithValue("username", username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            notes.Add(new Note
                            {
                                Text = reader.GetString(0),
                                CreatedAt = reader.GetDateTime(1)
                            });
                        }
                    }
                }
            }

            return notes;
        }
        public List<Note> GetAllNotes()
        {
            var notes = new List<Note>();

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(
                    "SELECT * FROM get_all_notes()", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            notes.Add(new Note
                            {
                                Username = reader.GetString(0),
                                Text = reader.GetString(1),
                                CreatedAt = reader.GetDateTime(2)
                            });
                        }
                    }
                }
            }

            return notes;
        } 

    }
}
