using System;

namespace ConsoleClient
{
    internal class VelibServiceCallbackSink : VelibServiceReference.IVelibServiceCallback{
        public void VelibAvailable(string city, string station, int velibAvailables)
        {
            var date = DateTime.Now;
            Console.WriteLine("update :\tStation :" + station + "\n\t Ville :" + city + "\n\tVélos disponibles à " + date.Hour + ":" + date.Minute + ":" + date.Second + " : " + velibAvailables+" vélos.");
        }
    }
}