using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfSoapVelib
{
    [ServiceContract]
    public interface IVelibOperations
    {
        [OperationContract]
        IList<string> getStations(string city);

        [OperationContract]
        int getAvailableBikes(string city, string station);

        [OperationContract]
        IList<string> getContrats();
    }
}
