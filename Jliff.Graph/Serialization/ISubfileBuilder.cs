using System.Collections.Generic;
using Localization.Jliff.Graph;
using Localization.Jliff.Graph.Interfaces;

namespace Jliff.Graph.Serialization
{
    public interface ISubfileBuilder
    {
        ISubfileBuilder AddUnit(XlfEventArgs args);
        ISubfileBuilder AddGroup(XlfEventArgs args);
        ISubUnitBuilder EndSubFiles();
    }
}