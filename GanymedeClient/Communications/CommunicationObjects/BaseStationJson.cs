using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ganymede.Communications
{
    [DataContract]
    internal class BaseStationJson
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "_id")]
        public string Id { get; set; }

        [DataMember(Name = "modified")]
        public string Modified { get; set; }

        [DataMember(Name = "flow")]
        public double FlowRate { get; set; }

        [DataMember(Name = "voltage")]
        public double Voltage { get; set; }

        [DataMember(Name = "__v")]
        public int Version { get; set; }

        [DataMember(Name = "valves")]
        public string[] Valves { get; set; }

        [DataMember(Name = "pods")]
        public string[] Pods { get; set; }
    }
}
