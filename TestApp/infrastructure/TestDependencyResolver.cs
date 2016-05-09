using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace TestApp.infrastructure
{
    public class TestDependencyResolver : IDependencyResolver
    {
        private IUnityContainer _unity;

        public TestDependencyResolver(IUnityContainer unityContainer)
        {
            _unity = unityContainer;
        }
        public object GetService(Type serviceType)
        {

            try
            {
                return _unity.Resolve(serviceType);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _unity.ResolveAll(serviceType);
            }
            catch (Exception)
            {

                return new List<object>();
            }
        }
    }
}