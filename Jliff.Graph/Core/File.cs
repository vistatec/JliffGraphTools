using System;
using System.Collections.Generic;
using Jliff.Graph.Core;
using Jliff.Graph.Interfaces;
using Jliff.Graph.Modules.ITS;
using Localization.Jliff.Graph.Interfaces;
using Localization.Jliff.Graph.Modules.Metadata;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class File : JlfNode, IJlfNode
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

        public AnnotatorsRef AnnotatorsRef { get; set; }
        public object ChangeTrack { get; set; }
        public object Data { get; set; }
        public string Domains { get; set; }
        public Enumerations.FormatStyle Fs { get; set; }

        [JsonIgnore]
        public override bool HasChildren => Subfiles.Count > 0;

        public string Id { get; set; }

        public override string Kind => Enumerations.JlfNodeType.file.ToString();
        public string LocQualityRatingProfileRef { get; set; }
        public float LocQualityRatingScore { get; set; }
        public float LocQualityRatingScoreThreshold { get; set; }
        public float LocQualityRatingVote { get; set; }
        public float LocQualityRatingVoteThreshold { get; set; }
        public List<MetaGroup> Metadata { get; set; }
        public string Org { get; set; }
        public Iri OrgRef { get; set; }
        public string Original { get; set; }
        public string Person { get; set; }
        public Iri PersonRef { get; set; }
        public object Profiles { get; set; }
        public List<object> ProvenanceRecords { get; set; }
        public Iri ProvenanceRecordsRef { get; set; }
        public object ResourceData { get; set; }
        public string RevOrg { get; set; }
        public Iri RevOrgRef { get; set; }
        public string RevPerson { get; set; }
        public Iri RevPersonRef { get; set; }
        public string RevTool { get; set; }
        public Iri RevToolRef { get; set; }
        public string SizeInfo { get; set; }
        public string SizeInfoRef { get; set; }
        public string SizeRestriction { get; set; }
        public Skeleton Skeleton { get; set; }
        public string SrcDir { get; set; }
        public string StorageRestriction { get; set; }
        public string SubFs { get; set; }
        public Iri TaClassRef { get; set; }
        public float TaConfidence { get; set; }
        public string TaIdent { get; set; }
        public Iri TaIdentRef { get; set; }
        public string TaSource { get; set; }
        public string Tool { get; set; }
        public Iri ToolRef { get; set; }
        public Enumerations.YesNo Translate { get; set; }
        public string TrgDir { get; set; }
        public object Userdata { get; set; }
        public string Validation { get; set; }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
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