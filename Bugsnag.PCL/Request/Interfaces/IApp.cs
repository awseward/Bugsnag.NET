using Newtonsoft.Json;

namespace Bugsnag.PCL.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    public interface IApp
    {
        [JsonProperty("version")]
        string Version { get; }

        [JsonProperty("releaseStage")]
        string ReleaseStage { get; }
    }
}
