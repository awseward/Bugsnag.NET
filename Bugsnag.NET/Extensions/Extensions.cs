using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Bugsnag.NET.Request;

namespace Bugsnag.NET.Extensions
{
    static class Extensions
    {
        public static IEvent CreateEvent(
            this IBugsnagger snagger,
            Severity severity,
            Exception ex,
            IUser user,
            object metadata)
        {
            return new Event(ex)
            {
                App = snagger.App,
                Device = snagger.Device,
                User = user,
                Severity = severity.ToString(),
                MetaData = metadata,
            };
        }

        public static IEvent CreateEvent(
            this IBugsnagger snagger,
            Severity severity,
            IEnumerable<Exception> unwrapped,
            IUser user,
            object metadata)
        {
            return new Event(unwrapped)
            {
                App = snagger.App,
                Device = snagger.Device,
                User = user,
                Severity = severity.ToString(),
                MetaData = metadata,
            };
        }

        /// <remarks>Not sure I'm really thrilled with this...</remarks>
        public static IEnumerable<Exception> Unwrap(this Exception ex)
        {
            if (ex == null)
            {
                return Enumerable.Empty<Exception>();
            }
            else if (ex.InnerException == null)
            {
                return new Exception[] { ex };
            }

            return new Exception[] { ex }.Concat(ex.InnerException.Unwrap());
        }

        public static IEnumerable<string> ToLines(this Exception ex)
        {
            if (ex == null || ex.StackTrace == null)
            {
                return new string[] { String.Empty };
            }

            return ex.StackTrace.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries);
        }

        public static string ParseFile(this string line)
        {
            var match = Regex.Match(line, "in (.+):line");
            if (match.Groups.Count < 2) { return "[file]"; }

            return match.Groups[1].Value;
        }

        public static string ParseMethodName(this string line)
        {
            // to extract the full method name (with namespace)
            var match = Regex.Match(line, "at ([^)]+[)])");
            if (match.Groups.Count < 2) { return "[method]"; }

            return match.Groups[1].Value;
        }

        public static int ParseLineNumber(this string line)
        {
            var match = Regex.Match(line, ":line ([0-9]+)");
            if (match.Groups.Count < 2) { return -1; }

            return Convert.ToInt32(match.Groups[1].Value);
        }
    }
}
