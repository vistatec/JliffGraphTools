using Localization.Jliff.Graph;

namespace Jliff.Graph.Serialization
{
    public interface IElementBuilder
    {
        IElementBuilder Source(FilterEventArgs args);
        IElementBuilder Target(FilterEventArgs args);
        JliffDocument Build();
    }
}