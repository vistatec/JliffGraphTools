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
using AutoMapper;
using Localization.Jliff.Graph;
using File = Localization.Jliff.Graph.File;

namespace Localization.Jliff.Samples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            JliffBuilder builder = new JliffBuilder("en", "fr");
            Xliff20Filter xliff20Filter = new Xliff20Filter();
            xliff20Filter.XlfRootEvent += builder.XlfRoot;
            xliff20Filter.XlfFileEvent += builder.File;
            xliff20Filter.XlfUnitEvent += builder.Unit;
            xliff20Filter.XlfGroupEvent += builder.Group;
            xliff20Filter.XlfSegmentEvent += builder.Segment;
            xliff20Filter.XlfSourceEvent += builder.Source;
            xliff20Filter.XlfTargetEvent += builder.Target;
            xliff20Filter.XlfIgnorableEvent += builder.Ignorable;
            xliff20Filter.XlfPhElementEvent += builder.PhElement;
            xliff20Filter.XlfSkeletonEvent += builder.Skeleton;
            xliff20Filter.XlfTextEvent += builder.Text;
            xliff20Filter.XlfSmElementEvent += builder.SmElement;
            xliff20Filter.XlfEmElementEvent += builder.EmElement;
            xliff20Filter.XlfScElementEvent += builder.ScElement;
            xliff20Filter.XlfEcElementEvent += builder.EcElement;
            xliff20Filter.ModItsLocQualityIssue += builder.LocQualityIssue;
            DirectoryInfo output = new DirectoryInfo(Directory.GetCurrentDirectory());
            for (int i = 0; i < 1; i++)
                output = Directory.GetParent(output.FullName);
            //xliff20Filter.Filter(new StreamReader(Path.Combine($"{output}\\Jliff.Tests\\XlfFiles", "everything-core.xlf")));
            xliff20Filter.Filter(new StreamReader(@"e:\ExtDev\DotNet\JliffGraphTools\Jliff.Tests\XlfFiles\LQE_xliff_2.0.xlf"));
            builder.Serialize("XliffToJliff.json");
            

            JliffBuilder bldr = new JliffBuilder("en", "fr");
            Xliff12Filter xliff12Filter = new Xliff12Filter();
            xliff12Filter.XlfRootEvent += bldr.XlfRoot;
            xliff12Filter.XlfFileEvent += bldr.File;
            xliff12Filter.XlfUnitEvent += bldr.Unit;
            xliff12Filter.XlfGroupEvent += bldr.Group;
            xliff12Filter.XlfSegmentEvent += bldr.Segment;
            xliff12Filter.XlfSourceEvent += bldr.Source;
            xliff12Filter.XlfTargetEvent += bldr.Target;
            xliff12Filter.XlfIgnorableEvent += bldr.Ignorable;
            xliff12Filter.XlfPhElementEvent += bldr.PhElement;
            xliff12Filter.XlfSkeletonEvent += bldr.Skeleton;
            xliff12Filter.XlfTextEvent += bldr.Text;
            xliff12Filter.XlfSmElementEvent += bldr.SmElement;
            xliff12Filter.XlfEmElementEvent += bldr.EmElement;
            xliff12Filter.XlfScElementEvent += bldr.ScElement;
            xliff12Filter.XlfEcElementEvent += bldr.EcElement;
            xliff12Filter.ModItsLocQualityIssue += bldr.LocQualityIssue;
            DirectoryInfo output2 = new DirectoryInfo(Directory.GetCurrentDirectory());
            for (int i = 0; i < 1; i++)
                output2 = Directory.GetParent(output2.FullName);
            //xliff12Filter.Filter(new StreamReader(Path.Combine($"{output}\\Jliff.Tests\\XlfFiles", "everything-core.xlf")));
            xliff12Filter.Filter(new StreamReader(@"e:\ExtDev\DotNet\JliffGraphTools\Jliff.Tests\XlfFiles\ocelot12.xlf"));
            bldr.Serialize("Xliff12ToJliff.json");

            JliffDocument jd = new JliffDocument();
            Segment s1 = new Segment("segment-1");
            s1.Source.Add(new TextElement("This is a minimal jliff document."));
            jd.Subunits.Add(s1);
            Converter.Serialize(@"e:\ExtDev\DotNet\JliffGraphTools\Jliff.Tests\XlfFiles\minimal.json", jd);
        }
    }
}