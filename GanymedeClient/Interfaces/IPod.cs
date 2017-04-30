using System;
using System.Collections.Generic;
using System.Text;

namespace Ganymede.Interfaces
{
    public interface IPod
    {
        /// <summary>
        /// Unique Identifier of this Pod.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Name of this Pod.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Unit Less value representing the amount of moisture at a Pod.
        /// </summary>
        int Moisture { get; }
        
        /// <summary>
        /// Temperature, default in degrees C.
        /// </summary>
        double Temperature { get; }

        /// <summary>
        /// Lumens from Pod.
        /// </summary>
        int Light { get; }

        /// <summary>
        /// Battery voltage. This can tell when a battery is low.
        /// </summary>
        double Voltage { get; }
    }
}
