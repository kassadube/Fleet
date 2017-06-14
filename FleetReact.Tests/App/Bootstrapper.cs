using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;

namespace Fleet.Tests
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            new TestStructureRegestry();
            /*
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<TestStructureRegestry>();
                //x.PullConfigurationFromAppConfig = true;
            });
            */
        }
    }
}