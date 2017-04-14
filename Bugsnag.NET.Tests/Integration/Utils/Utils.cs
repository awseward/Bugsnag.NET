using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Bugsnag.Common;

namespace Bugsnag.Tests.Integration
{
    public static class Utils
    {
        public static void Inconclusive()
        {
            NUnit.Framework.Assert.Inconclusive("notify.bugsnag.com returns 200 no matter what, so this result is inconclusive pending a manual check of the actual test project on app.bugsnag.com");
        }

        internal static void Run(
            ITestApp app,
            [CallerMemberName] string callerName = "[unknown test]",
            [CallerFilePath] string sourceFilePath = "[unknown file]",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var fileHint = $"{sourceFilePath}:{sourceLineNumber}";

            app.Run($"{callerName} ({sourceFilePath}:{sourceLineNumber})");

            Inconclusive();
        }

        internal static async Task Run(
            IAsyncTestApp app,
            [CallerMemberName] string callerName = "[unknown test]",
            [CallerFilePath] string sourceFilePath = "[unknown file]",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var fileHint = $"{sourceFilePath}:{sourceLineNumber}";

            await app.Run($"{callerName} ({sourceFilePath}:{sourceLineNumber})");

            Inconclusive();
        }

        public static void TriggerError(string testInfo)
        {
            new System.Net.Http.HttpClient().GetAsync("https://thisdoesnot.exist.com:5678/youbetitdoesnt").Wait();
            var ex = new ApplicationException($"Error triggered by {testInfo}");
            ex.Data.Add("hello", "world");
            ex.Data.Add("uri", new Uri("https://google.com/robots.txt"));
            //ex.Data.Add("__foo__", new { Woah = "You bet" });
            throw ex;
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

    class InstanceApproach
    {
        public InstanceApproach(Func<IBugsnagger> getSnagger)
        {
            _getSnagger = getSnagger;
        }

        readonly Func<IBugsnagger> _getSnagger;

        public ITestApp TestApp(Action<IBugsnagger, Exception> notify)
        {
            return new InstanceApproachApp(_getSnagger(), notify);
        }

        public ITestApp TestApp(Func<IBugsnagger, Action<Exception, IUser, object>> getNotify)
        {
            return TestApp((snagger, ex) => getNotify(snagger)(ex, null, _GetMetadata()));
        }

        public IAsyncTestApp AsyncTestApp(Func<IBugsnagger, Exception, Task> notifyAsync)
        {
            return new InstanceApproachAsyncApp(_getSnagger(), notifyAsync);
        }

        public IAsyncTestApp AsyncTestApp(Func<IBugsnagger, Func<Exception, IUser, object, Task>> getNotify)
        {
            return AsyncTestApp((snagger, ex) => getNotify(snagger)(ex, null, _GetMetadata()));
        }

        object _GetMetadata() =>
            new
            {
                Foo = "bar",
                Baz = (object) null,
            };
    }
}
