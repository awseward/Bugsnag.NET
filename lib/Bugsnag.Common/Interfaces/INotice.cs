using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bugsnag.Common
{
    [JsonObject(MemberSerialization.OptIn)]
    public interface INotice
    {
        [JsonProperty("apiKey")]
        string ApiKey { get; }

        [JsonProperty("notifier")]
        INotifier Notifier { get; }

        [JsonProperty("events")]
        IEnumerable<IEvent> Events { get; }
    }
}
