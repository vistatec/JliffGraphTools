using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class Segment : ISubunit
    {
        [JsonProperty(Order = 10)]
        public List<IElement> Source = new List<IElement>();

        [JsonProperty(Order = 20)]
        public List<IElement> Target = new List<IElement>();

        public Segment()
        {
        }

        public Segment(string id)
        {
            Id = id;
        }

        [JsonConstructor]
        public Segment(List<IElement> source, List<IElement> target)
        {
            Source = source;
            Target = target;
        }

        public Segment(IElement source, IElement target = null)
        {
            Source.Add(source);
            if (target != null) Target.Add(target);
        }

        public string CanResegment { get; set; } = "no";

        public string Id { get; set; }

        public string Kind => "segment";

        public string State { get; set; }

        public string GetTargetTextAt(int index)
        {
            if (Target[index] is TextElement)
                return (Target[index] as TextElement).Text;
            throw new InvalidOperationException();
        }
    }
}