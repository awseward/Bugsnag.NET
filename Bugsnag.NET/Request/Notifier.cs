using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Bugsnag.Common;

namespace Bugsnag.NET.Request
{
    public class Notifier : INotifier
    {
        public string Name { get; } = "Bugsnag.NET";

        public string Version { get; } = new Func<string>(() =>
        {
            var assembly = typeof(Notifier).Assembly;
            var location = assembly.Location;
            return FileVersionInfo.GetVersionInfo(location).ProductVersion;
        }).Invoke();

        public string Url { get; } = "https://github.com/awseward/Bugsnag.NET";
    }
}
