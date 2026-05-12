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
            IUserRepository repository =
                new FakeUserRepository();
            AuthService authService =
                new AuthService(repository);
            bool result =
                authService.Login("admin", "1234");
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Login_WithWrongPassword_ReturnsFalse()
        {
            IUserRepository repository =
                new FakeUserRepository();
            AuthService authService =
                new AuthService(repository);
            bool result =
                authService.Login("admin", "wrong");
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void Login_BlockedUser_ReturnsFalse()
        {
            IUserRepository repository =
                new BlockedUserRepository();
            AuthService authService =
                new AuthService(repository);
            bool result =
                authService.Login("blocked", "1234");
            Assert.IsFalse(result);
        }
    }
}