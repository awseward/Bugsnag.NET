using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bugsnag.NET.Request
{
    class User : IUser
    {
        public object Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
