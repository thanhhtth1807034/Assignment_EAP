using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EAP_Assignment.App_Start;
using EAP_Assignment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(EAP_Assignment.Startup))]
namespace EAP_Assignment
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(MyDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            //app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);


            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),

            });

        }
    }
}