using System.Collections.Generic;

namespace HappyTravel.Telemetry.Options
{
    internal class SamplerIgnoredTagsOption
    {
        public HashSet<KeyValuePair<string, object>> IgnoredTags { get; set; } = new();
    }
}