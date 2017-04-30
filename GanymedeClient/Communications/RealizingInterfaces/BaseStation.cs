using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ganymede.Interfaces;
using System.Runtime.Serialization;

namespace Ganymede.Communications
{
    internal class BaseStation : IBaseStation
    {
        public BaseStation(string id)
        {
            Id = id;
        }

        public BaseStation(BaseStationJson translateableObject)
        {
            Id = translateableObject.Id;
            Name = translateableObject.Name;
            FlowRate = translateableObject.FlowRate;
            Voltage = translateableObject.Voltage;
            ValvesConnectedToBase = new List<string>(translateableObject.Valves);
            PodsConnectedToBase = new List<IPod>();
        }

        public string Name { get; set; }
        public string Id { get; private set; }
        public double FlowRate { get; set; }
        public double Voltage { get; set; }
        public IList<string> ValvesConnectedToBase { get; set; }
        public IList<IPod> PodsConnectedToBase { get; set; }
    }
}
