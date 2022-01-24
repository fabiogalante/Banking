using System;
using System.Net.Http;
using Account.Service.Client;
using Account.Service.Settings;
using Common.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using Refit;
using Serilog;

namespace Account.Service.Extensions
{
    public static class AccoutServiceCollectionExtensions
    {
        public static void AddAccountClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<LoggingDelegatingHandler>();

            services.Configure<AccountSettings>(configuration.GetSection(nameof(AccountSettings)));

            var configs = services.BuildServiceProvider().GetRequiredService<IOptions<AccountSettings>>().Value;

            services.AddRefitClient<IAccount>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configs.Uri))
                .AddHttpMessageHandler<LoggingDelegatingHandler>()
                .AddPolicyHandler(GetRetryPolicy());
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                    retryCount: 5,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, retryCount, context) =>
                    {
                        Log.Error($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
                    });
        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 5,
                    durationOfBreak: TimeSpan.FromSeconds(30)
                );
        }
    }
}
