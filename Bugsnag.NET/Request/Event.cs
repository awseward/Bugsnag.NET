using Bugsnag.NET.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bugsnag.NET.Request
{
    public class Event : IEvent
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

        string _payloadVersion = "2";

        public string PayloadVersion
        {
            get { return _payloadVersion; }
            set { _payloadVersion = value; }
        }

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
                "{0} @ {1} : {2}",
                _GetTypeName(ex),
                _GetContext(ex),
                _GetMessage(ex));
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

        static string _GetContext(Exception ex)
        {
            if (ex == null) { return string.Empty; }
            var targetSite = ex.TargetSite;

            return targetSite == null ? "null::null" :
                string.Format("{0}::{1}",
                targetSite.DeclaringType,
                targetSite.Name);
        }

        static string _GetContext(IEnumerable<Exception> unwrapped)
        {
            if (!unwrapped.Any()) { return string.Empty; }

            return _GetContext(unwrapped.First());
        }
    }
}
