using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Localization.Jliff.Graph
{
    public class Metadata
    {
        public List<MetaGroup> Groups = new List<MetaGroup>();
        public string Id { get; set; }

        private string prefix => "mda";

        [OnSerializing]
        internal void OnSerializing(StreamingContext context)
        {
            int g = 8;
        }
    }
}