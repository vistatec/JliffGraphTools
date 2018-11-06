using System.Collections.Generic;

namespace Localization.Jliff.Graph.Modules.Metadata
{
    public class MetaGroup : IMetadata
    {
        public List<IMetadata> Meta = new List<IMetadata>();
        public string AppliesTo { get; set; }
        public string Category { get; set; }
        public string Id { get; set; }
    }
}