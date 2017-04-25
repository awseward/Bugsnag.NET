using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bugsnag.Common;

namespace Bugsnag.Tests.Integration
{
    // NOTE: This is mostly here as a quick check for backwards compatibility
    class StaticApproachApp : ITestApp
    {
        public StaticApproachApp(
            Action<string> setApiKey,
            Action setApp,
            Action<Exception> onUnhandledException)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fr-fr");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr-fr");
            _setApiKey = setApiKey;
            _setApp = setApp;
            _onUnhandledException = onUnhandledException;
        }

        readonly Action<string> _setApiKey;
        readonly Action _setApp;
        readonly Action<Exception> _onUnhandledException;

        void _InitBugsnag()
        {
            _setApiKey(Utils.ReadApiKey());
            _setApp();
        }

        public void Run(string testInfo)
        {
            _InitBugsnag();

            try
            {
                // Main bulk of app code would go here

                Utils.TriggerError(testInfo);
            }
            catch (Exception ex)
            {
                _onUnhandledException(ex);
            }
        }
    }
}
