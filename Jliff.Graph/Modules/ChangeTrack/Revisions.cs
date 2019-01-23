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
using System.Xml.Schema;
using System.Xml.Serialization;
using Jliff.Graph.Core;
using Jliff.Graph.Modules.ITS;
using Jliff.Graph.Serialization;
using Newtonsoft.Json;

namespace Jliff.Graph.Modules.ChangeTrack
{
    public class Revisions : IXmlSerializable
    {
        [JsonProperty("its_annotatorsRef")]
        public AnnotatorsRef AnnotatorsRef { get; set; }

        public Nmtoken AppliesTo { get; set; }
        public Nmtoken CurrentVersion { get; set; }
        public List<Revision> Items { get; set; } = new List<Revision>();
        public Nmtoken Ref { get; set; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("appliesTo", AppliesTo.Token);
            writer.WriteAttributeString("currentVersion", CurrentVersion.Token);
            if (Items.Count > 0)
                foreach (Revision revision in Items)
                {
                    writer.WriteStartElement("revision", Namespaces.CTR);
                    (revision as IXmlSerializable).WriteXml(writer);
                    writer.WriteEndElement();
                }
        }
    }
}