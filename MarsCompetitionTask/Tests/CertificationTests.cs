using AventStack.ExtentReports;
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
    [TestFixture]
    public class CertificationTests : CommonDriver
    {
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
            profileHomePageObj = new ProfileHomePage();
            certificationPageObj = new CertificationPage();
        }
        [Test, Order(1), Description("This test adds a new entry in Certification feature")]
        public void TestAddCertification()
        {
            profileHomePageObj.NavigateToCerticationPanel();
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
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var mediaEntity = CaptureScreenshot(TestContext.CurrentContext.Test.Name);
                if ((popupMsgBox == popUpMsg2) || (popupMsgBox == popUpMsg3) || (popupMsgBox == popUpMsg4))
                {
                    test.Log(Status.Info, "Entered Invalid Data", mediaEntity);
                    cancelButton.Click();
                }
                else if (popupMsgBox == certPop || popupMsgBox == popUpMsg1)
                {
                    test.Log(Status.Pass, "Valid Certification Data Entered", mediaEntity);
                }
                else
                {
                    test.Log(Status.Fail, "Test Failed" + TestContext.CurrentContext.Result.Message);
                }
                Thread.Sleep(1000);
            }
            var ssAddCertAfterTest = CaptureScreenshot(TestContext.CurrentContext.Test.Name);
            test.Log(Status.Pass, "Adding Certification Test Passed", ssAddCertAfterTest);
        }
        [Test, Order(2), Description("This test updates in Certification feature")]
        public void TestEditCertification()
        {
            TestAddCertification();
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
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var mediaEntity = CaptureScreenshot(TestContext.CurrentContext.Test.Name);
                if ((editedPopupMsgBox == popUpMsg2) || (editedPopupMsgBox == popUpMsg3) || (editedPopupMsgBox == popUpMsg4))
                {
                    test.Log(Status.Info, "Entered Invalid Data", mediaEntity);
                    cancelButton.Click();
                }
                else if (editedPopupMsgBox == editcertPop)
                {
                    test.Log(Status.Info, "Valid Certification Data Entered", mediaEntity);
                }
                else
                {
                    test.Log(Status.Fail, "Test Failed" + TestContext.CurrentContext.Result.Message);
                }
            }
            var ssEditCertAfterTest = CaptureScreenshot(TestContext.CurrentContext.Test.Name);
            test.Log(Status.Pass, "Editing Certification Test Passed", ssEditCertAfterTest);
        }
        [Test, Order(3), Description("This test deletes in Certification feature")]
        public void TestDeleteCertification()
        {
            TestAddCertification();
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
                string deleteCertPop = deleteCertName + " has been deleted from your certification";
                Assert.That(deletePopupMsgBox, Is.EqualTo(deleteCertPop).Or.EqualTo(popUpMsg6));
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var mediaEntity = CaptureScreenshot(TestContext.CurrentContext.Test.Name);
                if (deletePopupMsgBox == deleteCertPop || deletePopupMsgBox == popUpMsg6)
                {
                    test.Log(Status.Pass, "Deleted Certification", mediaEntity);
                }
                else
                {
                    test.Log(Status.Fail, "Test Failed" + TestContext.CurrentContext.Result.Message);
                }
            }
            var ssDeleteCertAfterTest = CaptureScreenshot(TestContext.CurrentContext.Test.Name);
            test.Log(Status.Pass, "Deleting Certification Test Passed", ssDeleteCertAfterTest);
        }
    }
}
