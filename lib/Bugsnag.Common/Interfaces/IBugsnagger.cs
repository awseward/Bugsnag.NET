using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bugsnag.Common
{
    public interface IBugsnagger
    {
        string ApiKey { get; set; }

        INotifier Notifier { get; set; }
        IApp App { get; set; }
        IDevice Device { get; set; }

        Func<IMutableStackTraceLine, IStackTraceLine> FinalizeStacktraceLine { get; set; }
        Func<IMutableEvent, Exception, IEvent> FinalizeEvent { get; set; }

        void Error(Exception ex, IUser user, object metadata);
        void Warning(Exception ex, IUser user, object metadata);
        void Info(Exception ex, IUser user, object metadata);

        void Notify(IEvent @event);
        void Notify(IEvent @event, bool useHttps);
        void Notify(IEnumerable<IEvent> events);
        void Notify(IEnumerable<IEvent> events, bool useHttps);
    }
}
