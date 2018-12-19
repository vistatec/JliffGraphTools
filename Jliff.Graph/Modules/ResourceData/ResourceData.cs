using System;
using System.Collections.Generic;

namespace Localization.Jliff.Graph.Modules.ResourceData
{
    public class ResourceData
    {
        public ResourceData()
        {
            
        }

        public ResourceData(string id, params object[] content)
        {
            Id = id;

            foreach (var parobj in content)
                if (parobj is ResourceItem)
                    ResourceItems.Add(parobj as ResourceItem);
                else if (parobj is IEnumerable<ResourceItem>)
                    foreach (var grpparobj in parobj as IEnumerable<ResourceItem>)
                        ResourceItems.Add(grpparobj);
                else
                    throw new ArgumentException();

        }

        public string Id { get; set; }
        public List<ResourceItem> ResourceItems { get; set; } = new List<ResourceItem>();
        public List<ResourceItemRef> ResourceItemRefs { get; set; } = new List<ResourceItemRef>();

        public bool ShouldSerializeResourceItemRefs()
        {
            return ResourceItemRefs.Count > 0;
        }
    }
}