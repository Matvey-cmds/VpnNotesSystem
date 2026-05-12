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
            // Arrange
            FakeNoteRepository repository =
                new FakeNoteRepository();

            NoteService service =
                new NoteService(repository);

            // Act
            service.AddNote("admin", "hello");

            // Assert
            Assert.IsTrue(repository.AddCalled);
        }

        [TestMethod]
        public void GetUserNotes_ReturnsNotes()
        {
            // Arrange
            FakeNoteRepository repository =
                new FakeNoteRepository();

            NoteService service =
                new NoteService(repository);

            // Act
            var notes =
                service.GetUserNotes("admin");

            // Assert
            Assert.AreEqual(1, notes.Count);
        }
    }
}