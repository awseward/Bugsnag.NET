using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Bugsnag.Common;
using Bugsnag.NET.Request;
using Newtonsoft.Json;

namespace Bugsnag.NET
{
    static class BugsnagSender
    {
        public static IClient Client { get; } = new BugsnagClient();

        [Obsolete("All this does is delegate to an IClient instance. Prefer going straight to that.")]
        public static void Send(INotice notice) => Client.Send(notice);

        [Obsolete("All this does is delegate to an IClient instance. Prefer going straight to that.")]
        public static void Send(INotice notice, bool useSSL) => Client.Send(notice, useSSL);
    }
}
