
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fleet.SiteMVC
{

    public class StructureRegestry : Registry
    {
        public StructureRegestry()
        {
           
        }

    }

    public static class ObjectFactory
    {
        private static readonly Lazy<Container> _containerBuilder =
                new Lazy<Container>(defaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container
        {
            get { return _containerBuilder.Value; }
        }

        private static Container defaultContainer()
        {
            return new Container(x =>
            {
                x.AddRegistry(new StructureRegestry());

                //For<IAccountRepository>().Use<AccountRepository>();
                // default config
            });
        }
    }
}
