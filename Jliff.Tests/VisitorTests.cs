using System.Collections.Generic;
using Jliff.Graph.Core;
using Jliff.Graph.Serialization;
using Localization.Jliff.Graph;
using Localization.Jliff.Graph.Modules.ResourceData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class VisitorTests
    {
        [TestInitialize]


        [TestMethod]
        public void VisitFile()
        {
            ResourceData rd1 = new ResourceData("rd1",
                new ResourceItem("ri1", new Source("https://open.vistatec.com/ocelot")));

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

            jlb.Files[0].ResourceData = rd1;

            TestVisitor testVisitor = new TestVisitor();
            jlb.Files[0].Accept(testVisitor);
            (jlb.Files[0].Subfiles[0] as Unit).Accept(testVisitor);
        }

        [TestMethod]
        public void VisitSegments()
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

            SegmentVisitor sv = new SegmentVisitor();
            jlb.Files[0].Process(sv);
            Assert.AreEqual<int>(3, sv.Segments.Count);

            sv.Segments[2].Target = new List<IElement>
            {
                new TextElement("So long")
            };

            Unit u = jlb.Files[0].Subfiles[0] as Unit;
            Segment s = u.Subunits[2] as Segment;
            TextElement t = s.Target[0] as TextElement;
            Assert.AreEqual<string>("So long", t.Text);
        }
    }
}