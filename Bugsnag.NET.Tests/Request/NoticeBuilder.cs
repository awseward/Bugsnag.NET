using Bugsnag.NET.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugsnag.NET.Tests.Request
{
    class NoticeBuilder
    {
        static IUser _user = new User(42)
        {
            Name = "Hamburger Helper",
            Email = "hamburger@helper.io"
        };

        static IApp _app = new App
        {
            Version = "3.1.4",
            ReleaseStage = "development"
        };

        static string _apiKey = "wh47AB0gu5Ap1Key7h1sI5";

        static object _metaData = new
        {
            SomeData = new
            {
                Id = 12345,
                CreatedAt = DateTime.Now
            }
        };

        public static INotice GetNotice()
        {
            try
            {
                _OuterMethod();
                throw new InvalidOperationException("Shouldn't get here...");
            }
            catch (Exception ex)
            {
                Bugsnag.App = _app;
                var client = Bugsnag.Error;
                var evt = client.GetEvent(ex, _user, _metaData);
                return new Notice(_apiKey, new Notifier(), evt);
            }
        }

        static void _OuterMethod()
        {
            try
            {
                _InnerMethod();
            }
            catch (Exception ex)
            {
                throw new Exception("Second wrapping exception", ex);
            }
        }

        static void _InnerMethod()
        {
            try
            {
                var zero = 0;
                var garbage = 1 / zero;
            }
            catch (Exception ex)
            {
                throw new Exception("First wrapping exception", ex);
            }
        }
    }
}
