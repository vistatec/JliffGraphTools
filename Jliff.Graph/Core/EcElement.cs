using System.Collections.Generic;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class EcElement : AbstractElement, IElement
    {
        public EcElement()
        {
        }

        public EcElement(string text)
        {
            Text = text;
        }

        [JsonIgnore]
        public IDictionary<string, string> Attributes
        {
            get
            {
                Dictionary<string, string> atts = new Dictionary<string, string>();
                atts.Add("CanCopy", CanCopy.ToString());
                atts.Add("CanDelete", CanDelete.ToString());
                atts.Add("CanOverlap", CanOverlap.ToString());
                atts.Add("CanReorder", CanReorder);
                atts.Add("CopyOf", CopyOf);
                atts.Add("DataRef", DataRef);
                atts.Add("Disp", Disp);
                atts.Add("Equiv", Equiv);
                atts.Add("Id", Id);
                atts.Add("Isolated", Isolated.ToString());
                atts.Add("Kind", Kind);
                atts.Add("Subflows", SubFlows);
                atts.Add("SubType", SubType);
                return atts;
            }

            set
            {
                IDictionary<string, string> atts = value;
                foreach (KeyValuePair<string, string> att in atts)
                    switch (att.Key)
                    {
                        case "canCopy":
                            CanCopy = bool.Parse(att.Value);
                            break;
                        case "canDelete":
                            CanDelete = bool.Parse(att.Value);
                            break;
                        case "canOverlap":
                            CanOverlap = bool.Parse(att.Value);
                            break;
                        case "canReorder":
                            CanReorder = att.Value;
                            break;
                        case "copyOf":
                            CopyOf = att.Value;
                            break;
                        case "dataRefEnd":
                            DataRef = att.Value;
                            break;
                        case "disp":
                            Disp = att.Value;
                            break;
                        case "equiv":
                            Equiv = att.Value;
                            break;
                        case "id":
                            Id = att.Value;
                            break;
                        case "isolated":
                            Isolated = bool.Parse(att.Value);
                            break;
                        case "subFlows":
                            SubFlows = att.Value;
                            break;
                        case "subType":
                            SubType = att.Value;
                            break;
                    }
            }
        }

        public bool CanCopy { get; set; }
        public bool CanDelete { get; set; }
        public bool? CanOverlap { get; set; }
        public string CanReorder { get; set; }
        public string CopyOf { get; set; }
        public string DataRef { get; set; }
        public string Disp { get; set; }
        public string Equiv { get; set; }
        public string Id { get; set; }
        public bool Isolated { get; set; }

        public string Kind => Enumerations.ElementKind.ec.ToString();
        public string StartRef { get; set; }

        public string SubFlows { get; set; }
        public string SubType { get; set; }
        public string Text { get; set; }
    }
}