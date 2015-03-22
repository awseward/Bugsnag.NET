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
    public class Bugsnag
    {
        private Bugsnag(Severity severity)
        {
            _severity = severity;
        }

        static readonly Bugsnag _error = new Bugsnag(Severity.Error);
        static readonly Bugsnag _warning = new Bugsnag(Severity.Warning);
        static readonly Bugsnag _info = new Bugsnag(Severity.Info);

        public static Bugsnag Error { get { return _error; } }
        public static Bugsnag Warning { get { return _warning; } }
        public static Bugsnag Info { get { return _info; } }

        public static string ApiKey { get; set; }

        static INotifier _notifier = new Notifier();

        public static INotifier Notifier
        {
            get { return _notifier; }
            set { _notifier = value; }
        }

        static IApp _app = new App();

        public static IApp App
        {
            get { return _app; }
            set { _app = value; }
        }

        static IDevice _device = new Device();

        public static IDevice Device
        {
            get { return _device; }
            set { _device = value; }
        }

        readonly Severity _severity;

        public void Notify(Exception ex, IUser user, object metaData)
        {
            var evt = _BuildEvent(ex, user, metaData);
            var notice = new Notice(ApiKey, Notifier, new IEvent[] { evt });

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(notice, settings);

            var url = "https://notify.bugsnag.com";

            new WebClient().UploadString(url, json);
        }

        IEvent _BuildEvent(Exception ex, IUser user, object metaData)
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
    }
}
