using System.Collections.Generic;
using Jliff.Graph.Core;

namespace Jliff.Graph.Modules.ITS
{
    public class LocQualityIssues
    {
        public Nmtoken Id { get; set; }
        public List<LocQualityIssue> Items { get; set; }
    }
}