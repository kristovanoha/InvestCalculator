using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DataClasses
{
    public class DataBroker
    {
        public DataTypeEnums.Broker Broker { get; set; }
        public DataTypeEnums.StockExchange StockExchange { get; set; }
        public int CountShares { get; set; }
        public decimal PriceShareSell { get; set; }
        public decimal PriceShareBuy { get; set; }
        public DataExchange DateExchangePrice { get; set; }
    }
}
