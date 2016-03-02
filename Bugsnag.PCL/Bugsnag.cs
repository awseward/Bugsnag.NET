using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Bugsnag.PCL.Request;
using System.Linq;

namespace Bugsnag.PCL
{
    public class Bugsnag
    {
        private Bugsnag(Severity severity)
        {
            _severity = severity;
        }

        public static Bugsnag Error { get; } = new Bugsnag(Severity.Error);
        public static Bugsnag Warning { get; } = new Bugsnag(Severity.Warning);
        public static Bugsnag Info { get; } = new Bugsnag(Severity.Info);

        public static string ApiKey { get; set; }

        public static INotifier Notifier { get; set; } = new Notifier();

        public static IApp App { get; set; } = new App();

        readonly Severity _severity;

        public async Task<HttpResponseMessage> NotifyAsync(IEvent evt)
        {
            return await NotifyAsync(evt, true);
        }

        public async Task<HttpResponseMessage> NotifyAsync(IEvent evt, bool useSSL)
        {
            return await NotifyAsync(new IEvent[] { evt }, useSSL);
        }

        public async Task<HttpResponseMessage> NotifyAsync(IEnumerable<IEvent> events)
        {
            return await NotifyAsync(events, true);
        }

        public async Task<HttpResponseMessage> NotifyAsync(IEnumerable<IEvent> events, bool useSSL)
        {
            var notice = new Notice(ApiKey, Notifier, events);

            return await BugsnagSender.SendAsync(notice, useSSL);
        }

        public IEvent GetEvent(Exception ex, IUser user, object metaData)
        {
            return GetEvent(ex, user, null, metaData);
        }

        public IEvent GetEvent(Exception ex, IUser user, IDevice device, object metaData)
        {
            return new Event(ex)
            {
                App = App,
                Device = device,
                User = user,
                Severity = _severity.ToString(),
                MetaData = metaData,
            };
        }

        public IEvent GetEvent(IEnumerable<Exception> unwrapped, IUser user, object metaData)
        {
            return GetEvent(unwrapped, user, null, metaData);
        }

        public IEvent GetEvent(IEnumerable<Exception> unwrapped, IUser user, IDevice device, object metaData)
        {
            return new Event(unwrapped)
            {
                App = App,
                Device = device,
                User = user,
                Severity = _severity.ToString(),
                MetaData = metaData,
            };
        }

        public IEnumerable<IEvent> GetEvents(IEnumerable<Exception> exs, IUser user, object metaData)
        {
            return exs
                .Select(ex => GetEvent(exs, user, metaData))
                .ToArray();
        }

        public IEnumerable<IEvent> GetEvents(IEnumerable<Exception> exs, IUser user, IDevice device, object metaData)
        {
            return exs
                .Select(ex => GetEvent(ex, user, device, metaData))
                .ToArray();
        }
    }
}
