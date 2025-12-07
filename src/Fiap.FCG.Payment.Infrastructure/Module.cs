using Fiap.FCG.Payment.Domain.Pagamentos;
using Fiap.FCG.Payment.Infrastructure._Shared;
using Fiap.FCG.Payment.Infrastructure.Pagamentos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Fiap.FCG.Payment.Infrastructure
{
    public static class Module
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddRepositories(services);
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var fromEnv = Environment.GetEnvironmentVariable("PAYMENT_CONNECTION_STRING");
            var fromConfig = configuration["PAYMENT_CONNECTION_STRING"];

            var connectionString = !string.IsNullOrWhiteSpace(fromEnv) ? fromEnv : fromConfig;

            services.AddDbContext<PaymentDbContext>(options => options.UseNpgsql(connectionString));
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
        }
    }
}
