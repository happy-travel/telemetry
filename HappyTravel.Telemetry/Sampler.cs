using System.Collections.Generic;
using System.Linq;
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
            => _options.CurrentValue.IsEnabled && !samplingParameters.Tags.Intersect(_ignoredTags).Any()
                ? new SamplingResult(SamplingDecision.RecordAndSample)
                : new SamplingResult(SamplingDecision.Drop);


        private readonly HashSet<KeyValuePair<string, object>> _ignoredTags = new()
        {
            new KeyValuePair<string, object>("http.path", "/health"),
            new KeyValuePair<string, object>("http.path", "/metrics")
        };
        

        private readonly IOptionsMonitor<SamplerOptions> _options;
    }
}