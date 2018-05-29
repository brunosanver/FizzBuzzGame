using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FizzBuzzInterfaces
{
    [ServiceContract]
    public interface IWCFFizzBuzzService
    {
        [OperationContract]
        List<string> GetData(string value);
    }
}
