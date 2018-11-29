using Ninject;
using SeeAllClassLibrary.Abstract;
using SeeAllClassLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASUPWebApplication.Infrastructure
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
            kernel.Bind<ISeeAllRepository>().To<EFSeeAllRepository>();
            kernel.Bind<IDirectoriesRepository>().To<EFDirectoriesRepository>();
            kernel.Bind<IModelsRepository>().To<EFModelsRepository>();
            // kernel.Bind<ISettingsRepository>().To<EFSettingsRepository>();
        }
    }
}