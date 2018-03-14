using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Bugsnag.Common;
using Bugsnag.NET.Request;

namespace Bugsnag.NET
{
    static class Extensions
    {
        public static IEvent CreateEvent(
            this IBugsnagger snagger,
            Severity severity,
            Exception ex,
            IUser user,
            object metadata)
        {
            var @event = new Event(ex, snagger.FinalizeStacktraceLine)
            {
                App = snagger.App,
                Device = snagger.Device,
                User = user,
                Severity = severity.ToString(),
                MetaData = metadata,
            };

            return snagger.FinalizeEvent(@event, ex);
        }

        public static IEvent CreateEvent(
            this IBugsnagger snagger,
            Severity severity,
            IEnumerable<Exception> unwrapped,
            IUser user,
            object metadata)
        {
            var @event = new Event(unwrapped, snagger.FinalizeStacktraceLine)
            {
                App = snagger.App,
                Device = snagger.Device,
                User = user,
                Severity = severity.ToString(),
                MetaData = metadata,
            };

            return snagger.FinalizeEvent(
                @event,
                unwrapped.FirstOrDefault() // TODO: Think about this
            );
        }

    }
}
