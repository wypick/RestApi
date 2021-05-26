using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Net.Http;

namespace Autotests
{
    public class Utils
    {
        public static string host = "127.0.0.1:5000/pass";
        static readonly HttpClient client = new HttpClient();

        public static Pass Pass { get; set; }

        public static Uri GetUri(string path = null)
        {

            if (path != null)
            {
                return new Uri($"http://{host}/{path}");
            }
            else
            {
                return new Uri($"http://{host}");
            }
        }

        public static Uri GetUriValidate(string path)
        {
            return new Uri($"http://{host}/validate/{path}");
        }

        public static HttpResponseMessage Post(Uri uri, string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = client.PostAsync(uri, content).Result;

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Failed to POST data: ({result.StatusCode}): {result.Content.ReadAsStringAsync().Result}");

            Console.WriteLine($"Result: ({result.StatusCode}): {result.Content.ReadAsStringAsync().Result}");

            return result;
        }

        public static HttpResponseMessage Put(Uri uri, string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = client.PutAsync(uri, content).Result;

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Failed to PUT data: ({result.StatusCode}): {result.Content.ReadAsStringAsync().Result}");

            Console.WriteLine($"Result: ({result.StatusCode}): {result.Content.ReadAsStringAsync().Result}");

            return result;
        }

        public static HttpResponseMessage Delete(Uri uri)
        {
            var result = client.DeleteAsync(uri).Result;

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Failed to Delete data: ({result.StatusCode}): {result.Content.ReadAsStringAsync().Result}");

            Console.WriteLine($"Result: ({result.StatusCode}): {result.Content.ReadAsStringAsync().Result}");

            return result;
        }

        public static HttpResponseMessage Get(Uri uri)
        {
            var result = client.GetAsync(uri).Result;

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Failed to GET data: ({result.StatusCode}): {result.Content.ReadAsStringAsync().Result}");

            Console.WriteLine($"Result: ({result.StatusCode}): {result.Content.ReadAsStringAsync().Result}");

            return result;
        }

       /* public static void Init()
        {
            Rnd = new Random();
        }*/

        public static string GenerateString(int length, Random random, bool? number = null)
        {
            string characters;

            if (number == true)
            {
                characters = "0123456789";
            }
            else
            {
                characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            }
           
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }
    }
}

