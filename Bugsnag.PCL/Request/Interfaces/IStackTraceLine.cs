using Newtonsoft.Json;

namespace Bugsnag.PCL.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    public interface IStackTraceLine
    {
        [JsonProperty("file")]
        string File { get; }

        [JsonProperty("lineNumber")]
        int LineNumber { get; }

        [JsonProperty("columnNumber")]
        int? ColumnNumber { get; }

        [JsonProperty("method")]
        string Method { get; }

        [JsonProperty("inProject")]
        bool InProject { get; }

        // TODO: Code
    }
}
