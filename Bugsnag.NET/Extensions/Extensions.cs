using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Bugsnag.Common;
using Bugsnag.Common.Extensions;
using Bugsnag.NET.Request;
using System.Dynamic;

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
            return new Event(ex)
            {
                App = snagger.App,
                Device = snagger.Device,
                User = user,
                Severity = severity.ToString(),
                MetaData = new Dictionary<string, object>
                {
                    { "metadata", metadata },
                    { $"0 {ex.GetType().Name}", ex.ReadData() },
                }
                // MetaData = new
                // {
                //     metadata,
                //     NestNestNest = new
                //     {
                //         metadata,
                //         exceptionData = ex.ReadData(),
                //         Device = new
                //         {
                //             _____________hello_________ = "you can totally sneak things in here even though it's not a \"custom\" tab",
                //         },
                //         App = "Wat",
                //     }
                // },
            };
        }

        public static IEvent CreateEvent(
            this IBugsnagger snagger,
            Severity severity,
            IEnumerable<Exception> unwrapped,
            IUser user,
            object metadata)
        {
            return new Event(unwrapped)
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
