using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bugsnag.NET.Request
{
    public class User : IUser
    {
        public User(object id)
        {
            Id = id;
        }

        public object Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
