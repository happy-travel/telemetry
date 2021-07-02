using System;
using HappyTravel.Telemetry.Options;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using StackExchange.Redis;

namespace HappyTravel.Telemetry.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTracing(this IServiceCollection services, Action<TelemetryOptions> action)
        {
            var options = new TelemetryOptions();
            action.Invoke(options);

            if (!options.IsEnabled)
                return services;

            services.AddOpenTelemetryTracing(builder =>
            {
                var resourceBuilder = ResourceBuilder.CreateDefault();
                if (!string.IsNullOrEmpty(options.ServiceName))
                    resourceBuilder.AddService(options.ServiceName);

                builder.SetResourceBuilder(resourceBuilder)
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .SetSampler(new AlwaysOnSampler());

                if (!string.IsNullOrEmpty(options.RedisEndpoint))
                {
                    var connection = ConnectionMultiplexer.Connect(options.RedisEndpoint);
                    builder.AddRedisInstrumentation(connection);
                }

                if (!string.IsNullOrEmpty(options.JaegerHost) && options.JaegerPort is not null)
                {
                    builder.AddJaegerExporter(o =>
                    {
                        o.AgentHost = options.JaegerHost;
                        o.AgentPort = options.JaegerPort.Value;
                    });
                }
            });

            return services;
        }
    }
}