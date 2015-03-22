using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bugsnag.NET.Request
{
    class App : IApp
    {
        public string Version
        {
            get { throw new NotImplementedException(); }
        }

        public string ReleaseStage
        {
            get { throw new NotImplementedException(); }
        }
    }
}
