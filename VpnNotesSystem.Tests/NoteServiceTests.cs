using Microsoft.VisualStudio.TestTools.UnitTesting;
using VpnNotesSystem;

namespace VpnNotesSystem.Tests
{
    [TestClass]
    public class NoteServiceTests
    {
        [TestMethod]
        [DataRow("hello")]
        [DataRow("vpn config")]
        [DataRow("123456")]
        [DataRow("note with спецсимволы !@#")]
        public void AddNote_CheckDifferentTexts(string text)
        {
            // Arrange
            FakeNoteRepository repository =
                new FakeNoteRepository();

            NoteService service =
                new NoteService(repository);

            // Act
            service.AddNote("admin", text);

            // Assert
            Assert.IsTrue(repository.AddCalled);
        }

        [TestMethod]
        [DataRow("admin")]
        [DataRow("user")]
        [DataRow("test")]
        public void GetUserNotes_ReturnsNotesForDifferentUsers(string username)
        {
            // Arrange
            FakeNoteRepository repository =
                new FakeNoteRepository();

            NoteService service =
                new NoteService(repository);

            // Act
            var notes =
                service.GetUserNotes(username);

            // Assert
            Assert.AreEqual(1, notes.Count);
        }
        [TestMethod]
        [DataRow(1, "new text")]
        [DataRow(2, "vpn updated")]
        [DataRow(999, "not existing")]
        public void UpdateNote_CheckDifferentIds(int id,string text)
        {
            // Arrange
            FakeNoteRepository repository =
                new FakeNoteRepository();

            NoteService service =
                new NoteService(repository);

            // Act
            service.UpdateNote(id, text);

            // Assert
            Assert.IsTrue(repository.UpdateCalled);
        }

        [TestMethod]
        public void GetUserNotes_ReturnsEmptyList()
        {
            // Arrange
            EmptyNoteRepository repository =
                new EmptyNoteRepository();

            NoteService service =
                new NoteService(repository);

            // Act
            var notes =
                service.GetUserNotes("admin");

            // Assert
            Assert.AreEqual(0, notes.Count);
        }
    }
}