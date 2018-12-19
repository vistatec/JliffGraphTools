using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Localization.Jliff.Graph;

namespace Jliff.Graph.Conversion
{
    public class XlfConverter
    {
        public JliffDocument ConvertXlf20(Stream stream)
        {
            try
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
                xliff20Filter.Filter(new StreamReader(stream, Encoding.UTF8));
                return builder.Jliff;
            }
            catch (Exception e)
            {
                throw new InvalidDataException();
            }
        }
    }
}
