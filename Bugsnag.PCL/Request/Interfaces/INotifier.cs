using Newtonsoft.Json;

namespace Bugsnag.PCL.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    public interface INotifier
    {
        [JsonProperty("name")]
        string Name { get; }

        [JsonProperty("version")]
        string Version { get; }

        [JsonProperty("url")]
        string Url { get; }
    }
}
