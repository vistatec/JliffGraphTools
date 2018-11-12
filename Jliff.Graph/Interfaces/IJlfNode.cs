using Localization.Jliff.Graph;

namespace Jliff.Graph.Interfaces
{
    public interface IJlfNode
    {
        void Accept(IVisitor visitor);
        //JlfNode NodeAccept(IVisitor visitor);
    }
}