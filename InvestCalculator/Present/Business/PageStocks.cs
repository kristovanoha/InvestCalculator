using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Resolve;
using Business.DataClasses;

namespace Business
{
    //stock
    public class PageStocks : INotifyPropertyChanged
    {

        #region Constructor

        public PageStocks()
        {
            initBrokerCombos();
            
        }

        #endregion


        #region Properties

        public bool UseExchange = false; //může dojit ke kurzovým ziskám/ztrátam

        private int countShares = 10;
        public int CountShares
        {
            get { return countShares; }
            set
            {
                countShares = value;
                NotifyPropertyChanged("CountShares");
            }
        }

        private decimal buyPrice = 100;
        public decimal BuyPrice
        {
            get { return buyPrice; }
            set
            {
                buyPrice = value;
                NotifyPropertyChanged("BuyPrice");
            }
        }


        private decimal sellPrice = 150;
        public decimal SellPrice
        {
            get { return sellPrice; }
            set
            {
                if (sellPrice != value)
                {
                    sellPrice = value;
                    NotifyPropertyChanged("SellPrice");
                }
            }
        }


        //čistý zisk
        private decimal profitPercent = 5;
        public decimal ProfitPercent
        {
            get { return profitPercent; }
            set
            {
                if (profitPercent != value)
                {
                    profitPercent = value;
                    NotifyPropertyChanged("ProfitPercent");
                }
            }
        }


        //čistý zisk
        private decimal profitPercentYear = 15;
        public decimal ProfitPercentYear
        {
            get { return profitPercentYear; }
            set
            {
                if (profitPercentYear != value)
                {
                    profitPercentYear = value;
                    NotifyPropertyChanged("ProfitPercentYear");
                }
            }
        }

        //daň
        private decimal tax = 0;
        public decimal Tax
        {
            get { return tax; }
            set
            {
                if (tax != value)
                {
                    tax = value;
                    NotifyPropertyChanged("Tax");
                }
            }
        }


        //profit častka
        private decimal amountProfit = 0;
        public decimal AmountProfit
        {
            get { return amountProfit; }
            set
            {
                if (amountProfit != value)
                {
                    amountProfit = value;
                    NotifyPropertyChanged("AmountProfit");
                }
            }
        }


        //poplatky
        private decimal fee = 20;
        public decimal Fee
        {
            get { return fee; }
            set
            {
                if (fee != value)
                {
                    fee = value;
                    NotifyPropertyChanged("Fee");
                }
            }
        }


        private decimal exchangeProfit = 1;
        public decimal ExchangeProfit
        {
            get { return exchangeProfit; }
            set
            {
                if (exchangeProfit != value)
                {
                    exchangeProfit = value;
                    NotifyPropertyChanged("ExchangeProfit");
                }
            }
        }


        /// <summary>
        /// Procento
        /// </summary>
        private bool typeOfResolve = true;
        public bool TypeOfResolve
        {
            get { return typeOfResolve; }
            set
            {
                typeOfResolve = value;
                NotifyPropertyChanged("TypeOfResolve");
            }
        }


        /// <summary>
        /// Povinost platit daň
        /// </summary>
        private bool mustPayTax = true;
        public bool MustPayTax
        {
            get { return mustPayTax; }
            set
            {
                mustPayTax = value;
                NotifyPropertyChanged("MustPayTax");
            }
        }


        /// <summary>
        /// Popis výpočtu
        /// </summary>
        private string descriptionRecalculate = "nějaký popis";
        public string DescriptionRecalculate
        {
            get { return descriptionRecalculate; }
            set
            {
                if (descriptionRecalculate != value)
                {
                    descriptionRecalculate = value;
                    NotifyPropertyChanged("DescriptionRecalculate");
                }
            }
        }


        private DateTime dateBuy = DateTime.Now;
        public DateTime DateBuy
        {
            get { return dateBuy; }
            set
            {
                if (dateBuy != value)
                {
                    dateBuy = value;
                    NotifyPropertyChanged("DateBuy");
                }
            }
        }


        private DateTime dateSell = DateTime.Now.AddYears(1);
        public DateTime DateSell
        {
            get { return dateSell; }
            set
            {
                if (dateSell != value)
                {
                    dateSell = value;
                    NotifyPropertyChanged("DateSell");
                }
            }
        }


        private int brokersSelected = 1;
        public int BrokersSelected
        {
            get { return brokersSelected; }
            set
            {
                brokersSelected = value;
                initStocksCombos(); //refactor
                NotifyPropertyChanged("BrokersSelected");
            }
        }


        private List<string> brokersList =  new List<string>();
        public List<string> BrokersList
        {
            get { return brokersList; }
            set
            {
                brokersList = value;
                NotifyPropertyChanged("BrokersList");
            }
        }


        private int stockSelected = 0;
        public int StockSelected
        {
            get { return stockSelected; }
            set
            {
                stockSelected = value;
                NotifyPropertyChanged("StockSelected");
            }
        }


        private List<string> stocksList = new List<string>();
        public List<string> StocksList
        {
            get { return stocksList; }
            set
            {
                stocksList = value;
                NotifyPropertyChanged("StocksList");
            }
        }


        private int finProdSelected = 0;
        public int FinProdSelected
        {
            get { return finProdSelected; }
            set
            {
                finProdSelected = value;
                NotifyPropertyChanged("FinProdSelected");
            }
        }


        private List<string> finProdList = new List<string>();
        public List<string> FinProdList
        {
            get { return finProdList; }
            set
            {
                finProdList = value;
                NotifyPropertyChanged("FinProdList");
            }
        }


        #region Exchange kurzy a s tim souvisejici veci
        
        private int visibleExchange = 100;
        public int VisibleExchange
        {
            get { return visibleExchange; }
            set
            {
                if (visibleExchange != value)
                {
                    visibleExchange = value;
                    NotifyPropertyChanged("VisibleExchange");
                }
            }
        }


        private decimal excBuyEU = 27M;
        public decimal ExcBuyEU
        {
            get { return excBuyEU; }
            set
            {
                excBuyEU = value;
                NotifyPropertyChanged("ExcBuyEU");
            }
        }


        private decimal excSellEU = 27M;
        public decimal ExcSellEU
        {
            get { return excSellEU; }
            set
            {
                excSellEU = value;
                NotifyPropertyChanged("ExcSellEU");
            }
        }


        private decimal excBuyDolar = 24M;
        public decimal ExcBuyDolar
        {
            get { return excBuyDolar; }
            set
            {
                excBuyDolar = value;
                NotifyPropertyChanged("ExcBuyDolar");
            }
        }


        private decimal excSellDolar = 24M;
        public decimal ExcSellDolar
        {
            get { return excSellDolar; }
            set
            {
                excSellDolar = value;
                NotifyPropertyChanged("ExcSellDolar");
            }
        }

        #endregion

        #endregion


        #region Methods

        private int notifyCountHelp = 0;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                workingLogik();
                recalculateWorker();
                notifyCountHelp++;
                Debug.WriteLine("Zmena propertny notification " + notifyCountHelp);
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }


        private void workingLogik()
        {
          //  var nameSelectStock = StocksList[StockSelected].ToString();

            VisibleExchange = StockSelected != 0 ? 100 : 0;
            UseExchange = StockSelected != 0;
        }


        private void recalculateWorker()
        {
            Debug.WriteLine("worker");
            Task task = new Task(recalculate);
            task.Start();     
        }


        private int countRecalculate = 0;
        private void recalculate()
        {
            countRecalculate++;
            Debug.WriteLine("recalculate HLAVNI " + countRecalculate);
            ICalculate calculate = new Calculate();
            DataCalculate calcData = new DataCalculate();
            calcData.CountShares = CountShares;
            calcData.PriceShareBuy = BuyPrice;
            calcData.PriceShareSell = SellPrice;
            calcData.ProfitPercent = ProfitPercent;
            calcData.TypeResult = TypeOfResolve? DataTypeEnums.TypeOfResult.Profit : DataTypeEnums.TypeOfResult.Percent;
            calcData.MustPayTax = MustPayTax;
            calcData.DateBuy = DateBuy;
            calcData.DateSell = DateSell;
            calcData.UseExchange = UseExchange;
            calcData.Broker = BrokersSelected == 1 ? DataTypeEnums.Broker.Degiro : calcData.Broker;
            calcData.Broker = BrokersSelected == 0 ? DataTypeEnums.Broker.Fio : calcData.Broker;

            calcData.StockExchange = DataTypeEnums.StockExchange.Praha;
            calcData.StockExchange = StockSelected == 0 ? DataTypeEnums.StockExchange.Praha : calcData.StockExchange;
            calcData.StockExchange = StockSelected == 1 ? DataTypeEnums.StockExchange.USA : calcData.StockExchange;
            calcData.StockExchange = StockSelected == 2 ? DataTypeEnums.StockExchange.GB : calcData.StockExchange;

            calcData.DateExchangePrice = new DataExchange();
            calcData.DateExchangePrice.ExcBuyEU = ExcBuyEU;
            calcData.DateExchangePrice.ExcSellEU = ExcSellEU;
            calcData.DateExchangePrice.ExcBuyDolar = ExcBuyDolar;
            calcData.DateExchangePrice.ExcSellDolar = ExcSellDolar;
            
            //vysledky
            DataResult resultData = calculate.Calculates(calcData);
            DescriptionRecalculate = resultData.Description;
            Tax = resultData.Tax;
            Fee = resultData.Fee;
            ExchangeProfit = resultData.ExchangeCost;
            AmountProfit = resultData.AmountProfit;

            if (resultData.TypeResult == DataTypeEnums.TypeOfResult.Profit)
            {
                ProfitPercent = resultData.PercentProfit;
            }
            else
            {
                SellPrice = resultData.PriceShareSell;
            }

            ProfitPercentYear = resultData.PercentProfitYearly;
        }


        private void initBrokerCombos()
        {
            List<string> brokersListIs = new List<string>();
            brokersListIs.Add("Fio");
            brokersListIs.Add("Degiro");
           // brokersListIs.Add("CS");
            FinProdList.Add("Akcie");
            brokersList = brokersListIs;

            initStocksCombos();

        }

        private void initStocksCombos()
        {
            stocksList.Clear();

            stocksList.Add("BCPP");

            if (BrokersSelected == 1)
            {
                stocksList.Add("USA");
                stocksList.Add("GB");
            }
           

            NotifyPropertyChanged("StocksList");
            StockSelected = 0;
           // NotifyPropertyChanged("StockSelected");
        }


        #endregion


        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
