namespace Bugsnag.PCL.Request
{
    abstract class Severity
    {
        private Severity() { }

        abstract protected string Value { get; }
        public override string ToString() { return Value; }

        public static Severity Error { get; } = new ErrorSeverity();
        public static Severity Warning { get; } = new WarningSeverity();
        public static Severity Info { get; } = new InfoSeverity();

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
