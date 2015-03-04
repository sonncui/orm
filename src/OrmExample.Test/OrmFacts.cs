using System;
using NHibernate;
using OrmExample.Resource;
using Xunit;

namespace OrmExample.Test
{
    public class OrmFacts : IDisposable
    {
        private readonly ISession session;

        public OrmFacts()
        {
            session = NhibernateHelper.OpenSession();
        }

        [Fact]
        public void select_user()
        {
            User user = session.Get<User>(1L);

            Assert.Equal("Conan", user.Name);
        }

        public void Dispose()
        {
            session.Dispose();
        }
    }
}
