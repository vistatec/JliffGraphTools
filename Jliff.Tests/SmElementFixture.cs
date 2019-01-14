/*
 * Copyright (C) 2018, Vistatec or third-party contributors as indicated
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
using System.Text;
using Localization.Jliff.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class SmElementFixture
    {
        [TestMethod]
        public void WritesStringWithAnnotationsCorrectly()
        {
            Segment s = new Segment("s1");
            s.Source.Add(new SmElement("mrk1"));
            s.Source.Add(new TextElement("Ocelot"));
            s.Source.Add(new EmElement());
            s.Source.Add(new TextElement(" is a free, open source editor."));

            string expected = "\ue101mrk1\ue101Ocelot\ue102\ue102 is a free, open source editor.";

            Assert.AreEqual(expected, s.FlattenSource());
        }
    }
}
