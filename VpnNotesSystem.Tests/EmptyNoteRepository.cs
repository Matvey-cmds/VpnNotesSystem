using System.Collections.Generic;
using VpnNotesSystem;

namespace VpnNotesSystem.Tests
{
    public class EmptyNoteRepository: INoteRepository
    {
        public void AddNote(
            string username,
            string text)
        {

        }

        public void UpdateNote(
            int id,
            string newText)
        {

        }

        public List<Note> GetAllNotes()
        {
            return new List<Note>();
        }

        public List<Note> GetUserNotes(
            string username)
        {
            return new List<Note>();
        }
    }
}