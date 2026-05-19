using System.Collections.Generic;
using VpnNotesSystem;

namespace VpnNotesSystem.Tests
{
    public class FakeNoteRepository : INoteRepository
    {
        public bool AddCalled = false;
        public bool UpdateCalled = false;

        public void AddNote(string username, string text)
        {
            AddCalled = true;
        }

        public List<Note> GetAllNotes()
        {
            return new List<Note>();
        }
        public void UpdateNote(int id, string newText)
        {
            UpdateCalled = true;
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