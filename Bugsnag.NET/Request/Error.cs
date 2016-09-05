using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bugsnag.Common;

namespace Bugsnag.NET.Request
{
    public class Error : IError
    {
        public Error(Exception ex)
        {
            ErrorClass = ex.GetType().Name;
            Message = ex.Message;
            Stacktrace = StackTraceLine.Build(ex);
        }

        public string ErrorClass { get; }

        public string Message { get; }

        public IEnumerable<IStackTraceLine> Stacktrace { get; set; }
    }
}
