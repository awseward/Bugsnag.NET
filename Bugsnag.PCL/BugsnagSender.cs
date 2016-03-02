using System;
using System.Net.Http;
using System.Threading.Tasks;
using Bugsnag.PCL.Request;
using Newtonsoft.Json;

namespace Bugsnag.PCL
{
    class BugsnagSender
    {
        static Uri _uri = new Uri("http://notify.bugsnag.com");
        static Uri _sslUri = new Uri("https://notify.bugsnag.com");
        static JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public static async Task<HttpResponseMessage> SendAsync(INotice notice)
        {
            return await SendAsync(notice, true);
        }

        public static async Task<HttpResponseMessage> SendAsync(INotice notice, bool useSSL)
        {
            var uri = _GetUri(useSSL);
            var json = JsonConvert.SerializeObject(notice, _settings);

            var content = new StringContent(json);

            return await new HttpClient().PostAsync(uri, content);
        }

        static Uri _GetUri(bool useSSL)
        {
            return useSSL ? _sslUri : _uri;
        }
    }
}
