using System.Collections.Generic;
using Localization.Jliff.Graph.Interfaces;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class Unit : ISubfile
    {
        public List<GlossaryEntry> Glossary = new List<GlossaryEntry>();

        [JsonProperty(Order = 10)]
        public Dictionary<string, string> OriginalData = new Dictionary<string, string>();

        [JsonProperty(Order = 100)]
        public List<ISubunit> Subunits = new List<ISubunit>();

        public Unit(string id)
        {
            Id = id;
        }

        public Unit(string id, params object[] content)
        {
            Id = id;

            foreach (var parobj in content)
                if (parobj is Segment)
                    Subunits.Add(parobj as Segment);
                else if (parobj is Ignorable)
                    Subunits.Add(parobj as Ignorable);
                else if (parobj is IEnumerable<Segment>)
                    foreach (var grpparobj in parobj as IEnumerable<Segment>)
                        Subunits.Add(grpparobj);
                else if (parobj is IEnumerable<Ignorable>)
                    foreach (var grpparobj in parobj as IEnumerable<Ignorable>)
                        Subunits.Add(grpparobj);
        }

        public string CanResegment { get; set; } = "no";
        public string Id { get; set; }
        public string Kind => "unit";
        public Metadata Metadata { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public string SrcDir { get; set; }
        public bool? Translate { get; set; }
        public string TrgDir { get; set; }
        public string Type { get; set; }

        public bool ShouldSerializeGlossary()
        {
            return Glossary.Count > 0;
        }

        public bool ShouldSerializeOriginalData()
        {
            return OriginalData.Count > 0;
        }

        public bool ShouldSerializeSubunits()
        {
            return Subunits.Count > 0;
        }
    }
}