using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bugsnag.Common;
using Bugsnag.PCL.Request;

namespace Bugsnag.PCL
{
    public class Bugsnagger : IBugsnagger
    {
        public static IBugsnagger Default { get; } = new Bugsnagger();
        static IClient _Client => BugsnagSender.Client;

        public string ApiKey { get; set; }
        public IApp App { get; set; } = new App();
        public IDevice Device { get; set; } = new Device();
        public INotifier Notifier { get; set; } = new Notifier();
        public Func<IMutableStackTraceLine, IStackTraceLine> FinalizeStacktraceLine { get; set; } = x => x;
        public Func<IMutableEvent, Exception, IEvent> FinalizeEvent { get; set; } = (@event, ex) => @event;

        public void Error(Exception ex, IUser user, object metadata)
        {
            var @event = this.CreateEvent(Severity.Error, ex, user, metadata);

            Notify(@event);
        }

        public void Warning(Exception ex, IUser user, object metadata)
        {
            var @event = this.CreateEvent(Severity.Warning, ex, user, metadata);

            Notify(@event);
        }

        public void Info(Exception ex, IUser user, object metadata)
        {
            var @event = this.CreateEvent(Severity.Info, ex, user, metadata);

            Notify(@event);
        }

        public void Notify(IEvent @event) => Notify(@event, true);

        public void Notify(IEvent @event, bool useHttps) => Notify(new[] { @event }, useHttps);

        public void Notify(IEnumerable<IEvent> events) => Notify(events, true);

        public void Notify(IEnumerable<IEvent> events, bool useSSL)
        {
            var notice = new Notice(ApiKey, Notifier, events);

            _Client.Send(notice, useSSL);
        }
    }
}
