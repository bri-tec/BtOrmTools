using Loggers;

namespace TestProject1
{
    internal class LoggerTests
    {
        [Test]
        public void CheckConsoleLoggerWorks()
        {
            ILogger logger = new ConsoleLogger();
            if (!logger.Log(DateTime.Now, "Test Log", this))
                Assert.Fail();
        }

        [Test]
        public void CheckSQLLoggerWorks()
        {
            ILogger logger = new SQLLogger(new DatabaseConnection("HP-17\\SQLEXPRESS2022", "CallerDatabase", "tester", "tester123"));
            if (!logger.Log(DateTime.Now, "Test Log", this))
                Assert.Fail();
        }
    }
}
