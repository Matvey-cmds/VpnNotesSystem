using VpnNotesSystem;

namespace VpnNotesSystem.Tests
{
    public class FakeUserRepository : IUserRepository
    {
        public User GetByUsername(string username)
        {
            if (username == "admin")
            {
                return new User
                {
                    Username = "admin",
                    Password = "1234",
                    Role = "admin",
                    IsBlocked = false
                };
            }

            return null;
        }
        public void BlockUser(string username)
        {

        }
    }
}