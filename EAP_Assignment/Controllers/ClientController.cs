using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EAP_Assignment.Models;

namespace EAP_Assignment.Controllers
{
    public class ClientController : Controller
    {
        private MyDbContext db = new MyDbContext();
        // GET: Client
        public ActionResult ByCategory(string id)
        {
            ViewData["category"] = db.Markets.Find(id);
            var listCoins = db.Coins.Where(s => s.MarketId == id).ToList(); // lọc theo category
            return View("Index", listCoins);
        }
        // GET: Coins
        public ActionResult Index(string market, string searchKeyword, string currentFilter)
        {
            if (searchKeyword == null)
            {
                searchKeyword = currentFilter;
            }


            ViewBag.CurrentFilter = searchKeyword;

            var coins = from c in db.Coins select c;
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                coins = coins.Where(s => s.Name.Contains(searchKeyword) && s.MarketId.Contains(market));
            }

            return View(coins.ToList());
        }
    }
}