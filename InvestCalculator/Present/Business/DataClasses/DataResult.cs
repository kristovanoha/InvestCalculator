using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Business.DataClasses;

namespace Business.DataClasses
{
    public class DataResult
    {
        public DataTypeEnums.TypeOfResult TypeResult { get; set; }
        public decimal PriceShareSell { get; set; }
        public decimal PercentProfit { get; set; }
        public decimal PercentProfitYearly { get; set; }
        public string Description { get; set; }
        public decimal Fee { get; set; }
        public decimal Tax { get; set; }

        public decimal ExchangeCost { get; set; }

        public decimal AmountProfit { get; set; }
    }
}
