/*
 * Copyright (C) 2018-2019, Vistatec or third-party contributors as indicated
 * by the @author tags or express copyright attribution statements applied by
 * the authors. All third-party contributions are distributed under license by
 * Vistatec.
 *
 * This file is part of JliffGraphTools.
 *
 * JliffGraphTools is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * JliffGraphTools is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with this program. If not, write to:
 *
 *     Free Software Foundation, Inc.
 *     51 Franklin Street, Fifth Floor
 *     Boston, MA 02110-1301
 *     USA
 *
 * Also, see the full LGPL text here: <http://www.gnu.org/copyleft/lesser.html>
 */


using System;
using System.Collections.Generic;
using Jliff.Graph.Interfaces;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class PhElement : JlfNode, IElement
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
                //atts.Add("CanOverlap", CanOverlap.ToString());
                atts.Add("CanReorder", CanReorder.ToString());
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
                        //case "canOverlap":
                        //    CanOverlap = Enum.Parse(typeof(YesNo), att.Value);
                        //    break;
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


        public Enumerations.YesNo CanCopy { get; set; }

        public Enumerations.YesNo CanDelete { get; set; }

        //public bool CanOverlap { get; set; }
        public Enumerations.YesNoFirstNo CanReorder { get; set; }
        public string CopyOf { get; set; }
        public string DataRef { get; set; }
        public string Disp { get; set; }
        public string Equiv { get; set; }

        public string EquivStorage { get; set; }

        [JsonProperty("fs_fs")]
        public Enumerations.FormatStyle Fs { get; set; }

        public string Id { get; set; }

        public override string Kind => Enumerations.JlfNodeType.ph.ToString();
        public string ProfileSizeInfo { get; set; }
        public string ProfileSizeInfoRef { get; set; }
        public string SubFlows { get; set; }

        [JsonProperty("fs_subFs")]
        public string SubFs { get; set; }

        public string SubType { get; set; }
        public string Type { get; set; }

        public override void Process(ICompositeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}