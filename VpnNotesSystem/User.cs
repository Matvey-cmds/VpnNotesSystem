using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpnNotesSystem
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public interface IUserRepository
    {
        User GetByUsername(string username);
    }
}
