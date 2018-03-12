using System.Collections.Generic;

namespace Localization.Jliff.Graph
{
    public class Metaitem : Dictionary<string, string>, IMetadata
    {
        public Metaitem()
        {
        }

        public Metaitem(string type, string value)
        {
            Add(type, value);
        }
    }
}