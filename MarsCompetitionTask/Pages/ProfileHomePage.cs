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
            WaitUtils.WaitToBeClickable(driver, "XPath", "//a[text()='Certifications']", 100);
            certificationTab.Click();
            Thread.Sleep(3000);
        }
        public void ClearCertData()
        {
            try
            {
                var delButton = driver.FindElements(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/tbody/tr/td[4]/span[2]/i"));
                foreach (var button in delButton)
                {
                    Thread.Sleep(100);
                    button.Click();
                }
                Thread.Sleep(100);
            }

            catch (StaleElementReferenceException e)
            {
                var delButton = driver.FindElements(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/tbody/tr/td[4]/span[2]/i"));
                foreach (var button1 in delButton)
                {
                    Thread.Sleep(100);
                    button1.Click();
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Nothing to delete");
            }
        }
    }
}
