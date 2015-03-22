using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bugsnag.NET.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    interface IError
    {
        [JsonProperty("errorClass")]
        string ErrorClass { get; }

        [JsonProperty("message")]
        string Message { get; }

        [JsonProperty("stacktrace")]
        IEnumerable<IStackTraceLine> Stacktrace { get; }
    }
}
