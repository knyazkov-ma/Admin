﻿using NHibernate;
using Admin.Data.Query;
using Admin.Entity;


namespace Admin.Data.NHibernate
{
    public class QueryRunner: IQueryRunner
    {
        private readonly ISession session;
        public QueryRunner(ISession session)
        {
            this.session = session;
        }

        public TResult Run<TResult, TEntity1, TEntity2, TEntity3, TEntity4>(IQuery<TResult, TEntity1, TEntity2, TEntity3, TEntity4> query)
            where TEntity1 : BaseEntity
            where TEntity2 : BaseEntity
            where TEntity3 : BaseEntity
            where TEntity4 : BaseEntity
        {
            return query.Run(session.Query<TEntity1>(), session.Query<TEntity2>(), session.Query<TEntity3>(), session.Query<TEntity4>());
        }
        public TResult Run<TResult, TEntity1, TEntity2, TEntity3>(IQuery<TResult, TEntity1, TEntity2, TEntity3> query)
            where TEntity1 : BaseEntity
            where TEntity2 : BaseEntity
            where TEntity3 : BaseEntity
        {
            return query.Run(session.Query<TEntity1>(), session.Query<TEntity2>(), session.Query<TEntity3>());
        }

        public TResult Run<TResult, TEntity1, TEntity2>(IQuery<TResult, TEntity1, TEntity2> query)
            where TEntity1 : BaseEntity
            where TEntity2 : BaseEntity
           
        {
            return query.Run(session.Query<TEntity1>(), session.Query<TEntity2>());
        }

        public TResult Run<TResult, TEntity1>(IQuery<TResult, TEntity1> query)
            where TEntity1 : BaseEntity
            
        {
            return query.Run(session.Query<TEntity1>());
        }
    }
}
