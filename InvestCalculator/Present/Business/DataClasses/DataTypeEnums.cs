using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DataClasses
{
    public class DataTypeEnums
    {
        public enum TypeOfResult { Percent = 1, Profit = 2 };

        public enum Broker{Degiro = 1, Fio = 2, CeskaSpor = 3};

        public enum StockExchange { Praha = 1, USA = 2, GB = 3 };
    }
}
