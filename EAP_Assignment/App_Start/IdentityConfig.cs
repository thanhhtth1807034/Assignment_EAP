using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EAP_Assignment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace EAP_Assignment.App_Start
{
    public class IdentityConfig
    {

    }

    public class ApplicationUserManager : UserManager<Account>
    {
        public ApplicationUserManager(IUserStore<Account> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<Account>(context.Get<MyDbContext>()));

            return manager;
        }
    }
}