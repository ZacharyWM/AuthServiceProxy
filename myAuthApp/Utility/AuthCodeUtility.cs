using System;
using System.Linq;

namespace myAuthApp.Utility
{
    public class AuthCodeUtility
    {
        public static string GenerateAuthCode(){
            Random random = new Random();

            string numbers = "0123456789";
            string upperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lowerCaseLetters = upperCaseLetters.ToLower();
            
            string allPossibleChars = upperCaseLetters + lowerCaseLetters + numbers;

            return new string(Enumerable.Repeat(allPossibleChars, 16).Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}