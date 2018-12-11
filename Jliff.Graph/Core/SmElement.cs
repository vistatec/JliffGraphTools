using System;
using System.Collections.Generic;
using Jliff.Graph.Core;
using Jliff.Graph.Interfaces;
using Jliff.Graph.Modules.ITS;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class SmElement : JlfNode, IElement
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
		[JsonProperty("its_annotatorsRef")]
		public AnnotatorsRef AnnotatorsRef { get; set; }
		public List<Domain> Domains { get; set; }
        public string Id { get; set; }

        public override string Kind => Enumerations.JlfNodeType.sm.ToString();

        public Enumerations.FormatStyle fs { get; set; }
        public string LocaleFilterList { get; set; }
        public string LocaleFilterType { get; set; }
        public string LocQualityIssueComment { get; set; }
        public string LocQualityIssueEnabled { get; set; }
        public string LocQualityIssueProfileRef { get; set; }
        public string LocQualityIssuesRef { get; set; }
        public float LocQualityIssueSeverity { get; set; }
        public string LocQualityIssueType { get; set; }
        [JsonProperty("its_locQualityRatingProfileRef")]
		public string LocQualityRatingProfileRef { get; set; }
        [JsonProperty("its_locQualityRatingScore")]
		public float LocQualityRatingScore { get; set; }
        [JsonProperty("its_locQualityRatingScoreThreshold")]
		public float LocQualityRatingScoreThreshold { get; set; }
        [JsonProperty("its_locQualityRatingVote")]
		public int LocQualityRatingScoreVote { get; set; }
        [JsonProperty("its_locQualityRatingVoteThreshold")]
		public int LocQualityRatingScoreVoteThreshold { get; set; }
        public float MtConfidence { get; set; }
        [JsonProperty("its_org")]
		public string Org { get; set; }
        [JsonProperty("its_orgRef")]
		public string OrgRef { get; set; }
        [JsonProperty("its_person")]
		public string Person { get; set; }
        [JsonProperty("its_personRef")]
		public string PersonRef { get; set; }
        public string ProfileStorageRestriction { get; set; }
        public string ProfileSizeRestriction { get; set; }

        [JsonProperty("its_provenanceRecordsRef")]
		public string ProvenanceRecordsRef { get; set; }
        public Iri Ref { get; set; }
        [JsonProperty("its_revOrg")]
		public string RevOrg { get; set; }
        [JsonProperty("its_revOrgRef")]
		public string RevOrgRef { get; set; }
        [JsonProperty("its_revPerson")]
		public string RevPerson { get; set; }
        [JsonProperty("its_revPersonRef")]
		public string RevPersonRef { get; set; }
        [JsonProperty("its_revTool")]
		public string RevTool { get; set; }
        [JsonProperty("its_revToolRef")]
		public string RevToolRef { get; set; }
		[JsonProperty("fs_subFs")]
		public string SubFs { get; set; }
		[JsonProperty("its_taClassRef")]
		public string TaClassRef { get; set; }
		[JsonProperty("its_taConfidence")]
		public float TaConfidence { get; set; }
		[JsonProperty("its_taIdent")]
        public string TaIdent { get; set; }
		[JsonProperty("its_taIdentRef")]
        public string TaIdentRef { get; set; }
		[JsonProperty("its_taSource")]
		public string TaSource { get; set; }
        public bool Translate { get; set; }
        public float TermConfidence { get; set; }
		[JsonProperty("its_tool")]
		public string Tool { get; set; }
		[JsonProperty("its_toolRef")]
		public string ToolRef { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

        public override void Process(ICompositeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}