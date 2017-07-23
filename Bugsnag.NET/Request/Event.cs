using Bugsnag.Common.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bugsnag.Common;

namespace Bugsnag.NET.Request
{
    public class Event : IEvent
    {
        public Event(Exception ex) : this(ex, x => x) { }
        public Event(Exception ex, Func<IMutableStackTraceLine, IStackTraceLine> transformStacktraceLine)
        {
            Errors = _GetErrors(ex, transformStacktraceLine);
            GroupingHash = _GetGroupingHash(ex);
            Context = _GetContext(ex);
        }

        public Event(IEnumerable<Exception> unwrapped) : this(unwrapped, x => x) { }
        public Event(IEnumerable<Exception> unwrapped, Func<IMutableStackTraceLine, IStackTraceLine> transformStacktraceLine)
        {
            Errors = _GetErrors(unwrapped, transformStacktraceLine);
            GroupingHash = _GetGroupingHash(unwrapped);
            Context = _GetContext(unwrapped);
        }

        public string PayloadVersion { get; set; } = "2";

        IEnumerable<IError> _errors;
        public IEnumerable<IError> Errors
        {
            get { return _errors ?? Enumerable.Empty<IError>(); }
            set { _errors = value; }
        }

        IEnumerable<IThread> _threads;
        public IEnumerable<IThread> Threads
        {
            get { return _threads ?? Enumerable.Empty<IThread>(); }
            set { _threads = value; }
        }

        public string Context { get; set; }

        public string GroupingHash { get; set; }

        public string Severity { get; set; }

        public IUser User { get; set; }

        public IApp App { get; set; }

        public IDevice Device { get; set; }

        public object MetaData { get; set; }

        static IEnumerable<IError> _GetErrors(Exception ex, Func<IMutableStackTraceLine, IStackTraceLine> transformStacktraceLine)
        {
            if (ex == null) { return Enumerable.Empty<IError>(); }

            return _GetErrors(ex.Unwrap(), transformStacktraceLine);
        }

        static IEnumerable<IError> _GetErrors(IEnumerable<Exception> unwrapped, Func<IMutableStackTraceLine, IStackTraceLine> transformStacktraceLine)
        {
            return unwrapped.Select(ex => new Error(ex, transformStacktraceLine));
        }

        static string _GetGroupingHash(Exception ex)
        {
            if (ex == null) { return string.Empty; }

            return string.Format(
                "{0}@{1}",
                _GetTypeName(ex),
                _GetContext(ex)
            );
        }

        static string _GetMessage(Exception ex)
        {
            return ex.Message;
        }

        static string _GetTypeName(Exception ex)
        {
            return ex.GetType().Name;
        }

        static string _GetGroupingHash(IEnumerable<Exception> unwrapped)
        {
            if (!unwrapped.Any()) { return string.Empty; }

            return _GetGroupingHash(unwrapped.First());
        }

        static IExceptionInspector _exceptionInspector =
            new ExceptionInspector(ex => ex?.TargetSite);

        static string _GetContext(Exception ex) => _exceptionInspector.GetContext(ex);

        static string _GetContext(IEnumerable<Exception> unwrapped) =>
            _exceptionInspector.GetContext(unwrapped.FirstOrDefault());

        [Obsolete]
        public void AddContext(string memberName, string sourceFilePath, int sourceLineNumber)
        {
            Context = memberName;

            foreach (var error in Errors.Where(error => !error.Stacktrace.Any()))
            {
                error.Stacktrace = StackTraceLine.Build(memberName, sourceFilePath, sourceLineNumber);
            }
        }
    }
}
