using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugsnag.Common
{
    public interface IExceptionInspector
    {
        string GetContext(Exception ex);
    }
}
