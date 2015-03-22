using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bugsnag.NET.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    interface IApp
    {
        [JsonProperty("version")]
        string Version { get; }

        [JsonProperty("releaseVersion")]
        string ReleaseStage { get; }
    }
}
