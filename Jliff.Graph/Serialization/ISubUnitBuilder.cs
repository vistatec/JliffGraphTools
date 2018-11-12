using Jliff.Graph.Serialization;

namespace Localization.Jliff.Graph
{
    public interface ISubUnitBuilder
    {
        ISubUnitBuilder AddSegment(XlfEventArgs args);
        IElementBuilder EndSubUnits();
    }
}