using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bugsnag.Common
{
    [JsonObject(MemberSerialization.OptIn)]
    public interface IStackTraceLine
    {
        [JsonProperty("file")]
        string File { get; }

        [JsonProperty("lineNumber")]
        int? LineNumber { get; }

        [JsonProperty("columnNumber")]
        int? ColumnNumber { get; }

        [JsonProperty("method")]
        string Method { get; }

        [JsonProperty("inProject")]
        bool InProject { get; }

        [JsonProperty("code")]
        IDictionary<int, string> Code { get; }
    }

    public interface IMutableStackTraceLine : IStackTraceLine
    {
        new string File { get; set; }
        new int? LineNumber { get; set; }
        new int? ColumnNumber { get; set; }
        new string Method { get; set; }
        new bool InProject { get; set; }
    }
}
