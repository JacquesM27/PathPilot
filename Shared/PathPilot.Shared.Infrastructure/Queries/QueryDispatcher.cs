using Microsoft.Extensions.DependencyInjection;
using PathPilot.Shared.Abstractions.Queries;

namespace PathPilot.Shared.Infrastructure.Queries;

internal sealed class QueryDispatcher(IServiceProvider serviceProvider) : IQueryDispatcher
{
    public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
    {
        using var scope = serviceProvider.CreateScope();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);

        return await (Task<TResult>)handlerType
            .GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.HandleAsync))
            .Invoke(handler, [query]);
    }
}