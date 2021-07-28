using System.Collections.Generic;
using System.Linq;
using HappyTravel.Telemetry.Options;
using Microsoft.Extensions.Options;
using OpenTelemetry.Trace;

namespace HappyTravel.Telemetry
{
    internal class Sampler : OpenTelemetry.Trace.Sampler
    {
        public Sampler(IOptionsMonitor<SamplerEnableOption> enabledOption, IOptions<SamplerIgnoredTagsOption> ignoredTagsOption)
        {
            _enabledOption = enabledOption;
            _ignoredTagsOption = ignoredTagsOption;
        }


        public override SamplingResult ShouldSample(in SamplingParameters samplingParameters) 
            => _enabledOption.CurrentValue.IsEnabled && !HasIgnoredTags(samplingParameters)
                ? new SamplingResult(SamplingDecision.RecordAndSample)
                : new SamplingResult(SamplingDecision.Drop);


        private bool HasIgnoredTags(in SamplingParameters samplingParameters)
            => samplingParameters.Tags.Intersect(_ignoredTagsOption.Value.IgnoredTags).Any() ||
               samplingParameters.Tags.Intersect(_ignoredTags).Any();


        private readonly HashSet<KeyValuePair<string, object>> _ignoredTags = new()
        {
            new KeyValuePair<string, object>("http.path", "/health"),
            new KeyValuePair<string, object>("http.path", "/metrics")
        };
        

        private readonly IOptionsMonitor<SamplerEnableOption> _enabledOption;
        private readonly IOptions<SamplerIgnoredTagsOption> _ignoredTagsOption;
    }
}