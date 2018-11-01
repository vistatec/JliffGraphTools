using System;
using System.Collections.Generic;
using Localization.Jliff.Graph.Interfaces;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class File
    {
        [JsonProperty(Order = 10)]
        public List<ISubfile> Subfiles = new List<ISubfile>();

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

        public object AnnotatorsRef { get; set; }
        public object ChangeTrack { get; set; }
        public string Fs { get; set; }

        public string Id { get; set; }
        public string LocQualityRatingProfileRef { get; set; }
        public float LocQualityRatingScore { get; set; }
        public float LocQualityRatingScoreThreshold { get; set; }
        public float LocQualityRatingVote { get; set; }
        public float LocQualityRatingVoteThreshold { get; set; }
        public List<MetaGroup> Metadata { get; set; }
        public string Org { get; set; }
        public string OrgRef { get; set; }
        public string Original { get; set; }
        public string Person { get; set; }
        public string PersonRef { get; set; }
        public object ProfileData { get; set; }
        public object Profiles { get; set; }
        public string ProfileSizeInfo { get; set; }
        public string ProfileSizeInfoRef { get; set; }
        public string ProfileSizeRestriction { get; set; }
        public string ProfileStorageRestriction { get; set; }
        public List<object> ProvenanceRecords { get; set; }
        public string ProvenanceRecordsRef { get; set; }
        public object ResourceData { get; set; }
        public string RevOrg { get; set; }
        public Uri RevOrgRef { get; set; }
        public string RevPerson { get; set; }
        public string RevPersonRef { get; set; }
        public string RevTool { get; set; }
        public string RevToolRef { get; set; }
        public Skeleton Skeleton { get; set; }
        public string SubFs { get; set; }
        public string TaClassRef { get; set; }
        public float TaConfidence { get; set; }
        public string TaIdent { get; set; }
        public Uri TaIdentRef { get; set; }
        public string TaSource { get; set; }
        public string Tool { get; set; }
        public string ToolRef { get; set; }
        public string Validation { get; set; }
        public object Domains { get; set; }
        public object Userdata { get; set; }
    }
}