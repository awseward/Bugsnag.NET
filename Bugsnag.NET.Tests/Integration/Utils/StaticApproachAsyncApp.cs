using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugsnag.Tests.Integration
{
    class StaticApproachAsyncApp : IAsyncTestApp
    {
        public StaticApproachAsyncApp(
            Action<string> setApiKey,
            Action setApp,
            Func<Exception, Task> onUnhandledException)
        {
            _setApiKey = setApiKey;
            _setApp = setApp;
            _onUnhandledException = onUnhandledException;
        }

        readonly Action<string> _setApiKey;
        readonly Action _setApp;
        readonly Func<Exception, Task> _onUnhandledException;

        void _InitBugsnag()
        {
            _setApiKey(Utils.ReadApiKey());
            _setApp();
        }

        public async Task Run(string testInfo)
        {
            _InitBugsnag();

            try
            {
                Utils.TriggerError(testInfo);
            }
            catch (Exception ex)
            {
                await _onUnhandledException(ex);
            }
        }
    }
}
