using System.Collections.Generic;
using System.Linq;
using Bugsnag.Common;

namespace Bugsnag.PCL.Request
{
    class Notice : INotice
    {
        public Notice(string apiKey, INotifier notifier, IEnumerable<IEvent> events)
        {
            ApiKey = apiKey;
            Notifier = notifier;
            Events = events ?? Enumerable.Empty<Event>();
        }

        public Notice(string apiKey, INotifier notifier, IEvent evt)
            : this(apiKey, notifier, new IEvent[] { evt }) { }

        public string ApiKey { get; private set; }

        public INotifier Notifier { get; private set; }

        public IEnumerable<IEvent> Events { get; } = Enumerable.Empty<Event>();
    }
}
