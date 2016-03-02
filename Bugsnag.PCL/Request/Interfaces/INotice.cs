using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bugsnag.PCL.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    interface INotice
    {
        [JsonProperty("apiKey")]
        string ApiKey { get; }

        [JsonProperty("notifier")]
        INotifier Notifier { get; }

        [JsonProperty("events")]
        IEnumerable<IEvent> Events { get; }
    }
}
