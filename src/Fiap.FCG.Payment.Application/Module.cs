using Microsoft.Extensions.DependencyInjection;

namespace Fiap.FCG.Payment.Application
{
    public static class Module
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Module)));
        }
    }
}
