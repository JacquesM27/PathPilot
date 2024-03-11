using Microsoft.Extensions.DependencyInjection;
using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Shared.Infrastructure.Exceptions;

internal sealed class ExceptionCompositionRoot(
    IServiceProvider serviceProvider
    ) : IExceptionCompositionRoot
{
    public ExceptionResponse Map(Exception exception)
    {
        using var scope = serviceProvider.CreateScope();
        var mappers = scope.ServiceProvider
            .GetServices<IExceptionToResponseMapper>().ToArray();

        var nonDefaultMappers = mappers
            .Where(x => x is not ExceptionToResponseMapper);

        var result = nonDefaultMappers
            .Select(x => x.Map(exception))
            .SingleOrDefault(x => x is not null);

        if (result is not null)
            return result;

        var defaultMapper = mappers.SingleOrDefault(x => x is ExceptionToResponseMapper);
        return defaultMapper!.Map(exception)!;
    }
}