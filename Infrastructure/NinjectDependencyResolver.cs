using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Web.WebApi;

namespace WindowsService1
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IItem>().To<Item>();
        }


        public void Dispose()
        {
            IDisposable disposable = kernel as IDisposable;
            if (disposable != null)
                disposable.Dispose();

            kernel = null;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(kernel.BeginBlock());
        }
    }
}
