﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Practices.Unity;
using Admin.DataService.Interface;
using Admin.WebApp.App_Start;

namespace Admin.WebApp.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser, long>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, long> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            UserStore userStore = new UserStore(new UnityServiceLocator(UnityConfig.GetConfiguredContainer()).GetInstance<IWorkerUserService>());
            var manager = new ApplicationUserManager(userStore);

            return manager;
        }
        
    }
}