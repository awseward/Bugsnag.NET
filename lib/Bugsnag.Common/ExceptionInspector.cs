using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bugsnag.Common
{
    public class ExceptionInspector : IExceptionInspector
    {
        static readonly string _unknownType = "UNKNOWN_TYPE";
        static readonly string _unknownMethod = "UNKNOWN_METHOD";

        public ExceptionInspector(Func<Exception, MethodBase> getTargetSite)
        {
            _getTargetSite = getTargetSite;
        }

        readonly Func<Exception, MethodBase> _getTargetSite;

        public string GetContext(Exception ex)
        {
            var targetSite = _getTargetSite(ex);

            return targetSite == null
                ? $"{_unknownType}::{_unknownMethod}"
                : $"{targetSite.DeclaringType}::{targetSite.Name}";
        }
    }
}
