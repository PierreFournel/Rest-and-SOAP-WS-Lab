using System;
using System.Text;
using System.ServiceModel;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Threading;

namespace EventsLib
{
    public class VelibService : IVelibService
    {
        private static readonly string APIKey = "3bbc5d82e844bec70aca161b6604188db3b873c5";

        static Action<string, string, int> velibAvailableEvent = delegate { };

        public void SubscribeVelibAvailable(string city, string station)
        {
            IVelibServiceEvents subscriber =
            OperationContext.Current.GetCallbackChannel<IVelibServiceEvents>();
            velibAvailableEvent += subscriber.VelibAvailable;

            string[] param = new string[] { city, station};

            Thread updater = new Thread(new ParameterizedThreadStart(Update));
            updater.Start(param);
        }

        public void Update(object param)
        {
            string[] mParam = (string[])param;
            string city = mParam[0];
            string station = mParam[1];

            while (true)
            {
                string cityJson = getJson(city);

                JArray json = JArray.Parse(cityJson);

             
                foreach (var child in json)
                {
                    string name = (string)child["name"];
                    if (name.ToLower().Contains(station.ToLower()))
                    {
                        int availableVelibs = Convert.ToInt32(child["available_bikes"]);
                        velibAvailableEvent(city, name, availableVelibs);
                        break;
                    }
                   
                }

                Thread.Sleep(5000);
            }
        }

        private string getJson(string city)
        {
            WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract=" + city + "&apiKey=" + APIKey);
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            return responseFromServer;
        }
    }
}