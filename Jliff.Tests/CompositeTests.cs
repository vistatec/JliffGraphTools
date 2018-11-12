using System;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using Jliff.Graph.Serialization;
using Localization.Jliff.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class CompositeTests
    {
        [TestMethod]
        public void Traverse()
        {
            JliffDocument jlb = new JliffBuilder("en-US", "it-IT")
                .AddFile(new XlfEventArgs("f1"))
                .AddUnit(new XlfEventArgs("u1"))
                .EndSubFiles()
                .AddSegment(new XlfEventArgs("s1"))
                .EndSubUnits()
                .AddSource("Hello")
                .AddSmElement("mrk1", true)
                .AddSource("there")
                .AddEmElement("", true)
                .AddTarget("Buongiorno")
                .MoreSubUnits()
                .AddSegment(new XlfEventArgs("s2"))
                .EndSubUnits()
                .AddSource("Congratulations")
                .MoreSubUnits()
                .AddSegment(new XlfEventArgs("s3"))
                .EndSubUnits()
                .AddSource("Goodbye")
                .AddTarget("Arrivederci")
                .Build();

            string h = jlb.Files[0].Traverse(NullMethod);
        }

        public delegate string op();

        public string NullMethod()
        {
            return "";
        }

        [TestMethod]
        public void Process()
        {
            JliffDocument jlb = new JliffBuilder("en-US", "it-IT")
                .AddFile(new XlfEventArgs("f1"))
                .AddUnit(new XlfEventArgs("u1"))
                .EndSubFiles()
                .AddSegment(new XlfEventArgs("s1"))
                .EndSubUnits()
                .AddSource("Hello")
                .AddSmElement("mrk1", true)
                .AddSource("there")
                .AddEmElement("", true)
                .AddTarget("Buongiorno")
                .MoreSubUnits()
                .AddSegment(new XlfEventArgs("s2"))
                .EndSubUnits()
                .AddSource("Congratulations")
                .MoreSubUnits()
                .AddSegment(new XlfEventArgs("s3"))
                .EndSubUnits()
                .AddSource("Goodbye")
                .AddTarget("Arrivederci")
                .Build();

            TestCompositeVisitor tcv = new TestCompositeVisitor();
            jlb.Files[0].Process(tcv);
            var visitedIds = tcv.sb.ToString();
            Assert.AreEqual<string>("f1\r\nu1\r\ns1\r\ns2\r\ns3\r\n", visitedIds);
        }
    }
}