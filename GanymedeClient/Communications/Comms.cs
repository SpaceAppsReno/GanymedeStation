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
        private static Uri uri = new Uri("");

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
            var baseStationsTask = GetBaseStations();

            return baseStationsTask.Result;
        }

        public List<Pod> GetAllPodsOnAStation(string baseStation)
        {
            var podTask = GetPodsFromStation(baseStation);

            return podTask.Result;
        }

        public BaseStation GetABaseStation(string Id)
        {
            return GetBaseStation(Id).Result;
        }

        public Pod GetAPod(string BaseStationId, string PodId)
        {
            return GetPod(BaseStationId, PodId).Result;
        }


        #endregion

        #region Private Methods

        private static async Task<bool> RunPOSTTask(BaseStation baseStationToUpdate)
        {
            string newUri = string.Format("{0}/{1}", uri, baseStationToUpdate.Id);
            HttpResponseMessage response = await client.PostAsJsonAsync<BaseStation>(newUri, baseStationToUpdate);

            return response.IsSuccessStatusCode;
        }

        private static async Task<List<BaseStation>> GetBaseStations()
        {
            HttpResponseMessage response = await client.GetAsync(uri);

            List<BaseStation> baseStations = new List<BaseStation>();
            
            if(response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();
                baseStations = SerializerWrapper.DeserializeFromJson<List<BaseStation>>(stream);
            }

            return baseStations;
        }

        private static async Task<List<Pod>> GetPodsFromStation(string baseStationId)
        {
            string newUri = string.Format("{0}/{1}", uri, baseStationId);
            HttpResponseMessage response = await client.GetAsync(newUri);

            List<Pod> pods = new List<Pod>();

            if (response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();
                pods = SerializerWrapper.DeserializeFromJson<List<Pod>>(stream);
            }

            return pods;
        }

        private static async Task<BaseStation> GetBaseStation(string baseStationID)
        {
            string baseStationURI = string.Format("{0}/{1}", uri.AbsolutePath, baseStationID);
            HttpResponseMessage response = await client.GetAsync(baseStationURI);

            BaseStation thisBaseStation = null;
            if (response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();
                thisBaseStation = SerializerWrapper.DeserializeFromJson<BaseStation>(stream);
            }

            return thisBaseStation;
        }

        private static async Task<Pod> GetPod (string baseStationID, string PodID)
        {
            string podURI = string.Format("{0}/{1}/{2}",uri.AbsolutePath, baseStationID, PodID);
            HttpResponseMessage response = await client.GetAsync(podURI);

            Pod thisPod = null;
            if (response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();
                thisPod = SerializerWrapper.DeserializeFromJson<Pod>(stream);
            }

            return thisPod;
        }
        
        #endregion
    }
}
