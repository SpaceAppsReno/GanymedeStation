using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ganymede.Communications
{
    [DataContract]
    internal class PodJson
    {
        [DataMember(Name = "_id")]
        public string Id { get; set; }

        [DataMember(Name = "light")]
        public int Light { get; set; }

        [DataMember(Name = "moisture")]
        public int Moisture { get; set; }

        [DataMember(Name = "temperature")]
        public double Temp { get; set; }

        [DataMember(Name = "voltage")]
        public double Voltage { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "basestation")]
        public string ParentStation { get; set; }

        [DataMember(Name = "created")]
        public string Created { get; set; }

        [DataMember(Name = "__v")]
        public int Version { get; set; }

    }
}
