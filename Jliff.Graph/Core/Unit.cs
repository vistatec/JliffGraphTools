using System;
using System.Collections.Generic;
using Jliff.Graph.Modules.LocQualityIssue;
using Jliff.Graph.Modules.Matches;
using Localization.Jliff.Graph.Modules.Metadata;
using Localization.Jliff.Graph.Interfaces;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class Unit : ISubfile
    {
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

        public Unit() { }

        public string CanResegment { get; set; } = "no";
        public object Domains { get; set; }
        public string Id { get; set; }
        public string Kind => "unit";

        public List<LocQualityIssue> LocQualityIssues { get; set; } = new List<LocQualityIssue>();

        [JsonIgnore]
        public string LocQualityIssuesRef { get; set; }

        public string LocQualityRatingProfileRef { get; set; }
        public float LocQualityRatingScore { get; set; }
        public float LocQualityRatingScoreThreshold { get; set; }
        public float LocQualityRatingVote { get; set; }
        public float LocQualityRatingVoteThreshold { get; set; }
        public List<Match> Matches { get; set; } = new List<Match>();
        public Metadata Metadata { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
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

        public bool ShouldSerializeGlossary()
        {
            return Glossary.Count > 0;
        }

        public bool ShouldSerializeOriginalData()
        {
            return OriginalData.Count > 0;
        }

        public bool ShouldSerializeSubunits()
        {
            return Subunits.Count > 0;
        }

        public bool ShouldSerializeLocQualityIssues()
        {
            return LocQualityIssues.Count > 0;
        }
    }
}