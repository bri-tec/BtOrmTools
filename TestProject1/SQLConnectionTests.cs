
namespace TestProject1
{
    [TestFixture("HP-17\\SQLEXPRESS2022")]
    public class SQLConnectionTests
    {
        private IDatabaseConnection _dbConn;
        private string sqlServerAddress;

        public SQLConnectionTests(string SqlServerAddress)
        {
            sqlServerAddress = SqlServerAddress;
            _dbConn = new DatabaseConnection(sqlServerAddress, "", "", "");
        }

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void CheckDatabaseConnectionStringWithINVALIDConnectionInfo()
        {
            try
            {
                _dbConn = new DatabaseConnection("CallerDatabase", "", "", "");
                if (_dbConn.VerifyConnectionInfo())
                    Assert.Fail();
            }
            catch
            {
                Assert.Pass();
            }
        }

        [Test]
        public void CheckDatabaseConnectionStringWithVALIDConnectionInfo()
        {            
            _dbConn = new DatabaseConnection(sqlServerAddress, "CallerDatabase", "tester", "tester123");

            if (_dbConn.VerifyConnectionInfo())
                Assert.Pass($"Connection to {_dbConn.ConnectionString} open and closed sucessfully.");
        }

    }
}