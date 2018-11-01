using System;
using System.Collections.Generic;
using System.Xml;

namespace Localization.Jliff.Graph
{
    public class FilterEventArgs : EventArgs
    {
        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();

        public string Id { get; set; }

        public bool IsEndElement
        {
            get
            {
                if (NodeType.Equals("EndElement"))
                    return true;
                return false;
            }
        }

        public string NodeType { get; set; }

        public string sourceOrTarget { get; set; }
        public string Text { get; set; }

        public static FilterEventArgs FromReader(XmlReader reader)
        {
            FilterEventArgs args = new FilterEventArgs();

            args.NodeType = reader.NodeType.ToString();

            if (reader.HasAttributes)
                while (reader.MoveToNextAttribute())
                    args.Attributes.Add(reader.Name, reader.Value);

            return args;
        }
    }
}