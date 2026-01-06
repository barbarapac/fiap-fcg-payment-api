using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Fiap.FCG.Payment.Application
{
    public static class Module
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Module)));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Fiap.FCG.Payment.Application.Observability.LoggingBehavior<,>));
        }
    }
}
