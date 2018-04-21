using Bugsnag.Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugsnag.NET.Tests
{
    class StacktraceLineParserTests
    {
        static StacktraceLineParser Parser => new StacktraceLineParser();

        [TestCase(
            "   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)",
            "System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)")]
        [TestCase(
            "   en System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)",
            "System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)")]
        [TestCase(
            "   à System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)",
            "System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)")]

        [TestCase(
            @"   at NLogAsyncExceptionTestCase.Program.<Foo>d__8.MoveNext() in c:\Users\ComputerUser\Documents\Visual Studio 2012\Projects\NLogAsyncExceptionTestCase\NLogAsyncExceptionTestCase.Console\Program.cs:line 44",
            "NLogAsyncExceptionTestCase.Program.<Foo>d__8.MoveNext()")]
        [TestCase(
            @"   en NLogAsyncExceptionTestCase.Program.<Foo>d__8.MoveNext() en c:\Users\ComputerUser\Documents\Visual Studio 2012\Projects\NLogAsyncExceptionTestCase\NLogAsyncExceptionTestCase.Console\Program.cs:línea 44",
            "NLogAsyncExceptionTestCase.Program.<Foo>d__8.MoveNext()")]
        [TestCase(
            @"   à NLogAsyncExceptionTestCase.Program.<Foo>d__8.MoveNext() dans c:\Users\ComputerUser\Documents\Visual Studio 2012\Projects\NLogAsyncExceptionTestCase\NLogAsyncExceptionTestCase.Console\Program.cs:ligne 44",
            "NLogAsyncExceptionTestCase.Program.<Foo>d__8.MoveNext()")]

        [TestCase(
            "--- End of stack trace from previous location where exception was thrown ---",
            "--- End of stack trace from previous location where exception was thrown ---")]
        [TestCase(
            "--- Fin de la trace de la pile à partir de l'emplacement précédent au niveau duquel l'exception a été levée ---",
            "--- Fin de la trace de la pile à partir de l'emplacement précédent au niveau duquel l'exception a été levée ---")]
        public void ParseMethodName(string line, string expected)
        {
            var parser = Parser;

            Assert.AreEqual(
                expected: expected,
                actual: parser.ParseMethodName(line));
        }

        [TestCase(
            "   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)",
            "")]
        [TestCase(
            "   en System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)",
            "")]
        [TestCase(
            "   à System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)",
            "")]

        [TestCase(
            @"   at NLogAsyncExceptionTestCase.Program.<Foo>d__8.MoveNext() in c:\Users\ComputerUser\Documents\Visual Studio 2012\Projects\NLogAsyncExceptionTestCase\NLogAsyncExceptionTestCase.Console\Program.cs:line 44",
            @"c:\Users\ComputerUser\Documents\Visual Studio 2012\Projects\NLogAsyncExceptionTestCase\NLogAsyncExceptionTestCase.Console\Program.cs")]
        [TestCase(
            @"   en NLogAsyncExceptionTestCase.Program.<Foo>d__8.MoveNext() en c:\Users\ComputerUser\Documents\Visual Studio 2012\Projects\NLogAsyncExceptionTestCase\NLogAsyncExceptionTestCase.Console\Program.cs:línea 44",
            @"c:\Users\ComputerUser\Documents\Visual Studio 2012\Projects\NLogAsyncExceptionTestCase\NLogAsyncExceptionTestCase.Console\Program.cs")]
        [TestCase(
            @"   à NLogAsyncExceptionTestCase.Program.<Foo>d__8.MoveNext() dans c:\Users\ComputerUser\Documents\Visual Studio 2012\Projects\NLogAsyncExceptionTestCase\NLogAsyncExceptionTestCase.Console\Program.cs:ligne 44",
            @"c:\Users\ComputerUser\Documents\Visual Studio 2012\Projects\NLogAsyncExceptionTestCase\NLogAsyncExceptionTestCase.Console\Program.cs")]

        [TestCase(
            "--- End of stack trace from previous location where exception was thrown ---",
            "")]
        [TestCase(
            "--- Fin de la trace de la pile à partir de l'emplacement précédent au niveau duquel l'exception a été levée ---",
            "")]
        public void ParseFile(string line, string expected)
        {
            var parser = Parser;

            Assert.AreEqual(
                expected: expected,
                actual: parser.ParseFile(line));
        }

        [TestCase(
            "   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)",
            null)]
        [TestCase(
            "   en System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)",
            null)]
        [TestCase(
            "   à System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)",
            null)]
        [TestCase(
            @"   at NLogAsyncExceptionTestCase.Program.<Foo>d__8.MoveNext() in c:\Users\ComputerUser\Documents\Visual Studio 2012\Projects\NLogAsyncExceptionTestCase\NLogAsyncExceptionTestCase.Console\Program.cs:line 44",
            44)]
        [TestCase(
            @"   en NLogAsyncExceptionTestCase.Program.<Foo>d__8.MoveNext() en c:\Users\ComputerUser\Documents\Visual Studio 2012\Projects\NLogAsyncExceptionTestCase\NLogAsyncExceptionTestCase.Console\Program.cs:línea 44",
            44)]
        [TestCase(
            @"   à NLogAsyncExceptionTestCase.Program.<Foo>d__8.MoveNext() dans c:\Users\ComputerUser\Documents\Visual Studio 2012\Projects\NLogAsyncExceptionTestCase\NLogAsyncExceptionTestCase.Console\Program.cs:ligne 44",
            44)]
        [TestCase(
            "--- End of stack trace from previous location where exception was thrown ---",
            null)]
        [TestCase(
            "--- Fin de la trace de la pile à partir de l'emplacement précédent au niveau duquel l'exception a été levée ---",
            null)]
        public void ParseLineNumber(string line, int? expected)
        {
            var parser = Parser;

            Assert.AreEqual(
                expected: expected,
                actual: parser.ParseLineNumber(line));
        }
    }
}
