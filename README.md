# HappyTravel.Telemetry
OpenTelemetry meta-package. Provides a unified way for setup telemetry

## Options
- ServiceName - set service name. If not set default name will be used (**optional**)
- JaegerHost - set jaeger host. If not set jaeger exporter not be added (**optional**) 
- JaegerPort - set jaeger port. If not set jaeger exporter not be added (**optional**)
- RedisEndpoint - set redis endpoint. If not set data collection for redis not be added (**optional**)
- Sources - set activity source names (**optional**)

## Usage
Add to appsettings.json or consul
```json
{
    "Telemetry": {
        "IsEnabled": true
    }
}
```

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        ...
        
        services.AddTracing(options => {
            options.ServiceName = "service-name";
            options.JaegerHost = "localhost";
            options.JaegerPort = 6831;
            options.RedisEndpoint = "localhost:6379";
            options.Sources = new[] {nameof(ExampleService1), nameof(ExampleService2)};
        });
        
        ...
    }
}
```