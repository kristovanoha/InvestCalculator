using System.ComponentModel;
using System.Runtime.InteropServices;
using Business.DataClasses;
using System;

namespace Business.Brokers
{
    public class BrokerDegiro : IBroker
    {

        private IocContainer.IocContainer _container;
        public BrokerDegiro()
        {
            _container = IocContainer.IocContainer.GetContainer; // for test
        }


        public decimal FeeBuy(DataBroker dataBroker, out string description)
        {
            //old
            IStockExchange stockExchange = FactoryStockExchange.GetStockExchange(dataBroker.StockExchange);
            decimal feeBuy = stockExchange.FeeBuy(dataBroker, out description);
            return feeBuy;

            //POKUS
            //použití conteineru
            var con = IocContainer.IocContainer.GetContainer;
            var popis = con.Resolve<IBroker>().Description(dataBroker);
            decimal newFeeBuy = _container.Resolve<IBroker>().FeeBuy(dataBroker, out description);

            return feeBuy;
        }


        public decimal FeeSell(DataBroker dataBroker, out string description)
        {
            IStockExchange stockExchange = FactoryStockExchange.GetStockExchange(dataBroker.StockExchange);
            decimal feeSell = stockExchange.FeeSell(dataBroker, out description);
            return feeSell;
        }


        public string Description(DataBroker dataBroker)
        {
            IStockExchange stockExchange = FactoryStockExchange.GetStockExchange(dataBroker.StockExchange);
            string nameStock = stockExchange.Description(dataBroker);
            return "Broker Degiro: " + nameStock;
        }


        public decimal ExchangeCost(DataBroker dataBroker, out string description)
        {
            IStockExchange stockExchange = FactoryStockExchange.GetStockExchange(dataBroker.StockExchange);
            decimal exCost = stockExchange.ExchangeCost(dataBroker, out description);
            return exCost;
        }
    }

    //DI container
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
                    result = new USA();
                    break;
                case DataTypeEnums.StockExchange.GB:
                    result = new GB();
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
        private const decimal FIX_AMOUNT = 20;
        private const decimal PERCENT = 0.001M;
        private const decimal MAXIMUM = 830;

        public string Description(DataBroker dataBroker)
        {
            return " Pražská burza";
        }

        public decimal FeeBuy(DataBroker dataBroker, out string description)
        {

            decimal feeBuy = Math.Round(dataBroker.CountShares * dataBroker.PriceShareBuy * PERCENT + FIX_AMOUNT);
            feeBuy = feeBuy > MAXIMUM ? MAXIMUM : feeBuy;
            description = "Poplatek nákup " + feeBuy;
            return feeBuy;
        }

        public decimal FeeSell(DataBroker dataBroker, out string description)
        {
            decimal feeSell = Math.Round(dataBroker.CountShares * dataBroker.PriceShareSell * PERCENT + FIX_AMOUNT);
            feeSell = feeSell > MAXIMUM ? MAXIMUM : feeSell;
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
        private const decimal FIX_AMOUNT = 0.5M;
        private const decimal AMOUNT_BY_ONE = 0.004M;

        public string Description(DataBroker dataBroker)
        {
            return " USA burza";
        }

        public decimal FeeBuy(DataBroker dataBroker, out string description)
        {

            decimal feeBuy = Math.Round(dataBroker.CountShares * AMOUNT_BY_ONE * dataBroker.DateExchangePrice.ExcBuyDolar + dataBroker.DateExchangePrice.ExcBuyEU  + FIX_AMOUNT);
            decimal dolar = dataBroker.CountShares * AMOUNT_BY_ONE;
            description = descriptionCreate("Poplatek nákup ", dolar, dataBroker.DateExchangePrice.ExcBuyEU, dataBroker.DateExchangePrice.ExcBuyDolar, feeBuy);
            return feeBuy;
        }


        public decimal FeeSell(DataBroker dataBroker, out string description)
        {
            decimal feeSell = Math.Round(dataBroker.CountShares * AMOUNT_BY_ONE * dataBroker.DateExchangePrice.ExcSellDolar + dataBroker.DateExchangePrice.ExcBuyEU + FIX_AMOUNT);
            decimal dolar = dataBroker.CountShares * AMOUNT_BY_ONE;
            description = descriptionCreate("Poplatek prodej ", dolar, dataBroker.DateExchangePrice.ExcSellEU, dataBroker.DateExchangePrice.ExcSellDolar, feeSell);
            return feeSell;
        }


        public decimal ExchangeCost(DataBroker dataBroker, out string description)
        {
            decimal dif = (dataBroker.DateExchangePrice.ExcSellDolar - dataBroker.DateExchangePrice.ExcBuyDolar) * dataBroker.CountShares;
            description = dif < 0 ? "Kurzová ztráta " : "Kurzový zisk ";
            return dif;
        }


        private string descriptionCreate(string sellbuy, decimal dolar, decimal exEu, decimal exDolar, decimal fee)
        {
            string result = '\n' + sellbuy + FIX_AMOUNT + " Eu (" + exEu + " Kč/Eu) + " + Math.Round(dolar, 2) + " Usd ("
                + exDolar + " Kč/Usd) " + fee + " Kč";
            return result;
        }
    }


    internal class GB : IStockExchange
    {
        private const decimal FIX_AMOUNT = 4;
        private const decimal AMOUNT_BY_ONE = 0.04M;
        private const decimal MAXIMUM = 60;

        public string Description(DataBroker dataBroker)
        {
            return "Britanie burza";
        }

        public decimal FeeBuy(DataBroker dataBroker, out string description)
        {

            decimal feeBuy = Math.Round(dataBroker.CountShares * AMOUNT_BY_ONE * dataBroker.DateExchangePrice.ExcBuyDolar + dataBroker.DateExchangePrice.ExcBuyEU + FIX_AMOUNT);
            decimal dolar = dataBroker.CountShares * AMOUNT_BY_ONE;
            feeBuy = feeBuy > MAXIMUM ? MAXIMUM : feeBuy;
            description = descriptionCreate("Poplatek nákup ", dolar, dataBroker.DateExchangePrice.ExcBuyEU, dataBroker.DateExchangePrice.ExcBuyDolar, feeBuy);
            return feeBuy;
        }


        public decimal FeeSell(DataBroker dataBroker, out string description)
        {
            decimal feeSell = Math.Round(dataBroker.CountShares * AMOUNT_BY_ONE * dataBroker.DateExchangePrice.ExcSellDolar + dataBroker.DateExchangePrice.ExcBuyEU + FIX_AMOUNT);
            decimal dolar = dataBroker.CountShares * AMOUNT_BY_ONE;
            description = descriptionCreate("Poplatek prodej ", dolar, dataBroker.DateExchangePrice.ExcBuyEU, dataBroker.DateExchangePrice.ExcSellDolar, feeSell);
            return feeSell;
        }

        private string descriptionCreate(string sellbuy, decimal dolar, decimal exEu, decimal exDolar, decimal fee)
        {
            string result = '\n' + sellbuy + FIX_AMOUNT + " Eu (" + exEu + " Kč/Eu) + " + Math.Round(dolar, 2) + " Usd ("
                + exDolar + " Kč/Usd) " + fee + " Kč";
            return result;
        }

        public decimal ExchangeCost(DataBroker dataBroker, out string description)
        {
            description = string.Empty;
            return 0;
        }
    }


}

