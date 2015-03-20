using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Business.DataClasses;
using Business.IocContainer;

namespace Business.Brokers
{
    public class FactoryBroker
    {
        public static IBroker CreateBroker(DataTypeEnums.Broker broker)
        {
            IBroker result;

            switch (broker)
            {
                case DataTypeEnums.Broker.Degiro :
                    result = ContainerManager.Container.Resolve<BrokerDegiro>();
                    break;
                case DataTypeEnums.Broker.CeskaSpor :
                    result = new BrokerCS();
                    break;
                case DataTypeEnums.Broker.Fio:
                    result = new BrokerFIO();
                    break;
                default:
                    result = new BrokerDegiro();
                    break;
            }

            return result;
        }

    }
}
