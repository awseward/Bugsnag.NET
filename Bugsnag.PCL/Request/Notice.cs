using System.Collections.Generic;
using System.Linq;

namespace Bugsnag.PCL.Request
{
    class Notice : INotice
    {
        public Notice(string apiKey, INotifier notifier, IEnumerable<IEvent> events)
        {
            ApiKey = apiKey;
            Notifier = notifier;
            Events = events;
        }

        public Notice(string apiKey, INotifier notifier, IEvent evt)
            : this(apiKey, notifier, new IEvent[] { evt }) { }

        public string ApiKey { get; private set; }

        public INotifier Notifier { get; private set; }

        IEnumerable<IEvent> _events;

        public IEnumerable<IEvent> Events
        {
            get { return _events ?? Enumerable.Empty<Event>(); }
            private set { _events = value; }
        }
    }
}
