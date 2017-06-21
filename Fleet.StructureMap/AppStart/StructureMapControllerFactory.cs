using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Fleet.StructureMap
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        
        private readonly string _controllerNamespace;
        public StructureMapControllerFactory(string controllerNamespace)
        {
            _controllerNamespace = controllerNamespace;
        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            try
            {
                
                 Type controllerType = Type.GetType(string.Concat(_controllerNamespace, ".", controllerName, "Controller"));
                return (IController)ObjectFactory.Container.GetInstance(controllerType);
                // return base.CreateController(requestContext, controllerName);   
            }
            catch (StructureMapException)
            {
                System.Diagnostics.Debug.WriteLine(ObjectFactory.Container.WhatDoIHave());
                throw new Exception(ObjectFactory.Container.WhatDoIHave());
            }
        }
        
    }
}
