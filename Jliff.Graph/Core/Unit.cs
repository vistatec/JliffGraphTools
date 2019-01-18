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
using Jliff.Graph.Interfaces;
using Jliff.Graph.Modules.ChangeTrack;
using Jliff.Graph.Modules.ITS;
using Jliff.Graph.Modules.Matches;
using Localization.Jliff.Graph.Interfaces;
using Localization.Jliff.Graph.Modules.Metadata;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class Unit : JlfNode, ISubfile, IJlfNode, IXmlSerializable
    {
        [JsonProperty("gls_glossary")]
        public List<GlossaryEntry> Glossary = new List<GlossaryEntry>();

        [JsonProperty(Order = 10)]
        public Dictionary<string, string> OriginalData = new Dictionary<string, string>();

        [JsonProperty(Order = 100)]
        public List<ISubunit> Subunits = new List<ISubunit>();

        public Unit(string id)
        {
            Id = id;
        }

        public Unit(string id, params object[] content)
        {
            Id = id;

            foreach (var parobj in content)
                if (parobj is Segment)
                    Subunits.Add(parobj as Segment);
                else if (parobj is Ignorable)
                    Subunits.Add(parobj as Ignorable);
                else if (parobj is IEnumerable<Segment>)
                    foreach (var grpparobj in parobj as IEnumerable<Segment>)
                        Subunits.Add(grpparobj);
                else if (parobj is IEnumerable<Ignorable>)
                    foreach (var grpparobj in parobj as IEnumerable<Ignorable>)
                        Subunits.Add(grpparobj);
        }

        public Unit()
        {
        }

        [JsonProperty("its_annotatorsRef")]
        public AnnotatorsRef AnnotatorsRef { get; set; }

        public string CanResegment { get; set; } = "no";
        public ChangeTrack ChangeTrack { get; set; }

        [JsonProperty("slr_data")]
        public object Data { get; set; }

        [JsonProperty("its_domains")]
        public object Domains { get; set; }

        public string Id { get; set; }
        public override string Kind => Enumerations.JlfNodeType.unit.ToString();

        public LocQualityIssues LocQualityIssues { get; set; }

        [JsonIgnore]
        public string LocQualityIssuesRef { get; set; }

        [JsonProperty("its_locQualityRatingProfileRef")]
        public string LocQualityRatingProfileRef { get; set; }

        [JsonProperty("its_locQualityRatingScore")]
        public float LocQualityRatingScore { get; set; }

        [JsonProperty("its_locQualityRatingScoreThreshold")]
        public float LocQualityRatingScoreThreshold { get; set; }

        public float LocQualityRatingVote { get; set; }
        public float LocQualityRatingVoteThreshold { get; set; }

        [JsonProperty("mtc_matches")]
        public List<Match> Matches { get; set; } = new List<Match>();

        public Metadata Metadata { get; set; }
        public string Name { get; set; }

        public List<Note> Notes { get; set; } = new List<Note>();

        [JsonProperty("its_org")]
        public string Org { get; set; }

        [JsonProperty("its_orgRef")]
        public string OrgRef { get; set; }

        [JsonProperty("its_person")]
        public string Person { get; set; }

        public Iri PersonRef { get; set; }

        [JsonProperty("slr_profiles")]
        public object Profiles { get; set; }

        [JsonProperty("its_provenanceRecords")]
        public List<object> ProvenanceRecords { get; set; }

        [JsonProperty("its_provenanceRecordsRef")]
        public Iri ProvenanceRecordsRef { get; set; }

        [JsonProperty("res_resourceData")]
        public object ResourceData { get; set; }

        [JsonProperty("its_revOrg")]
        public string RevOrg { get; set; }

        public Iri RevOrgRef { get; set; }

        [JsonProperty("its_revPerson")]
        public string RevPerson { get; set; }

        public Iri RevPersonRef { get; set; }

        [JsonProperty("its_revTool")]
        public string RevTool { get; set; }

        public Iri RevToolRef { get; set; }

        [JsonProperty("slr_sizeInfo")]
        public string SizeInfo { get; set; }

        [JsonProperty("slr_sizeInfoRef")]
        public string SizeInfoRef { get; set; }

        [JsonProperty("slr_sizeRestriction")]
        public string SizeRestriction { get; set; }

        public string SrcDir { get; set; }

        [JsonProperty("slr_storageRestriction")]
        public string StorageRestriction { get; set; }

        [JsonProperty("fs_subFs")]
        public string SubFs { get; set; }

        [JsonProperty("its_taClassRef")]
        public string TaClassRef { get; set; }

        [JsonProperty("its_taConfidence")]
        public float TaConfidence { get; set; }

        [JsonProperty("its_taIdent")]
        public string TaIdent { get; set; }

        [JsonProperty("its_taIdentRef")]
        public Iri TaIdentRef { get; set; }

        [JsonProperty("its_taSource")]
        public string TaSource { get; set; }

        [JsonProperty("its_tool")]
        public string Tool { get; set; }

        [JsonProperty("its_toolRef")]
        public string ToolRef { get; set; }

        public Enumerations.YesNo Translate { get; set; }
        public string TrgDir { get; set; }
        public string Type { get; set; }
        public object Userdata { get; set; }

        [JsonProperty("val_validation")]
        public string Validation { get; set; }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
            foreach (JlfNode node in Subunits)
            {
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

            if (ChangeTrack != null)
            {
                writer.WriteStartElement("ctr:changeTrack");
                (ChangeTrack as IXmlSerializable).WriteXml(writer);
                writer.WriteEndElement();
            }

            if (Glossary.Count > 0)
            {
                writer.WriteStartElement("gls:glossary");
                foreach (GlossaryEntry glossaryEntry in Glossary)
                {
                    writer.WriteStartElement("gls:glossEntry");
                    (glossaryEntry as IXmlSerializable).WriteXml(writer);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }

            if (Metadata != null)
            {
                writer.WriteStartElement("mda:metadata");
                (Metadata as IXmlSerializable).WriteXml(writer);
                writer.WriteEndElement();
            }

            if (Matches.Count > 0)
            {
                writer.WriteStartElement("mtc:matches");
                foreach (Match match in Matches)
                {
                    writer.WriteStartElement("mtc:match");
                    (match as IXmlSerializable).WriteXml(writer);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }

            if (Notes.Count > 0)
            {
                writer.WriteStartElement("notes");
                foreach (Note note in Notes)
                {
                    writer.WriteStartElement("note");
                    (note as IXmlSerializable).WriteXml(writer);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }

            foreach (ISubunit subunit in Subunits)
                switch (subunit)
                {
                    case Segment s:
                        writer.WriteStartElement("segment");
                        (s as IXmlSerializable).WriteXml(writer);
                        writer.WriteEndElement();
                        break;
                    default:
                        break;
                }
        }

        public JlfNode NodeAccept(IVisitor visitor)
        {
            return this;
        }

        public override void Process(ICompositeVisitor visitor)
        {
            visitor.Visit(this);
            foreach (JlfNode node in Subunits) node.Process(visitor);
        }

        public bool ShouldSerializeGlossary()
        {
            return Glossary.Count > 0;
        }

        public bool ShouldSerializeMatches()
        {
            return Matches.Count > 0;
        }

        //public bool ShouldSerializeLocQualityIssues()
        //{
        //    return LocQualityIssues.Count > 0;
        //}

        public bool ShouldSerializeOriginalData()
        {
            return OriginalData.Count > 0;
        }

        public bool ShouldSerializeSubunits()
        {
            return Subunits.Count > 0;
        }

        public override string Traverse(Func<string> func)
        {
            string z = string.Empty;
            foreach (JlfNode node in Subunits) z = node.Traverse(func);

            return $"{Id}/ {z}";
        }
    }
}