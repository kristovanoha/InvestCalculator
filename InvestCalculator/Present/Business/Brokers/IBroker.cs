using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DataClasses;

namespace Business.Brokers
{
    public interface IBroker
    {
        string Description(DataBroker dataBroker);

        decimal FeeBuy(DataBroker dataBroker, out string description );
        decimal FeeSell(DataBroker dataBroker, out string description);
        decimal ExchangeCost(DataBroker dataBroker, out string description);
    }
}
