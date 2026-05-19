using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;

namespace VpnNotesSystem.Tests
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        public void DatabaseConnection_ShouldOpen()
        {
            // Arrange
            string connectionString = "Host=localhost;" + "Port=5432;" + "Username=postgres;" +  "Password=111;" + "Database=postgres";
            // Act
            bool connected = false;
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    connected = conn.State == System.Data.ConnectionState.Open;
                }
            }
            catch
            {
                connected = false;
            }
            // Assert
            Assert.IsTrue(connected);
        }
        [TestMethod]
        public void DatabaseQuery_ShouldReturnValue()
        {
            // Arrange
            string connectionString = "Host=localhost;" + "Port=5432;" +  "Username=postgres;" +  "Password=111;" +  "Database=postgres";
            int result = 0;
            // Act
            using (var conn =
                new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd =  new NpgsqlCommand( "SELECT 1", conn))
                {
                    result = (int)cmd.ExecuteScalar();
                }
            }
            // Assert
            Assert.AreEqual(1, result);
        }
    }
}