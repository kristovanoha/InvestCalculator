using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Business.Brokers;
using Business.DataClasses;
using Business.IocContainer;
using Business.Resolve;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Core.Resource;

namespace Present
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      //  public IocContainer container;


        public MainWindow()
        {
            // s použitím singletonu
            ContainerManager.InitContainer();
            
            //castle windsdor
            //hlavní inicializace IOC containeru
            //var container = new WindsorContainer(
            //    new XmlInterpreter(new ConfigResource("castle"))
            //);

           // var container = ContainerManager.Container;
           
         //   personRepository = container.Resolve<IPersonRepository>();
         //   var container = new WindsorContainer();

          //  container.Register(Component.For<FactoryBroker>());
         //   container.Register(Component.For<IBroker, BrokerDegiro>());
            var a = 1;

            //container.Register(Component.For<IBroker>().ImplementedBy<BrokerDegiro>().Named("BrokerDegiro"));
            //container.Register(Component.For<IBroker>().ImplementedBy<BrokerCS>().Named("BrokerCS"));
            //container.Register(Component.For<ICalculate>().ImplementedBy<Calculate>().Named("Calculate"));


            //end castle


            //container = new IocContainer();
            //container.Register<IBroker, IBroker>();
            //container.Register<IStockExchange, BrokerDegiro>();

            //var neco = container.Resolve<BrokerDegiro>();
            //neco.FeeBuy()

            InitializeComponent();
           // gbxExchange.Visibility = System.Windows.Visibility.Hidden;
        }


        private void changeEnabled(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
