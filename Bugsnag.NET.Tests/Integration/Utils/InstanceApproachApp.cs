using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bugsnag.Common;

namespace Bugsnag.Tests.Integration
{
    class InstanceApproachApp : ITestApp
    {
        public InstanceApproachApp(
            IBugsnagger snagger,
            Action<IBugsnagger, Exception> notify)
        {
            _snagger = snagger;
            _notify = notify;
        }

        readonly IBugsnagger _snagger;

        readonly Action<IBugsnagger, Exception> _notify;

        public void Run(string testInfo)
        {
            try
            {
                // Main bulk of app code would go here

                Utils.TriggerError(testInfo);
            }
            catch (Exception ex)
            {
                _OnUnhandledException(ex);
            }
        }

        void _OnUnhandledException(Exception ex) => _notify(_snagger, ex);
    }
}
