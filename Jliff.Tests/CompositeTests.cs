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
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using Localization.Jliff.Graph.Serialization;
using Localization.Jliff.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class CompositeTests
    {
        [TestMethod]
        public void Traverse()
        {
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

            string h = jlb.Files[0].Traverse(NullMethod);
        }

        public delegate string op();

        public string NullMethod()
        {
            return "";
        }

        [TestMethod]
        public void Process()
        {
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

            TestCompositeVisitor tcv = new TestCompositeVisitor();
            jlb.Files[0].Process(tcv);
            var visitedIds = tcv.sb.ToString();
            Assert.AreEqual<string>("f1\r\nu1\r\ns1\r\ns2\r\ns3\r\n", visitedIds);
        }
    }
}