using HappyTravel.Telemetry.Options;
using Microsoft.Extensions.Options;
using OpenTelemetry.Trace;

namespace HappyTravel.Telemetry
{
    internal class Sampler : OpenTelemetry.Trace.Sampler
    {
        public Sampler(IOptionsMonitor<SamplerOptions> options)
        {
            _options = options;
        }


        public override SamplingResult ShouldSample(in SamplingParameters samplingParameters) 
            => _options.CurrentValue.IsEnabled
                ? new SamplingResult(SamplingDecision.RecordAndSample)
                : new SamplingResult(SamplingDecision.Drop);


        private readonly IOptionsMonitor<SamplerOptions> _options;
    }
}