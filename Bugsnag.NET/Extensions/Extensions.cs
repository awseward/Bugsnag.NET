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
            return new Event(ex, snagger.FinalizeStacktraceLine)
            {
                App = snagger.App,
                Device = snagger.Device,
                User = user,
                Severity = severity.ToString(),
                MetaData = metadata,
            };
        }

        public static IEvent CreateEvent(
            this IBugsnagger snagger,
            Severity severity,
            IEnumerable<Exception> unwrapped,
            IUser user,
            object metadata)
        {
            return new Event(unwrapped, snagger.FinalizeStacktraceLine)
            {
                App = snagger.App,
                Device = snagger.Device,
                User = user,
                Severity = severity.ToString(),
                MetaData = metadata,
            };
        }

    }
}
