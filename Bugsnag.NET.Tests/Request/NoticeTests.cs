using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugsnag.NET.Tests.Request
{
    class NoticeTests
    {
        [Test]
        public void HasCorrectSchema()
        {
            var schema = _GetSchema();
            var notice = NoticeBuilder.GetNotice();
            var json = JsonConvert.SerializeObject(notice);
            var jObject = JObject.Parse(json);

            Assert.IsTrue(jObject.IsValid(schema));
        }

        JsonSchema _GetSchema()
        {
            var json = File.ReadAllText("Data/sampleRequest.json");
            return JsonSchema.Parse(json);
        }
    }
}
