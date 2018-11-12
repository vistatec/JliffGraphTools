using System;
using Jliff.Graph.Interfaces;

namespace Localization.Jliff.Graph
{
    public abstract class JlfNode
    {
        public virtual bool HasChildren { get; }
        public abstract string Kind { get; }
        public virtual string Traverse(Func<string> func) { return String.Empty; }
        public abstract void Process(ICompositeVisitor visitor);
    }
}