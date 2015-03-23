using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Bugsnag.NET.Extensions
{
    static class Extensions
    {
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

            return (new Exception[] { ex }).Concat(Unwrap(ex.InnerException));
        }

        public static IEnumerable<string> ToLines(this Exception ex)
        {
            if (ex == null) { return Enumerable.Empty<string>(); }

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
            var match = Regex.Match(line, "at .+\\.([^\\.]+\\(.*\\))");
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
