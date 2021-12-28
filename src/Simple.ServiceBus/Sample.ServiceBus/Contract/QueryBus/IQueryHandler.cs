using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ServiceBus.Contract.QueryBus
{

    public interface IQueryHandler<in TQueryParameter, TResult>
                    where TResult : IResult where TQueryParameter : IQuery
    {
        Task<TResult> GetQueryAsync(TQueryParameter query);
    }
}
