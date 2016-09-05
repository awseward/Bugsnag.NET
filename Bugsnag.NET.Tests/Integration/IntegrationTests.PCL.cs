using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bugsnag.Common;
using Bugsnag.PCL.Request;
using NUnit.Framework;
using BsPCL = Bugsnag.PCL;

namespace Bugsnag.Tests.Integration
{
    [Ignore]
    public class IntegrationTests_PCL
    {
        [Test]
        public void SendError_StaticApproach()
        {
            Utils.Run(StaticApproach.TestApp);
        }

        [Test]
        public async Task SendError_StaticApproachAsync()
        {
            await Utils.Run(StaticApproach.AsyncTestApp);
        }

        [Test]
        public void SendError_InstanceApproach()
        {
            Utils.Run(
                _instanceApproach.TestApp(snagger => snagger.Error)
            );
        }

        [Test]
        public async Task SendError_InstanceApproachAsync()
        {
            await Utils.Run(
                _instanceApproach.AsyncTestApp(snagger => snagger.ErrorAsync)
            );
        }

        [Test]
        public void SendWarning_InstanceApproach()
        {
            Utils.Run(
                _instanceApproach.TestApp(snagger => snagger.Warning)
            );
        }

        [Test]
        public async Task SendWarning_InstanceApproachAsync()
        {
            await Utils.Run(
                _instanceApproach.AsyncTestApp(snagger => snagger.WarningAsync)
            );
        }

        [Test]
        public void SendInfo_InstanceApproach()
        {
            Utils.Run(
                _instanceApproach.TestApp(snagger => snagger.Info)
            );
        }

        [Test]
        public async Task SendInfo_InstanceApproachAsync()
        {
            await Utils.Run(
                _instanceApproach.AsyncTestApp(snagger => snagger.InfoAsync)
            );
        }

        static InstanceApproach _instanceApproach = new InstanceApproach(_GetSnagger);

        static IBugsnagger _GetSnagger() => new BsPCL.Bugsnagger
        {
            ApiKey = Utils.ReadApiKey(),
            App = new App
            {
                Version = "1.2.3",
                ReleaseStage = "test",
            },
        };

        class StaticApproach
        {
            public static ITestApp TestApp { get; } = new StaticApproachApp(
                _SetApiKey,
                _SetApp,
                _OnUnhandledException
            );
            public static IAsyncTestApp AsyncTestApp { get; } = new StaticApproachAsyncApp(
                _SetApiKey,
                _SetApp,
                _OnUnhandledExceptionAsync
            );

            static void _SetApiKey(string key) => BsPCL.Bugsnag.ApiKey = key;
            static void _SetApp() => BsPCL.Bugsnag.App = new App
            {
                Version = "1.2.3",
                ReleaseStage = "test",
            };
            static void _OnUnhandledException(Exception ex)
            {
                var client = BsPCL.Bugsnag.Error;
                var @event = client.GetEvent(ex, null, null);

                client.Notify(@event);
            }
            static Task _OnUnhandledExceptionAsync(Exception ex)
            {
                var client = BsPCL.Bugsnag.Error;
                var @event = client.GetEvent(ex, null, null);

                return client.NotifyAsync(@event);
            }
        }
    }
}
