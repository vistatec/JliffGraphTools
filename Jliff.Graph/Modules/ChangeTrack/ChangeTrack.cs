using System.Collections.Generic;
using Jliff.Graph.Modules.ITS;

namespace Jliff.Graph.Modules.ChangeTrack
{
    public class ChangeTrack
    {
        public ChangeTrack()
        {
            
        }

        public AnnotatorsRef AnnotatorsRef { get; set; }
        public Revisions Revisions { get; set; } 
    }
}