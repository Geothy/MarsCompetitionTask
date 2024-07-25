using MarsCompetitionTask.Models;
using MarsCompetitionTask.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsCompetitionTask.Pages
{
    public class EducationPage : CommonDriver
    {
        private static IWebElement addNewEduBtn => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/thead/tr/th[6]/div"));
        private static IWebElement addInstName => driver.FindElement(By.Name("instituteName"));
        private static IWebElement addDegree => driver.FindElement(By.Name("degree"));
        private static IWebElement addCountry => driver.FindElement(By.Name("country"));
        private static IWebElement addTitle => driver.FindElement(By.Name("title"));
        private static IWebElement addYrOfGrdtn => driver.FindElement(By.Name("yearOfGraduation"));
        private static IWebElement addEduButton => driver.FindElement(By.XPath("//input[@value='Add']"));
        private static IWebElement editEducationButton => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody/tr/td[6]/span[1]/i"));
        private static IWebElement editInstName => driver.FindElement(By.Name("instituteName"));
        private static IWebElement editDegree => driver.FindElement(By.Name("degree"));
        private static IWebElement editCountry => driver.FindElement(By.Name("country"));
        private static IWebElement editTitle => driver.FindElement(By.Name("title"));
        private static IWebElement editYrOfGrdtn => driver.FindElement(By.Name("yearOfGraduation"));
        private static IWebElement updateEduButton => driver.FindElement(By.XPath("//input[@value='Update']"));
        // private static IWebElement deleteEducation => driver.FindElement(By.CssSelector("i[class='remove icon']"));
        private static IWebElement deleteEducation => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody/tr/td[6]/span[2]/i"));
        
        public void AddEducation(string country, string university, string title, string degree, string gradYr)
        {
            addNewEduBtn.Click();
            Thread.Sleep(1000);
            addInstName.SendKeys(university);
            addDegree.SendKeys(degree);
            addCountry.Click();
            addCountry.SendKeys(country);
            addTitle.Click();
            addTitle.SendKeys(title);
            addYrOfGrdtn.Click();
            addYrOfGrdtn.SendKeys(gradYr);
            addEduButton.Click();
            Thread.Sleep(2000);
        }
        public void EditEducation(string country, string university, string title, string degree, string gradYr)
        {
            Thread.Sleep(2000);
            editEducationButton.Click();
            editInstName.Clear();
            editInstName.SendKeys(university);
            editDegree.Clear();
            editDegree.SendKeys(degree);
            editCountry.Click();
            editCountry.SendKeys(country);
            editTitle.Click();
            editTitle.SendKeys(title);
            editYrOfGrdtn.Click();
            editYrOfGrdtn.SendKeys(gradYr);
            updateEduButton.Click();
        }
        public void DeleteEducation(string country, string university, string title, string degree, string gradYr)
        {            
            deleteEducation.Click();
            Thread.Sleep(3000);
        }
    }
}
