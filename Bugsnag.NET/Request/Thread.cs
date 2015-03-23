using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bugsnag.NET.Request
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
