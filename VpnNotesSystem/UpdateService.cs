using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpnNotesSystem
{
    public class UpdateService
    {
        private readonly IUpdateRepository _updateRepository;

        private readonly string _currentVersion = "1.1";

        public UpdateService(
            IUpdateRepository updateRepository)
        {
            _updateRepository = updateRepository;
        }

        public bool HasUpdate()
        {
            string latestVersion =
                _updateRepository.GetLatestVersion();

            return latestVersion != _currentVersion;
        }

        public string GetCurrentVersion()
        {
            return _currentVersion;
        }
    }
    public interface IUpdateRepository
    {
        string GetLatestVersion();
    }
}
