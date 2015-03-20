using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Brokers
{
    public class BrokerCS: IBroker
    {

        public string Description(DataClasses.DataBroker dataBroker)
        {
            throw new NotImplementedException();
        }

        public decimal FeeBuy(DataClasses.DataBroker dataBroker, out string description)
        {
            throw new NotImplementedException();
        }

        public decimal FeeSell(DataClasses.DataBroker dataBroker, out string description)
        {
            throw new NotImplementedException();
        }


        public decimal ExchangeCost(DataClasses.DataBroker dataBroker, out string description)
        {
            throw new NotImplementedException();
        }
    }
}
