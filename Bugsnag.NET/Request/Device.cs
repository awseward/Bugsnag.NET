using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bugsnag.NET.Request
{
    public class Device : IDevice
    {
        string _osVersion = Environment.OSVersion.VersionString;

        public string OsVersion
        {
            get { return _osVersion; }
            set { _osVersion = value; }
        }

        public string Hostname { get; set; }
    }
}
