using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bugsnag.PCL.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    public interface IError
    {
        [JsonProperty("errorClass")]
        string ErrorClass { get; }

        [JsonProperty("message")]
        string Message { get; }

        [JsonProperty("stacktrace")]
        IEnumerable<IStackTraceLine> Stacktrace { get; }
    }
}
