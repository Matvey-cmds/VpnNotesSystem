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

        public CommandHandler(AuthService authService)
        {
            _authService = authService;
        }

        public void Handle(string[] args)
        {
            // 👉 если аргументов нет — обычный вход
            if (args.Length == 0)
            {
                RunInteractiveLogin();
                return;
            }

            // 👉 если есть аргументы — CLI режим
            if (args[0] == "--login")
            {
                RunInteractiveLogin();
            }
            else if (args[0] == "--help")
            {
                ShowHelp();
            }
            else
            {
                Console.WriteLine("Неизвестная команда");
            }
        }

        private void RunInteractiveLogin()
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

        private void ShowHelp()
        {
            Console.WriteLine("Доступные команды:");
            Console.WriteLine("--login   Вход в систему");
            Console.WriteLine("--help    Помощь");
        }
    }
}
