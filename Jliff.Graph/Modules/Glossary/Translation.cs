namespace Localization.Jliff.Graph
{
    public class Translation
    {
        public Translation(string id, string source, string text)
        {
            Id = id;
            Source = source;
            Text = text;
        }

        public Translation()
        {
            
        }

        public string Id { get; set; }
        public string Source { get; set; }
        public string Text { get; set; }
    }
}