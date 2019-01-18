using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Localization.Jliff.Graph;

namespace Jliff.Graph.Serialization
{
    public static class JliffDocumentExtensions
    {
        public static void ToXlf(this JliffDocument document, string filename)
        {
            XmlSerializer xs = new XmlSerializer(typeof(JliffDocument));
            xs.Serialize(new XmlTextWriter(filename, Encoding.UTF8), document);
        }
    }
}