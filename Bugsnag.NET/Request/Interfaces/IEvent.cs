using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bugsnag.NET.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    interface IEvent
    {
        [JsonProperty("payloadVersion")]
        string PayloadVersion { get; }

        [JsonProperty("exceptions")]
        IEnumerable<IError> Errors { get; }

        [JsonProperty("threads")]
        IEnumerable<IThread> Threads { get; }

        [JsonProperty("context")]
        string Context { get; }

        [JsonProperty("groupingHash")]
        string GroupingHash { get; }

        [JsonProperty("severity")]
        string Severity { get; }

        [JsonProperty("user")]
        IUser User { get;}

        [JsonProperty("app")]
        IApp App { get; }

        [JsonProperty("device")]
        IDevice Device { get; }

        [JsonProperty("metaData")]
        object MetaData { get; }
    }
}
