using System;
using System.Collections.Generic;
using Jliff.Graph.Interfaces;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class ScElement : JlfNode, IElement
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
                            CanCopy = (Enumerations.YesNo) Enum.Parse(typeof(Enumerations.YesNo), att.Value);
                            break;
                        case "canDelete":
                            CanDelete = (Enumerations.YesNo) Enum.Parse(typeof(Enumerations.YesNo), att.Value);
                            break;
                        case "canOverlap":
                            CanOverlap = (Enumerations.YesNo) Enum.Parse(typeof(Enumerations.YesNo), att.Value);
                            break;
                        case "canReorder":
                            CanReorder =
                                (Enumerations.YesNoFirstNo) Enum.Parse(typeof(Enumerations.YesNoFirstNo), att.Value);
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
                            Isolated = (Enumerations.YesNo) Enum.Parse(typeof(Enumerations.YesNo), att.Value);
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

        public Enumerations.YesNo CanCopy { get; set; }
        public Enumerations.YesNo CanDelete { get; set; }
        public Enumerations.YesNo CanOverlap { get; set; }
        public Enumerations.YesNoFirstNo CanReorder { get; set; }
        public string CopyOf { get; set; }
        public string DataRef { get; set; }
        public string Dir { get; set; }
        public string Disp { get; set; }
        public string Equiv { get; set; }
        public string EquivStorage { get; set; }
        public Enumerations.FormatStyle Fs { get; set; }
        public string Id { get; set; }
        public Enumerations.YesNo Isolated { get; set; }

        public override string Kind => Enumerations.JlfNodeType.sc.ToString();

        public string ProfileData { get; set; }
        public string ProfileSizeInfo { get; set; }
        public string ProfileSizeInfoRef { get; set; }
        public string SubFlows { get; set; }
        public string SubFs { get; set; }
        public string SubType { get; set; }
        public string Type { get; set; }

        public override void Process(ICompositeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}