using Jliff.Graph.Interfaces;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class TextElement : JlfNode, IElement
    {
        public TextElement()
        {
        }

        public TextElement(string text)
        {
            Text = text;
        }

        public string Text { get; set; }

        [JsonIgnore]
        public override string Kind => Enumerations.JlfNodeType.text.ToString();

        public override void Process(ICompositeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}