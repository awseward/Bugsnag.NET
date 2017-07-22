using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Bugsnag.Common;
using Bugsnag.NET.Request;

namespace Bugsnag.NET
{
    public partial class Bugsnag
    {
        public static Bugsnag Error => new Bugsnag(Severity.Error);
        public static Bugsnag Warning => new Bugsnag(Severity.Warning);
        public static Bugsnag Info => new Bugsnag(Severity.Info);

        public static string ApiKey { get; set; }
        public static INotifier Notifier { get; set; } = new Notifier();
        public static IApp App { get; set; } = new App();
        public static IDevice Device { get; set; } = new Device();
    }

    [Obsolete("Prefer Bugsnagger.Default")]
    public partial class Bugsnag
    {
        private Bugsnag(Severity severity)
        {
            _severity = severity;
            Snagger = new Bugsnagger
            {
                ApiKey = ApiKey,
                Notifier = Notifier,
                App = App,
                Device = Device,
            };
        }

        readonly Severity _severity;

        public IBugsnagger Snagger { get; }

        public void Notify(IEvent evt) => Snagger.Notify(evt);
        public void Notify(IEvent evt, bool useSSL) => Snagger.Notify(evt, useSSL);
        public void Notify(IEnumerable<IEvent> events) => Snagger.Notify(events);
        public void Notify(IEnumerable<IEvent> events, bool useSSL) => Snagger.Notify(events, useSSL);

        public Task<HttpResponseMessage> NotifyAsync(IEvent evt) => Snagger.NotifyAsync(evt);
        public Task<HttpResponseMessage> NotifyAsync(IEvent evt, bool useSSL) => Snagger.NotifyAsync(evt, useSSL);
        public Task<HttpResponseMessage> NotifyAsync(IEnumerable<IEvent> events) => Snagger.NotifyAsync(events);
        public Task<HttpResponseMessage> NotifyAsync(IEnumerable<IEvent> events, bool useSSL) => Snagger.NotifyAsync(events, useSSL);

        public IEvent GetEvent(Exception ex, IUser user, object metaData)
        {
            return new Event(ex, Snagger.TransformStacktraceLine)
            {
                App = App,
                Device = Device,
                User = user,
                Severity = _severity.ToString(),
                MetaData = metaData,
            };
        }

        public IEvent GetEvent(IEnumerable<Exception> unwrapped, IUser user, object metaData)
        {
            return new Event(unwrapped, Snagger.TransformStacktraceLine)
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
