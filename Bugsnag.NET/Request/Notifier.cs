using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Bugsnag.NET.Request
{
    public class Notifier : INotifier
    {
        string _name = "Bugsnag.NET";

        public string Name { get { return _name; } }

        string _version = new Func<string>(() =>
        {
            var assembly = typeof(Notifier).Assembly;
            var location = assembly.Location;
            return FileVersionInfo.GetVersionInfo(location).ProductVersion;
        }).Invoke();

        public string Version { get { return _version; } }


        string _url = "https://github.com/awseward/Bugsnag.NET";

        public string Url { get { return _url; } }
    }
}
