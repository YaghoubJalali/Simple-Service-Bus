using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Framework.Common.ServiceProvider
{
    public class ServicesProvider: IServicesProvider
    {
        private readonly IServiceProvider _provier;

        public ServicesProvider(IServiceProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            _provier = provider;
        }

        public T GetService<T>()
        {
            var service =  (T)_provier.GetService(typeof(T));
            return service;
        }
    }
}
