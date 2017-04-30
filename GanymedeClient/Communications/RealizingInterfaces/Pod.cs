using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ganymede.Interfaces;
using System.Runtime.Serialization;

namespace Ganymede.Communications
{
    [DataContract]
    internal class Pod : IPod
    {
        public Pod(string id)
        {
            Id = id;
        }

        [DataMember]
        public string Id { get; private set; }

        [DataMember]
        public int Moisture { get; set; }

        [DataMember]
        public double Temperature { get; set; }

        [DataMember]
        public int Light { get; set; }

        [DataMember]
        public double Voltage { get; set; }

    }
}
