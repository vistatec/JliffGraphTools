using System;
using System.Dynamic;
using System.Text.RegularExpressions;

namespace Jliff.Graph.Core
{
    public class Nmtoken
    {
        public Nmtoken()
        {
            
        }

        public Nmtoken(string token)
        {
            Token = token;
        }

        private string token;
        public string Token
        {
            get { return token; }

            set
            {
                bool foundMatch = false;
                try
                {
                    foundMatch = Regex.IsMatch(value, @"^[-._:A-Za-z0-9]+$", RegexOptions.IgnoreCase);
                }
                catch (ArgumentException ex)
                {
                    // Syntax error in the regular expression
                }
                if (foundMatch) token = value;
            }
        }

    }
}