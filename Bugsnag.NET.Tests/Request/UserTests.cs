using Bugsnag.NET.Request;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bugsnag.Common;

namespace Bugsnag.NET.Tests.Request
{
    class UserTests
    {
        static Guid _guidId = Guid.Parse("72eae7d5-b3c9-43fd-8fa6-5dae60d509a7");
        static string _stringId = "q5OKLQpuYNmtiZ5wx/Aa+JkozBKaSBKI9ImYRG+1==";
        static int _intId = 8675309;
        static object[] _commonIds = new object[] { _guidId, _stringId, _intId };
        static string _name = "Robert Paulson";
        static string _email = "rpaulson@fight.club";

        public static object[] CommonIds { get { return _commonIds; } }

        [TestCaseSource("CommonIds")]
        public void IdIsCorrect(object id)
        {
            var user = _BuildUser(id, _name, _email);
            Assert.AreEqual(id, user.Id);
        }

        [Test]
        public void GuidIdIsCorrect()
        {
            _AssertId(_guidId);
        }

        [Test]
        public void StringIdIsCorrect()
        {
            _AssertId(_stringId);
        }

        [Test]
        public void IntIdIsCorrect()
        {
            _AssertId(_intId);
        }

        [Test]
        public void NameIsCorrect()
        {
            var user = _BuildUser(_guidId, _name, _email);
            Assert.AreEqual(_name, user.Name);
        }

        [Test]
        public void EmailIsCorrect()
        {
            var user = _BuildUser(_guidId, _name, _email);
            Assert.AreEqual(_email, user.Email);
        }

        IUser _BuildUser(object id, string name, string email)
        {
            return new User(id)
            {
                Name = name,
                Email = email
            };
        }

        void _AssertId(object id)
        {
            var user = _BuildUser(id, _name, _email);
            Assert.AreEqual(id, user.Id);
        }
    }
}
