using System.IO;
using System.Text;

namespace Jliff.Graph.Serialization
{
    public class StringWriterWithEncoding : StringWriter
    {
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        public StringWriterWithEncoding(StringBuilder sb) : base(sb)
        {

        }
    }
}