using Newtonsoft.Json;

namespace Bugsnag.PCL.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    public interface IDevice
    {
        [JsonProperty("osVersion")]
        string OsVersion { get; }

        [JsonProperty("hostname")]
        string Hostname { get; }
    }
}
