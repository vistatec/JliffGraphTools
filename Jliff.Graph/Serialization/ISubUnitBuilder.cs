using Jliff.Graph.Serialization;

namespace Localization.Jliff.Graph
{
    public interface ISubUnitBuilder
    {
        ISubUnitBuilder Segment(FilterEventArgs args);
        IElementBuilder EndSubUnits();
    }
}