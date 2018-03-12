using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Localization.Jliff.Graph
{
    public class IElementConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            var canConvert = false;

            if (objectType.Name.Equals("IElement")) canConvert = true;

            return canConvert;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var jobject = JObject.Load(reader);

            object resolvedType = null;

            var kind = jobject["kind"]?.Value<string>();

            if (kind != null)
                switch (kind)
                {
                    case "ec":
                        resolvedType = new EcElement();
                        break;
                    case "em":
                        resolvedType = new EmElement();
                        break;
                    case "ph":
                        resolvedType = new PhElement();
                        break;
                    case "sc":
                        resolvedType = new ScElement();
                        break;
                    case "sm":
                        resolvedType = new SmElement();
                        break;
                }
            else
                resolvedType = new TextElement();


            serializer.Populate(jobject.CreateReader(), resolvedType);

            return resolvedType;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}