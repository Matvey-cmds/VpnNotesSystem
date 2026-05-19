using Microsoft.VisualStudio.TestTools.UnitTesting;
using VpnNotesSystem;

namespace VpnNotesSystem.Tests
{
    [TestClass]
    public class AuthServiceTests
    {
        [TestMethod]
        [DataRow("admin", "1234", true)]
        [DataRow("admin", "wrong", false)]
        [DataRow("wrongUser", "1234", false)]
        public void Login_CheckDifferentCredentials(
        string username,
        string password,
        bool expectedResult)
        {
            // Arrange
            IUserRepository repository =
                new FakeUserRepository();

            AuthService authService =
                new AuthService(repository);

            // Act
            bool result =
                authService.Login(username, password);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void Login_BlockedUser_ReturnsFalse()
        {
            // Arrange
            IUserRepository repository =
                new BlockedUserRepository();

            AuthService authService =
                new AuthService(repository);

            // Act
            bool result =
                authService.Login("blocked", "1234");

            // Assert
            Assert.IsFalse(result);
        }
    }
}