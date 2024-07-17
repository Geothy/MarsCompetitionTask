using AventStack.ExtentReports;
using MarsCompetitionTask.Models;
using MarsCompetitionTask.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MarsCompetitionTask.Pages
{
    public class LoginPage : CommonDriver
    {

        private IWebElement signInButton => driver.FindElement(By.XPath("//a[text()='Sign In']"));
        private IWebElement usernameTextbox => driver.FindElement(By.XPath("//input[@name='email']"));
        private IWebElement passwordTextbox => driver.FindElement(By.XPath("//input[@name='password']"));
        private IWebElement logInButton => driver.FindElement(By.XPath("//button[text()='Login']"));

        public void LoginActions()
        {

            //Launch Turnup Portal & Navigate to Login Page
            string baseURL = "http://localhost:5000/";
            driver.Navigate().GoToUrl(baseURL);
            //Identify Signin Button & Click on Signin Button
            signInButton.Click();
            //Reading Json data
            string loginFile = "LoginData.json";
            List<LoginModel> LoginData = JsonUtil.ReadJsonData<LoginModel>(loginFile);

            foreach (var item in LoginData)
            {
                string email = item.Email;
                string password = item.Password;
                //Identify Username textbox & enter valid username
                usernameTextbox.SendKeys(email);
                //Identify Password textbox & enter valid password
                passwordTextbox.SendKeys(password);
                //Identify Login Button & Click on Login Button
                logInButton.Click();
                Thread.Sleep(1000);
                VerifyLoggedInUser();

            }
        }
        public void VerifyLoggedInUser()
        {
            //Check if loggedin successfully
            WaitUtils.WaitToBeVisible(driver, "XPath", "//span[contains(text(),'Hi')]", 100);
            IWebElement checkUser = driver.FindElement(By.XPath("//span[contains(text(),'Hi')]"));
            Console.WriteLine(checkUser.Text);
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
