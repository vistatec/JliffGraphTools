using Newtonsoft.Json;

namespace Jliff.Graph.Modules.ITS
{
    public class ProvenanceRecord
    {
        [JsonProperty("its_person")]
        public string Person { get; set; }
        [JsonProperty("its_revPerson")]
        public string RevPerson { get; set; }
        [JsonProperty("its_tool")]
        public string Tool { get; set; }
        [JsonProperty("its_toolRef")]
        public string ToolRef { get; set; }
    }
}