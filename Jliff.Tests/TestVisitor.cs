using Jliff.Graph.Interfaces;
using Localization.Jliff.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    public class TestVisitor : IVisitor
    {
        public void Visit(File file)
        {
            Assert.AreEqual<string>("f1", file.Id);
        }

        public void Visit(Group group)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Unit unit)
        {
            Assert.AreEqual<string>("u1", unit.Id);
        }
    }
}