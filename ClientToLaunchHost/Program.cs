using EventsLib;
using System;
using System.ServiceModel;


namespace ClientToLaunchHost
{
    class Program{
        static void Main(string[] args){

            try
            {
                ServiceHost host = new ServiceHost(typeof(VelibService));
                host.Open();
                Console.WriteLine("Service is Hosted as http://localhost:9011/CalcService");
                Console.WriteLine("\nPress  key to stop the service.");
                Console.ReadLine();
                host.Close();
            }
            catch (Exception eX)
            {
                Console.WriteLine("There was en error while Hosting Service [" + eX.Message + "]");
                Console.WriteLine("\nPress  key to close.");
                Console.ReadLine();
            }
        }
    }
}
