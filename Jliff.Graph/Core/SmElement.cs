using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class SmElement : IElement
    {
        public SmElement()
        {
        }

        public SmElement(string id)
        {
            Id = id;
        }

        [JsonIgnore]
        public IDictionary<string, string> Attributes
        {
            get
            {
                Dictionary<string, string> atts = new Dictionary<string, string>();
                atts.Add("Id", Id);
                atts.Add("Translate", Translate.ToString());
                atts.Add("Type", Type);
                return atts;
            }

            set
            {
                IDictionary<string, string> atts = value;
                foreach (KeyValuePair<string, string> att in atts)
                    switch (att.Key)
                    {
                        case "id":
                            Id = att.Value;
                            break;
                        case "translate":
                            Translate = bool.Parse(att.Value);
                            break;
                        case "type":
                            Type = att.Value;
                            break;
                    }
            }
        }

        public string AllowedCharacters { get; set; }
        public string AnnotatorsRef { get; set; }
        public List<string> Domains { get; set; }
        public Enumerations.FormatStyle fs { get; set; }
        public string Id { get; set; }

        public string Kind => Enumerations.ElementKind.sm.ToString();
        public string LocaleFilterList { get; set; }
        public string LocaleFilterType { get; set; }
        public string LocQualityIssueComment { get; set; }
        public string LocQualityIssueEnabled { get; set; }
        public string LocQualityIssueProfileRef { get; set; }
        public float LocQualityIssueSeverity { get; set; }
        public string LocQualityIssuesRef { get; set; }
        public string LocQualityIssueType { get; set; }
        public string LocQualityRatingProfileRef { get; set; }
        public float LocQualityRatingScore { get; set; }
        public float LocQualityRatingScoreThreshold { get; set; }
        public int LocQualityRatingVote { get; set; }
        public int LocQualityRatingVoteThreshold { get; set; }
        public float MtConfidence { get; set; }
        public string Org { get; set; }
        public Uri OrgRef { get; set; }
        public string Person { get; set; }
        public Uri PersonRef { get; set; }
        public string ProfileSizeRestriction { get; set; }
        public string ProfileStorageRestriction { get; set; }
        public Uri ProvenanceRecordsRef { get; set; }
        public Uri Ref { get; set; }
        public string RevOrg { get; set; }
        public Uri RevOrgRef { get; set; }
        public string RevPerson { get; set; }
        public Uri RevPersonRef { get; set; }
        public string RevTool { get; set; }
        public Uri RevToolRef { get; set; }
        public string SubFs { get; set; }
        public Uri TaClassRef { get; set; }
        public float TaConfidence { get; set; }
        public string TaIdent { get; set; }
        public Uri TaIdentRef { get; set; }
        public string TaSource { get; set; }
        public float TermConfidence { get; set; }
        public string Tool { get; set; }
        public Uri ToolRef { get; set; }
        public bool Translate { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}