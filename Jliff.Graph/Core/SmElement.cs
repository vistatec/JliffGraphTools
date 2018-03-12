using System.Collections.Generic;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class SmElement : IElement
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

        public string Id { get; set; }

        public string Kind => ElementKind.sm.ToString();

        public bool Translate { get; set; }

        public string Type { get; set; }
    }
}