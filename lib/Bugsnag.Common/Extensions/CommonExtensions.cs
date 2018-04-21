using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bugsnag.Common.Extensions
{
    public static class CommonExtensions
    {
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
                return new string[] { string.Empty };
            }

            return ex.StackTrace.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries
            );
        }

        static StacktraceLineParser _stacktraceLineParser = new StacktraceLineParser();

        public static string FileParseFailureDefaultValue = "";

        public static string ParseFile(this string line) =>
            _stacktraceLineParser.ParseFile(line);

        public static string ParseMethodName(this string line) =>
            _stacktraceLineParser.ParseMethodName(line);

        public static int? ParseLineNumber(this string line) =>
            _stacktraceLineParser.ParseLineNumber(line);
    }
}
