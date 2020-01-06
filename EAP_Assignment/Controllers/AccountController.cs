using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EAP_Assignment.App_Start;
using EAP_Assignment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;

namespace EAP_Assignment.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private MyDbContext db = new MyDbContext();
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(string username, string email, string password)
        {
            if (ModelState.IsValid)
            {
                var account = new Account()
                {
                    UserName = username,
                    Email = email
                };

                IdentityResult result = UserManager.Create(account, password);
                Debug.WriteLine("@@@" + result.Succeeded);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(account.Id, "User");

                    return View("Login");
                }
                else
                {
                    Debug.WriteLine("@@@");

                    Debug.WriteLine(JsonConvert.SerializeObject(result.Errors));
                }

            }

            return View("Login");
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            Account user = UserManager.Find(username, password);
            //if (user != null)
            //{
            //    var authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
            //    var userIdentity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            //    authenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity);
            //    //return Redirect("/Home");
            //}
            //else
            //{
            //    Debug.WriteLine("@@@");

            //    Debug.WriteLine(JsonConvert.SerializeObject(HttpNotFound()));
            //}

            if (user == null)
            {
                Debug.WriteLine("@@@");

                Debug.WriteLine(JsonConvert.SerializeObject(HttpNotFound()));
                return HttpNotFound();
            }
            // success
            var ident = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            //use the instance that has been created. 
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignIn(
                new AuthenticationProperties { IsPersistent = false }, ident);

            return Redirect("/Client/Index");
        }

        public ActionResult Logout()
        {
            var authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return View("Login");
        }
    }
}