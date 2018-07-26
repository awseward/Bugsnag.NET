using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bugsnag.Common
{
    public interface IClient
    {
        void Send(INotice notice);
        void Send(INotice notice, Action<Exception> onException);
        void Send(INotice notice, bool useSSL);
        void Send(INotice notice, bool useSSL, Action<Exception> onException);
        Task<HttpResponseMessage> SendAsync(INotice notice);
        Task<HttpResponseMessage> SendAsync(INotice notice, bool useSSL);
    }
}
