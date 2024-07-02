using MarsCompetitionTask.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsCompetitionTask.Pages
{
    public class ProfileHomePage:CommonDriver
    {
        private static IWebElement educationTab => driver.FindElement(By.XPath("//a[text()='Education']"));
        private static IWebElement certificationTab => driver.FindElement(By.XPath("//a[text()='Certifications']"));
       
     
        public void NavigateToEducationPanel()
        {            
            educationTab.Click();
            Thread.Sleep(3000);
        }
        public void NavigateToCerticationPanel()
        {

            certificationTab.Click();
            Thread.Sleep(3000);
            
        }
        public void VerifyLoggedInUser(IWebDriver driver)
        {
            //Check if loggedin successfully
            WaitUtils.WaitToBeVisible(driver, "XPath", "//span[contains(text(),'Hi')]", 1000);
            IWebElement checkUser = driver.FindElement(By.XPath("//span[contains(text(),'Hi')]"));
           
            if (checkUser.Text == "Hi Geothy")
            {

                Console.WriteLine("Logged in");

            }
            else
            {
                Console.WriteLine("Not Logged in");
            }
        }
    }
}
