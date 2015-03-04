using FluentNHibernate.Mapping;

namespace OrmExample.Resource
{
    public class User
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
    }

    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("[user]");
            Id(u => u.Id).Column("id");
            Map(u => u.Name).Column("name");
        }
    }
}