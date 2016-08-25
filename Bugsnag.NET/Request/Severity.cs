using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bugsnag.NET.Request
{
    public abstract class Severity
    {
        private Severity() { }

        abstract protected string Value { get; }
        public override string ToString() { return Value; }

        static readonly ErrorSeverity _error = new ErrorSeverity();
        static readonly WarningSeverity _warning = new WarningSeverity();
        static readonly InfoSeverity _info = new InfoSeverity();

        public static Severity Error { get { return _error; } }
        public static Severity Warning { get { return _warning; } }
        public static Severity Info { get { return _info; } }

        private class ErrorSeverity : Severity
        {
            protected override string Value { get { return "error"; } }
        }

        private class WarningSeverity : Severity
        {
            protected override string Value { get { return "warning"; } }
        }

        private class InfoSeverity : Severity
        {
            protected override string Value { get { return "info"; } }
        }
    }
}
