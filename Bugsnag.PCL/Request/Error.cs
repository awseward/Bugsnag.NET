using System;
using System.Collections.Generic;
using Bugsnag.Common;

namespace Bugsnag.PCL.Request
{
    public class Error : IError
    {
        public Error(Exception ex)
        {
            ErrorClass = ex.GetType().Name;
            Message = ex.Message;
            Stacktrace = StackTraceLine.Build(ex);
        }

        public string ErrorClass { get; private set; }

        public string Message { get; private set; }

        public IEnumerable<IStackTraceLine> Stacktrace { get; set; }
    }
}
