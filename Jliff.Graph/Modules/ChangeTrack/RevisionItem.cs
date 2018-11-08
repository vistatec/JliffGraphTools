using Jliff.Graph.Core;

namespace Jliff.Graph.Modules.ChangeTrack
{
    public class RevisionItem
    {
        public RevisionItem()
        {
            
        }

        public string Author { get; set; }
        public string DateTime { get; set; }
        public Nmtoken Version { get; set; }
        public RevisionItemText Item { get; set; }
    }
}