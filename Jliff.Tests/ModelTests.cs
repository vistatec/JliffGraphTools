using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using Jliff.Graph.Serialization;
using Localization.Jliff.Graph;
using Localization.Jliff.Graph.Core;
using Localization.Jliff.Graph.Interfaces;
using Localization.Jliff.Graph.Modules.Metadata;
using Localization.Jliff.Graph.Modules.ResourceData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using File = System.IO.File;

namespace UnitTests
{
    [DeploymentItem(OutputFolder)]
    [TestClass]
    public class ModelTests
    {
        private static string schemaDef = string.Empty;
        private const string OutputFolder = "Output";
        private const string schemasLocation = "Schemas";


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

            JliffBuilder builder = new JliffBuilder("en", "fr");
            TextReader tr = new StringReader(sb.ToString());
            Xliff20Filter filter = new Xliff20Filter();
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
            Unit unit = builder.Jliff.Files[0].Subfiles[0] as Unit;
            Assert.IsNotNull(unit);
            Segment segment = unit.Subunits[0] as Segment;
            Assert.IsNotNull(segment);
            SmElement smElement = segment.Source[11] as SmElement;
            Assert.IsNotNull(smElement);
            Assert.AreEqual("freme-9", smElement.Id);
            Assert.AreEqual("term", smElement.Type);
        }

        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            DirectoryInfo output = new DirectoryInfo(Directory.GetCurrentDirectory());
            for (int i = 0; i < 3; i++)
                output = Directory.GetParent(output.FullName);
            schemaDef = File.ReadAllText(Path.Combine($"{output}\\Schemas\\JLIFF-2.1", "jliff-schema-2.1.json"));
        }

        [TestMethodAttribute]
        public void RoundtripExample1()
        {
            JliffDocument model = new JliffDocument("en", "fr", new Localization.Jliff.Graph.File("f1"));

            var u1 = new Unit("u1");

            u1.OriginalData.Add("d1", "[C1/]");
            u1.OriginalData.Add("d2", "[C2]");
            u1.OriginalData.Add("d3", "[/C2]");

            var seg = new Segment();
            seg.State = Enumerations.State.translated.ToString();
            seg.CanResegment = "yes";

            var ph1 = new PhElement("c1");
            ph1.DataRef = "d1";
            var tph1 = new PhElement("c1");
            tph1.DataRef = "d1";

            var sc1 = new ScElement("c2");
            sc1.DataRef = "d2";
            var tsc1 = new ScElement("c2");
            tsc1.DataRef = "d2";

            var te3 = new TextElement("text");
            var tte3 = new TextElement("AAA");

            var ec1 = new EcElement();
            ec1.DataRef = "d3";
            ec1.StartRef = "c2";
            var tec1 = new EcElement();
            tec1.DataRef = "d3";
            tec1.StartRef = "c2";

            var te1 = new TextElement("aaa");
            var tte1 = new TextElement("AAA");

            var te2 = new TextElement(". ");

            var i1 = new Ignorable();
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

            var g = new Group("g1");

            var u2 = new Unit("u2");

            var g2 = new Group("g2");

            model.Files[0].Subfiles.Add(g);

            g.Subgroups.Add(u2);
            g.Subgroups.Add(g2);

            model.Context = new Context21();

            var dllPath = Assembly.GetAssembly(typeof(ModelTests)).Location;
            var dllName = Assembly.GetAssembly(typeof(ModelTests)).GetName().Name;
            var outputPath = dllPath.Replace(dllName + ".dll", "");


            Converter.Serialize(Path.Combine(outputPath, "example1.json"), model);

            Converter.Deserialize(new FileInfo(Path.Combine(outputPath, "example1.json")));

            JsonSchema schema = JsonSchema.Parse(schemaDef);
            var obGraph = JObject.FromObject(model);
            Assert.IsTrue(obGraph.IsValid(schema));
        }

        [TestMethodAttribute]
        public void RoundtripExample2()
        {
            var unit1 = new Unit("1");
            var unit2 = new Unit("2");
            var unit3 = new Unit("3");

            var segment1 = new Segment(
                new TextElement("Quetzal"),
                new TextElement("Quetzal"));

            var segment2 = new Segment(
                new TextElement("An application to manipulate and process XLIFF documents"),
                new TextElement("XLIFF 文書を編集、または処理 するアプリケーションです。"));

            var segment3 = new Segment(
                new TextElement("XLIFF Data Manager"),
                new TextElement("XLIFF データ・マネージャ"));

            unit1.Subunits.Add(segment1);
            unit2.Subunits.Add(segment2);
            unit3.Subunits.Add(segment3);

            var model = new JliffDocument("en", "fr");
            model.Files.Add(new Localization.Jliff.Graph.File("f1"));
            model.Files[0].Skeleton = new Skeleton("Graphic Example.psd.skl");
            model.Files[0].Subfiles = new List<ISubfile>
            {
                unit1,
                unit2,
                unit3
            };

            var dllPath = Assembly.GetAssembly(typeof(ModelTests)).Location;
            var dllName = Assembly.GetAssembly(typeof(ModelTests)).GetName().Name;
            var outputPath = dllPath.Replace(dllName + ".dll", "");


            Converter.Serialize(Path.Combine(outputPath, "example2.json"), model);

            Converter.Deserialize(new FileInfo(Path.Combine(outputPath, "example2.json")));

            JsonSchema schema = JsonSchema.Parse(schemaDef);
            var obGraph = JObject.FromObject(model);
            Assert.IsTrue(obGraph.IsValid(schema));
        }

        [TestMethodAttribute]
        public void RoundtripExample3()
        {
            var unit1 = new Unit("1");
            var unit2 = new Unit("2");

            var segment1 = new Segment(
                new TextElement("Quetzal"),
                new TextElement("Quetzal"));

            var segment2 = new Segment(
                new TextElement("An application to manipulate and process XLIFF documents"),
                new TextElement("XLIFF 文書を編集、または処理 するアプリケーションです。"));

            var grp = new MetaGroup();

            unit1.Subunits.Add(segment1);
            unit2.Subunits.Add(segment2);

            var md = new Metadata();
            md.Id = "md14";

            unit1.Metadata = md;

            var breakfast = new Metaitem("breakfast", "eggs");
            var phase = new Metaitem("phase", "draft");
            var version = new Metaitem("version", "3");

            var grp2 = new MetaGroup();
            grp2.Meta.Add(breakfast);

            var grp1 = new MetaGroup();
            grp1.Meta.Add(version);
            grp1.Meta.Add(phase);
            grp1.Meta.Add(grp2);

            md.Groups.Add(grp1);


            var model = new JliffDocument("en-US", "ja-JP");
            model.Files.Add(new Localization.Jliff.Graph.File("f1"));
            var test = new Metaitem("key", "value");
            var test1 = new MetaGroup();
            test1.Meta.Add(test);
            var list = new List<MetaGroup>();
            list.Add(test1);
            model.Files[0].Metadata = list;
            model.Files[0].Skeleton = new Skeleton("Graphic Example.psd.skl");
            model.Files[0].Subfiles = new List<ISubfile>
            {
                unit1,
                unit2
            };

            var dllPath = Assembly.GetAssembly(typeof(ModelTests)).Location;
            var dllName = Assembly.GetAssembly(typeof(ModelTests)).GetName().Name;
            var outputPath = dllPath.Replace(dllName + ".dll", "");


            Converter.Serialize(Path.Combine(outputPath, "example3.json"), model);

            Converter.Deserialize(new FileInfo(Path.Combine(outputPath, "example3.json")));

            JsonSchema schema = JsonSchema.Parse(schemaDef);
            var obGraph = JObject.FromObject(model);
            Assert.IsTrue(obGraph.IsValid(schema));
        }

        [TestMethod]
        public void RoundtripExample4()
        {
            var u1 = new Unit("u1");

            var te1 = new TextElement("Press the ");
            var te2 = new SmElement("m1");
            var te3 = new TextElement("TAB key");
            var te4 = new EmElement();
            var te5 = new TextElement(".");

            var te6 = new TextElement("Drücken Sie die ");
            var te7 = new SmElement("m2");
            var te8 = new TextElement("TAB-TASTE");
            var te9 = new EcElement();
            var te10 = new TextElement(".");

            var s1 = new Segment();
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

            var unit1 = new Unit("u1");
            unit1.Subunits.Add(s1);

            var gloss = new GlossaryEntry();
            var def = new Definition();
            def.Source = "publicTermbase";
            def.Text = "A keyboard key that is traditionally used to insert tab characters into a document.";
            gloss.Definition = def;


            var t1 = new Translation("1", "myTermbase", "Tabstopptaste");
            var t2 = new Translation("2", "myTermbase", "TAB-TASTE");

            gloss.Translations.Add(t1);
            gloss.Translations.Add(t2);

            unit1.Glossary.Add(gloss);

            var model = new JliffDocument("en", "de");
            model.Files.Add(new Localization.Jliff.Graph.File("f1"));
            model.Files[0].Subfiles = new List<ISubfile>
            {
                unit1
            };

            var dllPath = Assembly.GetAssembly(typeof(ModelTests)).Location;
            var dllName = Assembly.GetAssembly(typeof(ModelTests)).GetName().Name;
            var outputPath = dllPath.Replace(dllName + ".dll", "");


            Converter.Serialize(Path.Combine(outputPath, "example4.json"), model);

            Converter.Deserialize(new FileInfo(Path.Combine(outputPath, "example4.json")));

            JsonSchema schema = JsonSchema.Parse(schemaDef);
            var obGraph = JObject.FromObject(model);
            Assert.IsTrue(obGraph.IsValid(schema));
        }

        [TestMethodAttribute]
        public void RoundtripExample5()
        {
            JliffDocument model = new JliffDocument("en", "de",
                new Group("u1",
                    new Unit("u2",
                        new Segment(new List<IElement>
                            {
                                new TextElement("Press the "),
                                new SmElement(),
                                new TextElement("TAB KEY"),
                                new EmElement(),
                                new TextElement(".")
                            },
                            new List<IElement>
                            {
                                new TextElement("Drücken Sie die "),
                                new SmElement(),
                                new TextElement("TAB-TASTE"),
                                new EmElement(),
                                new TextElement(".")
                            }
                        )
                    )
                )
            );
        }

        [TestMethod]
        public void RoundTripExample6()
        {
            ResourceData rd1 = new ResourceData("rd1",
                new ResourceItem("ri1", new Source("https://open.vistatec.com/ocelot")));


            JliffDocument graph = new JliffDocument("en-US", "it-IT",
                new Localization.Jliff.Graph.File("f1",
                    new Unit("u1",
                        new Segment(new List<IElement>
                        { 
                            new SmElement("mrk1"),
                            new TextElement("Ocelot"),
                            new EmElement(),
                            new TextElement(" is a free, open source editor with advanced features for reviewing and correcting translations.")
                        },
                        new List<IElement>()
                        {
                        }
                        )
                    )
                )
            );

            graph.Files[0].ResourceData = rd1;
        }

        [TestMethod]
        public void RoundTripExample7()
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

            Converter.Serialize(Path.Combine(OutputFolder, "example7.json"), jlb);

            schemaDef = System.IO.File.ReadAllText(Path.Combine($"{schemasLocation}\\JLIFF-2.1", "jliff-schema-2.1-no-prefix.json"));
            JsonSchema schema = JsonSchema.Parse(schemaDef);
            var obGraph = JObject.FromObject(jlb);
            Assert.IsTrue(obGraph.IsValid(schema));

        }
    }
}