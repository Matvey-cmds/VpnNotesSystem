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
        private readonly LogService _logService;
        private readonly UpdateService _updateService;
        public CommandHandler(AuthService authService,NoteService noteService,UserService userService,LogService logService,UpdateService updateService)
        {
            _authService = authService;
            _noteService = noteService;
            _userService = userService;
            _logService = logService;
            _updateService = updateService;
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
                _logService.AddLog(UserSession.CurrentUser,"Viewed all notes");
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

                _logService.AddLog(
                    UserSession.CurrentUser,
                    "Blocked user: " + username);

                Console.WriteLine("Пользователь заблокирован");
            }
            else if (args[0] == "--logout")
            {
                if (UserSession.CurrentUser == null)
                {
                    Console.WriteLine("Вы не вошли");
                    return;
                }
                _logService.AddLog(
                    UserSession.CurrentUser,
                    "User logged out");
                OnlineManager.OnlineUsers.Remove(
                    UserSession.CurrentUser);

                Console.WriteLine("Выход выполнен");

                UserSession.CurrentUser = null;
                UserSession.CurrentRole = null;
            }
            else if (args[0] == "--onlineUsers")
            {
                if (UserSession.CurrentRole != "admin")
                {
                    Console.WriteLine("Нет прав");
                    return;
                }

                if (OnlineManager.OnlineUsers.Count == 0)
                {
                    Console.WriteLine("Нет пользователей онлайн");
                    return;
                }

                foreach (var user in OnlineManager.OnlineUsers)
                {
                    Console.WriteLine(user);
                }
            }
            else if (args[0] == "--watchersCount")
            {
                if (UserSession.CurrentRole != "admin")
                {
                    Console.WriteLine("Нет прав");
                    return;
                }

                Console.WriteLine(
                    "Активных подключений: " +
                    OnlineManager.OnlineUsers.Count);
            }
            else if (args[0] == "--logs")
            {
                if (UserSession.CurrentRole != "admin")
                {
                    Console.WriteLine("Нет прав");
                    return;
                }

                var logs = _logService.GetLogs();

                if (logs.Count == 0)
                {
                    Console.WriteLine("Логи отсутствуют");
                    return;
                }

                foreach (var log in logs)
                {
                    Console.WriteLine(
                        log.Id + " | " +
                        log.Username + " | " +
                        log.Action + " | " +
                        log.CreatedAt);
                }
            }
            else if (args[0] == "--checkUpdates")
            {
                bool hasUpdate =
                    _updateService.HasUpdate();

                if (hasUpdate)
                {
                    Console.WriteLine(
                        "Доступно обновление");
                }
                else
                {
                    Console.WriteLine(
                        "Установлена актуальная версия");
                }
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
            {
                Console.WriteLine("Успешный вход");

                _logService.AddLog(
                    username,
                    "User logged in");
            }
            else
            {
                Console.WriteLine("Ошибка авторизации");
            }
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

            _logService.AddLog(
                UserSession.CurrentUser,
                "Added new note");

            Console.WriteLine("Заметка добавлена"); ;
        }

        private void ShowHelp()
        {
            Console.WriteLine("Доступные команды:");

            Console.WriteLine(
                "--login : вход в систему");

            Console.WriteLine(
                "--help : список команд");

            Console.WriteLine(
                "--exit : выход из программы");
            Console.WriteLine(
                "--checkUpdates : проверка обновлений");

            if (UserSession.CurrentUser != null)
            {
                Console.WriteLine(
                    "--logout : выход из аккаунта");

                Console.WriteLine(
                    "--addNewNote <text> : добавить заметку");

                Console.WriteLine(
                    "--myNotes : мои заметки");
            }

            if (UserSession.CurrentRole == "admin")
            {
                Console.WriteLine(
                    "--allNotes : все заметки пользователей");

                Console.WriteLine(
                    "--blockUser <username> : блокировка пользователя");

                Console.WriteLine(
                    "--onlineUsers : список пользователей онлайн");

                Console.WriteLine(
                    "--watchersCount : количество активных подключений");

                Console.WriteLine(
                    "--logs : просмотр логов системы");
            }
        }
    }
}
