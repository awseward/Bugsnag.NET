using Bugsnag.NET.Request;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugsnag.NET.Tests.Request
{
    class UserTests
    {
        static Guid _guidId = Guid.NewGuid();
        static string _stringId = System.IO.Path.GetRandomFileName().Replace(".", "");
        static int _intId = 8675309;
        static object[] _commonIds = new object[] { _guidId, _stringId, _intId };
        public static object[] CommonIds { get { return _commonIds; } }

        static string _name = "Robert Paulson";
        static string _email = "rpaulson@fight.club";

        [TestCaseSource("CommonIds")]
        [Ignore("Possible bug in NUnit (`Test adapter sent back a result for an unknown test case.`)")]
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
            return new User
            {
                Id = id,
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
