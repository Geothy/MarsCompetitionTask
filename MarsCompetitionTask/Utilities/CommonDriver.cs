using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using MarsCompetitionTask.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace MarsCompetitionTask.Utilities
{
    public class CommonDriver
    {
        public static IWebDriver driver;
        public static ExtentReports extent;
        public static ExtentTest test;
        public static LoginPage loginPageObj;
        public static ProfileHomePage profileHomePageObj;

        [OneTimeSetUp]
        public void ExtentReportSetup()
        {
            try
            {
                var htmlReporter = new ExtentHtmlReporter("C:\\GitProjects\\MarsCompetitionProj\\MarsCompetitionTask\\MarsCompetitionTask\\Reports\\");
                extent = new ExtentReports();
                extent.AttachReporter(htmlReporter);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        [SetUp]
        public void BrowserSetup()
        {
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            loginPageObj = new LoginPage();
            loginPageObj.LoginActions();
            CleanUp();
            var testName = TestContext.CurrentContext.Test.Name;
            test = extent.CreateTest(testName);
        }

        public void CleanUp()
        {
            profileHomePageObj = new ProfileHomePage();
            profileHomePageObj.NavigateToEducationPanel();
            profileHomePageObj.ClearData();
            profileHomePageObj.NavigateToCerticationPanel();
            profileHomePageObj.ClearCertData();
        }
        public void ClearData()
        {
            try
            {
                var delEButton = driver.FindElements(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody/tr/td[6]/span[2]/i"));
                foreach (var button in delEButton)
                {
                    Thread.Sleep(100);
                    button.Click();
                }
                Thread.Sleep(100);
            }

            catch (StaleElementReferenceException e)
            {
                var delButton = driver.FindElements(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody/tr/td[6]/span[2]/i"));
                foreach (var button in delButton)
                {
                    Thread.Sleep(100);
                    button.Click();
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Nothing to delete");
            }
        }
        [TearDown]
        public void CloseTestrun()
        {
            driver.Quit();
        }

        public MediaEntityModelProvider CaptureScreenshot(string screenShotName)
        {
            var screenShot = ((ITakesScreenshot)driver).GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenShot, screenShotName).Build();
        }
        [OneTimeTearDown]
        public void TeardownReport()
        {
            extent.Flush();
        }
    }
}

