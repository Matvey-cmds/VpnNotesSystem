using System.Collections.Generic;
using VpnNotesSystem;

namespace VpnNotesSystem.Tests
{
    public class FakeNoteRepository : INoteRepository
    {
        public bool AddCalled = false;

        public void AddNote(string username, string text)
        {
            AddCalled = true;
        }

        public List<Note> GetAllNotes()
        {
            return new List<Note>();
        }

        public List<Note> GetUserNotes(string username)
        {
            return new List<Note>
            {
                new Note
                {
                    Username = username,
                    Text = "Test note"
                }
            };
        }
    }
}