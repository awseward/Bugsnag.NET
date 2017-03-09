using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Bugsnag.NET.Tests
{
    public class StacktracelineParsingTests
    {
        // NOTE: Cases here based on this random stacktrace I grabbed off of StackOverflow:
        // at NLogAsyncExceptionTestCase.Program.<Bar>d__d.MoveNext() in c:\Users\ComputerUser\Documents\Visual Studio 2012\Projects\NLogAsyncExceptionTestCase\NLogAsyncExceptionTestCase.Console\Program.cs:line 53
        // --- End of stack trace from previous location where exception was thrown ---
        //    at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
        //    at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
        //    at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
        //    at NLogAsyncExceptionTestCase.Program.<Foo>d__8.MoveNext() in c:\Users\ComputerUser\Documents\Visual Studio 2012\Projects\NLogAsyncExceptionTestCase\NLogAsyncExceptionTestCase.Console\Program.cs:line 44
        // --- End of stack trace from previous location where exception was thrown ---
        //    at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
        //    at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
        //    at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
        //    at NLogAsyncExceptionTestCase.Program.<CheckFooAndBar>d__0.MoveNext() in c:\Users\ComputerUser\Documents\Visual Studio 2012\Projects\NLogAsyncExceptionTestCase\NLogAsyncExceptionTestCase.Console\Program.cs:line 30

        readonly string _sourceLine = @"at NLogAsyncExceptionTestCase.Program.<Bar>d__d.MoveNext() in c:\Users\ComputerUser\Documents\Visual Studio 2012\Projects\NLogAsyncExceptionTestCase\NLogAsyncExceptionTestCase.Console\Program.cs:line 53";
        readonly string _frameworkLine = @"   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)";
        readonly string _asyncAwaitPreviousLocationLine = @"--- End of stack trace from previous location where exception was thrown ---";

        [Test]
        public void CanParseSourceLineWithFileInfo()
        {
            var line = _sourceLine;

            Assert.Fail();
        }

        [Test]
        public void CanParseFrameworkLineLackingFileInfo()
        {
            var line = _frameworkLine;

            Assert.Fail();
        }

        [Test]
        public void CanParseAsyncAwaitPreviousLocationLine()
        {
            var line = _asyncAwaitPreviousLocationLine;

            Assert.Fail();
        }
    }
}
