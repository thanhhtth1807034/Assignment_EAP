using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace EAP_Assignment.Models
{
    public class AppUserManager : UserManager<Account>
    {
        public AppUserManager(IUserStore<Account> store) : base(store)
        {
        }
        public static AppUserManager Create(
            IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            var manager = new AppUserManager(
                new UserStore<Account>(context.Get<MyDbContext>()));

            // optionally configure your manager

            return manager;
        }
    }
}