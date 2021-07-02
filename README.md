# HappyTravel.Telemetry
OpenTelemetry meta-package. Provides a unified way for setup telemetry

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