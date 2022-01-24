using Microsoft.Extensions.DependencyInjection;
using Processing.Domain.Repositories.Context;
using Processing.Infra;
using Processing.Infra.Repository.Context;

namespace Processing.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IProcessingContext, ProcessingContext>();
            services.RegisterRepository();
            return services;
        }
    }
}
