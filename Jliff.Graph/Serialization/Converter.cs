using System.IO;
using Localization.Jliff.Graph.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Localization.Jliff.Graph
{
    public class Converter
    {
        public static JliffDocument Deserialize(FileInfo jsonFilename)
        {
            string json;

            using (var reader = System.IO.File.OpenText(jsonFilename.FullName))
            {
                json = reader.ReadToEnd();
            }

            var set = new JsonSerializerSettings();
            set.ContractResolver = new CamelCasePropertyNamesContractResolver();
            set.Formatting = Formatting.Indented;
            set.NullValueHandling = NullValueHandling.Ignore;
            set.DefaultValueHandling = DefaultValueHandling.Ignore;
            set.Converters.Add(new ISubfileConverter());
            set.Converters.Add(new ISubunitConverter());
            set.Converters.Add(new IElementConverter());
            set.Converters.Add(new IMetadataConverter());

            var model = JsonConvert.DeserializeObject<JliffDocument>(json, set);

            return model;
        }

        public static JliffDocument Deserialize(string json)
        {
            var set = new JsonSerializerSettings();
            set.ContractResolver = new CamelCasePropertyNamesContractResolver();
            set.Formatting = Formatting.Indented;
            set.NullValueHandling = NullValueHandling.Ignore;
            set.DefaultValueHandling = DefaultValueHandling.Ignore;
            set.Converters.Add(new ISubfileConverter());
            set.Converters.Add(new ISubunitConverter());
            set.Converters.Add(new IElementConverter());
            set.Converters.Add(new IMetadataConverter());

            var model = JsonConvert.DeserializeObject<JliffDocument>(json, set);

            return model;
        }

        public static void Serialize(string filename, object model)
        {
            var set = new JsonSerializerSettings();
            set.ContractResolver = new CamelCasePropertyNamesContractResolver();
            set.Formatting = Formatting.Indented;
            set.NullValueHandling = NullValueHandling.Ignore;
            set.DefaultValueHandling = DefaultValueHandling.Ignore;
            set.Converters.Add(new ISubfileConverter());
            set.Converters.Add(new ISubunitConverter());
            set.Converters.Add(new IElementConverter());
            set.Converters.Add(new IMetadataConverter());

            var output = JsonConvert.SerializeObject(model, set);

            using (var writer = System.IO.File.CreateText(filename))
            {
                writer.Write(output);
            }
        }

        public static string Serialize(JliffDocument model)
        {
            var set = new JsonSerializerSettings();
            set.ContractResolver = new CamelCasePropertyNamesContractResolver();
            set.Formatting = Formatting.Indented;
            set.NullValueHandling = NullValueHandling.Ignore;
            set.DefaultValueHandling = DefaultValueHandling.Ignore;
            set.Converters.Add(new ISubfileConverter());
            set.Converters.Add(new ISubunitConverter());
            set.Converters.Add(new IElementConverter());
            set.Converters.Add(new IMetadataConverter());

            var output = JsonConvert.SerializeObject(model, set);

            return output;
        }
    }
}