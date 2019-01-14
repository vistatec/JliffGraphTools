using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Localization.Jliff.Graph;

namespace Jliff.Graph.Core
{
    /// <summary>
    /// A class to facilitate conversion from a <code>List&lt;IElement&gt;</code> to a flat representation
    /// and back.
    /// </summary>
    public class FragmentManager
    {
        private const string OPENINGTAG = "\ue101";

        private const string CLOSINGTAG = "\ue102";

        private List<string> codes = new List<string>();

        private enum ParseState
        {
            None,
            Sm,
            Em,
            Text
        }

        public List<IElement> Parse(string text)
        {
            StringInfo info = new StringInfo();
            Stack<IElement> parsed = new Stack<IElement>();
            StringBuilder data = new StringBuilder();
            ParseState parserState = ParseState.None;
            string token = String.Empty;
            TextElementEnumerator d = StringInfo.GetTextElementEnumerator(text);
            while (d.MoveNext())
            {
                token = d.GetTextElement();
                switch (token)
                {
                    case OPENINGTAG:
                        if (parserState == ParseState.None)
                        {
                            parserState = ParseState.Sm;
                            data.Append(token);
                        }
                        else if (parserState == ParseState.Sm)
                        {
                            data.Append(token);
                            parsed.Push(new SmElement(data.ToString()));
                            data.Clear();
                            parserState = ParseState.None;
                        }
                        break;
                    case CLOSINGTAG:
                        if (parserState == ParseState.None)
                        {
                            parsed.Push(new TextElement(data.ToString()));
                            data.Clear();
                            parserState = ParseState.Em;
                        }
                        else if (parserState == ParseState.Em)
                        {
                            parserState = ParseState.Text;
                            parsed.Push(new EmElement());
                            data.Clear();
                        }
                        break;
                    default:
                        data.Append(token);
                        break;
                }
            }
            if (parserState == ParseState.Text || parserState == ParseState.None)
            {

                parsed.Push(new TextElement(data.ToString()));
            }

            return parsed.Reverse().ToList();
        }

        public string Flatten(List<IElement> elements)
        {
            return elements.Aggregate(new StringBuilder(), (sb, s) => sb.Append(s)).ToString();
        }
    }
}