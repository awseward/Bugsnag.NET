using Bugsnag.NET.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Bugsnag.NET
{
    class BugsnagSender
    {
        static Uri _uri = new Uri("http://notify.bugsnag.com");
        static Uri _sslUri = new Uri("https://notify.bugsnag.com");
        static JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public static void Send(INotice notice)
        {
            Send(notice, true);
        }

        public static void Send(INotice notice, bool useSSL)
        {
            var uri = _GetUri(useSSL);
            var json = JsonConvert.SerializeObject(notice, _settings);

            new WebClient().UploadString(uri, json);
        }

        static Uri _GetUri(bool useSSL)
        {
            return (useSSL) ? _sslUri : _uri;
        }
    }
}
