using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Bugsnag.Common.Extensions;
using Bugsnag.Common;

namespace Bugsnag.NET.Tests
{
    class ExceptionExtensionTests
    {
        [Test]
        public void Null_Exception_Returns_EmptyList()
        {
            Exception exception = null;

            var exceptions = exception.Unwrap();

            Assert.IsEmpty(exceptions);
        }

        [Test]
        public void Single_Exception_Returns_A_List_Of_1()
        {
            Exception exception = new Exception("Please don't ever use the base exception.");

            var exceptions = exception.Unwrap();

            Assert.IsTrue(exceptions.Any());
            Assert.AreEqual(exception, exceptions.FirstOrDefault());
            Assert.AreEqual(1, exceptions.Count());
        }

        [Test]
        public void Nested_Exceptions_Returns_List_Of_All_Nested_Exceptions()
        {
            Exception innerException = new Exception("Inner exception");
            Exception topLevelException = new Exception("Top level exception", innerException);

            var exceptions = topLevelException.Unwrap();

            Assert.IsTrue(exceptions.Any());
            Assert.AreEqual(2, exceptions.Count());
        }

        [Test]
        public void Nested_Exceptions_Preserves_Exception_Order()
        {
            Exception innerException = new Exception("Inner exception");
            Exception topLevelException = new Exception("Top level exception", innerException);

            var exceptions = topLevelException.Unwrap();

            Assert.IsTrue(exceptions.Any());
            Assert.AreEqual(2, exceptions.Count());

            Assert.AreEqual(topLevelException, exceptions.FirstOrDefault());
            Assert.AreEqual(innerException, exceptions.LastOrDefault());
        }

        public void GenerateDeepTypesKey_works_on_simple_exceptions()
        {
            var ex = new InvalidOperationException("");

            var expectedTypesKey = "InvalidOperationException()";

            Assert.AreEqual(
                expected: expectedTypesKey,
                actual: ExceptionExtensions.GenerateDeepTypesKey(ex)
            );
        }

        [Test]
        public void GenerateDeepTypesKey_works_on_decently_nested_exceptions_including_AggregateExceptions()
        {
            var ex =
                new AggregateException(
                    new InvalidOperationException("",
                        new AggregateException(
                            new InvalidOperationException(),
                            new Exception())
                    ),
                    new DivideByZeroException(),
                    new ArgumentException("", new FieldAccessException())
                );

            var expectedTypesKey = "AggregateException[InvalidOperationException(AggregateException[InvalidOperationException(),Exception()]),DivideByZeroException(),ArgumentException(FieldAccessException())]";

            Assert.AreEqual(
                expected: expectedTypesKey,
                actual: ExceptionExtensions.GenerateDeepTypesKey(ex)
            );
        }

        [Test]
        public void GenerateDeepTypesKey_returns_empty_string_when_given_null_input()
        {
            Exception ex = null;

            Assert.IsEmpty(ExceptionExtensions.GenerateDeepTypesKey(ex));
        }
    }
}
