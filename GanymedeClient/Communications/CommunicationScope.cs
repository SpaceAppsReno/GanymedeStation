using System;
using System.Collections.Generic;

// Ganymede Specific Namespaces.
using Ganymede.Interfaces;
using System.Linq;

namespace Ganymede.Communications
{
    public class CommunicationScope : IDisposable
    {
        #region Private Members

        private bool isDisposed = false;
        private Comms communicationEngine = null;

        #endregion Private Members

        #region Constructor

        public CommunicationScope(string uriAddress)
        {
            communicationEngine = new Comms(uriAddress);
        }

        #endregion Constructor

        #region IDisposable

        public void Dispose()
        {
            communicationEngine = null;
            isDisposed = true;
        }

        #endregion IDisposable

        #region Public Methods     

        public IEnumerable<IBaseStation> GetAllBaseStations()
        {
            if(!CanBeUsed())
            {
                // Try to connect.
            }

            List<BaseStation> tempBaseStations = communicationEngine.GetAllBaseStations();
            IEnumerable<IBaseStation> baseStations = tempBaseStations.Select(stations => (stations as IBaseStation));

            return baseStations;
        }

        public IEnumerable<IPod> GetAllPodsFromStation(string baseStationId)
        {
            if (!CanBeUsed())
            {
                // Try to connect.
            }

            List<Pod> tempPods = communicationEngine.GetAllPodsOnAStation(baseStationId);
            IEnumerable<IPod> pods = tempPods.Select(podses => (podses as IPod));

            return pods;
        }

        public IBaseStation GetABaseStation(string baseStationId)
        {
            if (!CanBeUsed())
            {
                // Try to connect.
            }

            return communicationEngine.GetABaseStation(baseStationId) as IBaseStation;
        }

        public IPod GetAPod(string baseStationId, string podId)
        {
            if (!CanBeUsed())
            {
                // Try to connect.
            }

            return communicationEngine.GetAPod(baseStationId, podId) as IPod;
        }
       

        #endregion Public Methods

        #region Private Methods

        private bool CanBeUsed()
        {
            bool isUsable = true;

            if (isDisposed)
                throw new ObjectDisposedException("CommunicationScope", "This object has been disposed.");

            if (!CheckIfConnectionEstablished())
            {
                isUsable = false;
            }

            return isUsable;
        }

        private bool CheckIfConnectionEstablished()
        {
            bool connectionEstablished = false;

            return connectionEstablished;
        }

        #endregion
    }
}
