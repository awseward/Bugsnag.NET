using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bugsnag.Common
{
    public class BugsnagClient : IClient
    {
        static Uri _uri = new Uri("http://notify.bugsnag.com");
        static Uri _sslUri = new Uri("https://notify.bugsnag.com");
        static JsonSerializerSettings _settings = new JsonSerializerSettings { };
        static HttpClient _HttpClient { get; } = new HttpClient();

        public void Send(INotice notice) => Send(notice, true);

        public void Send(INotice notice, bool useSSL) => SendAsync(notice, useSSL).Wait();

        public Task<HttpResponseMessage> SendAsync(INotice notice) => SendAsync(notice, true);

        public Task<HttpResponseMessage> SendAsync(INotice notice, bool useSSL)
        {
            var uri = useSSL ? _sslUri : _uri;
            var json = JsonConvert.SerializeObject(notice, _settings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return _HttpClient.PostAsync(uri, content);
        }
    }
}
