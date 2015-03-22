using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bugsnag.NET.Request
{
    class StackTraceLine : IStackTraceLine
    {
        public string File
        {
            get { throw new NotImplementedException(); }
        }

        public int LineNumber
        {
            get { throw new NotImplementedException(); }
        }

        public int? ColumnNumber
        {
            get { throw new NotImplementedException(); }
        }

        public string Method
        {
            get { throw new NotImplementedException(); }
        }

        public bool InProject
        {
            get { throw new NotImplementedException(); }
        }
    }
}
