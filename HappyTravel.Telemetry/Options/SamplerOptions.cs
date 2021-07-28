using System.Collections.Generic;

namespace HappyTravel.Telemetry.Options
{
    internal class SamplerOptions
    {
        public bool IsEnabled { get; set; }
        public Dictionary<string, object> IgnoredTags { get; set; } = new();
    }
}