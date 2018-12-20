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
using System.ComponentModel;
using Jliff.Graph.Core;
using Localization.Jliff.Graph.Core;
using Localization.Jliff.Graph.Interfaces;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class JliffDocument
    {
        private const string jliff = "2.1";

        [JsonProperty(Order = 10)]
        public List<File> Files = new List<File>();

        [JsonIgnore]
        private readonly List<Segment> segments = new List<Segment>();

        [JsonProperty(Order = 20)]
        public List<ISubfile> Subfiles = new List<ISubfile>();

        [JsonProperty(Order = 30)]
        public List<ISubunit> Subunits = new List<ISubunit>();

        [JsonConstructor]
        public JliffDocument(string srcLang, string trgLang) : this(srcLang, trgLang, jliff)
        {
        }

        private JliffDocument(string srcLang, string trgLang, string version)
        {
            SrcLang = srcLang;
            TrgLang = trgLang;
            Version = version;
        }

        public JliffDocument(string srcLang, string trgLang, params object[] content)
        {
            SrcLang = srcLang;
            TrgLang = trgLang;
            Version = jliff;

            foreach (var parobj in content)
                if (parobj is File)
                    Files.Add(parobj as File);
                else if (parobj is Unit)
                    Subfiles.Add(parobj as Unit);
                else if (parobj is Group)
                    Subfiles.Add(parobj as Group);
                else if (parobj is IEnumerable<File>)
                    foreach (var grpparobj in parobj as IEnumerable<File>)
                        Files.Add(grpparobj);
                else if (parobj is IEnumerable<Unit>)
                    foreach (var grpparobj in parobj as IEnumerable<Unit>)
                        Subfiles.Add(grpparobj);
                else if (parobj is IEnumerable<Group>)
                    foreach (var grpparobj in parobj as IEnumerable<Group>)
                        Subfiles.Add(grpparobj);
                else
                    throw new InvalidEnumArgumentException();
        }

        [JsonProperty(PropertyName = "@context")]
        public Context21 Context { get; set; }

        [JsonIgnore]
        public List<Segment> Segments
        {
            get
            {
                segments.Clear();
                SegmentVisitor v = new SegmentVisitor();
                foreach (JlfNode node in Files) node.Process(v);
                segments.AddRange(v.Segments);
                return segments;
            }
        }

        [JsonProperty(Order = 2)]
        public string SrcLang { get; set; }

        [JsonProperty(Order = 3)]
        public string TrgLang { get; set; }

        [JsonProperty(PropertyName = "Jliff", Order = 1)]
        public string Version { get; set; }

        public List<string> GetFilenames()
        {
            List<string> filenames = new List<string>();

            foreach (File file in Files)
                filenames.Add(file.Original);

            return filenames;
        }

        public bool ShouldSerializeFiles()
        {
            return Files.Count > 0;
        }

        public bool ShouldSerializeSubfiles()
        {
            return Subfiles.Count > 0;
        }

        public bool ShouldSerializeSubunits()
        {
            return Subunits.Count > 0;
        }

        public Segment UpdateTargetTextElementFromPath(string path, string text)
        {
            string[] indicies = path.Split('/');
            int iFile = int.Parse(indicies[0]);
            int iSubfile = int.Parse(indicies[1]);
            int iUnit = int.Parse(indicies[2]);
            int iSubunit = int.Parse(indicies[3]);
            int iElement = int.Parse(indicies[4]);

            if (iFile < 0 || iFile >= Files.Count)
                throw new IndexOutOfRangeException("Invalid index for Files.");
            if (iSubfile < 0 || iSubfile >= Files[iFile].Subfiles.Count)
                throw new IndexOutOfRangeException("Invalid index for Subfiles.");

            Unit unit;
            if (Files[iFile].Subfiles[iSubfile] is Unit)
                unit = Files[iFile].Subfiles[iSubfile] as Unit;
            else
                throw new InvalidCastException("The supplied path does not resolve to a Unit.");

            Segment segment;
            if (unit.Subunits[iSubunit] is Segment)
                segment = unit.Subunits[iSubunit] as Segment;
            else
                throw new InvalidCastException("The supplied path does not resolve to a Segment.");

            TextElement tElement;
            if (segment.Target[iElement] is TextElement)
            {
                tElement = segment.Target[iElement] as TextElement;
                tElement.Text = text;
            }
            else
            {
                throw new InvalidCastException();
            }

            return segment;
        }
    }
}