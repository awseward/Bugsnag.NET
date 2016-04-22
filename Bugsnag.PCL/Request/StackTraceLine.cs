using System;
using System.Collections.Generic;
using Bugsnag.PCL.Extensions;

namespace Bugsnag.PCL.Request
{
    public class StackTraceLine : IStackTraceLine
    {
        public static IEnumerable<IStackTraceLine> Build(Exception ex)
        {
            foreach (var line in ex.ToLines())
            {
                yield return new StackTraceLine
                {
                    File = line.ParseFile(),
                    LineNumber = line.ParseLineNumber(),
                    Method = line.ParseMethodName()
                };
            }
        }

        internal static IEnumerable<IStackTraceLine> BuildWithContext(string memberName, string sourceFilePath, int sourceLineNumber)
        {
            yield return new StackTraceLine
            {
                File = sourceFilePath,
                LineNumber = sourceLineNumber,
                Method = memberName
            };
        }

        public string File { get; private set; }

        public int LineNumber { get; private set; }

        public int? ColumnNumber { get; private set; }

        public string Method { get; private set; }

        public bool InProject { get; private set; }
    }
}
