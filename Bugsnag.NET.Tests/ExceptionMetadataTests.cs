using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bugsnag.NET.FSharp;
using Microsoft.FSharp.Core;

namespace Bugsnag.NET.Tests
{
    class ExceptionMetadataTests
    {
        [Test]
        public void Hello()
        {
            var ex = new Exception();

            var metadataId = ExceptionMetadata.tryReadMetadataId(ex);

            Assert.True(FSharpOption<Guid>.get_IsNone(metadataId));
        }

        [Test]
        public void Hello2()
        {
            var ex = new Exception();
            var metadataId = ExceptionMetadata.tryWriteNewMetadataId(ex);

            Assert.True(FSharpOption<Guid>.get_IsSome(metadataId));
        }
    }
}
