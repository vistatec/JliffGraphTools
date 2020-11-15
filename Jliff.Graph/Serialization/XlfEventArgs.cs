/*
 * Copyright (C) 2018-2019, Vistatec or third-party contributors as indicated
 * by the @author tags or express copyright attribution statements applied by
 * the authors. All third-party contributions are distributed under license by
 * Vistatec.
 *
 * This file is part of JliffGraphTools.
 *
 * JliffGraphTools is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * JliffGraphTools is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with this program. If not, write to:
 *
 *     Free Software Foundation, Inc.
 *     51 Franklin Street, Fifth Floor
 *     Boston, MA 02110-1301
 *     USA
 *
 * Also, see the full LGPL text here: <http://www.gnu.org/copyleft/lesser.html>
 */


using System;
using System.Collections.Generic;
using System.Xml;

namespace Localization.Jliff.Graph.Serialization
{
    public class XlfEventArgs : EventArgs
    {
        public XlfEventArgs()
        {
            NodeType = "Element";
        }

        public XlfEventArgs(string id)
        {
            Attributes["id"] = id;
            NodeType = "Element";
        }

        public XlfEventArgs(string id, string text, Dictionary<string, string> attributes)
        {
            Text = text;
            Attributes = attributes;
            NodeType = "Element";
            Attributes["id"] = id;
        }

        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();

        //public string Id { get; set; }

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

        public static XlfEventArgs FromReader(XmlReader reader)
        {
            XlfEventArgs args = new XlfEventArgs();

            args.NodeType = reader.NodeType.ToString();

            if (reader.HasAttributes)
                while (reader.MoveToNextAttribute())
                    args.Attributes.Add(reader.Name, reader.Value);

            return args;
        }
    }
}