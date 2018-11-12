using System;
using Localization.Jliff.Graph.Modules.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Localization.Jliff.Graph
{
    internal class IMetadataConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.Name.Equals("IMetadata");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var jobject = JObject.Load(reader);

            object resolvedType = null;

            var h = jobject["meta"];


            if (h != null)
                resolvedType = new MetaGroup();
            else
                resolvedType = new Metaitem();


            serializer.Populate(jobject.CreateReader(), resolvedType);

            return resolvedType;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}