using Business.Brokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business.IocContainer
{

    //NOT USE, ONLY TEST
    public class IocContainer
    {

        private static IocContainer _container;

        public static IocContainer GetContainer
        {
            get
            {
                if (_container == null)
                {
                    containerInit();
                }
                return _container;
            }
        }


        private static void containerInit()
        {
            _container = new IocContainer();
         //   _container.Register<IBroker, IBroker>();
            _container.Register<IBroker, BrokerDegiro>();

            //container.Register<IBroker, IBroker>();
            //container.Register<IStockExchange, BrokerDegiro>();
        }


        //vzor
        private readonly Dictionary<Type, Type> _dependencyMap = new Dictionary<Type, Type>();

        public T Resolve<T>()
        {
            return (T) Resolve(typeof (T));
        }

        public void Register<TFrom, TTo>()
        {
             _dependencyMap.Add(typeof(TFrom), typeof(TTo));
        }

        private object Resolve(Type type)
        {
            Type resolvedType = findDependency(type);

            ConstructorInfo constructor = resolvedType.GetConstructors().First();

            ParameterInfo[] parameters = constructor.GetParameters();

            if (!parameters.Any())
            {
                return Activator.CreateInstance(resolvedType);
            }
            else
            {
                return constructor.Invoke(ResolveParameters(parameters).ToArray());
            }
        }


        private IEnumerable<object> ResolveParameters(IEnumerable<ParameterInfo> parameters)
        {
            return parameters.Select(p => Resolve(p.ParameterType)).ToList();
        }


        private Type findDependency(Type type)
        {
            return _dependencyMap[type];
        }



    }
}
