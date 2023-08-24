using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using System.Diagnostics;

namespace SqlValidator.Tests
{
    [TestClass]
    public class SqlValidationTests
    {
        SqlValidator validator = new SqlValidator();
        public SqlValidationTests() { }


        [TestMethod]
        [DataRow("SELECT * INTO CustomersBackup2017 FROM Customers;",false)]
        [DataRow("Select * from dbo.users", true)]
        [DataRow("Select username,1 from dbo.users union Select username,2 from dbo.users where enabled = 0 ", true)]
        [DataRow("not a sql statement", false)]
        [DataRow("select DROP from users", false)]
        [DataRow("DROP table users", false)]
        [DataRow("truncate table users", false)]
        [DataRow("select * from (select * from users) u", true)]
        [DataRow("select test from (insert into dbo.Students)", false)]
        [DataRow("WITH Employee_CTE (EmployeeNumber, Title) AS (SELECT NationalIDNumber, JobTitle FROM HumanResources.Employee) SELECT EmployeeNumber, Title FROM Employee_CTE",true)]
        public void IsValidSqlSelectQuery(string sqlQuery, bool isValid)
        {
            var result = validator.IsValidSelectStatement(sqlQuery);

            Assert.AreEqual(isValid, result.IsSuccess);
            if (!isValid)
            {
                Assert.AreNotEqual(0, result.Errors.Count);
                Debug.WriteLine(@$"{sqlQuery}:{Environment.NewLine}{string.Join(Environment.NewLine, result.Errors)}");
            }
        }
    }
}