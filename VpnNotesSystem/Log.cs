using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpnNotesSystem
{
    public class Log
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Action { get; set; }

        public DateTime CreatedAt { get; set; }
    }
    public interface ILogRepository
    {
        void AddLog(string username, string action);

        List<Log> GetLogs();
    }
}
