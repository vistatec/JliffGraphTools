﻿/*
 * Copyright (C) 2018, Vistatec or third-party contributors as indicated
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


using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Jliff.Graph.Core;

namespace Jliff.Graph.Modules.ChangeTrack
{
    public class Revision : IXmlSerializable
    {
        public Revision()
        {
            
        }

        public string Author { get; set; }
        public string DateTime { get; set; }
        public Nmtoken Version { get; set; }
        public RevisionItem Item { get; set; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new System.NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("author", Author);
            writer.WriteAttributeString("datetime", DateTime);
            writer.WriteAttributeString("version", Version.Token);
            if (Item != null)
            {
                writer.WriteStartElement("ctr:item");
                (Item as IXmlSerializable).WriteXml(writer);
                writer.WriteEndElement();
            }
        }
    }
}