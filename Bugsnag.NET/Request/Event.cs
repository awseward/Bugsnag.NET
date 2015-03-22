using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bugsnag.NET.Request
{
    class Event : IEvent
    {
        public string PayloadVersion
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IError> Errors
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IThread> Threads
        {
            get { throw new NotImplementedException(); }
        }

        public string Context
        {
            get { throw new NotImplementedException(); }
        }

        public string GroupingHash
        {
            get { throw new NotImplementedException(); }
        }

        public string Severity
        {
            get { throw new NotImplementedException(); }
        }

        public IUser User
        {
            get { throw new NotImplementedException(); }
        }

        public IApp App
        {
            get { throw new NotImplementedException(); }
        }

        public IDevice Device
        {
            get { throw new NotImplementedException(); }
        }

        public object MetaData
        {
            get { throw new NotImplementedException(); }
        }
    }
}
