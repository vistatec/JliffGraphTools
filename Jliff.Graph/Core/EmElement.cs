using System.Collections.Generic;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class EmElement : IElement
    {
        private string kind;

        [JsonIgnore]
        public IDictionary<string, string> Attributes
        {
            get
            {
                Dictionary<string, string> atts = new Dictionary<string, string>();
                atts.Add("startRef", StartRef);
                return atts;
            }

            set
            {
                IDictionary<string, string> atts = value;
                foreach (KeyValuePair<string, string> att in atts)
                    switch (att.Key)
                    {
                        case "startRef":
                            StartRef = att.Value;
                            break;
                    }
            }
        }

        public string Kind => ElementKind.em.ToString();

        public string StartRef { get; set; }
    }
}