using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bugsnag.NET.Extensions;
using NUnit.Framework;
using BsNET = Bugsnag.NET;
using BsReq = Bugsnag.NET.Request;

namespace Bugsnag.NET.Tests
{
    [Ignore]
    public class IntegrationTests
    {
        [Test]
        public void SendError_StaticApproach()
        {
            StaticApproachApplication.Main();
        }

        [Test]
        public void SendError_InstanceApproach()
        {
            new InstanceApproachApplication(
                (snagger, ex) => snagger.Error(ex, null, null)
            ).Run();
        }

        [Test]
        public void SendWarning_Instance()
        {
            new InstanceApproachApplication(
                (snagger, ex) => snagger.Warning(ex, null, null)
            ).Run();
        }

        [Test]
        public void SendInfo_InstanceApproach()
        {
            new InstanceApproachApplication(
                (snagger, ex) => snagger.Info(ex, null, null)
            ).Run();
        }
    }

    class InstanceApproachApplication
    {
        public InstanceApproachApplication(Action<IBugsnagger, Exception> notifyBugsnag)
        {
            _notifyBugsnag = notifyBugsnag ?? ( (snagger, ex) => { } );
        }

        readonly Action<IBugsnagger, Exception> _notifyBugsnag;

        public void Run()
        {
            try
            {
                // Main bulk of app code would go here

                Utils.TriggerError();
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

        void _OnUnhandledException(Exception ex) => _notifyBugsnag(_Bugsnagger, ex);
    }

    // NOTE: This is mostly here to check for backwards compatibility
    class StaticApproachApplication
    {
        public static void Main()
        {
            _InitBugsnag();

            try
            {
                // Main bulk of app code would go here

                Utils.TriggerError();
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
        public static void TriggerError()
        {
            throw new ApplicationException($"Hello, Bugsnag! {Guid.NewGuid()}");
        }

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
