namespace HappyTravel.Telemetry.Options
{
    public class TelemetryOptions
    {
        public string? ServiceName { get; set; }
        public string? JaegerHost { get; set; }
        public int? JaegerPort { get; set; }
        public string? RedisEndpoint { get; set; }
    }
}