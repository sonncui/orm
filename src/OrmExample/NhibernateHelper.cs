using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace OrmExample
{
    public class NhibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static ISessionFactory sessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    InitializeSessionFactory();
                }
                return _sessionFactory;
            }
        }

        private static void InitializeSessionFactory()
        {
            const string connectionString = 
                "Data Source=localhost;Initial Catalog=orm_example;Integrated Security=True";

            _sessionFactory = Fluently
                .Configure()
                .Database(MsSqlConfiguration.MsSql2008.ShowSql().ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NhibernateHelper>())
                .BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return sessionFactory.OpenSession();
        } 
    }
}