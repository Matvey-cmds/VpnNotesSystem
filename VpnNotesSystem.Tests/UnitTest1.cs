using Microsoft.VisualStudio.TestTools.UnitTesting;
using VpnNotesSystem;

namespace VpnNotesSystem.Tests
{
    [TestClass]
    public class AuthServiceTests
    {
        [TestMethod]
        public void Login_WithCorrectPassword_ReturnsTrue()
        {
            // Arrange
            IUserRepository repository =
                new FakeUserRepository();

            AuthService authService =
                new AuthService(repository);

            // Act
            bool result =
                authService.Login("admin", "1234");

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Login_WithWrongPassword_ReturnsFalse()
        {
            // Arrange
            IUserRepository repository =
                new FakeUserRepository();

            AuthService authService =
                new AuthService(repository);

            // Act
            bool result =
                authService.Login("admin", "wrong");

            // Assert
            Assert.IsFalse(result);
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