using System;
using System.Collections.Generic;
using RestSharp;
using OpenQA.Selenium;
using System.IO;

namespace _25._01._22Api
{
    public static class ApiHelper
    {
        public static IRestResponse SendApiRequest(object body, Dictionary<string, string> headers, string link, Method type)
        {
            RestClient client = new RestClient(link)
            {
                Timeout = 300000
            };

            RestRequest request = new RestRequest(type);
            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
            bool isBodyJson = false;
            foreach (var header in headers)
            {
                if (header.Value.Contains("application/json"))
                {
                    isBodyJson = true;
                    break;
                }
            }
            if (!isBodyJson)
            {
                foreach (var data in (Dictionary<string, string>)body)
                {
                    request.AddParameter(data.Key, data.Value);
                }
            }
            else
            {
                request.AddJsonBody(body);
                request.RequestFormat = DataFormat.Json;
            }

            // Send request.

            IRestResponse response = client.Execute(request);
            return response;
        }

        public static Cookie ExtractCookie(IRestResponse response, string cookieName)
        {
            Cookie resp = null;
            foreach (var cookie in response.Cookies)
            {
                if (cookie.Name.Equals(cookieName))
                {
                    resp = new Cookie(cookie.Name, cookie.Value, cookie.Domain, cookie.Path, null);
                }
            }
            return resp;
        }

        public static List<Cookie> ExtractAllCookies(IRestResponse response)
        {
            List<Cookie> res = new List<Cookie>();
            foreach (var cookie in response.Cookies)
                res.Add(new Cookie(cookie.Name, cookie.Value, cookie.Domain, cookie.Path, null));
            return res;
        }


        public static byte[] TransmitJsonApiRequest(string imageUrl)
        {
            var client = new RestClient("https://img.freepik.com/free-photo/jpg-word-illustration_93826-818.jpg?size=626&ext=jpg");
            var apiRequest = new RestRequest(Method.GET);
            byte[] pictureInBytes = client.DownloadData(apiRequest);
            return pictureInBytes;
        }

    }
        
}
    

