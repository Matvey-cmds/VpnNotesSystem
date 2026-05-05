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

        public CommandHandler(AuthService authService, NoteService noteService)
        {
            _authService = authService;
            _noteService = noteService;
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
