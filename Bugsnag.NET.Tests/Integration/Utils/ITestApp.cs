using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugsnag.Tests.Integration
{
    interface ITestApp
    {
        void Run(string testInfo);
    }

    interface IAsyncTestApp
    {
        Task Run(string testInfo);
    }
}
