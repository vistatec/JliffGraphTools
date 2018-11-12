using System.Collections.Generic;
using Jliff.Graph.Interfaces;
using Localization.Jliff.Graph;

namespace Jliff.Graph.Core
{
    public class SegmentVisitor : ICompositeVisitor
    {
        public List<Segment> Segments = new List<Segment>();

        public void Visit(JlfNode node)
        {
            switch (node)
            {
                case Segment s:
                    Segments.Add(s);
                    break;
            }
        }
    }
}