using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Localization.Jliff.Graph
{
    public class ISubunitConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            var canConvert = false;

            if (objectType.Name.Equals("ISubunit")) canConvert = true;

            return canConvert;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var jobject = JObject.Load(reader);

            object resolvedType = null;

            JToken token;
            bool gotVal = jobject.TryGetValue("kind", StringComparison.InvariantCultureIgnoreCase, out token);
            if (gotVal)
                if (token.Value<string>().Equals("segment"))
                    resolvedType = new Segment();
                else
                    resolvedType = new Ignorable();

            serializer.Populate(jobject.CreateReader(), resolvedType);

            return resolvedType;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}