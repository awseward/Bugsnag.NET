using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bugsnag.Common;

namespace Bugsnag.Tests.Integration
{
    class InstanceApproachAsyncApp : IAsyncTestApp
    {
        public InstanceApproachAsyncApp(
            IBugsnagger snagger,
            Func<IBugsnagger, Exception, Task> notifyAsync)
        {
            _snagger = snagger;
            _notifyAsync = notifyAsync;
        }

        readonly IBugsnagger _snagger;

        readonly Func<IBugsnagger, Exception, Task> _notifyAsync;

        public async Task Run(string testInfo)
        {
            try
            {
                // Main bulk of app code would go here

                Utils.TriggerError(testInfo);
            }
            catch (Exception ex)
            {
                await _OnUnhandledException(ex);
            }
        }

        Task _OnUnhandledException(Exception ex) => _notifyAsync(_snagger, ex);
    }
}
