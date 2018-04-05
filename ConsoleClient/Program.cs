using System;
using System.ServiceModel;

namespace ConsoleClient
{
    class Program{
        static void Main(string[] args){

            VelibServiceCallbackSink objsink = new VelibServiceCallbackSink();
            InstanceContext iCntxt = new InstanceContext(objsink);
            VelibServiceReference.VelibServiceClient objClient = new VelibServiceReference.VelibServiceClient(iCntxt);

            Console.WriteLine("Choisissez une ville :");
            string cityChoice = Console.ReadLine();

            Console.WriteLine("Choisissez une station de " + cityChoice + " :");
            string stationChoice = Console.ReadLine();

            Console.WriteLine("Mise à jour automatique toutes les 5secondes des donéées de la station " + stationChoice.ToUpper() + " à " + cityChoice.ToUpper()+".\n");


            objClient.SubscribeVelibAvailable(cityChoice, stationChoice);

            Console.ReadLine();
        }
    }
}