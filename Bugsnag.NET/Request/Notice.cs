using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bugsnag.NET.Request
{
    class Notice : INotice
    {
        public string ApiKey
        {
            get { throw new NotImplementedException(); }
        }

        public INotifier Notifier
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IEvent> Events
        {
            get { throw new NotImplementedException(); }
        }
    }
}
