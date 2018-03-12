using System;
using System.Collections.Generic;
using Localization.Jliff.Graph.Interfaces;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class File
    {
        [JsonProperty(Order = 10)]
        public List<ISubfile> Subfiles = new List<ISubfile>();

        [JsonConstructor]
        public File(string id)
        {
            Id = id;
        }

        public File(string id, params object[] content)
        {
            Id = id;

            foreach (var parobj in content)
                if (parobj is Unit)
                    Subfiles.Add(parobj as Unit);
                else if (parobj is Group)
                    Subfiles.Add(parobj as Group);
                else if (parobj is IEnumerable<Unit>)
                    foreach (var grpparobj in parobj as IEnumerable<Unit>)
                        Subfiles.Add(grpparobj);
                else if (parobj is IEnumerable<Ignorable>)
                    foreach (var grpparobj in parobj as IEnumerable<Group>)
                        Subfiles.Add(grpparobj);
                else
                    throw new ArgumentException();
        }

        public string Id { get; set; }
        public List<MetaGroup> Metadata { get; set; }
        public string Original { get; set; }
        public Skeleton Skeleton { get; set; }
    }
}