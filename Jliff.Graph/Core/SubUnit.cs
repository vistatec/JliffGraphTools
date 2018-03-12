using System.Collections.Generic;

namespace Localization.Jliff.Graph
{
    public class Subunit : ISubunit
    {
        public Subunit(string id, List<ISubunit> segments)
        {
            Id = id;
            Segments = segments;
        }

        public Subunit(string id, ISubunit segment)
        {
            Id = id;
            Segments = new List<ISubunit>();
            Segments.Add(segment);
        }

        public bool CanResegment { get; set; }

        public string Id { get; set; }

        public List<ISubunit> Segments { get; set; }
        public List<IElement> Source { get; set; }
        public string State { get; set; }
        public List<IElement> Target { get; set; }

        public string Type { get; set; }
    }
}