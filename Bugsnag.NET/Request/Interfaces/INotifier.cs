using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bugsnag.NET.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    interface INotifier
    {
        [JsonProperty("name")]
        string Name { get; }

        [JsonProperty("version")]
        string Version { get; }

        [JsonProperty("url")]
        string Url { get; }
    }
}
