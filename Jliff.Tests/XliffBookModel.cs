using System.IO;
using System.Reflection;
using Localization.Jliff.Graph;
using Localization.Jliff.Graph.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using File = Localization.Jliff.Graph.File;

namespace UnitTests
{
    [TestClass]
    public class XliffBookModel
    {
        private const string schemasLocation = "Schemas";
        private static string schemaDef = string.Empty;

        [TestMethod]
        [DeploymentItem(schemasLocation)]
        public void Fluent()
        {
            JliffDocument maximalist = new JliffDocument("en", "de",
                new File("d1e1",
                    new Group("d1e1",
                        new Group("d1e2",
                            new Unit("d1e3",
                                new Segment(
                                    new TextElement("Birds in Oregon"),
                                    new TextElement(""))),
                            new Unit("d1e5",
                                new Segment(
                                    new TextElement(
                                        "Oregon is a mostly temperate state. There are many different kinds of birds that thrive."),
                                    new TextElement(""))
                            ),
                            new Group("d1e7",
                                new Unit("d1e8",
                                    new Segment(
                                        new TextElement("High Altitude Birds"),
                                        new TextElement("")
                                    )
                                ),
                                new Unit("d1e10",
                                    new Segment(
                                        new TextElement(
                                            "Birds that thrive in the high altitude include the White-tailed Ptarmigan, Sharp-tailed Grouse, Yellow-billed Loon, Cattle Egret, Gyrfalcon, Snowy Owl, Yellow-billed Cuckoo, and Boreal Owl."),
                                        new TextElement("")
                                    )
                                )
                            ),
                            new Group("d1e12",
                                new Unit("d1e13",
                                    new Segment(
                                        new TextElement("Ocean Birds"),
                                        new TextElement("")
                                    )
                                ),
                                new Unit("d1e15",
                                    new Segment(
                                        new TextElement(
                                            "Common ocean birds are Brandt's Cormorant, Double-crested Cormorant, Pelagic Cormorant, Pigeon Guillemot, and the Tufted Puffin."),
                                        new TextElement("")
                                    )
                                )
                            ),
                            new Group("d1e17",
                                new Unit("d1e18",
                                    new Segment(
                                        new TextElement("Desert Birds"),
                                        new TextElement("")
                                    )
                                ),
                                new Unit("d1e20",
                                    new Segment(
                                        new TextElement(
                                            "Birds you find in the desert include the Sage Grouse, California Quail, and Prairie Falcon."),
                                        new TextElement("")
                                    )
                                )
                            )
                        )
                    )
                )
            );

            maximalist.Context = new Context21();

            var dllPath = Assembly.GetAssembly(typeof(ModelTests)).Location;
            var dllName = Assembly.GetAssembly(typeof(ModelTests)).GetName().Name;
            var outputPath = dllPath.Replace(dllName + ".dll", "");


            Converter.Serialize(Path.Combine(outputPath, "XliffBook.json"), maximalist);

            JliffDocument w = Converter.Deserialize(new FileInfo(Path.Combine(outputPath, "XliffBook.json")));

            Assert.AreEqual("de", w.TrgLang);
            JsonSchema schema = JsonSchema.Parse(schemaDef);
            var obGraph = JObject.FromObject(maximalist);
            Assert.IsTrue(obGraph.IsValid(schema));
        }

        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            DirectoryInfo output = new DirectoryInfo(Directory.GetCurrentDirectory());
            for (int i = 0; i < 3; i++)
                output = Directory.GetParent(output.FullName);
            schemaDef = System.IO.File.ReadAllText(Path.Combine($"{output}\\Schemas\\JLIFF-2.1", "jliff-schema-2.1.json"));
        }

        [TestMethod]
        public void Procedural()
        {
            JliffDocument j = new JliffDocument("en", "de");
            j.Context = new Context21();
            File f = new File("d1e1");
            j.Files.Add(f);
            Group gd1e1 = new Group("d1e1");
            f.Subfiles.Add(gd1e1);
            Group gd1e2 = new Group("d1e2");
            gd1e1.Subgroups.Add(gd1e2);

            Unit ud1e3 = new Unit("d1e3");
            TextElement sd1e3 = new TextElement("Birds in Oregon");
            TextElement td1e3 = new TextElement("");
            Segment sgd1e3 = new Segment(sd1e3, td1e3);
            ud1e3.Subunits.Add(sgd1e3);
            gd1e2.Subgroups.Add(ud1e3);

            Unit ud1e5 = new Unit("d1e5");
            TextElement sd1e5 =
                new TextElement(
                    "Oregon is a mostly temperate state. There are many different kinds of birds that thrive.");
            TextElement td1e5 = new TextElement("");
            Segment sgd1e5 = new Segment(sd1e5, td1e5);
            ud1e5.Subunits.Add(sgd1e5);
            gd1e2.Subgroups.Add(ud1e5);

            Group gd1e7 = new Group("d1e7");
            gd1e2.Subgroups.Add(gd1e7);

            Unit ud1e8 = new Unit("d1e8");
            TextElement sd1e8 = new TextElement("High Altitude Birds");
            TextElement td1e8 = new TextElement("");
            Segment sgd1e8 = new Segment(sd1e8, td1e8);
            ud1e8.Subunits.Add(sgd1e8);
            gd1e7.Subgroups.Add(ud1e8);

            Unit ud1e10 = new Unit("d1e10");
            TextElement sd1e10 =
                new TextElement(
                    "Birds that thrive in the high altitude include the White-tailed Ptarmigan, Sharp-tailed Grouse, Yellow-billed Loon, Cattle Egret, Gyrfalcon, Snowy Owl, Yellow-billed Cuckoo, and Boreal Owl.");
            TextElement td1e10 = new TextElement("");
            Segment sgd1e10 = new Segment(sd1e10, td1e10);
            ud1e10.Subunits.Add(sgd1e10);
            gd1e7.Subgroups.Add(ud1e10);

            Group gd1e12 = new Group("d1e12");
            gd1e2.Subgroups.Add(gd1e12);

            Unit ud1e13 = new Unit("d1e13");
            TextElement sd1e13 = new TextElement("Ocean Birds");
            TextElement td1e13 = new TextElement("");
            Segment sgd1e13 = new Segment(sd1e13, td1e13);
            ud1e13.Subunits.Add(sgd1e13);
            gd1e12.Subgroups.Add(ud1e13);

            Group gd1e17 = new Group("d1e17");
            gd1e2.Subgroups.Add(gd1e17);

            Unit ud1e15 = new Unit("d1e15");
            TextElement sd1e15 =
                new TextElement(
                    "Common ocean birds are Brandt's Cormorant, Double-crested Cormorant, Pelagic Cormorant, Pigeon Guillemot, and the Tufted Puffin.");
            TextElement td1e15 = new TextElement("");
            Segment sgd1e15 = new Segment(sd1e15, td1e15);
            ud1e15.Subunits.Add(sgd1e15);
            gd1e12.Subgroups.Add(ud1e15);

            Unit ud1e18 = new Unit("d1e18");
            TextElement sd1e18 = new TextElement("Desert Birds");
            TextElement td1e18 = new TextElement("");
            Segment sgd1e18 = new Segment(sd1e18, td1e18);
            ud1e18.Subunits.Add(sgd1e18);
            gd1e17.Subgroups.Add(ud1e18);

            Unit ud1e20 = new Unit("d1e20");
            TextElement sd1e20 =
                new TextElement(
                    "Birds you find in the desert include the Sage Grouse, California Quail, and Prairie Falcon.");
            TextElement td1e20 = new TextElement("");
            Segment sgd1e20 = new Segment(sd1e20, td1e20);
            ud1e20.Subunits.Add(sgd1e20);
            gd1e17.Subgroups.Add(ud1e20);

            var dllPath = Assembly.GetAssembly(typeof(ModelTests)).Location;
            var dllName = Assembly.GetAssembly(typeof(ModelTests)).GetName().Name;
            var outputPath = dllPath.Replace(dllName + ".dll", "");


            Converter.Serialize(Path.Combine(outputPath, "XliffBook2.json"), j);

            JsonSchema schema = JsonSchema.Parse(schemaDef);
            var obGraph = JObject.FromObject(j);
            Assert.IsTrue(obGraph.IsValid(schema));
        }
    }
}