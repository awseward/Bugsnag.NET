using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bugsnag.NET.Request
{
    class Device : IDevice
    {
        public string OsVersion
        {
            get { throw new NotImplementedException(); }
        }

        public string Hostname
        {
            get { throw new NotImplementedException(); }
        }
    }
}
