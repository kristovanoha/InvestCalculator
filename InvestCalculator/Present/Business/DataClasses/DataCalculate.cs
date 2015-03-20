using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Business.DataClasses.;

namespace Business.DataClasses
{
    public class DataCalculate
    {
        public DataTypeEnums.TypeOfResult TypeResult { get; set; }

        public DataTypeEnums.Broker Broker { get; set; }
        public DataTypeEnums.StockExchange StockExchange { get; set; }

        public DateTime DateBuy { get; set; }
        public DateTime DateSell { get; set; }

        public int CountShares { get; set; }
        public decimal PriceShareSell { get; set; }
        public decimal PriceShareBuy { get; set; }
        public decimal ProfitPercent { get; set; }
        public bool MustPayTax { get; set; }

        public bool UseExchange { get; set; }

        //Exchange
        public DataExchange DateExchangePrice { get; set; }
    }
}
