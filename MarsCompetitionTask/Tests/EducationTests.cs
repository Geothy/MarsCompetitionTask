using MarsCompetitionTask.Pages;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MarsCompetitionTask.Utilities;
using MarsCompetitionTask.Models;
using System.Collections.Immutable;
using System.Collections.Frozen;
using System.Net;
using Newtonsoft.Json;

namespace MarsCompetitionTask.Tests
{
    public class EducationTests : CommonDriver
    {
        LoginPage loginPageObj;
        ProfileHomePage profileHomePageObj;
        EducationPage educationPageObj;
        private static IWebElement popupMsg => driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
        private static IWebElement cancelButton => driver.FindElement(By.XPath("//input[@value='Cancel']"));
        string popUpMsg1 = "Education has been added";
        string popUpMsg2 = "This information is already exist.";
        string popUpMsg3 = "Duplicated data";
        string popUpMsg4 = "Please enter all the fields";
        string popUpMsg5 = "Education as been updated";
        string popUpMsg6 = "Education information was invalid";
        string popUpMsg7 = "Education entry successfully removed";
       

        public EducationTests()
        {
            loginPageObj = new LoginPage();
            profileHomePageObj = new ProfileHomePage();
            educationPageObj = new EducationPage();
        }

        [SetUp]
        public void SetupEducation()
        {
            BrowserSetup();
            loginPageObj.LoginActions(driver, "geothy@gmail.com", "7geothy*");
            profileHomePageObj.VerifyLoggedInUser(driver);
            profileHomePageObj.NavigateToEducationPanel();

        }
        [Test, Order(1), Description("This test adds a new entry in education feature")]
        public void TestAddEducation()
        {
            educationPageObj.ClearData();
            string addEduFile = "AddEducationData.json";
            List<EducationModel> AddEduData = JsonUtil.ReadJsonData<EducationModel>(addEduFile);
            foreach (var item in AddEduData)
            {
                string country = item.Country;
                string university = item.University;
                string title = item.Title;
                string degree = item.Degree;
                string gradYr = item.GraduationYear;
                educationPageObj.AddEducation(country, university, title, degree, gradYr);
                WaitUtils.WaitToBeVisible(driver, "XPath", "//div[@class='ns-box-inner']", 1000);
                //Verifying Education added successfully
                string popupMsgBox = popupMsg.Text;
                Console.WriteLine(popupMsgBox);
                Assert.That(popupMsgBox, Is.EqualTo(popUpMsg1).Or.EqualTo(popUpMsg2).Or.EqualTo(popUpMsg3).Or.EqualTo(popUpMsg4).Or.EqualTo(popUpMsg6));
                if ((popupMsgBox == popUpMsg2) || (popupMsgBox == popUpMsg3) || (popupMsgBox == popUpMsg4) || (popupMsgBox == popUpMsg6))
                {
                    cancelButton.Click();

                }
                Thread.Sleep(3000);
            }
        }
        [Test, Order(2), Description("This test updates in education feature")]
        public void TestEditEducation()
        {
             string editEduFile = "EditEducationData.json";
             List<EducationModel> EditEduData = JsonUtil.ReadJsonData<EducationModel>(editEduFile);

                    /*
                        string link = @"C:\GitProjects\MarsCompetitionProj\MarsCompetitionTask\MarsCompetitionTask\bin\Debug\net8.0\JsonData\EditEducationData.json";
                         WebRequest request = WebRequest.Create(link);
                         WebResponse response= request.GetResponse();
                          using (Stream datastream = response.GetResponseStream())
                          {
                            StreamReader reader = new StreamReader(datastream);
                          string responsefromserver = reader.ReadToEnd();
                            List<EducationModel>EditEduData = JsonConvert.DeserializeObject<List<EducationModel>>(responsefromserver);*/
                foreach (var item in EditEduData)
                {
                    string country = item.Country;
                    string university = item.University;
                    string title = item.Title;
                    string degree = item.Degree;
                    string gradYr = item.GraduationYear;
                    educationPageObj.EditEducation(country, university, title, degree, gradYr);
                    WaitUtils.WaitToBeVisible(driver, "XPath", "//div[@class='ns-box-inner']", 1000);
                    //Verifying Education updated successfully
                    string editpopupMsgBox = popupMsg.Text;
                    Console.WriteLine(editpopupMsgBox);
                    Assert.That(editpopupMsgBox, Is.EqualTo(popUpMsg5).Or.EqualTo(popUpMsg6).Or.EqualTo(popUpMsg2).Or.EqualTo(popUpMsg3).Or.EqualTo(popUpMsg4));
                    if ((editpopupMsgBox == popUpMsg2) || (editpopupMsgBox == popUpMsg3) || (editpopupMsgBox == popUpMsg4) || (editpopupMsgBox == popUpMsg6))
                    {
                        cancelButton.Click();
                    }
                    Thread.Sleep(3000);
                }
            
        }
        [Test, Order(3), Description("This test deletes in education feature")]
        public void TestDeleteEducation()
        {
            string deleteEduFile = "DeleteEducationData.json";
            List<EducationModel> DeleteEduData = JsonUtil.ReadJsonData<EducationModel>(deleteEduFile);
            foreach (var item in DeleteEduData)
            {
                string country = item.Country;
                string university = item.University;
                string title = item.Title;
                string degree = item.Degree;
                string gradYr = item.GraduationYear;
                educationPageObj.DeleteEducation(country, university, title, degree, gradYr);

                WaitUtils.WaitToBeVisible(driver, "XPath", "//div[@class='ns-box-inner']", 100);
                //Verifying Education updated successfully
                string deletepopupMsgBox = popupMsg.Text;
                Console.WriteLine(deletepopupMsgBox);
                Assert.That(deletepopupMsgBox, Is.EqualTo(popUpMsg7));
            }
                          
        }
        [TearDown]

        public void CloseTestrun()
        {
            driver.Quit();

        }

    }
}
