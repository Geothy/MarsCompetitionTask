using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsCompetitionTask.Utilities
{
    public class WaitUtils
    {

        public static void WaitToBeClickable(IWebDriver driver, string locatorType, string locatorValue, int seconds)
        {
            By by = GetBy(locatorType, locatorValue);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.Until(ExpectedConditions.ElementToBeClickable(by));
           
        }

        public static void WaitToBeVisible(IWebDriver driver, string locatorType, string locatorValue, int seconds)
        {
            By by = GetBy(locatorType, locatorValue);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.Until(ExpectedConditions.ElementIsVisible(by));
        }
        public static void WaitTillAlertIsPresent(IWebDriver driver, string locatorType, string locatorValue, int seconds)
        {
            By by = GetBy(locatorType, locatorValue);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.Until(ExpectedConditions.AlertIsPresent());

        }
        private static By GetBy(string locatorType, string locatorValue)
        {
            switch (locatorType.ToLower())
            {
                case "xpath":
                    return By.XPath(locatorValue);
                case "id":
                    return By.Id(locatorValue);
                case "cssselector":
                    return By.CssSelector(locatorValue);
                case "name":
                    return By.Name(locatorValue);
                case "classname":
                    return By.Name(locatorValue);

                default:
                    throw new ArgumentException($"Unsupported locator type: {locatorType}");
            }
        }
    }
}
