using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

public class CsvDataReader
{
    public static IEnumerable<TestCaseData> GetTestDataLog_In()
    {
        var csvFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "DataTestLogin.csv");
        using (var reader = new StreamReader(csvFilePath))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            var records = csv.GetRecords<TestDataLogIn>();
            foreach (var record in records)
            {
                yield return new TestCaseData(record.username, record.password, record.expectedResult);
            }
        }
    }

    public static IEnumerable<TestCaseData> GetTestDataSign_In()
    {
        var csvFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "DataTestSignIn.csv");
        using (var reader = new StreamReader(csvFilePath))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            var records = csv.GetRecords<TestDataSignIn>();
            foreach (var record in records)
            {
                yield return new TestCaseData(record.name, record.username,
                    record.password, record.email, record.phoneNumber,
                    record.checkBox);
            }
        }
    }
}

public class TestDataSignIn
{
    public string name { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string email { get; set; }
    public string phoneNumber { get; set; }
    public string checkBox { get; set; }
}

public class TestDataLogIn
{
    public string username { get; set; }
    public string password { get; set; }
    public string expectedResult { get; set; }
}
