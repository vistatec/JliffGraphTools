﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class PhElement : AbstractElement, IElement
    {
        public PhElement()
        {
        }

        public PhElement(string id)
        {
            Id = id;
        }

        [JsonIgnore]
        public IDictionary<string, string> Attributes
        {
            get
            {
                Dictionary<string, string> atts = new Dictionary<string, string>();
                atts.Add("CanOverlap", CanOverlap.ToString());
                atts.Add("CanReorder", CanReorder);
                atts.Add("CopyOf", CopyOf);
                atts.Add("DataRef", DataRef);
                atts.Add("Disp", Disp);
                atts.Add("Equiv", Equiv);
                atts.Add("Id", Id);
                atts.Add("Kind", Kind);
                atts.Add("Subflows", SubFlows);
                atts.Add("SubType", SubType);
                atts.Add("type", Type);
                return atts;
            }

            set
            {
                IDictionary<string, string> atts = value;
                foreach (KeyValuePair<string, string> att in atts)
                    switch (att.Key)
                    {
                        case "canOverlap":
                            CanOverlap = bool.Parse(att.Value);
                            break;
                        case "canReorder":
                            CanReorder = att.Value;
                            break;
                        case "copyOf":
                            CopyOf = att.Value;
                            break;
                        case "dataRef":
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
                        case "subFlows":
                            SubFlows = att.Value;
                            break;
                        case "subType":
                            SubType = att.Value;
                            break;
                        case "type":
                            Type = att.Value;
                            break;
                    }
            }
        }


        public bool CanOverlap { get; set; }
        public string CanReorder { get; set; }
        public string CopyOf { get; set; }
        public string DataRef { get; set; }
        public string Disp { get; set; }
        public string Equiv { get; set; }
        public string Id { get; set; }

        public string Kind => ElementKind.ph.ToString();

        public string SubFlows { get; set; }
        public string SubType { get; set; }
        public string Type { get; set; }
    }
}