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
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Jliff.Graph.Core;
using Jliff.Graph.Interfaces;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    /// <summary>
    ///     Much of the way in which source and target text is stored and manipulated is inspired by the Okpai Framework
    ///     TextFragment class and encoding <see cref="http://okapiframework.org/devguide/gettingstarted.html#textUnits" />.
    /// </summary>
    public class Segment : JlfNode, ISubunit, IXmlSerializable
    {
        [JsonProperty(Order = 10)]
        public List<IElement> Source = new List<IElement>();

        [JsonProperty(Order = 20)]
        public List<IElement> Target = new List<IElement>();

        private string targetText;

        public Segment()
        {
            FragmentManager = new FragmentManager();
        }

        public Segment(string id)
        {
            Id = id;
            FragmentManager = new FragmentManager();
        }

        [JsonConstructor]
        public Segment(List<IElement> source, List<IElement> target)
        {
            Source = source;
            Target = target;
        }

        public Segment(IElement source, IElement target = null)
        {
            Source.Add(source);
            if (target != null) Target.Add(target);
        }

        public string CanResegment { get; set; } = "no";


        [JsonIgnore]
        public FragmentManager FragmentManager { get; set; }

        public string Id { get; set; }

        public override string Kind => Enumerations.JlfNodeType.segment.ToString();

        [JsonIgnore]
        public string SourceText
        {
            get
            {
                //return Source.Where(t => t is TextElement)
                //    .Cast<TextElement>()
                //    .Aggregate(new StringBuilder(), (sb, s) => sb.Append(s.Text))
                //    .ToString();
                return Source.Aggregate(new StringBuilder(), (sb, s) => sb.Append(s)).ToString();
            }
        }

        public string State { get; set; }

        [JsonIgnore]
        public string TargetText
        {
            get
            {
                return Target.Where(t => t is TextElement)
                    .Cast<TextElement>()
                    .Aggregate(new StringBuilder(), (sb, s) => sb.Append(s.Text))
                    .ToString();
            }
        }

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
            writer.WriteAttributeString("id", Id);
            writer.WriteStartElement("source");
            foreach (IElement element in Source)
                switch (element)
                {
                    case EcElement ec:
                        writer.WriteStartElement("ec");
                        (ec as IXmlSerializable).WriteXml(writer);
                        writer.WriteEndElement();
                        break;
                    case EmElement em:
                        writer.WriteStartElement("em");
                        writer.WriteEndElement();
                        break;
                    case ScElement sc:
                        writer.WriteStartElement("sc");
                        (sc as IXmlSerializable).WriteXml(writer);
                        writer.WriteEndElement();
                        break;
                    case SmElement sm:
                        writer.WriteStartElement("sm");
                        (sm as IXmlSerializable).WriteXml(writer);
                        writer.WriteEndElement();
                        break;
                    case TextElement te:
                        (te as IXmlSerializable).WriteXml(writer);
                        break;
                }
            writer.WriteEndElement();

            writer.WriteStartElement("target");
            foreach (IElement element in Target)
                switch (element)
                {
                    case EcElement ec:
                        writer.WriteStartElement("ec");
                        (ec as IXmlSerializable).WriteXml(writer);
                        writer.WriteEndElement();
                        break;
                    case EmElement em:
                        writer.WriteStartElement("em");
                        writer.WriteEndElement();
                        break;
                    case ScElement sc:
                        writer.WriteStartElement("sc");
                        (sc as IXmlSerializable).WriteXml(writer);
                        writer.WriteEndElement();
                        break;
                    case SmElement sm:
                        writer.WriteStartElement("sm");
                        (sm as IXmlSerializable).WriteXml(writer);
                        writer.WriteEndElement();
                        break;
                    case TextElement te:
                        (te as IXmlSerializable).WriteXml(writer);
                        break;
                }
            writer.WriteEndElement();
        }

        public string FlattenSource()
        {
            return FragmentManager.Flatten(Source);
        }

        public string FlattenTarget()
        {
            return FragmentManager.Flatten(Target);
        }

        public string GetTargetTextAt(int index)
        {
            if (Target[index] is TextElement)
                return (Target[index] as TextElement).Text;
            throw new InvalidOperationException();
        }

        public void ParseSource(string text)
        {
            Source = FragmentManager.Parse(text);
        }

        public void ParseTarget(string text)
        {
            Target = FragmentManager.Parse(text);
            //Target.Parse();
        }

        public override void Process(ICompositeVisitor visitor)
        {
            visitor.Visit(this);
            //foreach (JlfNode node in Source)
            //{
            //    node.Process(visitor);
            //}

            //foreach (JlfNode node in Target)
            //{

            //}
        }

        public bool ShouldSerializeTarget()
        {
            return Target.Count > 0;
        }

        public override string Traverse(Func<string> func)
        {
            return $"{Id}/ ";
        }
    }
}