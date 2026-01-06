using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Fiap.FCG.Payment.Application;

[ExcludeFromCodeCoverage]
public static class Module
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Module)));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Fiap.FCG.Payment.Application.Observability.LoggingBehavior<,>));
    }
}