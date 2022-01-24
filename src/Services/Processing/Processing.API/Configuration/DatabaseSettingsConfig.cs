using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Processing.API.Settigns;
using Processing.Infra.Settings;

namespace Processing.API.Configuration
{
    public static class DatabaseSettingsConfig
    {
        public static void AddDatabaseSettingsConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ProcessingDatabaseSettings>(configuration.GetSection(nameof(ProcessingDatabaseSettings)));

            services.AddSingleton<IProcessingDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ProcessingDatabaseSettings>>().Value);
        }
    }
}
