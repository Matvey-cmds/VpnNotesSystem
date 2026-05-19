using Microsoft.VisualStudio.TestTools.UnitTesting;
using VpnNotesSystem;

namespace VpnNotesSystem.Tests
{
    [TestClass]
    public class UpdateServiceTests
    {
        [TestMethod]
        [DataRow("1.0", true)]
        [DataRow("1.1", false)]
        [DataRow("2.0", true)]
        public void HasUpdate_CheckVersions(string latestVersion, bool expectedResult)
        {
            // Arrange
            IUpdateRepository repository =  new FakeUpdateRepository(latestVersion);
            UpdateService service = new UpdateService(repository);
            // Act
            bool result = service.HasUpdate();
            // Assert
            Assert.AreEqual(
                expectedResult,
                result);
        }
    }
}