using Microsoft.Practices.Unity;
using NHibernate;
using Admin.Data.Query;


namespace Admin.Data.NHibernate
{
    public static class NHibernateDataInstaller
    {
        public static void Install(IUnityContainer container, LifetimeManager lifetimeManager)
        {
            //регистрация ISession
            container.RegisterType<ISession>(new InjectionFactory(c => NHibernateSessionManager.Instance.GetSession()));
            container.RegisterType<ISession>(lifetimeManager);
            
            container.RegisterType<IQueryRunner, QueryRunner>();
            
        }
    }
}
