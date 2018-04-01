using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


namespace WcfSoapVelib
{
    public class VelibOperations : IVelibOperations
    {
        private static readonly string key = "3bbc5d82e844bec70aca161b6604188db3b873c5";
        private IList<string> cities = new List<string>();
        private Dictionary<string, string> cache_extension = new Dictionary<string, string>();



        private string getJsonStationsOneCity(string city)
        {
            if (!cache_extension.ContainsKey(city))
            {
                WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract=" + city + "&apiKey=" + key);
                WebResponse response = request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                cache_extension.Add(city, responseFromServer);

                return responseFromServer;
            }
            else
            {
                return cache_extension[city];
            }
        }

        private string getJSonAllContrats()
        {
            WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/contracts?apiKey=" + key);
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            return responseFromServer;
        }

        private Task<string> getJsonAsync(string city)
        {
            return Task<string>.Run(() => {
                return getJsonStationsOneCity(city);
            });
        }

/*
        public async Task<IList<string>> getContratsAsync()
        {
            IList<string> result = new List<string>();
            //Task<string> getJsonContrats = getJsonAsync();
            string contrats = await getJsonContrats;

            JArray json = JArray.Parse(contrats);

            foreach (var child in json)
            {
                result.Add((string)child["name"]);
            }

            return result;
        }
        */
        public IList<string> getContrats()
        {

            IList<string> listContrats = new List<string>();
            string contrats = getJSonAllContrats();

            JArray json = JArray.Parse(contrats);

            foreach (var child in json)
            {
                listContrats.Add((string)child["name"]);
            }

            return listContrats;

        }

        public int getAvailableBikes(string city, string station)
        {
            string cityj = getJsonStationsOneCity(city);


            JArray json = JArray.Parse(cityj);

            foreach (var child in json)
            {
                string name = (string)child["name"];
                if (name.ToLower().Contains(station.ToLower()))
                {
                    return Convert.ToInt32(child["available_bikes"]);
                }
            }

            return -1;
        }


        public async Task<int> getAvailableBikesAsync(string city, string station)
        {
            Task<string> getJson = getJsonAsync(city);
            string cityJson = await getJson;

            JArray json = JArray.Parse(cityJson);

            foreach (var child in json)
            {
                string name = (string)child["name"];
                if (name.ToLower().Contains(station.ToLower()))
                {
                    return Convert.ToInt32(child["available_bikes"]);
                }
            }

            return -1;
        }

        public IList<string> getStations(string city)
        {
            IList<string> result = new List<string>();
            string cityJson = getJsonStationsOneCity(city);

            JArray json = JArray.Parse(cityJson);

            foreach (var child in json)
            {
                result.Add((string)child["name"]);
            }

            return result;
        }

        public async Task<IList<string>> getStationsAsync(string city)
        {
            IList<string> result = new List<string>();
            Task<string> getJson = getJsonAsync(city);
            string cityJson = await getJson;

            JArray json = JArray.Parse(cityJson);

            foreach (var child in json)
            {
                result.Add((string)child["name"]);
            }

            return result;
        }




    }
}
