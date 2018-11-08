using Jliff.Graph.Core;

namespace Jliff.Graph.Modules.ChangeTrack
{
    public class Revision
    {
        public Revision()
        {
            
        }

        public string Author { get; set; }
        public string DateTime { get; set; }
        public Nmtoken Version { get; set; }
        public RevisionItem Item { get; set; }
    }
}