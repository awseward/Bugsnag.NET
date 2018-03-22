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
        public void SendError_InstanceApproach()
        {
            Utils.Run(
                _instanceApproach.TestApp(snagger => snagger.Error)
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
        public void SendInfo_InstanceApproach()
        {
            Utils.Run(
                _instanceApproach.TestApp(snagger => snagger.Info)
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
    }
}
