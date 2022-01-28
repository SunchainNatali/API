using System;
using Xunit;
using RestSharp;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using RestSharp.Extensions;
using System.Net;
using System.IO;

namespace _25._01._22Api
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var body = new Dictionary<string, string>
            {
                {"ulogin","art1613122" },
                {"upassword","505558545" }
            };

            var headers = new Dictionary<string, string>
            {
                {"Content-Type","application/x-www-form-urlencoded" }
            };

            // send API LOgin
            var response = ApiHelper.SendApiRequest(body, headers, "https://my.soyuz.in.ua/", Method.POST);


            // Get Cookie Api response (of login) & Change Cookie (API) for browser (to UI Cookie)
            var cookie = ApiHelper.ExtractCookie(response,"zbs_lang");
            var cookie2 = ApiHelper.ExtractCookie(response, "ulogin");
            var cookie3 = ApiHelper.ExtractCookie(response, "upassword");
             
            // Open browser
            IWebDriver chrome = new ChromeDriver();
            chrome.Navigate().GoToUrl("https://my.soyuz.in.ua");
            System.Threading.Thread.Sleep(3000);


            // Set Cookie (UI) to browser
            chrome.Manage().Cookies.AddCookie(cookie);
            chrome.Manage().Cookies.AddCookie(cookie2);
            chrome.Manage().Cookies.AddCookie(cookie3);
            // Open Site (profile page)
            chrome.Navigate().GoToUrl("https://my.soyuz.in.ua/index.php");

            System.Threading.Thread.Sleep(15000);
            //chrome.Close();

        }


        [Fact]
        public void SendAPictureRequest()
        {
            var client = new RestClient("https://postimages.org/ru/");
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "image/gif");
            request.AddFile("content", "/Users/natalanikulina/Desktop/C#/test3.jpg");
            IRestResponse response = client.Execute(request);
            Assert.Equal("OK", response.StatusCode.ToString());
           
        }

        [Fact]
        public void DownloadData()
        {
            byte[] picture = ApiHelper.TransmitJsonApiRequest("https://img.freepik.com/free-photo/jpg-word-illustration_93826-818.jpg?size=626&ext=jpg");
            File.WriteAllBytes(Path.Combine("/Users/natalanikulina/Desktop/C#", "test3.jpg"), picture);
        }

      

    }
}
