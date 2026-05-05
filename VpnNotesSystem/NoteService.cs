using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpnNotesSystem
{
    public class NoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public void AddNote(string username, string text)
        {
            _noteRepository.AddNote(username, text);
        }
        public List<Note> GetUserNotes(string username)
        {
            return _noteRepository.GetUserNotes(username);
        }
        public List<Note> GetAllNotes()
        {
            return _noteRepository.GetAllNotes();
        }
    }
}
