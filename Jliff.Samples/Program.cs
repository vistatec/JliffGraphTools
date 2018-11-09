using System.IO;
using AutoMapper;
using Localization.Jliff.Graph;
using File = Localization.Jliff.Graph.File;

namespace Jliff.Samples
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
        }
    }
}