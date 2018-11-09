using System.Collections.Generic;

namespace Localization.Jliff.Graph.Modules.ResourceData
{
    public class ResourceData
    {
        public string Id { get; set; }
        public List<ResourceItem> ResourceItems { get; set; } = new List<ResourceItem>();
        public List<ResourceItemRef> ResourceItemRefs { get; set; } = new List<ResourceItemRef>();
    }
}