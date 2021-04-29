using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;


namespace RestApi
{
    public class Utils
    {
        public static string GetValidGuid()
        {
            var guid = GenerateString(6) + "-" + GenerateString(4) + "-" + GenerateString(4) 
                + "-" + GenerateString(4) + "-" + GenerateString(12);
            return guid;
        }

        public static string GenerateString(int length)
        {
            var random = new Random();
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }
    }
}
