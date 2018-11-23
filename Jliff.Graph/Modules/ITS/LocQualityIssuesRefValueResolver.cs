using System;
using System.Collections.Generic;
using AutoMapper;
using Localization.Jliff.Graph;

namespace Jliff.Graph.Modules.ITS
{
    public class LocQualityIssuesRefValueResolver : IValueResolver<FilterEventArgs, IElement, string>
    {
        public string Resolve(FilterEventArgs source, IElement destination, string destMember, ResolutionContext context)
        {
            if (source.Attributes.ContainsKey("its:locQualityIssuesRef")) return source.Attributes["its:locQualityIssuesRef"];
            if (source.Attributes.ContainsKey("locQualityIssuesRef")) return source.Attributes["locQualityIssuesRef"];
            return String.Empty;
        }
    }
}