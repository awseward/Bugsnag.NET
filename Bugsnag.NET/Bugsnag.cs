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
    public interface IBugsnagger
    {
        string ApiKey { get; set; }

        INotifier Notifier { get; set; }
        IApp App { get; set; }
        IDevice Device { get; set; }

        void Notify(IEvent @event);
        void Notify(IEvent @event, bool useHttps);
        void Notify(IEnumerable<IEvent> events);
        void Notify(IEnumerable<IEvent> events, bool useHttps);
    }

    public partial class Bugsnagger : IBugsnagger
    {
        public static IBugsnagger Default { get; } = new Bugsnagger();
    }

    public partial class Bugsnagger : IBugsnagger
    {
        public string ApiKey { get; set; }

        public IApp App { get; set; } = new App();
        public IDevice Device { get; set; } = new Device();
        public INotifier Notifier { get; set; } = new Notifier();

        public void Notify(IEvent @event)
        {
            Notify(@event, true);
        }

        public void Notify(IEvent @event, bool useHttps)
        {
            Notify(new List<IEvent> { @event }, useHttps);
        }

        public void Notify(IEnumerable<IEvent> events)
        {
            Notify(events, true);
        }

        public void Notify(IEnumerable<IEvent> events, bool useHttps)
        {
            var notice = new Notice(ApiKey, Notifier, events);

            BugsnagSender.Send(notice, useHttps);
        }
    }

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

        public IEvent GetEvent(IEnumerable<Exception> unwrapped, IUser user, object metaData)
        {
            return new Event(unwrapped)
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
