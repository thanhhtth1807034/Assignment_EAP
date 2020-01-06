using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EAP_Assignment.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace EAP_Assignment.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManagerUserController : Controller
    {
        private MyDbContext dbContext = new MyDbContext();
        // GET: Admin/ManageUser
        public ActionResult Index(string sortOrder, string searchKeyword, string currentFilter, int? page, string Id)
        {
            IEnumerable<Account> model = dbContext.Users.AsEnumerable();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.EmailSortParm = string.IsNullOrEmpty(sortOrder) ? "email_desc" : "";

            if (searchKeyword != null)
            {
                page = 1;
            }
            else
            {
                searchKeyword = currentFilter;
            }

            ViewBag.CurrentFilter = searchKeyword;

            if (!string.IsNullOrEmpty(searchKeyword))
            {
                model = from s in dbContext.Users.Where(s => s.UserName.Contains(searchKeyword) || s.Email.Contains(searchKeyword)) select s;
            }

            switch (sortOrder)
            {
                case "name_desc":
                    model = model.OrderByDescending(s => s.UserName);
                    break;
                case "email_desc":
                    model = model.OrderByDescending(s => s.Email);
                    break;
                default:
                    model = model.OrderBy(s => s.UserName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);


            return View(model.ToPagedList(pageNumber, pageSize));
            //return View(model);
        }

        public ActionResult Edit(string Id)
        {
            Account model = dbContext.Users.Find(Id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(Account modelAccount)
        {
            try
            {
                dbContext.Entry(modelAccount).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);

                return View(modelAccount);
            }
        }

        public ActionResult EditRole(string Id)
        {
            Account model = dbContext.Users.Find(Id);
            ViewData["RoleId"] =
                new SelectList(
                    dbContext.Roles.ToList().Where(item => model.Roles.FirstOrDefault(r => r.RoleId == item.Id) == null)
                        .ToList(), "Id", "Name");

            return View(model);
        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        public ActionResult AddToRole(string UserId, string[] RoleId)
        {
            Account model = dbContext.Users.Find(UserId);
            if (RoleId != null && RoleId.Count() > 0)
            {
                foreach (string item in RoleId)
                {
                    IdentityRole role = dbContext.Roles.Find(RoleId);
                    model.Roles.Add(new IdentityUserRole() { UserId = UserId, RoleId = item });
                }

                dbContext.SaveChanges();
            }

            ViewBag.RoleId =
                new SelectList(
                    dbContext.Roles.ToList().Where(item => model.Roles.FirstOrDefault(r => r.RoleId == item.Id) == null)
                        .ToList(), "Id", "Name");

            return RedirectToAction("EditRole", new { Id = UserId });

        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        public ActionResult DeleteRoleFromUser(string UserId, string RoleId)

        {

            Account model = dbContext.Users.Find(UserId);

            model.Roles.Remove(model.Roles.Single(m => m.RoleId == RoleId));

            dbContext.SaveChanges();

            ViewBag.RoleId = new SelectList(dbContext.Roles.ToList().Where(item => model.Roles.FirstOrDefault(r => r.RoleId == item.Id) == null).ToList(), "Id", "Name");

            return RedirectToAction("EditRole", new { Id = UserId });

        }

        public ActionResult Delete(string Id)

        {

            var model = dbContext.Users.Find(Id);

            return View(model);

        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        [ActionName("Delete")]

        public ActionResult DeleteConfirmed(string Id)

        {

            Account model = null;

            try

            {

                model = dbContext.Users.Find(Id);

                dbContext.Users.Remove(model);

                dbContext.SaveChanges();

                return RedirectToAction("Index");

            }

            catch (Exception ex)

            {

                ModelState.AddModelError("", ex.Message);

                return View("Delete", model);

            }

        }
    }
}