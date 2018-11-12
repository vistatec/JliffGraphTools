using System.Linq.Expressions;
using Jliff.Graph.Interfaces;

namespace Localization.Jliff.Graph
{
    public abstract class JlfNode
    {
        public virtual string Kind { get; set; }
    }
}