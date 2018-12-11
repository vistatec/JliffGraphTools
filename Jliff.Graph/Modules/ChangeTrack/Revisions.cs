using System.Collections.Generic;
using Jliff.Graph.Core;
using Jliff.Graph.Modules.ITS;
using Newtonsoft.Json;

namespace Jliff.Graph.Modules.ChangeTrack
{
    public class Revisions
    {
        public Revisions()
        {
            
        }

        [JsonProperty("its_annotatorsRef")]
        public AnnotatorsRef AnnotatorsRef { get; set; }
        public Nmtoken AppliesTo { get; set; }
        public Nmtoken CurrentVersion { get; set; }
        public Nmtoken Ref { get; set; }
        public List<Revision> Items { get; set; } = new List<Revision>();

    }
}