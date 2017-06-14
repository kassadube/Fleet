using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using StructureMap.Attributes;
using StructureMap.Configuration.DSL;
using Fleet.Data;
using Fleet.Model;
using System.Threading;

namespace Fleet.Tests
{
    public class TestStructureRegestry : Registry
    {
        public TestStructureRegestry()
        {
            For<IAccountRepository>().Use<AccountRepository>(); 
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
                x.AddRegistry(new TestStructureRegestry());

                //For<IAccountRepository>().Use<AccountRepository>();
                // default config
            });
        }
    }
}
