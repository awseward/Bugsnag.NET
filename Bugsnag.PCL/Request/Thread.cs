using System;
using System.Collections.Generic;
using Bugsnag.Common;

namespace Bugsnag.PCL.Request
{
    [Obsolete("Not implemented", true)]
    public class Thread : IThread
    {
        public object Id
        {
            get { throw new NotImplementedException(); }
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IStackTraceLine> Stacktrace
        {
            get { throw new NotImplementedException(); }
        }
    }
}
