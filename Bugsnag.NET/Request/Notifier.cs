using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bugsnag.NET.Request
{
    class Notifier : INotifier
    {
        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public string Version
        {
            get { throw new NotImplementedException(); }
        }

        public string Url
        {
            get { throw new NotImplementedException(); }
        }
    }
}
