using VpnNotesSystem;

namespace VpnNotesSystem.Tests
{
    public class BlockedUserRepository : IUserRepository
    {
        public User GetByUsername(string username)
        {
            return new User
            {
                Username = "blocked",
                Password = "1234",
                Role = "user",
                IsBlocked = true
            };
        }

        public void BlockUser(string username)
        {

        }
    }
}