using System.Collections.Generic;

namespace Localization.Jliff.Graph
{
    public class GlossaryEntry
    {
        public List<Translation> Translations = new List<Translation>();
        public Definition Definition { get; set; }
        public string Ref { get; set; }
        public Term Term { get; set; }
    }
}