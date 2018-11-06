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
    [DeploymentItem(XlfFiles)]
    [TestClass]
    public class JliffBuilderTests
    {
        private const string schemasLocation = "Schemas";
        private static string schemaDef = string.Empty;
        private const string XlfFiles = "XlfFiles";

        [TestMethod]
        [DeploymentItem(schemasLocation)]
 
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            DirectoryInfo output = new DirectoryInfo(Directory.GetCurrentDirectory());
            for (int i = 0; i < 3; i++)
                output = Directory.GetParent(output.FullName);
            schemaDef = System.IO.File.ReadAllText(Path.Combine($"{schemasLocation}\\JLIFF-2.1", "jliff-schema-2.1-no-prefix.json"));
        }

        [TestMethod]
        public void XlfWithModules()
        {
            JliffBuilder bldr = new JliffBuilder("en-US", "it-IT");
            Xliff20Filter fltr = new Xliff20Filter();
            fltr.XlfRootEvent += bldr.XlfRoot;
            fltr.XlfFileEvent += bldr.File;
            fltr.XlfUnitEvent += bldr.Unit;
            fltr.XlfGroupEvent += bldr.Group;
            fltr.XlfSegmentEvent += bldr.Segment;
            fltr.XlfSourceEvent += bldr.Source;
            fltr.XlfTargetEvent += bldr.Target;
            fltr.XlfIgnorableEvent += bldr.Ignorable;
            fltr.XlfPhElementEvent += bldr.PhElement;
            fltr.XlfSkeletonEvent += bldr.Skeleton;
            fltr.XlfTextEvent += bldr.Text;
            fltr.XlfSmElementEvent += bldr.SmElement;
            fltr.XlfEmElementEvent += bldr.EmElement;
            fltr.XlfScElementEvent += bldr.ScElement;
            fltr.XlfEcElementEvent += bldr.EcElement;
            fltr.ModItsLocQualityIssue += bldr.LocQualityIssue;
            fltr.ModMetadataEvent += bldr.Metadata;
            fltr.ModMetaGroupEvent += bldr.MetaGroup;
            fltr.ModMetaitemEvent += bldr.Metaitem;
            fltr.ModResourceDataEvent += bldr.ResourceData;
            fltr.ModResourceItemEvent += bldr.ResourceItem;
            fltr.ModResourceSourceEvent += bldr.ResourceSource;
            fltr.Filter(new StreamReader(Path.Combine(XlfFiles, "Ocelot.xlf")));
            bldr.Serialize(Path.Combine(XlfFiles, "Ocelot.json"));
            JsonSchema schema = JsonSchema.Parse(schemaDef);
            var obGraph = JObject.FromObject(bldr.Jliff);
            Assert.IsTrue(obGraph.IsValid(schema));

        }
    }
}