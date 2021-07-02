# HappyTravel.Telemetry
OpenTelemetry meta-package. Provides a unified way for setup telemetry

## Options
- ServiceName - set service name. If not set default name will be used (**optional**)
- JaegerHost - set jaeger host. If not set jaeger exporter not be added (**optional**) 
- JaegerPort - set jaeger port. If not set jaeger exporter not be added (**optional**)
- RedisEndpoint - set redis endpoint. If not set data collection for redis not be added (**optional**)
- IsEnabled - if false completely disable telemetry (**optional**)

## Usage

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        ...
        
        services.AddTracing(options => {
            ServiceName = "service-name",
            JaegerHost = "localhost",
            JaegerPort = 6831,
            RedisEndpoint = "localhost:6379"
            IsEnabled = true
        });
        
        ...
    }
}
```