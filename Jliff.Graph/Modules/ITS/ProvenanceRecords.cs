using System;
using System.Collections.Generic;
using System.Text;
using Localization.Jliff.Graph.Core;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph.Modules.ITS
{
    public class ProvenanceRecords
    {
        public List<ProvenanceRecord> Items { get; set; }

        [JsonProperty("its_id")]
        public Ncname Id { get; set; }
    }
}
