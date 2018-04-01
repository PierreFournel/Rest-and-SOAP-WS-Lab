# Rest-and-SOAP-WS-Lab

Contenu du projet :

* ConsoleClientVelib: client console utilisant méthodes synchrones de WcfSoapVelib.
* GUIClientVelib: client GUI utilisant méthodes asynchrones du WcfSoapVelib.
* WcfSoapVelib: service SOAP utilisant le WSVelib.

Extensions choisies :

* Interface graphique pour le client.
* Ajout d'un cache pour réduire les interactions déjà effectuées.
* Méthodes asynchrones dans le service SOAP pour utilisées dans l'inteface graphique.


Execution : 
* Importer le projet (fichier Rest-and-SOAP-WS-Lab à la racine (Microsoft Visual Studio Solution (.sln)))
* Dans l'onglet "Projet" -> Gérer les packages NuGet -> Désinstaller et réinstaller le package Newtonsoft.Json version 11.0.2
* Définir au choix le sous projet GUIClientVelib ou ConsoleClientVelib comme projet de démarrage puis "Démarrer"


