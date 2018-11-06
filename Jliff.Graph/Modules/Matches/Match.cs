using Localization.Jliff.Graph;
using Localization.Jliff.Graph.Modules.Metadata;

namespace Jliff.Graph.Modules.Matches
{
    public class Match
    {
        public string Domains { get; set; }
        public string Id { get; set; }
        public string LocQualityRatingProfileRef { get; set; }
        public float LocQualityRatingScore { get; set; }
        public float LocQualityRatingScoreThreshold { get; set; }
        public int LocQualityRatingVote { get; set; }
        public int LocQualityRatingVoteThreshold { get; set; }
        public int MatchQuality { get; set; }
        public int MatchSuitability { get; set; }
        public Metadata Metadata { get; set; }
        public string Origin { get; set; }
        public string OriginalData { get; set; }
        public string Ref { get; set; }
        public Enumerations.YesNo Reference { get; set; }
        public int Similarity { get; set; }
        public IElement Source { get; set; }
        public string SubType { get; set; }
        public IElement Target { get; set; }
        public Enumerations.YesNo Translate { get; set; }
        public Enumerations.MatchType Type { get; set; }
    }
}