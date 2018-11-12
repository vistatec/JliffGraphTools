using Localization.Jliff.Graph;

namespace Jliff.Graph.Interfaces
{
    public interface ICompositeVisitor
    {
        void Visit(JlfNode node);
    }
}