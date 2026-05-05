using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpnNotesSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=111;Database=postgres";

            IUserRepository userRepo = new PostgresUserRepository(connectionString);
            INoteRepository noteRepo = new PostgresNoteRepository(connectionString);
            AuthService authService = new AuthService(userRepo);
            NoteService noteService = new NoteService(noteRepo);
            UserService userService = new UserService(userRepo);
            CommandHandler handler = new CommandHandler(authService, noteService, userService);

            handler.Run();
        }
    }
}
