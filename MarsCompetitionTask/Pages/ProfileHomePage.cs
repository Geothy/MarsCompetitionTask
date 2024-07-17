using MarsCompetitionTask.Utilities;
using OpenQA.Selenium;

namespace MarsCompetitionTask.Pages
{
    public class ProfileHomePage : CommonDriver
    {
        private static IWebElement educationTab => driver.FindElement(By.XPath("//a[text()='Education']"));
        private static IWebElement certificationTab => driver.FindElement(By.XPath("//a[text()='Certifications']"));

        public void NavigateToEducationPanel()
        {
            WaitUtils.WaitToBeClickable(driver, "XPath", "//a[text()='Education']", 100);
            educationTab.Click();
            Thread.Sleep(3000);
        }
        public void NavigateToCerticationPanel()
        {
            certificationTab.Click();
            Thread.Sleep(3000);
        }
    }
}
