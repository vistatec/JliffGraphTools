using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class Item
    {
        public Item(string id, string data)
        {
            Id = id;
            Data = data;
        }

        [JsonProperty(Order = 10)]
        public string Data { get; set; }

        public string Id { get; set; }
    }
}