﻿using System.ServiceModel;

namespace EventsLib
{
    [ServiceContract(CallbackContract = typeof(IVelibServiceEvents))]
    public interface IVelibService {
        [OperationContract]
        void SubscribeVelibAvailable(string city, string station);
    }
}