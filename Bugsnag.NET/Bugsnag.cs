using Bugsnag.NET.Request;
using Bugsnag.NET.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Bugsnag.NET
{
    public class Bugsnag
    {
        private Bugsnag(Severity severity)
        {
            _severity = severity;
        }

        static readonly Bugsnag _error = new Bugsnag(Severity.Error);
        static readonly Bugsnag _warning = new Bugsnag(Severity.Warning);
        static readonly Bugsnag _info = new Bugsnag(Severity.Info);

        public static Bugsnag Error { get { return _error; } }
        public static Bugsnag Warning { get { return _warning; } }
        public static Bugsnag Info { get { return _info; } }

        public static string ApiKey { get; set; }

        static INotifier _notifier = new Notifier();

        public static INotifier Notifier
        {
            get { return _notifier; }
            set { _notifier = value; }
        }

        static IApp _app = new App();

        public static IApp App
        {
            get { return _app; }
            set { _app = value; }
        }

        static IDevice _device = new Device();

        public static IDevice Device
        {
            get { return _device; }
            set { _device = value; }
        }

        readonly Severity _severity;

        public void Notify(IEvent evt)
        {
            Notify(evt, true);
        }

        public void Notify(IEvent evt, bool useSSL)
        {
            Notify(new IEvent[] { evt }, useSSL);
        }

        public void Notify(IEnumerable<IEvent> events)
        {
            Notify(events, true);
        }

        public void Notify(IEnumerable<IEvent> events, bool useSSL)
        {
            var notice = new Notice(ApiKey, Notifier, events);

            BugsnagSender.Send(notice, useSSL);
        }

        public IEvent GetEvent(Exception ex, IUser user, object metaData)
        {
            return new Event(ex)
            {
                App = App,
                Device = Device,
                User = user,
                Severity = _severity.ToString(),
                MetaData = metaData,
            };
        }

        public IEnumerable<IEvent> GetEvents(IEnumerable<Exception> exs, IUser user, object metaData)
        {
            foreach (var ex in exs)
            {
                yield return GetEvent(ex, user, metaData);
            }
        }
    }
}
