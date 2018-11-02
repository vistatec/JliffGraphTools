using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class ScElement : AbstractElement, IElement
    {
        public ScElement()
        {
        }

        public ScElement(string id)
        {
            Id = id;
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
                atts.Add("CanReorder", CanReorder.ToString());
                atts.Add("CopyOf", CopyOf);
                atts.Add("DataRef", DataRef);
                atts.Add("Dir", Dir);
                atts.Add("Disp", Disp);
                atts.Add("Equiv", Equiv);
                atts.Add("Id", Id);
                atts.Add("Isolated", Isolated.ToString());
                atts.Add("Kind", Kind);
                atts.Add("Subflows", SubFlows);
                atts.Add("SubType", SubType);
                atts.Add("Type", Type);
                return atts;
            }

            set
            {
                IDictionary<string, string> atts = value;
                foreach (KeyValuePair<string, string> att in atts)
                    switch (att.Key)
                    {
                        case "canCopy":
                            CanCopy = (YesNo) Enum.Parse(typeof(YesNo), att.Value);
                            break;
                        case "canDelete":
                            CanDelete = (YesNo) Enum.Parse(typeof(YesNo), att.Value);
                            break;
                        case "canOverlap":
                            CanOverlap = (YesNo) Enum.Parse(typeof(YesNo), att.Value);
                            break;
                        case "canReorder":
                            CanReorder = (YesNoFirstNo) Enum.Parse(typeof(YesNoFirstNo), att.Value);
                            break;
                        case "copyOf":
                            CopyOf = att.Value;
                            break;
                        case "dataRef":
                            DataRef = att.Value;
                            break;
                        case "dir":
                            Dir = att.Value;
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
                            Isolated = (YesNo) Enum.Parse(typeof(YesNo), att.Value);
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

        public YesNo CanCopy { get; set; }
        public YesNo CanDelete { get; set; }
        public YesNo CanOverlap { get; set; }
        public YesNoFirstNo CanReorder { get; set; }
        public string CopyOf { get; set; }
        public string DataRef { get; set; }
        public string Dir { get; set; }
        public string Disp { get; set; }
        public string Equiv { get; set; }
        public FormatStyle Fs { get; set; }
        public string Id { get; set; }
        public YesNo Isolated { get; set; }

        public string Kind => ElementKind.sc.ToString();
        public string ProfileData { get; set; }
        public string ProfileEquivStorage { get; set; }
        public string ProfileSizeInfo { get; set; }
        public string ProfileSizeInfoRef { get; set; }
        public string SubFlows { get; set; }
        public string SubFs { get; set; }
        public string SubType { get; set; }
        public string Type { get; set; }
    }
}