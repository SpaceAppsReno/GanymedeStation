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
    internal class BaseStation : IBaseStation
    {
        public BaseStation(string id)
        {
            Id = id;
        }

        [DataMember]
        public string Id { get; private set; }
        [DataMember]
        public double FlowRate { get; set; }
        [DataMember]
        public IEnumerable<IPod> PodsConnectedToBase { get; set; }

    }
}
