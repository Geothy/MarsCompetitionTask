using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using MarsCompetitionTask.Models;
using MarsCompetitionTask.Pages;
using MarsCompetitionTask.Utilities;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V125.HeadlessExperimental;
using RazorEngine.Compilation.ImpromptuInterface;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Runtime.Intrinsics.X86;

namespace MarsCompetitionTask.Tests
{
    [TestFixture]
    public class EducationTests : CommonDriver
    {
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
            educationPageObj = new EducationPage();
            profileHomePageObj = new ProfileHomePage();
        }
        [Test, Order(1), Description("This test adds a new entry in education feature")]
        public void TestAddEducation()
        {
            profileHomePageObj.NavigateToEducationPanel();
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
                Thread.Sleep(1000);
                string popupMsgBox = popupMsg.Text;
                Console.WriteLine(popupMsgBox);
                Assert.That(popupMsgBox, Is.EqualTo(popUpMsg1).Or.EqualTo(popUpMsg2).Or.EqualTo(popUpMsg3).Or.EqualTo(popUpMsg4).Or.EqualTo(popUpMsg6));
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var mediaEntity = CaptureScreenshot(TestContext.CurrentContext.Test.Name);
                if ((popupMsgBox == popUpMsg2) || (popupMsgBox == popUpMsg3) || (popupMsgBox == popUpMsg4) || (popupMsgBox == popUpMsg6))
                {
                    test.Log(Status.Info, "Invalid Data Entered", mediaEntity);
                    cancelButton.Click();
                }
                else if (popupMsgBox == popUpMsg1)
                {
                    test.Log(Status.Pass, "Valid Education Data Entered", mediaEntity);
                }
                else
                {
                    test.Log(Status.Fail, "Test Failed:{TestContext.CurrentContext.Result.Message}");
                }
                Thread.Sleep(1000);
            }
            var ssAddAfterTest = CaptureScreenshot(TestContext.CurrentContext.Test.Name);
            test.Log(Status.Pass, "Adding Education Test Passed", ssAddAfterTest);
        }

        [Test, Order(2), Description("This test updates in education feature")]
        public void TestEditEducation()
        {
            TestAddEducation();
            string editEduFile = "EditEducationData.json";
            List<EducationModel> EditEduData = JsonUtil.ReadJsonData<EducationModel>(editEduFile);
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
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var mediaEntity = CaptureScreenshot(TestContext.CurrentContext.Test.Name);
                if ((editpopupMsgBox == popUpMsg2) || (editpopupMsgBox == popUpMsg3) || (editpopupMsgBox == popUpMsg4) || (editpopupMsgBox == popUpMsg6))
                {
                    test.Log(Status.Info, "Entered Invalid Data", mediaEntity);
                    cancelButton.Click();
                }
                else if (editpopupMsgBox == popUpMsg5)
                {
                    test.Log(Status.Pass, "Valid Data Entered", mediaEntity);
                }
                else
                {
                    test.Log(Status.Fail, "Test Failed:{TestContext.CurrentContext.Result.Message}");
                }
                Thread.Sleep(3000);
            }
            var ssEditAfterTest = CaptureScreenshot(TestContext.CurrentContext.Test.Name);
            test.Log(Status.Pass, "Editing Education Test Passed", ssEditAfterTest);
        }

        [Test, Order(3), Description("This test deletes in education feature")]
        public void TestDeleteEducation()
        {
            TestAddEducation();
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
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var mediaEntity = CaptureScreenshot(TestContext.CurrentContext.Test.Name);
                if (deletepopupMsgBox == popUpMsg7)
                {
                    test.Log(Status.Pass, "Deletion Successfull", mediaEntity);
                }
                else
                {
                    test.Log(Status.Fail, "Test Failed:{TestContext.CurrentContext.Result.Message}");
                }
                Thread.Sleep(2000);
            }
            var ssDeleteAfterTest = CaptureScreenshot(TestContext.CurrentContext.Test.Name);
            test.Log(Status.Pass, "Deleting Education Test Passed", ssDeleteAfterTest);
        }
    }
}
