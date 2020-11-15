using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph.Modules.Validation
{
    public class Rule
    {
        [JsonProperty("val_isPresent")]
        public string IsPresent { get; set; }

        [JsonProperty("val_occurs")]
        public int Occurs { get; set; }

        [JsonProperty("val_existsInSource")]
        public string ExistsInSource { get; set; }

        [JsonProperty("val_caseSensitive")]
        public string CaseSensitive { get; set; }

        [JsonProperty("val_normalization")]
        public string Normalization { get; set; }

        [JsonProperty("val_disabled")]
        public string Disabled { get; set; }
    }
}
