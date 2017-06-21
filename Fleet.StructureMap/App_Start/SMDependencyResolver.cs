using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fleet.StructureMap
{
    public class SMDependencyResolver : IDependencyResolver
    {
        IContainer _container;
        public SMDependencyResolver(IContainer container)
        {
            _container = container;
        }
        public object GetService(Type serviceType)
        {
            return null;
        }


        public IEnumerable<object> GetServices(Type serviceType)
        {
            return null;
        }
        private void AddBindings()
        {
           

        }
    }
}
