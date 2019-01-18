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
using System.Net;
using System.Reflection;
using Jliff.Graph.Core;
using Localization.Jliff.Graph;
using Localization.Jliff.Graph.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace UnitTests
{
    /// <summary>
    ///     Summary description for EverythingCore
    /// </summary>
    [TestClass]
    public class SegmentFixture
    {
        private static string schemaDef = string.Empty;

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TargetRendersCorrectly()
        {
            Segment s = new Segment("s1");
            s.Source.Add(new SmElement("mrk1"));
            s.Source.Add(new TextElement("Ocelot"));
            s.Source.Add(new EmElement());
            s.Source.Add(new TextElement(" is a free, open source editor."));

            string expected = "\ue101mrk1\ue101Ocelot\ue102\ue102 is a free, open source editor.";

            Assert.AreEqual(expected, s.FlattenSource());

        }

        [TestMethod]
        public void TargetParsesCorrectly()
        {
            string input = "\ue101mrk1\ue101Ocelot\ue102\ue102 is a free, open source editor.";
            FragmentManager fm = new FragmentManager();
            List<IElement> elements = fm.Parse(input);

            Assert.AreEqual(typeof(SmElement), elements[0].GetType());
        }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}

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