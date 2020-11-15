/*
 * Copyright (C) 2018-2019, Vistatec or third-party contributors as indicated
 * by the @author tags or express copyright attribution statements applied by
 * the authors. All third-party contributions are distributed under license by
 * Vistatec.
 *
 * This file is part of JliffGraphTools.
 *
 * JliffGraphTools is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * JliffGraphTools is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with this program. If not, write to:
 *
 *     Free Software Foundation, Inc.
 *     51 Franklin Street, Fifth Floor
 *     Boston, MA 02110-1301
 *     USA
 *
 * Also, see the full LGPL text here: <http://www.gnu.org/copyleft/lesser.html>
 */


using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using Localization.Jliff.Graph.Modules.ChangeTrack;
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
            fltr.XlfDataEvent += bldr.Data;
            fltr.XlfNote += bldr.Note;
            fltr.ModItsLocQualityIssues += bldr.LocQualityIssues;
            fltr.ModItsLocQualityIssue += bldr.LocQualityIssue;
            fltr.ModMdaMetadataEvent += bldr.Metadata;
            fltr.ModMdaMetaGroupEvent += bldr.MetaGroup;
            fltr.ModMdaMetaitemEvent += bldr.Metaitem;
            fltr.ModResResourceDataEvent += bldr.ResourceData;
            fltr.ModResResourceItemEvent += bldr.ResourceItem;
            fltr.ModResSourceEvent += bldr.ResourceSource;
            fltr.ModGlsEntryEvent += bldr.GlossaryEntry;
            fltr.ModGlsDefinitionEvent += bldr.Definition;
            fltr.ModGlsTermEvent += bldr.Term;
            fltr.ModGlsTranslationEvent += bldr.Translation;
            fltr.ModMtcMatchEvent += bldr.Match;
            fltr.ModCtrChangeTrackEvent += bldr.ChangeTrack;
            fltr.ModCtrRevisionsEvent += bldr.Revisions;
            fltr.ModCtrRevisionEvent += bldr.Revision;
            fltr.ModCtrRevisionItemEvent += bldr.RevisionItem;
            fltr.Filter(new StreamReader(Path.Combine(XlfFiles, "Ocelot.xlf")));
            bldr.Serialize(Path.Combine(XlfFiles, "Ocelot.json"));
            JsonSchema schema = JsonSchema.Parse(schemaDef);
            var obGraph = JObject.FromObject(bldr.Jliff);
            Assert.IsTrue(obGraph.IsValid(schema));

            JliffDocument jd = Converter.Deserialize(new FileInfo(Path.Combine(XlfFiles, "Ocelot.json")));
            Group grp = jd.Files[0].Subfiles[1] as Group;
            Unit u = grp.Subgroups[1] as Unit;
            ChangeTrack ct = u.ChangeTrack;
            Assert.IsNotNull(ct);
            Assert.IsTrue(ct.Revisions.Items[1].Author.Equals("phil"));
            Assert.IsTrue(ct.Revisions.Items[1].Item.Text.StartsWith("Quando un segmento è selezionato nella Vista segmento"));
        }

        [TestMethod]
        public void XlfWithOneUnitTowSegmentsAndIts()
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
            fltr.XlfDataEvent += bldr.Data;
            fltr.XlfNote += bldr.Note;
            fltr.ModItsLocQualityIssues += bldr.LocQualityIssues;
            fltr.ModItsLocQualityIssue += bldr.LocQualityIssue;
            fltr.ModMdaMetadataEvent += bldr.Metadata;
            fltr.ModMdaMetaGroupEvent += bldr.MetaGroup;
            fltr.ModMdaMetaitemEvent += bldr.Metaitem;
            fltr.ModResResourceDataEvent += bldr.ResourceData;
            fltr.ModResResourceItemEvent += bldr.ResourceItem;
            fltr.ModResSourceEvent += bldr.ResourceSource;
            fltr.ModGlsEntryEvent += bldr.GlossaryEntry;
            fltr.ModGlsDefinitionEvent += bldr.Definition;
            fltr.ModGlsTermEvent += bldr.Term;
            fltr.ModGlsTranslationEvent += bldr.Translation;
            fltr.ModMtcMatchEvent += bldr.Match;
            fltr.ModCtrChangeTrackEvent += bldr.ChangeTrack;
            fltr.ModCtrRevisionsEvent += bldr.Revisions;
            fltr.ModCtrRevisionEvent += bldr.Revision;
            fltr.ModCtrRevisionItemEvent += bldr.RevisionItem;
            fltr.Filter(new StreamReader(Path.Combine(XlfFiles, "OneUnitTwoSegmentsPlusIts.xlf")));
            bldr.Serialize(Path.Combine(XlfFiles, "OneUnitTwoSegmentsPlusItsOutput.json"));

            var jd = bldr.Jliff;

            Unit u = jd.Files[0].Subfiles[0] as Unit;
            int x = u.LocQualityIssuesArray.Count;
            string id1 = u.LocQualityIssuesArray[0].Id.Token;
            string id2 = u.LocQualityIssuesArray[1].Id.Token;

            Assert.AreEqual(x, 2);
            Assert.AreEqual(id1, "lqi22");
            Assert.AreEqual(id2, "lqi44");
        }
    }
}