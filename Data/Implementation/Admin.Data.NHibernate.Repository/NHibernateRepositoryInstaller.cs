using Microsoft.Practices.Unity;
using Admin.Entity;
using Admin.Data.Repository;

namespace Admin.Data.NHibernate.Repository
{
    public static class NHibernateRepositoryInstaller
    {
        public static void Install(IUnityContainer container)
        {
            
            //регистрация репозиториев
            container.RegisterType<IRepository, BaseRepository>();
            container.RegisterType<IBaseRepository<TypeWorkerUser>, BaseRepository<TypeWorkerUser>>();
            container.RegisterType<IBaseRepository<WorkerUser>, BaseRepository<WorkerUser>>();
           
       }
        
    }
}
