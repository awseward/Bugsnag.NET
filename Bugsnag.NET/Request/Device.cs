using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bugsnag.Common;

namespace Bugsnag.NET.Request
{
    public class Device : IDevice
    {
        public string OsVersion { get; set; } = Environment.OSVersion.VersionString;

        public string Hostname { get; set; } = Environment.MachineName;
    }
}
