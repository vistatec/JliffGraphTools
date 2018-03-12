namespace Localization.Jliff.Graph
{
    public class TextElement : IElement
    {
        public TextElement()
        {
        }

        public TextElement(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}