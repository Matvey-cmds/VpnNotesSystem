using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpnNotesSystem
{
    internal class LogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public void AddLog(
            string username,
            string action)
        {
            _logRepository.AddLog(username, action);
        }

        public List<Log> GetLogs()
        {
            return _logRepository.GetLogs();
        }
    }
}
