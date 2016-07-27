﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bugsnag.PCL.Request
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
        IUser User { get; }

        [JsonProperty("app")]
        IApp App { get; }

        [JsonProperty("metaData")]
        object MetaData { get; }

        void AddContext(string memberName, string sourceFilePath, int sourceLineNumber);
    }
}
