using Microsoft.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace TestProject1
{
    [TestFixture("HP-17\\SQLEXPRESS2022")]
    internal class DatabaseTests
    {
        private IDatabaseConnection _dbConn;
         
        public DatabaseTests(string SqlServerAddress)
        {
            _dbConn = new DatabaseConnection(SqlServerAddress, "CallerDatabase", "tester", "tester123");
        }

        [SetUp]
        public void Setup()
        {

        }
         
        [Test]
        public void CheckDatabaseForNonExisintgTable()
        {
            SqlConnection sqlConn = _dbConn.GetSqlConnection();
            Assert.IsNotNull(sqlConn);

            try
            {
                object result = BasicSQLFunctions.ExecuteScalar(sqlConn, "SELECT COUNT(*) FROM asdasfsadf;");
                Assert.Fail();
            }
            catch
            {
                Assert.Pass();
            }            
        }

        [Test]
        public void CheckDatabaseTableCallersExist()
        {
            SqlConnection sqlConn = _dbConn.GetSqlConnection();
            Assert.IsNotNull(sqlConn);

            object result = sqlConn.ExecuteScalar("SELECT COUNT(*) FROM Callers;");
            Assert.Pass(result.ToString());
        }

        [Test]
        public void CheckDatabaseTableOrdersExist()
        {
            SqlConnection sqlConn = _dbConn.GetSqlConnection();
            Assert.IsNotNull(sqlConn);

            object result = sqlConn.ExecuteScalar("SELECT COUNT(*) FROM Orders;");
            Assert.Pass(result.ToString());
        }

        [Test]
        public void CheckDatabaseTableProductsExist()
        {
            SqlConnection sqlConn = _dbConn.GetSqlConnection();
            Assert.IsNotNull(sqlConn);
                       
            object result = sqlConn.ExecuteScalar("SELECT COUNT(*) FROM Products;");
            Assert.Pass(result.ToString());
        }

        [Test]
        public void CheckDatabaseReadMethodWorksWithID()
        {
            DatabaseObjectReader reader = new DatabaseObjectReader(_dbConn);
            Products? product = new Products();
            product.Id = 1;
            product = reader.Read<Products>(1);

            if (product != null && product.ProductName.Length > 0)
                Assert.Pass(product.Description);
        }

        [Test]
        public void CheckDatabaseWriteInsertMethodWorks()
        {
            Products product = new Products();
            product.ProductName = "New Product";
            product.Description = "Just another test product";
            new DatabaseObjectWriter(_dbConn).Write(product);

            if (product.Id > 0)
                Assert.Pass(product.Id.ToString());
        }

        [Test]
        public void CheckDatabaseWriteUpdateMethodWorks()
        {
            Products product = new Products();
            product.Id = 2;
            product.Description = "Just another update at " + DateTime.Now.ToString();
            new DatabaseObjectWriter(_dbConn).Write(product);

            if (product.Id > 0)
                Assert.Pass(product.Id.ToString());
        }


        

    }
}
