using Business.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Brokers
{
    public class BrokerFIO : IBroker
    {

        public decimal FeeBuy(DataBroker dataBroker, out string description)
        {           
            IStockExchange stockExchange = FactoryStockExchange.GetStockExchange(dataBroker.StockExchange);
            decimal feeBuy = stockExchange.FeeBuy(dataBroker, out description);
         
            return feeBuy;
        }


        public decimal FeeSell(DataBroker dataBroker, out string description)
        {
            IStockExchange stockExchange = FactoryStockExchange.GetStockExchange(dataBroker.StockExchange);
            decimal feeSell = stockExchange.FeeSell(dataBroker, out description);
            return feeSell;
        }
        

        public decimal ExchangeCost(DataBroker dataBroker, out string description)
        {
            IStockExchange stockExchange = FactoryStockExchange.GetStockExchange(dataBroker.StockExchange);
            decimal exchCost = stockExchange.ExchangeCost(dataBroker, out description);
            return exchCost;
        }


        public string Description(DataBroker dataBroker)
        {
            IStockExchange stockExchange = FactoryStockExchange.GetStockExchange(dataBroker.StockExchange);
            string nameStock = stockExchange.Description(dataBroker);
            return "Broker FIO: " + nameStock;
        }
  


        internal class FactoryStockExchange
        {

            public static IStockExchange GetStockExchange(DataTypeEnums.StockExchange stockExhange)
            {
                IStockExchange result;

                switch (stockExhange)
                {
                    case DataTypeEnums.StockExchange.Praha:
                        result = new Prague();
                        break;
                    case DataTypeEnums.StockExchange.USA:
                        result = new Prague();
                        break;
                    default:
                        result = new Prague();
                        break;
                }

                return result;
            }

        }


        internal class Prague : IStockExchange
        {
            private const decimal PAY_MINIMUM = 40;
            private const decimal PAY_MAXIMUM = 1190;
            private const decimal PERCENT = 0.0035M;

            private const decimal PAY_MINIMUM_BC = 10;
            private const decimal PAY_MAXIMUM_BC = 4000;
            private const decimal PERCENT_BC = 0.0001M;

            public string Description(DataBroker dataBroker)
            {
                return " Pražská burza";
            }


            public decimal FeeBuy(DataBroker dataBroker, out string description)
            {
                decimal feeSummary = feePrague(dataBroker.CountShares, dataBroker.PriceShareBuy);
                description = "Poplatek nákup " + feeSummary;
                return feeSummary;
            }


            private  decimal feePrague(int CountShares, decimal Price)
            {
                decimal feeBuy = CountShares * Price * PERCENT;
                feeBuy = feeBuy < PAY_MINIMUM ? PAY_MINIMUM : feeBuy;
                feeBuy = feeBuy > PAY_MAXIMUM ? PAY_MAXIMUM : feeBuy;

                decimal feeBuyBC = CountShares * Price * PERCENT_BC;
                feeBuyBC = feeBuyBC < PAY_MINIMUM_BC ? PAY_MINIMUM_BC : feeBuyBC;
                feeBuyBC = feeBuyBC > PAY_MAXIMUM_BC ? PAY_MAXIMUM_BC : feeBuyBC;

                decimal feeSummary = Math.Round(feeBuy + feeBuyBC);
                return feeSummary;
            }


            public decimal FeeSell(DataBroker dataBroker, out string description)
            {
                decimal feeSell =  feePrague(dataBroker.CountShares, dataBroker.PriceShareSell);
                description = "Poplatek prodej " + feeSell;
                return feeSell;
            }


            public decimal ExchangeCost(DataBroker dataBroker, out string description)
            {
                description = string.Empty;
                return 0;
            }
        }


        internal class USA : IStockExchange
        {
            private const decimal FIX_AMOUNT = 20;
            private const decimal PERCENT = 0.001M;

            public string Description(DataBroker dataBroker)
            {
                return " USA burza";
            }


            public decimal FeeBuy(DataBroker dataBroker, out string description)
            {

                decimal feeBuy = Math.Round(dataBroker.CountShares * dataBroker.PriceShareBuy * PERCENT + FIX_AMOUNT);
                description = "Poplatek nákup " + feeBuy;
                return feeBuy;
            }


            public decimal FeeSell(DataBroker dataBroker, out string description)
            {
                decimal feeSell = Math.Round(dataBroker.CountShares * dataBroker.PriceShareSell * PERCENT + FIX_AMOUNT);
                description = "Poplatek prodej " + feeSell;
                return feeSell;
            }


            public decimal ExchangeCost(DataBroker dataBroker, out string description)
            {
                description = string.Empty;
                return 0;
            }
        }


    }
}
