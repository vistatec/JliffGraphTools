using System.IO;
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

        public static void ToXlf(this JliffDocument document, Stream stream)
        {
            XmlSerializer xs = new XmlSerializer(typeof(JliffDocument));
            xs.Serialize(stream, document);
        }

        public static string ToXlf(this JliffDocument document)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings xws = new XmlWriterSettings()
            {
                Encoding = Encoding.UTF8
            };
            XmlSerializer xs = new XmlSerializer(typeof(JliffDocument));
            MemoryStream ms = new MemoryStream();
            XmlTextWriter xtw = new XmlTextWriter(ms, Encoding.UTF8);
            
            //xs.Serialize(ms, document);
            //ms.Seek(0, SeekOrigin.Begin);
            //StreamReader sr = new StreamReader(ms, Encoding.UTF8);
            //return sr.ReadToEnd();
            
            XmlWriter w = XmlWriter.Create(new StringWriterWithEncoding(sb), xws);
            //xs.Serialize(xtw, document);
            xs.Serialize(w, document);
            return sb.ToString();
        }
    }
}