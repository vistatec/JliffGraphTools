using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Localization.Jliff.Graph.Core;
using Localization.Jliff.Graph.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using JlGraph = Localization.Jliff.Graph;

namespace UnitTests
{
    [TestClass]
    public class ModelTests
    {
        private static string schemaDef = string.Empty;

        [TestMethod]
        public void EnrichedXliff()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            sb.Append(
                "<xliff xmlns=\"urn:oasis:names:tc:xliff:document:2.0\" srcLang=\"en\" trgLang=\"fr\" version=\"2.0\">");
            sb.Append("<file id=\"f1\"><unit id=\"1\"><segment id=\"s1\"><source>");
            sb.Append("<mrk id=\"freme-7\" ref=\"http://freme-project.eu/#char=3,10\" type=\"term\">Text</mrk>");
            sb.Append(
                " in <sc dataRef=\"d1\" id=\"sc1\"/><mrk id=\"freme-8\" ref=\"http://freme-project.eu/#char=16,20\" type=\"term\">");
            sb.Append("bold</mrk> <sc dataRef=\"d2\" id=\"sc2\"/>");
            sb.Append(
                "      and<ec dataRef=\"d3\" startRef=\"sc1\"/> <mrk id=\"freme-9\" ref=\"http://freme-project.eu/#char=46,53\" type=\"term\">");
            sb.Append("italics</mrk><ec dataRef=\"d4\" startRef=\"sc2\"/>.");
            sb.Append(
                "</source><target>Text in <sc dataRef=\"d1\" id=\"sc1\"/>bold <sc dataRef=\"d2\" id=\"sc2\"/>\r\n     and<ec dataRef=\"d3\" startRef=\"sc1\"/> italics<ec dataRef=\"d4\" startRef=\"sc2\"/>.\r\n     </target>");
            sb.Append("</segment></unit></file></xliff>");

            JlGraph.JliffBuilder builder = new JlGraph.JliffBuilder("en", "fr");
            TextReader tr = new StringReader(sb.ToString());
            JlGraph.Xliff20Filter filter = new JlGraph.Xliff20Filter();
            filter.XlfFileEvent += builder.File;
            filter.XlfUnitEvent += builder.Unit;
            filter.XlfGroupEvent += builder.Group;
            filter.XlfSegmentEvent += builder.Segment;
            filter.XlfSourceEvent += builder.Source;
            filter.XlfTargetEvent += builder.Target;
            filter.XlfIgnorableEvent += builder.Ignorable;
            filter.XlfPhElementEvent += builder.PhElement;
            filter.XlfSkeletonEvent += builder.Skeleton;
            filter.XlfTextEvent += builder.Text;
            filter.XlfSmElementEvent += builder.SmElement;
            filter.XlfEmElementEvent += builder.EmElement;
            filter.XlfScElementEvent += builder.ScElement;
            filter.XlfEcElementEvent += builder.EcElement;
            filter.Filter(tr);
            builder.Serialize("ModelTest.json");
            JsonSchema schema = JsonSchema.Parse(schemaDef);
            var obGraph = JObject.FromObject(builder.Jliff);
            Assert.IsTrue(obGraph.IsValid(schema));
            JlGraph.Unit unit = builder.Jliff.Files[0].Subfiles[0] as JlGraph.Unit;
            Assert.IsNotNull(unit);
            JlGraph.Segment segment = unit.Subunits[0] as JlGraph.Segment;
            Assert.IsNotNull(segment);
            JlGraph.SmElement smElement = segment.Source[11] as JlGraph.SmElement;
            Assert.IsNotNull(smElement);
            Assert.AreEqual("freme-9", smElement.Id);
            Assert.AreEqual("term", smElement.Type);
        }

        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            schemaDef = File.ReadAllText(@"e:\dev\dotnet\jliff\unittests\testfiles\jliff-schema-0.9.5.json");
        }

        [TestMethod]
        public void RoundtripExample1()
        {
            JlGraph.JliffDocument model = new JlGraph.JliffDocument("en", "fr", new JlGraph.File("f1"));

            var u1 = new JlGraph.Unit("u1");

            u1.OriginalData.Add("d1", "[C1/]");
            u1.OriginalData.Add("d2", "[C2]");
            u1.OriginalData.Add("d3", "[/C2]");

            var seg = new JlGraph.Segment();
            seg.State = JlGraph.State.translated.ToString();
            seg.CanResegment = false;

            var ph1 = new JlGraph.PhElement("c1");
            ph1.DataRef = "d1";
            var tph1 = new JlGraph.PhElement("c1");
            tph1.DataRef = "d1";

            var sc1 = new JlGraph.ScElement("c2");
            sc1.DataRef = "d2";
            var tsc1 = new JlGraph.ScElement("c2");
            tsc1.DataRef = "d2";

            var te3 = new JlGraph.TextElement("text");
            var tte3 = new JlGraph.TextElement("AAA");

            var ec1 = new JlGraph.EcElement();
            ec1.DataRef = "d3";
            ec1.StartRef = "c2";
            var tec1 = new JlGraph.EcElement();
            tec1.DataRef = "d3";
            tec1.StartRef = "c2";

            var te1 = new JlGraph.TextElement("aaa");
            var tte1 = new JlGraph.TextElement("AAA");

            var te2 = new JlGraph.TextElement(". ");

            var i1 = new JlGraph.Ignorable();
            i1.Source.Add(te2);

            seg.Source.Add(ph1);
            seg.Source.Add(te1);
            seg.Source.Add(sc1);
            seg.Source.Add(te3);
            seg.Source.Add(ec1);
            seg.Target.Add(tph1);
            seg.Target.Add(tte1);
            seg.Target.Add(tsc1);
            seg.Target.Add(tte3);
            seg.Target.Add(tec1);

            u1.Subunits.Add(seg);
            u1.Subunits.Add(i1);

            model.Files[0].Subfiles.Add(u1);

            var g = new JlGraph.Group("g1");

            var u2 = new JlGraph.Unit("u2");

            var g2 = new JlGraph.Group("g2");

            model.Files[0].Subfiles.Add(g);

            g.Subgroups.Add(u2);
            g.Subgroups.Add(g2);

            model.Context = new Context21();

            var dllPath = Assembly.GetAssembly(typeof(ModelTests)).Location;
            var dllName = Assembly.GetAssembly(typeof(ModelTests)).GetName().Name;
            var outputPath = dllPath.Replace(dllName + ".dll", "");


            JlGraph.Converter.Serialize(Path.Combine(outputPath, "example1.json"), model);

            JlGraph.Converter.Deserialize(new FileInfo(Path.Combine(outputPath, "example1.json")));

            JsonSchema schema = JsonSchema.Parse(schemaDef);
            var obGraph = JObject.FromObject(model);
            Assert.IsTrue(obGraph.IsValid(schema));
        }

        [TestMethod]
        public void RoundtripExample2()
        {
            var unit1 = new JlGraph.Unit("1");
            var unit2 = new JlGraph.Unit("2");
            var unit3 = new JlGraph.Unit("3");

            var segment1 = new JlGraph.Segment(
                new JlGraph.TextElement("Quetzal"),
                new JlGraph.TextElement("Quetzal"));

            var segment2 = new JlGraph.Segment(
                new JlGraph.TextElement("An application to manipulate and process XLIFF documents"),
                new JlGraph.TextElement("XLIFF 文書を編集、または処理 するアプリケーションです。"));

            var segment3 = new JlGraph.Segment(
                new JlGraph.TextElement("XLIFF Data Manager"),
                new JlGraph.TextElement("XLIFF データ・マネージャ"));

            unit1.Subunits.Add(segment1);
            unit2.Subunits.Add(segment2);
            unit3.Subunits.Add(segment3);

            var model = new JlGraph.JliffDocument("en", "fr");
            model.Files.Add(new JlGraph.File("f1"));
            model.Files[0].Skeleton = new JlGraph.Skeleton("Graphic Example.psd.skl");
            model.Files[0].Subfiles = new List<ISubfile>
            {
                unit1,
                unit2,
                unit3
            };

            var dllPath = Assembly.GetAssembly(typeof(ModelTests)).Location;
            var dllName = Assembly.GetAssembly(typeof(ModelTests)).GetName().Name;
            var outputPath = dllPath.Replace(dllName + ".dll", "");


            JlGraph.Converter.Serialize(Path.Combine(outputPath, "example2.json"), model);

            JlGraph.Converter.Deserialize(new FileInfo(Path.Combine(outputPath, "example2.json")));

            JsonSchema schema = JsonSchema.Parse(schemaDef);
            var obGraph = JObject.FromObject(model);
            Assert.IsTrue(obGraph.IsValid(schema));
        }

        [TestMethod]
        public void RoundtripExample3()
        {
            var unit1 = new JlGraph.Unit("1");
            var unit2 = new JlGraph.Unit("2");

            var segment1 = new JlGraph.Segment(
                new JlGraph.TextElement("Quetzal"),
                new JlGraph.TextElement("Quetzal"));

            var segment2 = new JlGraph.Segment(
                new JlGraph.TextElement("An application to manipulate and process XLIFF documents"),
                new JlGraph.TextElement("XLIFF 文書を編集、または処理 するアプリケーションです。"));

            var grp = new JlGraph.MetaGroup();

            unit1.Subunits.Add(segment1);
            unit2.Subunits.Add(segment2);

            var md = new JlGraph.Metadata();
            md.Id = "md14";

            unit1.Metadata = md;

            var breakfast = new JlGraph.Metaitem("breakfast", "eggs");
            var phase = new JlGraph.Metaitem("phase", "draft");
            var version = new JlGraph.Metaitem("version", "3");

            var grp2 = new JlGraph.MetaGroup();
            grp2.Meta.Add(breakfast);

            var grp1 = new JlGraph.MetaGroup();
            grp1.Meta.Add(version);
            grp1.Meta.Add(phase);
            grp1.Meta.Add(grp2);

            md.Groups.Add(grp1);


            var model = new JlGraph.JliffDocument("en-US", "ja-JP");
            model.Files.Add(new JlGraph.File("f1"));
            model.Files[0].Skeleton = new JlGraph.Skeleton("Graphic Example.psd.skl");
            model.Files[0].Subfiles = new List<ISubfile>
            {
                unit1,
                unit2
            };

            var dllPath = Assembly.GetAssembly(typeof(ModelTests)).Location;
            var dllName = Assembly.GetAssembly(typeof(ModelTests)).GetName().Name;
            var outputPath = dllPath.Replace(dllName + ".dll", "");


            JlGraph.Converter.Serialize(Path.Combine(outputPath, "example3.json"), model);

            JlGraph.Converter.Deserialize(new FileInfo(Path.Combine(outputPath, "example3.json")));

            JsonSchema schema = JsonSchema.Parse(schemaDef);
            var obGraph = JObject.FromObject(model);
            Assert.IsTrue(obGraph.IsValid(schema));
        }

        [TestMethod]
        public void RoundtripExample4()
        {
            var u1 = new JlGraph.Unit("u1");

            var te1 = new JlGraph.TextElement("Press the ");
            var te2 = new JlGraph.SmElement("m1");
            var te3 = new JlGraph.TextElement("TAB key");
            var te4 = new JlGraph.EmElement();
            var te5 = new JlGraph.TextElement(".");

            var te6 = new JlGraph.TextElement("Drücken Sie die ");
            var te7 = new JlGraph.SmElement("m2");
            var te8 = new JlGraph.TextElement("TAB-TASTE");
            var te9 = new JlGraph.EcElement();
            var te10 = new JlGraph.TextElement(".");

            var s1 = new JlGraph.Segment();
            s1.Source.Add(te1);
            s1.Source.Add(te2);
            s1.Source.Add(te3);
            s1.Source.Add(te4);
            s1.Source.Add(te5);
            s1.Target.Add(te6);
            s1.Target.Add(te7);
            s1.Target.Add(te8);
            s1.Target.Add(te9);
            s1.Target.Add(te10);

            var unit1 = new JlGraph.Unit("u1");
            unit1.Subunits.Add(s1);

            var gloss = new JlGraph.GlossaryEntry();
            var def = new JlGraph.Definition();
            def.Source = "publicTermbase";
            def.Text = "A keyboard key that is traditionally used to insert tab characters into a document.";
            gloss.Definition = def;


            var t1 = new JlGraph.Translation("1", "myTermbase", "Tabstopptaste");
            var t2 = new JlGraph.Translation("2", "myTermbase", "TAB-TASTE");

            gloss.Translations.Add(t1);
            gloss.Translations.Add(t2);

            unit1.Glossary.Add(gloss);

            var model = new JlGraph.JliffDocument("en", "de");
            model.Files.Add(new JlGraph.File("fl"));
            model.Files[0].Subfiles = new List<ISubfile>
            {
                unit1
            };

            var dllPath = Assembly.GetAssembly(typeof(ModelTests)).Location;
            var dllName = Assembly.GetAssembly(typeof(ModelTests)).GetName().Name;
            var outputPath = dllPath.Replace(dllName + ".dll", "");


            JlGraph.Converter.Serialize(Path.Combine(outputPath, "example4.json"), model);

            JlGraph.Converter.Deserialize(new FileInfo(Path.Combine(outputPath, "example4.json")));

            JsonSchema schema = JsonSchema.Parse(schemaDef);
            var obGraph = JObject.FromObject(model);
            Assert.IsTrue(obGraph.IsValid(schema));
        }

        [TestMethod]
        public void RoundtripExample5()
        {
            JlGraph.JliffDocument model = new JlGraph.JliffDocument("en", "de",
                new JlGraph.Group("u1",
                    new JlGraph.Unit("u2",
                        new JlGraph.Segment(new List<JlGraph.IElement>
                            {
                                new JlGraph.TextElement("Press the "),
                                new JlGraph.SmElement(),
                                new JlGraph.TextElement("TAB KEY"),
                                new JlGraph.EmElement(),
                                new JlGraph.TextElement(".")
                            },
                            new List<JlGraph.IElement>
                            {
                                new JlGraph.TextElement("Drücken Sie die "),
                                new JlGraph.SmElement(),
                                new JlGraph.TextElement("TAB-TASTE"),
                                new JlGraph.EmElement(),
                                new JlGraph.TextElement(".")
                            }
                        )
                    )
                )
            );
        }
    }
}