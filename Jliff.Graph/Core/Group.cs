using System;
using System.Collections.Generic;
using Localization.Jliff.Graph.Interfaces;
using Newtonsoft.Json;

namespace Localization.Jliff.Graph
{
    public class Group : ISubfile
    {
        [JsonProperty(Order = 10)]
        public List<ISubfile> Subgroups = new List<ISubfile>();

        public Group(string id)
        {
            Id = id;
        }

        public Group(string id, params object[] content)
        {
            Id = id;

            foreach (var parobj in content)
                if (parobj is Unit)
                    Subgroups.Add(parobj as Unit);
                else if (parobj is Group)
                    Subgroups.Add(parobj as Group);
                else if (parobj is IEnumerable<Unit>)
                    foreach (var grpparobj in parobj as IEnumerable<Unit>)
                        Subgroups.Add(grpparobj);
                else if (parobj is IEnumerable<Group>)
                    foreach (var grpparobj in parobj as IEnumerable<Group>)
                        Subgroups.Add(grpparobj);
                else
                    throw new ArgumentException();
        }

        public string CanResegment { get; set; } = "no";
        public string Id { get; set; }
        public string Kind => "group";
        public List<Metadata> Metadata { get; set; }
        public string Name { get; set; }
        public string SrcDir { get; set; }
        public bool Translate { get; set; }
        public string TrgDir { get; set; }
        public string Type { get; set; }
    }
}