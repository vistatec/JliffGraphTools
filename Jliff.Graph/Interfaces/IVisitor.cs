using Localization.Jliff.Graph;

namespace Jliff.Graph.Interfaces
{
    public interface IVisitor
    {
        void Visit(File file);
        void Visit(Group group);
        void Visit(Unit unit);
    }
}