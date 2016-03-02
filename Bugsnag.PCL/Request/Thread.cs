using System;
using System.Collections.Generic;

namespace Bugsnag.PCL.Request
{
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
