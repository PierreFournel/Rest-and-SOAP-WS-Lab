using System.ServiceModel;

namespace EventsLib
{
    interface IVelibServiceEvents
    {
        [OperationContract(IsOneWay = true)]
        void VelibAvailable(string city, string station, int velibAvailables);
    }
}
