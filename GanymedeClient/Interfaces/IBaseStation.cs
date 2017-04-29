using System;
using System.Collections.Generic;

namespace Ganymede.Interfaces
{
    public interface IBaseStation
    {
        #region Read Data

        /// <summary>
        /// The unique identifier for this base station.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The flow rate of water to the Pods. This is the main line, so total flow.
        /// </summary>
        double FlowRate { get; }

        /// <summary>
        /// The pods that are connected to this specific base.
        /// </summary>
        IEnumerable<IPod> PodsConnectedToBase { get; }

        #endregion
    }
}
