using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bugsnag.Common;

namespace Bugsnag.NET.Request
{
    public class Error : IError
    {
        public Error(Exception ex) : this(ex, x => x) { }
        public Error(Exception ex, Func<IMutableStackTraceLine, IStackTraceLine> transformStacktraceLine)
        {
            ErrorClass = ex.GetType().Name;
            Message = ex.Message;
            Stacktrace = StackTraceLine.Build(ex, transformStacktraceLine);
        }

        public string ErrorClass { get; }

        public string Message { get; }

        public IEnumerable<IStackTraceLine> Stacktrace { get; set; }
    }
}
