using System.Collections.Generic;
using Jliff.Graph.Interfaces;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class Ignorable : JlfNode, ISubunit
    {
        [JsonProperty(Order = 10)]
        public List<IElement> Source = new List<IElement>();

        [JsonProperty(Order = 20)]
        public List<IElement> Target = new List<IElement>();

        public Ignorable(string id, IElement source = null, IElement target = null)
        {
            Id = id;
            if (source != null) Source.Add(source);
            if (target != null) Target.Add(target);
        }

        public Ignorable(IElement source = null, IElement target = null)
        {
            if (source != null) Source.Add(source);
            if (target != null) Target.Add(target);
        }

        public string Id { get; set; }

        public override string Kind => Enumerations.JlfNodeType.ignorable.ToString();

        public override void Process(ICompositeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}