using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bugsnag.PCL.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    public interface IThread
    {
        [JsonProperty("id")]
        object Id { get; }

        [JsonProperty("name")]
        string Name { get; }

        [JsonProperty("stacktrace")]
        IEnumerable<IStackTraceLine> Stacktrace { get; }
    }
}
