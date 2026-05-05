using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpnNotesSystem
{
    public class Note
    {
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public interface INoteRepository
    {
        void AddNote(string username, string text);
        List<Note> GetUserNotes(string username);
        List<Note> GetAllNotes();
    }
}
