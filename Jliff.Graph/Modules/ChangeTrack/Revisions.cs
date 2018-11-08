using System.Collections.Generic;
using Jliff.Graph.Core;

namespace Jliff.Graph.Modules.ChangeTrack
{
    public class Revisions
    {
        public Revisions()
        {
            
        }

        public Nmtoken AppliesTo { get; set; }
        public Nmtoken CurrentVersion { get; set; }
        public Nmtoken Ref { get; set; }
        public List<Revision> Items { get; set; } = new List<Revision>();

    }
}