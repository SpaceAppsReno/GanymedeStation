using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ganymede.Interfaces;
using System.Runtime.Serialization;

namespace Ganymede.Communications
{
    internal class Pod : IPod
    {
        public Pod(string id)
        {
            Id = id;
        }

        public Pod(PodJson translateableObject)
        {
            Name = translateableObject.Name;
            Id = translateableObject.Name;
            Moisture = translateableObject.Moisture;
            Temperature = translateableObject.Temp;
            Light = translateableObject.Light;
            Voltage = translateableObject.Voltage;
        }

        public string Name { get; set; }

        public string Id { get; private set; }

        public int Moisture { get; set; }

        public double Temperature { get; set; }

        public int Light { get; set; }

        public double Voltage { get; set; }
    }
}
