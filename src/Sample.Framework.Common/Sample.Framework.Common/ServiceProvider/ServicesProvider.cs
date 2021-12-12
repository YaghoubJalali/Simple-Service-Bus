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
            _provier = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public T GetService<T>()
        {
            var service =  (T)_provier.GetService(typeof(T));
            return service;
        }
    }
}
