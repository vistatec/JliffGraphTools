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
using System.Linq;
using System.Text;
using Jliff.Graph.Interfaces;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class Segment : JlfNode, ISubunit
    {
        [JsonProperty(Order = 10)]
        public List<IElement> Source = new List<IElement>();

        [JsonProperty(Order = 20)]
        public List<IElement> Target = new List<IElement>();

        public Segment()
        {
        }

        public Segment(string id)
        {
            Id = id;
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

        public string Id { get; set; }

        public override string Kind => Enumerations.JlfNodeType.segment.ToString();

        public string State { get; set; }

        public string GetTargetTextAt(int index)
        {
            if (Target[index] is TextElement)
                return (Target[index] as TextElement).Text;
            throw new InvalidOperationException();
        }

        public override string Traverse(Func<string> func)
        {
            return $"{Id}/ ";
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

        [JsonIgnore]
        public string SourceText
        {
            get
            {
                return Source.Where(t => t is TextElement)
                    .Cast<TextElement>()
                    .Aggregate(new StringBuilder(), (sb, s) => sb.Append(s.Text))
                    .ToString();
            }
        }

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

        public bool ShouldSerializeTarget()
        {
            return Target.Count > 0;
        }
    }
}