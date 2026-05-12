using VpnNotesSystem;

namespace VpnNotesSystem.Tests
{
    public class FakeUserRepository : IUserRepository
    {
        public User GetByUsername(string username)
        {
            return new User
            {
                Username = "admin",
                Password = "1234",
                Role = "admin",
                IsBlocked = false
            };
        }

        public void BlockUser(string username)
        {

        }
    }
}