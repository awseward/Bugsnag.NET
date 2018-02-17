using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bugsnag.Common
{
    [JsonObject(MemberSerialization.OptIn)]
    public interface IEvent
    {
        [JsonProperty("payloadVersion")]
        string PayloadVersion { get; }

        [JsonProperty("exceptions")]
        IEnumerable<IError> Errors { get; }

        [JsonProperty("threads")]
        IEnumerable<IThread> Threads { get; }

        [JsonProperty("context")]
        string Context { get; }

        [JsonProperty("groupingHash")]
        string GroupingHash { get; }

        [JsonProperty("severity")]
        string Severity { get; }

        [JsonProperty("user")]
        IUser User { get;}

        [JsonProperty("app")]
        IApp App { get; }

        [JsonProperty("device")]
        IDevice Device { get; }

        [JsonProperty("metaData")]
        object MetaData { get; }

        [Obsolete("Avoid using this if possible")]
        void AddContext(string memberName, string sourceFilePath, int sourceLineNumber);
    }

    public interface IMutableEvent : IEvent
    {
        new string PayloadVersion { get; set; }
        new IEnumerable<IError> Errors { get; set; }
        new IEnumerable<IThread> Threads { get; set; }
        new string Context { get; set; }
        new string GroupingHash { get; set; }
        new string Severity { get; set; }
        new IUser User { get; set; }
        new IApp App { get; set; }
        new IDevice Device { get; set; }
        new object MetaData { get; set; }
    }
}
