using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Localization.Jliff.Graph;
using Newtonsoft.Json;

namespace Jliff.Graph.Core
{
    public class Note : IXmlSerializable
    {
        public string AppliesTo { get; set; }
        public string Category { get; set; }

        [JsonProperty("fs_fs")]
        public Enumerations.FormatStyle Fs { get; set; }
        public Nmtoken Id { get; set; }
        public int Priority { get; set; }

        [JsonProperty("fs_subFs")]
        public string SubFs { get; set; }

        [JsonProperty("note")]
        public string Text { get; set; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new System.NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            if (Id != null) writer.WriteAttributeString("id", Id.Token);
            if (AppliesTo != null) writer.WriteAttributeString("appliesTo", AppliesTo);
            if (Category != null) writer.WriteAttributeString("category", Category);
            if (Priority != null) writer.WriteAttributeString("priority", Priority.ToString());
            writer.WriteString(Text);
        }
    }
}