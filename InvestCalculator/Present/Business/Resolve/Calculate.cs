using Business.Brokers;
using Business.DataClasses;
using Business.IocContainer;
using Castle.Windsor;
using System;
using Castle.Windsor.Configuration.Interpreters;
using System.Runtime.Remoting.Contexts;
using Castle.Windsor.Installer;

namespace Business.Resolve
{
    public class Calculate : ICalculate
    {
        public DataResult Calculates(DataCalculate dataCalculate)
        {
            DataResult result = new DataResult();
            
            //end sazebník
            if (dataCalculate.TypeResult == DataTypeEnums.TypeOfResult.Profit)
            {
                result = calculateProfit(dataCalculate);
            }
            else if (dataCalculate.TypeResult == DataTypeEnums.TypeOfResult.Percent)
            {
                result = calculateByPercent(dataCalculate);
            }
            return result;
        }


        private DataBroker transferToBrokerData(DataCalculate dataCalculate)
        {
            DataBroker brokerData = new DataBroker();
            brokerData.Broker = dataCalculate.Broker;
            brokerData.StockExchange = dataCalculate.StockExchange;
            brokerData.CountShares = dataCalculate.CountShares;
            brokerData.PriceShareBuy = dataCalculate.PriceShareBuy;
            brokerData.PriceShareSell = dataCalculate.PriceShareSell;
            brokerData.DateExchangePrice = dataCalculate.DateExchangePrice;
            return brokerData;
        }


        private DataResult calculateByPercent(DataCalculate dataCalculate)
        {
            DataResult result = new DataResult();
            bool isNotFinish = true;

            dataCalculate.PriceShareSell = dataCalculate.PriceShareBuy + 1;
            decimal addPrice= dataCalculate.ProfitPercent > 0 ? 1 : -1;
            while (isNotFinish)
            {
                dataCalculate.PriceShareSell += addPrice;
                decimal profit = calculateProfit(dataCalculate).PercentProfit;

                if (profit >= dataCalculate.ProfitPercent && addPrice> 0)
                {
                    isNotFinish = false;
                }
                else if (profit <= dataCalculate.ProfitPercent && addPrice< 0)
                {
                    isNotFinish = false;
                }
            }

            result = calculateProfit(dataCalculate);
            result.PriceShareSell = dataCalculate.PriceShareSell;

            return result;
        }


        private DataResult calculateProfit(DataCalculate dataCalculate)
        {
            DataResult result = new DataResult();

            //sazebník- načtení

            decimal brokerFixFee = 20;
            decimal brokerPercent = 0.001M;
            decimal stateTax = 0.15M;

            decimal profit = (dataCalculate.PriceShareSell - dataCalculate.PriceShareBuy) * dataCalculate.CountShares;
            //decimal feeBuyOld = Math.Round(dataCalculate.CountShares * dataCalculate.PriceShareBuy * brokerPercent + brokerFixFee);
            //decimal feeSellOld = Math.Round(dataCalculate.CountShares * dataCalculate.PriceShareSell * brokerPercent + brokerFixFee);


            IBroker broker = FactoryBroker.CreateBroker(dataCalculate.Broker);


            string feeBuyDesc, feeSellDesc,excCostDesc;
            decimal feeBuy = broker.FeeBuy(transferToBrokerData(dataCalculate), out feeBuyDesc);
            decimal feeSell = broker.FeeSell(transferToBrokerData(dataCalculate), out feeSellDesc);
            decimal exchCost = broker.ExchangeCost(transferToBrokerData(dataCalculate), out excCostDesc);

            decimal feeAll = feeBuy + feeSell ;




            decimal profitBeforeTax = profit - feeAll - exchCost; //nezdanení zisk
            decimal taxAmountPay = dataCalculate.MustPayTax && profitBeforeTax > 0 ? Math.Round(profitBeforeTax * stateTax) : 0;

            decimal profitAmount = Math.Round(profit - taxAmountPay - feeAll);

            decimal buyShareAndAllFee = feeAll + (dataCalculate.PriceShareBuy * dataCalculate.CountShares);
           
            decimal profitPercent = Math.Round((profitAmount / buyShareAndAllFee *100), 1) ;

            result.Description = "Popis výpočtu kalkulace: ";
            result.Description += broker.Description(transferToBrokerData(dataCalculate));
            result.Description += ", " + feeBuyDesc;
            result.Description += ", " + feeSellDesc;
            if (dataCalculate.UseExchange) //kurzy
            {
                result.Description += '\n' + excCostDesc + exchCost;
            }
            result.Description += '\n' + "Daň " + taxAmountPay;
            result.Description += ", Zisk částka " + profitAmount;

            result.Fee = feeAll;
            result.Tax = taxAmountPay;

            result.ExchangeCost = exchCost;

            result.AmountProfit = profitAmount;
            result.PercentProfit = profitPercent;

            result.TypeResult = dataCalculate.TypeResult;

            result.PercentProfitYearly = calculateYearlyPercentProfit(result.PercentProfit, dataCalculate.DateBuy, dataCalculate.DateSell);

            return result;

        }


        /// <summary>
        /// Roční zisk 
        /// </summary>
        private decimal calculateYearlyPercentProfit(decimal profit, DateTime dateBuy, DateTime dateSell)
        {
            decimal result = 0;

            var roz = dateSell - dateBuy;

            decimal diffDays = (int)(roz.TotalDays);

            result = profit * 365 / (diffDays == 0 ? 1 : diffDays);

            return Math.Round(result, 2);
        }

    }
}
