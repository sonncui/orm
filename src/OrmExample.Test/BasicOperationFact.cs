using System.Linq;
using NHibernate.Linq;
using OrmExample.Resource;
using Xunit;

namespace OrmExample.Test
{
    public class BasicOperationFact : OrmFactBase
    {
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
    }
}
