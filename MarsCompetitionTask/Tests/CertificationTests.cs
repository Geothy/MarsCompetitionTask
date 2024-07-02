using MarsCompetitionTask.Models;
using MarsCompetitionTask.Pages;
using MarsCompetitionTask.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsCompetitionTask.Tests
{
    public class CertificationTests:CommonDriver
    {
        LoginPage loginPageObj;
        ProfileHomePage profileHomePageObj;
        CertificationPage certificationPageObj;

        private static IWebElement popupMsg => driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
        private static IWebElement cancelButton => driver.FindElement(By.XPath("//input[@value='Cancel']"));
        string popUpMsg1 = "has been added to your certification";
        string popUpMsg2 = "This information is already exist.";
        string popUpMsg3 = "Duplicated data";
        string popUpMsg4 = "Please enter Certification Name, Certification From and Certification Year";
        string popUpMsg5 = "has been updated to your certification";
        
        string popUpMsg6 = "has been deleted from your certification";



        public CertificationTests()
        {
            loginPageObj = new LoginPage();
            profileHomePageObj = new ProfileHomePage();
            certificationPageObj = new CertificationPage();
        }

        [SetUp]
        public void SetupEducation()
        {
            BrowserSetup();
            loginPageObj.LoginActions(driver, "geothy@gmail.com", "7geothy*");
            profileHomePageObj.VerifyLoggedInUser(driver);
            profileHomePageObj.NavigateToCerticationPanel();

        }
        [Test, Order(1), Description("This test adds a new entry in education feature")]
        public void TestAddCertification()
        {
            certificationPageObj.ClearData();
            string addCertFile = "AddCertificationData.json";
            List<CertificationModel> AddCertData = JsonUtil.ReadJsonData<CertificationModel>(addCertFile);
            foreach (var item in AddCertData)
            {
                string certificationName = item.CertificationName;
                string certificationFrom = item.CertificationFrom;
                string certificationYr = item.CertificationYear;

                string cnm = certificationPageObj.AddCertification(certificationName, certificationFrom, certificationYr);
                string certPop = cnm + " has been added to your certification";
                string popupMsgBox = popupMsg.Text;
                Console.WriteLine(popupMsgBox);
                Assert.That(popupMsgBox, Is.EqualTo(certPop).Or.EqualTo(popUpMsg1).Or.EqualTo(popUpMsg2).Or.EqualTo(popUpMsg3).Or.EqualTo(popUpMsg4));
                if ((popupMsgBox == popUpMsg2) || (popupMsgBox == popUpMsg3) || (popupMsgBox == popUpMsg4))
                {
                    cancelButton.Click();

                }
            }
        }
        [Test, Order(2), Description("This test updates in education feature")]
        public void TestEditCertification()
        {
            string editCertFile = "EditCertificationData.json";
            List<CertificationModel> EditCertData = JsonUtil.ReadJsonData<CertificationModel>(editCertFile);
            foreach (var item in EditCertData)
            {
                string certificationName = item.CertificationName;
                string certificationFrom = item.CertificationFrom;
                string certificationYr = item.CertificationYear;
                string edtcnm = certificationPageObj.EditCertification(certificationName, certificationFrom, certificationYr);
                //Verifying Education added successfully
                string editedPopupMsgBox = popupMsg.Text;
                string editcertPop = edtcnm + " has been updated to your certification";
                Console.WriteLine(editedPopupMsgBox);
                Assert.That(editedPopupMsgBox, Is.EqualTo(editcertPop).Or.EqualTo(popUpMsg2).Or.EqualTo(popUpMsg3).Or.EqualTo(popUpMsg4).Or.EqualTo(popUpMsg5));
                if ((editedPopupMsgBox == popUpMsg2) || (editedPopupMsgBox == popUpMsg3) || (editedPopupMsgBox == popUpMsg4))
                {
                    cancelButton.Click();

                }
            }
        }
        [Test, Order(3), Description("This test deletes in education feature")]
        public void TestDeleteCertification()
        {
            string deleteCertFile = "DeleteCertificationData.json";
            List<CertificationModel> DeleteCertData = JsonUtil.ReadJsonData<CertificationModel>(deleteCertFile);
            foreach (var item in DeleteCertData)
            {
                string certificationName = item.CertificationName;
                string certificationFrom = item.CertificationFrom;
                string certificationYr = item.CertificationYear;
                string deleteCertName = certificationPageObj.DeleteCertification(certificationName);
                string deletePopupMsgBox = popupMsg.Text;
                Console.WriteLine(deletePopupMsgBox);
                // WaitUtils.WaitToBeVisible(driver,"Name", "certificationName", 10);
                string deleteCertPop = deleteCertName + " has been deleted from your certification";
                Assert.That(deletePopupMsgBox, Is.EqualTo(deleteCertPop).Or.EqualTo(popUpMsg6));
            }

        }
        [TearDown]

        public void CloseTestrun()
        {
            driver.Quit();

        }
    }
}
