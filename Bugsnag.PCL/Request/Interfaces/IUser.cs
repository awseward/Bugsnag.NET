using Newtonsoft.Json;

namespace Bugsnag.PCL.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    public interface IUser
    {
        [JsonProperty("id")]
        object Id { get; }

        [JsonProperty("name")]
        string Name { get; }

        [JsonProperty("email")]
        string Email { get; }
    }
}
