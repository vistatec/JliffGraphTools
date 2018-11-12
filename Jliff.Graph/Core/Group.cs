using System;
using System.Collections.Generic;
using Jliff.Graph.Core;
using Jliff.Graph.Interfaces;
using Jliff.Graph.Modules.ITS;
using Localization.Jliff.Graph.Modules.Metadata;
using Localization.Jliff.Graph.Interfaces;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class Group : ISubfile, IJlfNode
    {
        [JsonProperty(Order = 10)]
        public List<ISubfile> Subgroups = new List<ISubfile>();

        public Group() { }

        public Group(string id)
        {
            Id = id;
        }

        public Group(string id, params object[] content)
        {
            Id = id;

            foreach (var parobj in content)
                if (parobj is Unit)
                    Subgroups.Add(parobj as Unit);
                else if (parobj is Group)
                    Subgroups.Add(parobj as Group);
                else if (parobj is IEnumerable<Unit>)
                    foreach (var grpparobj in parobj as IEnumerable<Unit>)
                        Subgroups.Add(grpparobj);
                else if (parobj is IEnumerable<Group>)
                    foreach (var grpparobj in parobj as IEnumerable<Group>)
                        Subgroups.Add(grpparobj);
                else
                    throw new ArgumentException();
        }

        public AnnotatorsRef AnnotatorsRef { get; set; }
        public string CanResegment { get; set; } = "no";
        public object Domains { get; set; }
        public string Id { get; set; }
        public string Kind => "group";
        public string LocQualityRatingProfileRef { get; set; }
        public float LocQualityRatingScore { get; set; }
        public float LocQualityRatingScoreThreshold { get; set; }
        public float LocQualityRatingVote { get; set; }
        public float LocQualityRatingVoteThreshold { get; set; }
        public List<Metadata> Metadata { get; set; }
        public string Name { get; set; }
        public string Org { get; set; }
        public string OrgRef { get; set; }
        public string Person { get; set; }
        public Iri PersonRef { get; set; }
        public object ProfileData { get; set; }
        public object Profiles { get; set; }
        public string ProfileSizeInfo { get; set; }
        public string ProfileSizeInfoRef { get; set; }
        public string ProfileSizeRestriction { get; set; }
        public string ProfileStorageRestriction { get; set; }
        public List<object> ProvenanceRecords { get; set; }
        public Iri ProvenanceRecordsRef { get; set; }
        public object ResourceData { get; set; }
        public string RevOrg { get; set; }
        public Iri RevOrgRef { get; set; }
        public string RevPerson { get; set; }
        public Iri RevPersonRef { get; set; }
        public string RevTool { get; set; }
        public Iri RevToolRef { get; set; }
        public string SrcDir { get; set; }
        public string SubFs { get; set; }
        public string TaClassRef { get; set; }
        public float TaConfidence { get; set; }
        public string TaIdent { get; set; }
        public Iri TaIdentRef { get; set; }
        public string TaSource { get; set; }
        public string Tool { get; set; }
        public string ToolRef { get; set; }
        public Enumerations.YesNo Translate { get; set; }
        public string TrgDir { get; set; }
        public string Type { get; set; }
        public object Userdata { get; set; }
        public string Validation { get; set; }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}