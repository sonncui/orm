using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
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
            CleanDatabase();
        }

        private void CleanDatabase()
        {
            session.CreateSQLQuery("delete from [User] where Id > 2").ExecuteUpdate();
            session.CreateSQLQuery("delete from [order] where id > 1").ExecuteUpdate();
            session.CreateSQLQuery("delete from [order_items] where id > 3").ExecuteUpdate();
            session.CreateSQLQuery("delete from [product] where id > 3").ExecuteUpdate();
            session.CreateSQLQuery("delete from [user_detail] where id > 3").ExecuteUpdate();
            session.Flush();
        }

        [Fact]
        public void should_select_user()
        {
            User user = session.Get<User>(1L);

            Assert.Equal("Conan", user.Name);
        }

        [Fact]
        public void should_add_user()
        {
            const string userName = "Huluwa";
            var user = new User{Name = userName};

            session.Save(user);
            session.Flush();

            int huluwaCount = session.Query<User>().Count(u => u.Name == userName);
            Assert.Equal(1, huluwaCount);
        }

        [Fact]
        public void should_delete_user()
        {
            const string userName = "Huluwa";

            AddUser(userName);

            var user = session.Query<User>().Single(u => u.Name == userName);

            session.Delete(user);
            session.Flush();

            int shejingCount = session.Query<User>().Count(u => u.Name == userName);
            Assert.Equal(0, shejingCount);
        }

        [Fact]
        public void shuold_update_user()
        {
            const string userName = "Huluwa";
            AddUser(userName);

            var user = session.Query<User>().Single(u => u.Name == userName);
            user.Name = "Shejing";

            session.Update(user);
            session.Flush();

            var shejingCount = session.Query<User>().Count(u => u.Name == "Shejing");

            Assert.Equal(1, shejingCount);
        }



        private void AddUser(string name)
        {
            var user = new User { Name = name };

            session.Save(user);
            session.Flush();
        }

        public void Dispose()
        {
            session.Dispose();
        }
    }
}
