using System.Collections.Generic;
using Localization.Jliff.Graph;

namespace Localization.Jliff.Graph.Modules.ResourceData
{
    public class ResourceItem
    {
        public ResourceItem()
        {
            
        }

        public ResourceItem(string id, Source source)
        {
            Id = id;
            Source = source;
        }

        public string Id { get; set; }
        public string MimeType { get; set; }
        public Enumerations.YesNo Context { get; set; }
        public Source Source { get; set; }
    }
}