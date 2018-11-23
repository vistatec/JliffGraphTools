using System;
using AutoMapper;
using Localization.Jliff.Graph;

namespace Jliff.Graph.Modules.ITS
{
    public class ProvenanceRecordsRefValueResolver : IValueResolver<FilterEventArgs, IElement, string>
    {
        public string Resolve(FilterEventArgs source, IElement destination, string destMember, ResolutionContext context)
        {
            if (source.Attributes.ContainsKey("its:provenanceRecordsRef")) return source.Attributes["its:provenanceRecordsRef"];
            if (source.Attributes.ContainsKey("provenanceRecordsRef")) return source.Attributes["provenanceRecordsRef"];
            return String.Empty;
        }

    }
}