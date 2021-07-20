using System;
using System.Linq;
using HappyTravel.Telemetry.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using StackExchange.Redis;

namespace HappyTravel.Telemetry.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTracing(this IServiceCollection services, IConfiguration configuration, Action<TelemetryOptions> action)
        {
            var options = new TelemetryOptions();
            action.Invoke(options);

            services.Configure<SamplerOptions>(configuration.GetSection("Telemetry"));
            services.AddTransient<Sampler>();

            services.AddOpenTelemetryTracing(builder =>
            {
                var resourceBuilder = ResourceBuilder.CreateDefault();
                if (!string.IsNullOrEmpty(options.ServiceName))
                    resourceBuilder.AddService(options.ServiceName);

                builder.SetResourceBuilder(resourceBuilder)
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .SetSampler<Sampler>();

                if (options.Sources is not null && options.Sources.Any())
                    builder.AddSource(options.Sources);

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