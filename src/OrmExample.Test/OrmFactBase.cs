using System;
using NHibernate;

namespace OrmExample.Test
{
    public abstract class OrmFactBase: IDisposable
    {
        protected ISession session;

        protected OrmFactBase()
        {
            session = NhibernateHelper.OpenSession();
            CleanDatabase();
        }

        public void CleanDatabase()
        {
            session.CreateSQLQuery("delete from [User] where Id > 2").ExecuteUpdate();
            session.CreateSQLQuery("delete from [order] where id > 1").ExecuteUpdate();
            session.CreateSQLQuery("delete from [order_items] where id > 3").ExecuteUpdate();
            session.CreateSQLQuery("delete from [product] where id > 3").ExecuteUpdate();
            session.CreateSQLQuery("delete from [user_detail] where id > 3").ExecuteUpdate();
            session.Flush();
        }

        public void Dispose()
        {
            session.Dispose();
        }
    }
}