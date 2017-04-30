using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Ganymede.Communications
{
    /// <summary>
    /// Comms is a class that receives the most up-to-date data from the Ganymede base station. It also 
    /// </summary>
    internal class Comms
    {
        private static HttpClient client = new HttpClient();
        private static Uri uri;

        public Comms(string uriAddress)
        {
            uri = new Uri(uriAddress);
            client.BaseAddress = uri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region Public Methods

        public List<BaseStation> GetAllBaseStations()
        {
            List<BaseStation> baseStations = new List<BaseStation>();

            var baseStationsTask = GetBaseStations();

            string[] baseStationNames = baseStationsTask.Result;

            foreach(string baseStationName in baseStationNames)
            {
                if(!string.IsNullOrEmpty(baseStationName))
                    baseStations.Add(GetABaseStation(baseStationName));
            }

            return baseStations;
        }

        public List<Pod> GetAllPodsOnAStation(string baseStation)
        {
            List<Pod> podsOnStation = new List<Pod>();

            var podTask = GetPodsFromStation(baseStation);
            string[] podNames = podTask.Result;

            foreach(string podName in podNames)
            {
                podsOnStation.Add(GetAPod(baseStation, podName));
            }

            return podsOnStation;
        }

        public BaseStation GetABaseStation(string Id)
        {
            BaseStationJson stationFromCall = GetBaseStation(Id).Result;
            BaseStation stationToReturn = new BaseStation(stationFromCall);

            foreach(string podName in stationFromCall.Pods)
            {
                stationToReturn.PodsConnectedToBase.Add(GetAPod(stationToReturn.Name, podName));
            }

            return stationToReturn;
        }

        public Pod GetAPod(string BaseStationId, string PodId)
        {
            Pod podtoReturn = new Pod(GetPod(BaseStationId, PodId).Result);
            return podtoReturn;
        }


        #endregion

        #region Private Methods

        private static async Task<bool> RunPOSTTask(BaseStation baseStationToUpdate)
        {
            string newUri = string.Format("{0}/{1}", uri, baseStationToUpdate.Id);
            HttpResponseMessage response = await client.PostAsJsonAsync<BaseStation>(newUri, baseStationToUpdate);

            return response.IsSuccessStatusCode;
        }

        private static async Task<string[]> GetBaseStations()
        {
            HttpResponseMessage response = await client.GetAsync(uri);

            NamesJson[] baseStations = null;
            
            if(response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();

                baseStations = SerializerWrapper.DeserializeFromJson<NamesJson[]>(stream);
            }
            List<string> AListToMakeItEasy = new List<string>();
            foreach(NamesJson name in baseStations)
            {
                AListToMakeItEasy.Add(name.Results);
            }

            return AListToMakeItEasy.ToArray();
        }

        private static async Task<string[]> GetPodsFromStation(string baseStationName)
        {
            string newUri = string.Format("{0}/{1}", uri, baseStationName);
            HttpResponseMessage response = await client.GetAsync(newUri);
            NamesJson[] pods = null;

            if (response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();

                pods = SerializerWrapper.DeserializeFromJson<NamesJson[]>(stream);
            }
            List<string> AListToMakeItEasy = new List<string>();
            foreach (NamesJson name in pods)
            {
                AListToMakeItEasy.Add(name.Results);
            }

            return AListToMakeItEasy.ToArray();
        }

        private static async Task<BaseStationJson> GetBaseStation(string baseStationID)
        {
            string baseStationURI = string.Format("{0}/{1}", uri.AbsolutePath, baseStationID);
            HttpResponseMessage response = await client.GetAsync(baseStationURI);

            BaseStationJson thisBaseStation = null;
            if (response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();
                thisBaseStation = SerializerWrapper.DeserializeFromJson<BaseStationJson>(stream);
            }

            return thisBaseStation;
        }

        private static async Task<PodJson> GetPod (string baseStationID, string PodID)
        {
            string podURI = string.Format("{0}/{1}/{2}",uri.AbsolutePath, baseStationID, PodID);
            HttpResponseMessage response = await client.GetAsync(podURI);

            PodJson thisPod = null;
            if (response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();
                thisPod = SerializerWrapper.DeserializeFromJson<PodJson>(stream);
            }

            return thisPod;
        }
        
        #endregion
    }
}
