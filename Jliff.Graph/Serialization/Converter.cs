/*
 * Copyright (C) 2018-2019, Vistatec or third-party contributors as indicated
 * by the @author tags or express copyright attribution statements applied by
 * the authors. All third-party contributions are distributed under license by
 * Vistatec.
 *
 * This file is part of JliffGraphTools.
 *
 * JliffGraphTools is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * JliffGraphTools is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with this program. If not, write to:
 *
 *     Free Software Foundation, Inc.
 *     51 Franklin Street, Fifth Floor
 *     Boston, MA 02110-1301
 *     USA
 *
 * Also, see the full LGPL text here: <http://www.gnu.org/copyleft/lesser.html>
 */


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