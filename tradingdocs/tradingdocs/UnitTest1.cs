using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.IO;
using System;
using MongoDB.Driver;
using MongoDB.Bson;

namespace tradingdocs
{
    public class Tests
    {
        private IWebDriver webDriver;

        [SetUp]
        public void Setup()
        {
            webDriver = new ChromeDriver();
        }
        
        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
        }

        /*
        [Test, TestCaseSource(typeof(CsvDataReader), nameof(CsvDataReader.GetTestDataLog_In))]
        public void Test_SignIn(String username, String password, String expectedResult)
        {
            webDriver.Navigate().GoToUrl("http://localhost:3001/sign-in");
            IWebElement usernameField = webDriver.FindElement(By.Id("username"));
            usernameField.SendKeys(username);
            IWebElement passwordField = webDriver.FindElement(By.Id("password"));
            passwordField.SendKeys(password);
            IWebElement btn_login = webDriver.FindElement(By.XPath("/html/body/div/main/div/form/button"));
            btn_login.Click();
            Thread.Sleep(2000);

            if (expectedResult == "success")
            {
                IWebElement isLogin = webDriver.FindElement(By.XPath("/html/body/div[1]/div[1]/header/div/div/div[2]/button/div/div/span"));
                Assert.AreEqual(isLogin.Text.ToString(), username);
            }
            else
            {
                bool loginPage = webDriver.Url.Contains("http://localhost:3001/sign-in");
                Assert.IsTrue(loginPage);
            }
        } */

        [Test, TestCaseSource(typeof(CsvDataReader), nameof(CsvDataReader.GetTestDataSign_In))]
        public void TestSignUp(String name, String username, String password, String email, String phoneNumber, String checkBox)
        {
            webDriver.Navigate().GoToUrl("http://localhost:3001/sign-up");
            IWebElement nameField = webDriver.FindElement(By.Id(":r0:"));
            nameField.SendKeys(name);

            IWebElement usernameField = webDriver.FindElement(By.Id(":r1:"));
            usernameField.SendKeys(username);

            IWebElement passwordField = webDriver.FindElement(By.Id(":r2:"));
            passwordField.SendKeys(password);

            IWebElement emailField = webDriver.FindElement(By.Id(":r3:"));
            emailField.SendKeys(email);

            IWebElement phoneNumberField = webDriver.FindElement(By.Id(":r4:"));
            phoneNumberField.SendKeys(phoneNumber);

            if(checkBox == "male")
            {
                IWebElement checkboxMaleField = webDriver.FindElement(By.XPath("/html/body/div/div[2]/div[2]/div/form/div/div[6]/label[1]/span[1]/input"));
                checkboxMaleField.Click();
            }
            else if(checkBox == "female")
            {
                IWebElement checkboxFemaleField = webDriver.FindElement(By.XPath("/html/body/div/div[2]/div[2]/div/form/div/div[6]/label[2]/span[1]/input"));
                checkboxFemaleField.Click();
            }

            IWebElement btn_Submit = webDriver.FindElement(By.XPath("/html/body/div/div[2]/div[2]/div/form/div/div[7]/button"));
            btn_Submit.Click();
            Thread.Sleep(2000);

            //kiểm tra username và email có tồn tại trong db
            string mongoConnectionString = "mongodb://localhost:27017";
            string databaseName = "tradingdocs";
            var client = new MongoClient(mongoConnectionString);
            var db = client.GetDatabase("databaseName");
            var collection = db.GetCollection<BsonDocument>("users");

            var emailFilter = Builders<BsonDocument>.Filter.Eq("email", email);
            var usernameFilter = Builders<BsonDocument>.Filter.Eq("username", username);

            var emailExists = collection.Find(emailFilter).Any();
            var usernameExists = collection.Find(usernameFilter).Any();

            if(emailExists)
            {
                Console.WriteLine("Test Failed: Email exist");
            }    
            else
            {
                Console.WriteLine("Test pass: Email not exist");
            }    

            if(usernameExists)
            {
                Console.WriteLine("Test Failed: username exist");
            }
            else
            {
                Console.WriteLine("Test pass: username not exist");
            }
        }
    }
}