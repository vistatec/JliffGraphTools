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

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Jliff.Graph.Serialization;
using Localization.Jliff.Graph;
using Localization.Jliff.Graph.Modules.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using File = Localization.Jliff.Graph.File;

namespace UnitTests
{
    [DeploymentItem(OutputFolder)]
    [DeploymentItem(XlfFiles)]
    [TestClass]
    public class JlfSerializationFixture
    {
        private const string OutputFolder = "Output";
        private const string XlfFiles = "XlfFiles";

        [TestMethod]
        public void SerializeModelAsJlf()
        {
            JliffDocument jd = JliffDocument.LoadXlf(Path.Combine(XlfFiles, "PlatformTest.xlf"));
            Converter.Serialize(Path.Combine(OutputFolder, "PlatformTest.json"), jd);
        }

        [TestMethod]
        public void Round()
        {
            var f = new JliffDocument("en-US", "it-IT",
                new File("f1",
                    new Unit("u1",
                        new Segment(new TextElement("Hello!"),
                            new TextElement("!Ola!")))));

            string s = Converter.Serialize(f, false);

            var g = Converter.Deserialize(s);
        }
    }
}
