using VpnNotesSystem;

namespace VpnNotesSystem.Tests
{
    public class FakeUpdateRepository
        : IUpdateRepository
    {
        private readonly string _version;

        public FakeUpdateRepository(
            string version)
        {
            _version = version;
        }
        public string GetLatestVersion()
        {
            return _version;
        }
    }
}