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


using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using JlGraph = Localization.Jliff.Graph;

namespace UnitTests
{
    /// <summary>
    ///     Summary description for EverythingCore
    /// </summary>
    [TestClass]
    public class EverythingCore
    {
        private static string schemaDef = string.Empty;

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Core()
        {
            var f = new List<JlGraph.IElement>();


            JlGraph.JliffDocument jliff = new JlGraph.JliffDocument("en", "fr",
                new JlGraph.File("f1",
                    new JlGraph.Unit("tu1",
                        new JlGraph.Segment(
                            new JlGraph.TextElement("Sample segment."),
                            new JlGraph.TextElement("Exemple de segment.")
                        ),
                        new JlGraph.Ignorable(
                            new JlGraph.TextElement("")
                        ),
                        new JlGraph.Segment(
                            new JlGraph.TextElement("Segment's content."),
                            new JlGraph.TextElement("Contenu du segment.")
                        )
                    ),
                    new JlGraph.Group("g1",
                        new JlGraph.Unit("tu3",
                            new JlGraph.Segment(
                                new JlGraph.TextElement("Bolded text"),
                                new JlGraph.TextElement("")
                            )
                        ),
                        new JlGraph.Unit("tu3end",
                            new JlGraph.Segment(
                                new JlGraph.TextElement("Extra stuff")
                            )
                        ),
                        new JlGraph.Unit("tu2")
                    )
                )
            );

            JlGraph.Group g1 = jliff.Files[0].Subfiles[1] as JlGraph.Group;
            if (g1 != null)
                g1.CanResegment = "yes";

            JlGraph.Unit tu2 = g1.Subgroups[2] as JlGraph.Unit;
            if (tu2 != null)
            {
                tu2.OriginalData.Add("d1", "[b [#$tu3]   ]");
                tu2.OriginalData.Add("d2", "[/b [#t3end]   ]");
                tu2.OriginalData.Add("d3", "[rtl-data   ]");
            }

            JlGraph.Segment s1 = new JlGraph.Segment("s1");
            List<JlGraph.IElement> s1Source = new List<JlGraph.IElement>();

            tu2.Subunits.Add(s1);
            s1.Source = s1Source;

            var dllPath = Assembly.GetAssembly(typeof(ModelTests)).Location;
            var dllName = Assembly.GetAssembly(typeof(ModelTests)).GetName().Name;
            var outputPath = dllPath.Replace(dllName + ".dll", "");


            JlGraph.Converter.Serialize(Path.Combine(outputPath, "everything-core.json"), jliff);

            JsonSchema schema = JsonSchema.Parse(schemaDef);
            var obGraph = JObject.FromObject(jliff);
            Assert.IsTrue(obGraph.IsValid(schema));
        }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            DirectoryInfo output = new DirectoryInfo(Directory.GetCurrentDirectory());
            for (int i = 0; i < 3; i++)
                output = Directory.GetParent(output.FullName);
            schemaDef = File.ReadAllText(Path.Combine($"{output}\\Schemas\\JLIFF-2.1", "jliff-schema-2.1.json"));
        }

        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion
    }
}