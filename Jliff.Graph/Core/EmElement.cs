using System.Collections.Generic;
using Jliff.Graph.Interfaces;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class EmElement : JlfNode, IElement
    {
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

        public override string Kind => Enumerations.JlfNodeType.em.ToString();

        public string StartRef { get; set; }

        public override void Process(ICompositeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}