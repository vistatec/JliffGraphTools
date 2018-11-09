using System.Collections.Generic;
using Localization.Jliff.Graph;
using Localization.Jliff.Graph.Interfaces;

namespace Jliff.Graph.Serialization
{
    public interface ISubfileBuilder
    {
        ISubfileBuilder Unit(FilterEventArgs args);
        ISubfileBuilder Group(FilterEventArgs args);
        ISubUnitBuilder EndSubFiles();
    }
}