using MarsCompetitionTask.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MarsCompetitionTask.Pages
{
    public class CertificationPage : CommonDriver
    {
        public static IWebElement addNewCertificationBtn => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/thead/tr/th[4]/div"));
        public static IWebElement certificationNm => driver.FindElement(By.Name("certificationName"));
        public static IWebElement certificationFrom => driver.FindElement(By.Name("certificationFrom"));
        public static IWebElement certificationYr => driver.FindElement(By.Name("certificationYear"));
        public static IWebElement addCertificationBtn => driver.FindElement(By.XPath("//input[@type='button'][@value='Add']"));

        public static IWebElement editCertificationButton => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/tbody/tr/td[4]/span[1]/i"));
        public static IWebElement editCertificationName => driver.FindElement(By.Name("certificationName"));
        public static IWebElement editCertificationFrom => driver.FindElement(By.Name("certificationFrom"));
        public static IWebElement editCertificationYr => driver.FindElement(By.Name("certificationYear"));
        public static IWebElement updateCertificationBtn => driver.FindElement(By.XPath("//input[@value='Update']"));
        public static IWebElement deleteCertification => driver.FindElement(By.CssSelector("i[class='remove icon']"));
        public static IWebElement popupMsg => driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
        string popUpMsg2 = "This information is already exist.";
        string popUpMsg3 = "Duplicated data";
        string popUpMsg4 = "Please enter Certification Name,Certification From, Certification Year";
        public void ClearData()
        {
            try
            {
                IWebElement deleteButton = driver.FindElement(By.CssSelector("i[class='remove icon']"));
                WaitUtils.WaitToBeClickable(driver, "CssSelector", "i[class='remove icon']", 100);
                var delButton = driver.FindElements(By.CssSelector("i[class='remove icon']"));

                foreach (var button in delButton)
                {
                    Thread.Sleep(100);
                    button.Click();

                }
            }
            catch (StaleElementReferenceException e)
            {

                IWebElement deleteButton = driver.FindElement(By.CssSelector("i[class='remove icon']"));
                WaitUtils.WaitToBeClickable(driver, "CssSelector", "i[class='remove icon']", 100);
                var delButton = driver.FindElements(By.CssSelector("i[class='remove icon']"));

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


        public string AddCertification(string certNM, string certFrm, string certYr)
        {
            //Adding Certification
            addNewCertificationBtn.Click();
            WaitUtils.WaitToBeClickable(driver, "Name", "certificationName", 100);
            certificationNm.SendKeys(certNM);
            certificationFrom.SendKeys(certFrm);
            certificationYr.Click();
            certificationYr.SendKeys(certYr);
            addCertificationBtn.Click();
            Thread.Sleep(2000);
            return certNM;
        }
        public string EditCertification(string certNM, string certFrm, string certYr)
        {
            //Edit Certification
            Thread.Sleep(2000);
            editCertificationButton.Click();
            editCertificationName.Clear();
            editCertificationName.SendKeys(certNM);
            editCertificationFrom.Clear();
            editCertificationFrom.SendKeys(certFrm);
            editCertificationYr.Click();
            editCertificationYr.SendKeys(certYr);
            updateCertificationBtn.Click();
            Thread.Sleep(2000);
            return certNM;

        }
        public string DeleteCertification(string cNm)
        {
            //Deleting Certification
            deleteCertification.Click();
            Thread.Sleep(3000);
            return cNm;
        }
    }
}
