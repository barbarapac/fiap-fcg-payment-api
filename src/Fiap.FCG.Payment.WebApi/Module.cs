using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Text.Json.Serialization;

namespace Fiap.FCG.Payment.WebApi
{
    public static class Module
    {
        public static void AddWebApi(this IServiceCollection services)
        {
            AddControllers(services);
            AddSwagger(services);
        }

        private static void AddControllers(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
        }

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();

                c.TagActionsBy(api =>
                {
                    var groupName = api.GroupName;
                    return !string.IsNullOrEmpty(groupName)
                        ? new[] { groupName }
                        : new[] { api.ActionDescriptor.RouteValues["controller"] };
                });

                c.DocInclusionPredicate((_, _) => true);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Insira seu token JWT: Bearer {token}",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
            });
        }
    }
}
