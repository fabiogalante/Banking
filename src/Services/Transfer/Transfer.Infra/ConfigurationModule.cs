using Microsoft.Extensions.DependencyInjection;
using Processing.Domain.Repositories.CreateTransfer;
using Processing.Infra.Repository.CreateTransfer;

namespace Processing.Infra
{
    public static class ConfigurationModule
    {
        public static void RegisterRepository(this IServiceCollection services)
        {
            services.AddScoped<IProcessingRepository, ProcessingRepository>();
        }
    }
}
