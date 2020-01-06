using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EAP_Assignment.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EAP_Assignment.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManagerRoleController : Controller
    {
        private MyDbContext dbContext = new MyDbContext();
        // GET: Admin/ManageRole
        public ActionResult Index()
        {
            // lấy tất cả các roles hiện có trong bảng dbo.AspNetRoles
            var model = dbContext.Roles.AsEnumerable();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        public ActionResult Create(IdentityRole role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dbContext.Roles.Add(role);
                    dbContext.SaveChanges();
                }

                return Redirect("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View(role);
        }

        public ActionResult Delete(string Id)
        {
            var model = dbContext.Roles.Find(Id);

            return View(model);
        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        [ActionName("Delete")]

        public ActionResult DeleteConfirmed(string Id)
        {
            IdentityRole model = null;

            try
            {
                model = dbContext.Roles.Find(Id);
                dbContext.Roles.Remove(model);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View(model);
        }
    }
}