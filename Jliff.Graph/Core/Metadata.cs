using System.Collections.Generic;

namespace Localization.Jliff.Graph
{
    public class Metadata
    {
        public List<MetaGroup> Groups = new List<MetaGroup>();
        public string Id { get; set; }
    }
}