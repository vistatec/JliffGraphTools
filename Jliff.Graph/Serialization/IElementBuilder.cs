using Localization.Jliff.Graph;

namespace Jliff.Graph.Serialization
{
    public interface IElementBuilder
    {
        IElementBuilder AddEmElement(string id, bool forSource);
        IElementBuilder AddSource(XlfEventArgs args);
        IElementBuilder AddSource(string text);
        IElementBuilder AddSmElement(string id, bool forSource);
        IElementBuilder AddTarget(XlfEventArgs args);
        IElementBuilder AddTarget(string text);
        JliffDocument Build();
        ISubUnitBuilder MoreSubUnits();
    }
}