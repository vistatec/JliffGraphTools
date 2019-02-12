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
using Jliff.Graph.Modules.ITS;
using Jliff.Graph.Serialization;
using Localization.Jliff.Graph.Interfaces;
using Localization.Jliff.Graph.Modules.Metadata;
using Localization.Jliff.Graph.Modules.ResourceData;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class File : JlfNode, IJlfNode, IXmlSerializable
    {
        [JsonProperty(Order = 10)]
        public List<ISubfile> Subfiles = new List<ISubfile>();

        public File()
        {
        }

        [JsonConstructor]
        public File(string id)
        {
            Id = id;
        }

        public File(string id, params object[] content)
        {
            Id = id;

            foreach (var parobj in content)
                if (parobj is Unit)
                    Subfiles.Add(parobj as Unit);
                else if (parobj is Group)
                    Subfiles.Add(parobj as Group);
                else if (parobj is IEnumerable<Unit>)
                    foreach (var grpparobj in parobj as IEnumerable<Unit>)
                        Subfiles.Add(grpparobj);
                else if (parobj is IEnumerable<Ignorable>)
                    foreach (var grpparobj in parobj as IEnumerable<Group>)
                        Subfiles.Add(grpparobj);
                else
                    throw new ArgumentException();
        }

        [JsonProperty("its_annotatorsRef")]
        public AnnotatorsRef AnnotatorsRef { get; set; }

        [JsonProperty("ctr_changeTrack")]
        public object ChangeTrack { get; set; }

        [JsonProperty("slr_data")]
        public object Data { get; set; }

        public string Domains { get; set; }

        [JsonProperty("fs_fs")]
        public Enumerations.FormatStyle Fs { get; set; }

        [JsonIgnore]
        public override bool HasChildren => Subfiles.Count > 0;

        public string Id { get; set; }

        public override string Kind => Enumerations.JlfNodeType.file.ToString();

        [JsonProperty("its_locQualityRatingProfileRef")]
        public string LocQualityRatingProfileRef { get; set; }

        [JsonProperty("its_locQualityRatingScore")]
        public float LocQualityRatingScore { get; set; }

        [JsonProperty("its_locQualityRatingScoreThreshold")]
        public float LocQualityRatingScoreThreshold { get; set; }

        public float LocQualityRatingVote { get; set; }
        public float LocQualityRatingVoteThreshold { get; set; }
        public List<MetaGroup> Metadata { get; set; } = new List<MetaGroup>();

        [JsonProperty("its_org")]
        public string Org { get; set; }

        public Iri OrgRef { get; set; }
        public string Original { get; set; }

        [JsonProperty("its_person")]
        public string Person { get; set; }

        public Iri PersonRef { get; set; }

        [JsonProperty("slr_profiles")]
        public object Profiles { get; set; }

        [JsonProperty("its_provenanceRecords")]
        public List<object> ProvenanceRecords { get; set; }

        public Iri ProvenanceRecordsRef { get; set; }

        [JsonProperty("res_resourceData")]
        public ResourceData ResourceData { get; set; }

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

        public Skeleton Skeleton { get; set; }
        public string SrcDir { get; set; }

        [JsonProperty("slr_storageRestriction")]
        public string StorageRestriction { get; set; }

        [JsonProperty("fs_subFs")]
        public string SubFs { get; set; }

        public Iri TaClassRef { get; set; }

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

        public Iri ToolRef { get; set; }
        public Enumerations.YesNo Translate { get; set; }
        public string TrgDir { get; set; }
        public object Userdata { get; set; }

        [JsonProperty("val_validation")]
        public string Validation { get; set; }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
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

            if (ResourceData != null)
            {
                writer.WriteStartElement("resourceData", Namespaces.RES);
                (ResourceData as IXmlSerializable).WriteXml(writer);
                writer.WriteEndElement();
            }

            foreach (ISubfile subfile in Subfiles)
                switch (subfile)
                {
                    case Group g:
                        writer.WriteStartElement("group");
                        (g as IXmlSerializable).WriteXml(writer);
                        writer.WriteEndElement();
                        break;
                    case Unit u:
                        writer.WriteStartElement("unit");
                        (u as IXmlSerializable).WriteXml(writer);
                        writer.WriteEndElement();
                        break;
                }
        }

        public override void Process(ICompositeVisitor visitor)
        {
            visitor.Visit(this);
            foreach (JlfNode node in Subfiles) node.Process(visitor);
        }

        public override string Traverse(Func<string> func)
        {
            string z = string.Empty;
            foreach (JlfNode subfile in Subfiles) z = subfile.Traverse(func);

            return $"{Id}/ {z}";
        }
    }
}