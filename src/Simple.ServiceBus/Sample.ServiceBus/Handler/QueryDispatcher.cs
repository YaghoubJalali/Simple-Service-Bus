using Sample.Framework.Common.ServiceProvider;
using Sample.ServiceBus.Contract.QueryBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ServiceBus.Handler
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServicesProvider _provier;

        public QueryDispatcher(IServicesProvider provider)
        {
            _provier = provider ?? throw new ArgumentNullException($"{nameof(IServicesProvider)} is null!");
        }

        public async Task<TResult> DispatchAsync<TQueryParameter, TResult>(TQueryParameter query)
            where TQueryParameter : IQuery
            where TResult : IResult
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var handler = _provier.GetService<IQueryHandler<TQueryParameter, TResult>>();
            if (handler == null)
                throw new ArgumentNullException(nameof(IQueryHandler<TQueryParameter, TResult>));

            return await handler.GetQueryAsync(query);
        }
    }
}
