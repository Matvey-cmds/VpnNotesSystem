using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpnNotesSystem
{
    internal class AuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Login(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);

            if (user == null)
                return false;

            if (user.Password == password)
            {
                UserSession.CurrentUser = username;
                return true;
            }

            return false;
        }
    }
}
