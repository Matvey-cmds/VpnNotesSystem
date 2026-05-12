using Microsoft.VisualStudio.TestTools.UnitTesting;
using VpnNotesSystem;

namespace VpnNotesSystem.Tests
{
    [TestClass]
    public class NoteServiceTests
    {
        [TestMethod]
        public void AddNote_CallsRepositoryMethod()
        {
            FakeNoteRepository repository =
                new FakeNoteRepository();
            NoteService service =
                new NoteService(repository);
            service.AddNote("admin", "hello");
            Assert.IsTrue(repository.AddCalled);
        }

        [TestMethod]
        public void GetUserNotes_ReturnsNotes()
        {
            FakeNoteRepository repository =
                new FakeNoteRepository();
            NoteService service =
                new NoteService(repository);
            var notes =
                service.GetUserNotes("admin");
            Assert.AreEqual(1, notes.Count);
        }
    }
}