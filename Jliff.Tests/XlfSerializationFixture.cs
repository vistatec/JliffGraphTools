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
    public class XlfSerializationFixture
    {
        private const string OutputFolder = "Output";
        private const string XlfFiles = "XlfFiles";

        [TestMethod]
        public void SerializeModelAsXlf()
        {
            List<IElement> source = new List<IElement>();
            source.Add(new SmElement("mrk1", "term"));
            source.Add(new TextElement("Ocelot"));
            source.Add(new EmElement());
            source.Add(new TextElement(" is a free, open source editor with advanced features for reviewing and correcting translations."));

            JliffDocument jd = new JliffDocument("en-US", "it-IT",
                new File("file1",
                    new Unit("unit1"),
                    new Unit("unit2",
                        new Segment(
                            source,
                            new List<IElement>() { new SmElement(), new TextElement("Target 1"), new EmElement()}
                        ))));

            ((jd.Files[0].Subfiles[1] as Unit).Subunits[0] as Segment).Id = "S1ofU2";

            jd.ToXlf(Path.Combine(OutputFolder, "Jlf2Xlf.xlf"));
        }

        [TestMethod]
        public void XlfInXlfOutViaJliffModel()
        {
            JliffDocument jd = JliffDocument.LoadXlf(Path.Combine(XlfFiles, "Ocelot.xlf"));
            jd.ToXlf(Path.Combine(OutputFolder, "OcelotSerializedFromModel.xlf"));
        }

        [TestMethod]
        public void XlfInXlfStringOutViaJliffModel()
        {
            JliffDocument jd = JliffDocument.LoadXlf(Path.Combine(XlfFiles, "Ocelot.xlf"));
            jd.Segments[0].Target.Add(new TextElement("áíóú"));
            string output = jd.ToXlf();
            Assert.IsTrue(output.Contains("<res:source href=\"https://open.vistatec.com/ocelot\" />"));
        }
    }
}
