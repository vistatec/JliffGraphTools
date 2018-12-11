using System.Collections.Generic;
using Jliff.Graph.Modules.ITS;
using Newtonsoft.Json;

namespace Jliff.Graph.Modules.ChangeTrack
{
    public class ChangeTrack
    {
        public ChangeTrack()
        {
            
        }

        [JsonProperty("its_annotatorsRef")]
        public AnnotatorsRef AnnotatorsRef { get; set; }
        public Revisions Revisions { get; set; } 
    }
}