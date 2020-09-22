using System;
using System.Linq;

namespace myAuthApp.Utility
{
    public class AuthCodeUtility
    {
        public static string GenerateAuthCode(){
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 16).Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}