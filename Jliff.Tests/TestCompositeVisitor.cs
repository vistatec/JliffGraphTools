using System.Text;
using Jliff.Graph.Interfaces;
using Localization.Jliff.Graph;

namespace UnitTests
{
    public class TestCompositeVisitor : ICompositeVisitor
    {
        public StringBuilder sb = new StringBuilder();

        public void Visit(JlfNode node)
        {
            switch (node)
            {
                case File f:
                    sb.AppendLine(f.Id);
                    break;
                case Group g:
                    sb.AppendLine(g.Id);
                    break;
                case Segment s:
                    sb.AppendLine(s.Id);
                    break;
                case Unit u:
                    sb.AppendLine(u.Id);
                    break;
                default:
                    break;
            }
        }

    }
}