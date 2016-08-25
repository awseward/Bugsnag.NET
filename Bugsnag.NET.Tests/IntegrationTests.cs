using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BsNET = Bugsnag.NET;
using BsReq = Bugsnag.NET.Request;

namespace Bugsnag.NET.Tests
{
    public class IntegrationTests
    {
        [Test]
        [Ignore]
        public void SendAnError_StaticApproach()
        {
            StaticApproachApplication.Main();
        }

        [Test]
        [Ignore]
        public void SendAnError_InstanceApproach()
        {
            InstanceApproachApplication.Main();
        }
    }

    class InstanceApproachApplication
    {
        public static void Main()
        {
            try
            {
                // Main bulk of app code would go here

                throw new ApplicationException("Hello, Bugsnag!");
            }
            catch (Exception ex)
            {
                _OnUnhandledException(ex);
            }
        }

        static IBugsnagger _Bugsnagger { get; } = new Bugsnagger
        {
            ApiKey = Utils.ReadApiKey(),
            App = new BsReq.App
            {
                Version = "1.2.3",
                ReleaseStage = "test",
            },
        };

        static void _OnUnhandledException(Exception ex)
        {
            // FIXME: Event creation is still a little funky...
            var @event = BsNET.Bugsnag.Error.GetEvent(ex, null, null);

            _Bugsnagger.Notify(@event);
        }
    }

    class StaticApproachApplication
    {
        public static void Main()
        {
            _InitBugsnag();

            try
            {
                // Main bulk of app code would go here

                throw new ApplicationException("Hello, Bugsnag!");
            }
            catch (Exception ex)
            {
                _OnUnhandledException(ex);
            }
        }

        static void _InitBugsnag()
        {
            BsNET.Bugsnag.ApiKey = Utils.ReadApiKey();
            BsNET.Bugsnag.App = new BsReq.App
            {
                Version = "1.2.3",
                ReleaseStage = "test",
            };
        }

        static void _OnUnhandledException(Exception ex)
        {
            var client = BsNET.Bugsnag.Error;
            var @event = client.GetEvent(ex, null, null);

            client.Notify(@event);
        }
    }

    static class Utils
    {
        public static string ReadApiKey()
        {
            var envVarName = "BUGSNAG_NET_API_KEY";
            var envVarType = EnvironmentVariableTarget.Machine;

            var apiKey = Environment.GetEnvironmentVariable(envVarName, envVarType);

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new InvalidOperationException(
                    $"Missing required {envVarType} environment variable `{envVarName}`"
                );
            }

            return apiKey;
        }
    }
}
