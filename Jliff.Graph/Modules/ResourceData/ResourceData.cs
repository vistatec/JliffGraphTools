/*
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


using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Localization.Jliff.Graph.Modules.ResourceData
{
    public class ResourceData : IXmlSerializable
    {
        public ResourceData()
        {
            
        }

        public ResourceData(string id, params object[] content)
        {
            Id = id;

            foreach (var parobj in content)
                if (parobj is ResourceItem)
                    ResourceItems.Add(parobj as ResourceItem);
                else if (parobj is IEnumerable<ResourceItem>)
                    foreach (var grpparobj in parobj as IEnumerable<ResourceItem>)
                        ResourceItems.Add(grpparobj);
                else
                    throw new ArgumentException();

        }

        public string Id { get; set; }
        public List<ResourceItem> ResourceItems { get; set; } = new List<ResourceItem>();
        public List<ResourceItemRef> ResourceItemRefs { get; set; } = new List<ResourceItemRef>();

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public bool ShouldSerializeResourceItemRefs()
        {
            return ResourceItemRefs.Count > 0;
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("id", Id);
            if (ResourceItems.Count > 0)
            {
                writer.WriteStartElement("res:resourceItem");
                foreach (ResourceItem resourceItem in ResourceItems)
                {
                    (resourceItem as IXmlSerializable).WriteXml(writer);
                }
                writer.WriteEndElement();
            }
        }
    }
}