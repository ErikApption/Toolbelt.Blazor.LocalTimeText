using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Toolbelt.Blazor.Globalization;
using Toolbelt.Blazor.Globalization.Internals;

namespace Toolbelt.Blazor.Extensions.DependencyInjection;

/// <summary>
/// Extension methods to adding Local Time Zone service for Blazor WebAssembly runtime.
/// </summary>
public static class LocalTimeZoneWebAssemblyExtensions
{
    /// <summary>
    ///  Adds an ITimeZoneIdMapper and an ILocalTimeZone service for Blazor WebAssembly runtime to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
    /// </summary>
    /// <param name="services">The Microsoft.Extensions.DependencyInjection.IServiceCollection to add the service to.</param>
    /// <param name="configure">An <see cref="Action{ILocalTimeZone}"/> to configure the provided <see cref="ILocalTimeZone"/>.</param>
    public static IServiceCollection AddLocalTimeZoneWebAssembly(this IServiceCollection services, Action<ILocalTimeZone>? configure = null)
    {
        services.TryAddSingleton<ITimeZoneIdMapper, TimeZoneIdMapper>();
        services.TryAddScoped<ILocalTimeZone>((serviceProvider) =>
        {
            var localTimeZoneService = new LocalTimeZone(serviceProvider);
            configure?.Invoke(localTimeZoneService);
            return localTimeZoneService;
        });
        return services;
    }
}
