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
        void Send(INotice notice, bool useSSL);
        Task<HttpResponseMessage> SendAsync(INotice notice);
        Task<HttpResponseMessage> SendAsync(INotice notice, bool useSSL);
    }
}
