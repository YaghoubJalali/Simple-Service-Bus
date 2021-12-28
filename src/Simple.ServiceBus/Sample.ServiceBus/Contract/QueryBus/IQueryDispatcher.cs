using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ServiceBus.Contract.QueryBus
{
    public interface IQueryDispatcher
    {
        Task<TResult> DispatchAsync<TQueryParameter, TResult>(TQueryParameter query) 
            where TQueryParameter : IQuery
            where TResult : IResult;
    }
}
