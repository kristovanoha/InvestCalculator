using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Business.Brokers;

namespace Business.IocContainer
{
    //container NOT USE ONLY TEST
    public class ContainerManager
    {

        private static IWindsorContainer _container;

        public static IWindsorContainer Container
        {
            get
            {
                if (_container == null)
                {
                    _container = new WindsorContainer();
                }

                return _container;
            }
        }


        public static void InitContainer()
        {
            var container = Container;
            container.Register(Component.For<IBroker, BrokerDegiro>());
        }
    }
}
