using System;

namespace ConsoleClientVelib
{
    class Program
    {
        static WcfSoapVelib.VelibOperationsClient client;
        static bool isRunning = true;


        static void help()
        {
            Console.WriteLine("\nEntrez une des commandes suivantes :");
            Console.WriteLine("\n\tvilles : Liste des villes disponibles");
            Console.WriteLine("\n\tstations [ville] : Liste des stations disponibles dans [ville]");
            Console.WriteLine("\n\tvelibs [ville] [station] : Nombre de velibs disponibles de la [station] dans la [ville]");
            Console.WriteLine("\n\thelp : Aide");
            Console.WriteLine("\n\texit : Quitte l'application\n");
        }

        static void Main(string[] args)
        {
            client = new WcfSoapVelib.VelibOperationsClient();

            Console.WriteLine("Bienvenue !");

            help();

           
            while (isRunning)
            {
                Console.WriteLine("Entrez une des commandes :");
                string client_action = Console.ReadLine();

                string[] client_action_split = client_action.Split(' ');

                switch (client_action_split[0])
                {
                    case "villes":
                        villes();
                        break;
                   
                    case "stations":
                        if (client_action_split.Length < 2)
                            Console.WriteLine("Aucune ville n'a été informé.");
                        else
                            stations(client_action_split[1]);
                        break;
                    case "velibs":
                        if (client_action_split.Length < 3)
                            Console.WriteLine("Aucune ville ou station n'a été informé.");
                        else
                            availableVelibs(client_action_split[1], client_action_split[2]);
                        break;
                    case "help":
                        help();
                        break;
                    case "exit":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Commande inconnue.");
                        break;
                }
            }
        }

        static void villes()
        {
            foreach (string ville in client.getContrats())
            {
                Console.WriteLine("\t"+ville);
            }
            Console.WriteLine();
        }

        static void stations(string ville)
        {
            foreach (string station in client.getStations(ville))
            {
                Console.WriteLine("\t"+station);
            }
            Console.WriteLine();
        }

        static void availableVelibs(string ville, string station)
        {
            Console.WriteLine("\t"+ client.getAvailableBikes(ville, station) + " velibs sont disponibles à la station " +station.ToUpper() +" dans la ville de "+ville.ToUpper()+".");
        }


    }
}
