using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Framework.Common.ServiceProvider
{
    public interface IServicesProvider
    {
        T GetService<T>();
        //T GetRequiredService<T>();
        //object GetRequiredService(Type serviceType);
    }
}
