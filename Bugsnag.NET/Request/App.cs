using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bugsnag.NET.Request
{
    public class App : IApp
    {
        public string Version { get; set; }

        public string ReleaseStage { get; set; }
    }
}
