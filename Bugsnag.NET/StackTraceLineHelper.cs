using Bugsnag.Common;
using Bugsnag.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bugsnag.NET
{
    public static class StackTraceLineHelper
    {
        static string _DefaultFileValue => CommonExtensions.FileParseFailureDefaultValue;

        public static bool HasSuccessfullyParsedFile(this IStackTraceLine line) => line?.File != _DefaultFileValue;

        public static bool IsFromNamespaces(
            this IStackTraceLine line,
            params string[] prefixes) => prefixes.Any(str => (line?.Method?.StartsWith(str)).GetValueOrDefault());

        public static string TryGetTrimmedFile(this IStackTraceLine line, Regex regex) => line.TryGetTrimmedFile(regex, str => str);
        public static string TryGetTrimmedFile(this IStackTraceLine line, Regex regex, Func<string, string> additionalTransformOnSuccess)
        {
            var fileName = line?.File ?? _DefaultFileValue; // NOTE: Not sure if a better thing here to coerce or just blow up
            var match = regex.Match(fileName);

            return match.Success
                ? additionalTransformOnSuccess(match.Value)
                : fileName;
        }
    }
}
