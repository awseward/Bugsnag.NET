using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Bugsnag.Common.Extensions;

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

        [Test]
        public void ReadData_reads_entries_with_string_keys()
        {
            var exception = new Exception("");
            var key = "key";
            var value = Guid.NewGuid();
            exception.Data.Add(key, value);

            var singleKvp = exception.ReadData().Single();

            Assert.AreEqual(singleKvp.Key, key);
            Assert.AreEqual(singleKvp.Value, value);
        }

        [Test]
        public void ReadData_skips_entries_with_nonstring_keys()
        {
            var exception = new Exception("Unimportant message");
            var key = 123L;
            var value = Guid.NewGuid();
            exception.Data.Add(key, value);

            var data = exception.ReadData();

            Assert.AreEqual(0, data.Count);
        }
    }
}
