using Bugsnag.NET.Request;
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

        [Test]
        public void StackIsAbsent()
        {
            const string InternalErrorMessage = "An internal error has occurred!";
            Exception ex = new Exception(InternalErrorMessage, new Exception("Debug Info: " + Environment.NewLine + "Some debug information goes here..."));
            var ev = new Event(ex);
            var notice = new Notice("apikey", new Notifier(), ev);
            var json = JsonConvert.SerializeObject(notice);
            // should reach the end of function without any exception thrown
        }

        JsonSchema _GetSchema()
        {
            var json = File.ReadAllText("Data/sampleRequest.json");
            return JsonSchema.Parse(json);
        }
    }
}
