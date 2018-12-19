using System;
using System.Text.RegularExpressions;

namespace Jliff.Graph.Core
{
    public class Iri
    {
        public Iri()
        {
            Identifier = String.Empty;
        }

        public Iri(string identifier)
        {
            Identifier = identifier;
        }

        private string identifier = String.Empty;
        public string Identifier
        {
            get { return identifier; }

            set
            {
                bool foundMatch = false;
                try
                {
                    foundMatch = Regex.IsMatch(value, @"\A\b((?#protocol)https?|ftp)://((?#domain)[-A-Z0-9.]+)((?#file)/[-A-Z0-9+&@#/%=~_|!:,.;]*)?((?#parameters)\?[A-Z0-9+&@#/%=~_|!:,.;]*)?\z", RegexOptions.IgnoreCase);
                }
                catch (ArgumentException ex)
                {
                    // Syntax error in the regular expression
                }
                if (foundMatch) identifier = value;
            }
        }
    }
}