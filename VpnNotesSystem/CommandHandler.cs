using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpnNotesSystem
{
    internal class CommandHandler
    {
        private readonly AuthService _authService;
        private readonly NoteService _noteService;
        private readonly UserService _userService;
        public CommandHandler(AuthService authService, NoteService noteService, UserService userService)
        {
            _authService = authService;
            _noteService = noteService;
            _userService = userService;
        }

        public void Run()
        {
            Console.WriteLine("Система заметок запущена");
            Console.WriteLine("Введите команду (--help для списка)");

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                string[] args = input.Split(' ');

                HandleCommand(args);
            }
        }
        private bool IsAdmin()
        {
            return UserSession.CurrentRole == "admin";
        }
        private void HandleCommand(string[] args)
        {
            if (args[0] == "--login")
            {
                RunLogin();
            }
            else if (args[0] == "--addNewNote")
            {
                AddNote(args);
            }
            else if (args[0] == "--help")
            {
                ShowHelp();
            }
            else if (args[0] == "--exit")
            {
                Environment.Exit(0);
            }
            else if (args[0] == "--allNotes")
            {
                if (UserSession.CurrentUser == null)
                {
                    Console.WriteLine("Сначала выполните вход");
                    return;
                }

                if (UserSession.CurrentRole != "admin")
                {
                    Console.WriteLine("Нет прав");
                    return;
                }

                var notes = _noteService.GetAllNotes();

                if (notes.Count == 0)
                {
                    Console.WriteLine("Нет заметок");
                    return;
                }

                foreach (var note in notes)
                {
                    Console.WriteLine(
                        note.Username + " | " +
                        note.Text + " | " +
                        note.CreatedAt
                    );
                }
            }
            else if (args[0] == "--myNotes")
            {
                if (UserSession.CurrentUser == null)
                {
                    Console.WriteLine("Сначала выполните вход");
                    return;
                }

                var notes = _noteService.GetUserNotes(UserSession.CurrentUser);

                if (notes.Count == 0)
                {
                    Console.WriteLine("У вас нет заметок");
                    return;
                }

                foreach (var note in notes)
                {
                    Console.WriteLine(note.Text + " | " + note.CreatedAt);
                }
            }
            else if (args[0] == "--blockUser")
            {
                if (UserSession.CurrentUser == null)
                {
                    Console.WriteLine("Сначала выполните вход");
                    return;
                }

                if (UserSession.CurrentRole != "admin")
                {
                    Console.WriteLine("Нет прав");
                    return;
                }

                if (args.Length < 2)
                {
                    Console.WriteLine("Укажи username");
                    return;
                }

                string username = args[1];

                _userService.BlockUser(username);

                Console.WriteLine("Пользователь заблокирован");
            }
            else
            {
                Console.WriteLine("Неизвестная команда");
            }
        }

        private void RunLogin()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            bool result = _authService.Login(username, password);

            if (result)
                Console.WriteLine("Успешный вход");
            else
                Console.WriteLine("Ошибка авторизации");
        }

        private void AddNote(string[] args)
        {
            if (UserSession.CurrentUser == null)
            {
                Console.WriteLine("Сначала выполните вход (--login)");
                return;
            }

            if (args.Length < 2)
            {
                Console.WriteLine("Введите текст заметки");
                return;
            }

            string text = string.Join(" ", args, 1, args.Length - 1);

            _noteService.AddNote(UserSession.CurrentUser, text);

            Console.WriteLine("Заметка добавлена");
        }

        private void ShowHelp()
        {
            Console.WriteLine("Команды:");
            Console.WriteLine("--login");
            Console.WriteLine("--addNewNote <text>");
            Console.WriteLine("--help");
            Console.WriteLine("--exit");
        }
    }
}
