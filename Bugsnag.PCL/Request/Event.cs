using System;
using System.Collections.Generic;
using System.Linq;
using Bugsnag.Common;
using Bugsnag.Common.Extensions;

namespace Bugsnag.PCL.Request
{
    public class Event : IMutableEvent
    {
        public Event(Exception ex)
        {
            Errors = _GetErrors(ex);
            GroupingHash = _GetGroupingHash(ex);
            Context = _GetContext(ex);
        }

        public Event(IEnumerable<Exception> unwrapped)
        {
            Errors = _GetErrors(unwrapped);
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

        static IEnumerable<IError> _GetErrors(Exception ex)
        {
            if (ex == null) { return Enumerable.Empty<IError>(); }

            return _GetErrors(ex.Unwrap());
        }

        static IEnumerable<IError> _GetErrors(IEnumerable<Exception> unwrapped)
        {
            return unwrapped.Select(ex => new Error(ex));
        }

        static string _GetGroupingHash(Exception ex)
        {
            if (ex == null) { return string.Empty; }

            return string.Format(
                "{0}@{1}",
                _GetTypeName(ex),
                _GetMessage(ex)
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

        // NOTE: Unable to grab TargetSite from Exception in Portable class library
        // TODO: Find an alternative
        static IExceptionInspector _exceptionInspector = new ExceptionInspector(ex => null);

        static string _GetContext(Exception ex) => _exceptionInspector.GetContext(ex);

        static string _GetContext(IEnumerable<Exception> unwrapped) =>
            _exceptionInspector.GetContext(unwrapped.FirstOrDefault());

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
