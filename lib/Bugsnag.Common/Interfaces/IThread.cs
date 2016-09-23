using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bugsnag.Common
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
