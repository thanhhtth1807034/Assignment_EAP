using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EAP_Assignment.Models;

namespace EAP_Assignment.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CoinsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        public ActionResult ByCategory(string id)
        {
            ViewData["category"] = db.Markets.Find(id);
            var listCoins = db.Coins.Where(s => s.MarketId == id).ToList(); // lọc theo category
            return View("Index", listCoins);
        }
        // GET: Coins
        public ActionResult Index(string market, string searchKeyword, string currentFilter)
        {
            var coins = db.Coins.Include(c => c.Market);

            if (searchKeyword == null)
            {
                searchKeyword = currentFilter;
            }
            

            ViewBag.CurrentFilter = searchKeyword;

            coins = from c in db.Coins select c;
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                coins = coins.Where(s => s.Name.Contains(searchKeyword) && s.MarketId.Contains(market));
            }

            return View(coins.ToList());
        }

        // GET: Coins/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coin coin = db.Coins.Find(id);
            if (coin == null)
            {
                return HttpNotFound();
            }
            return View(coin);
        }

        // GET: Coins/Create
        public ActionResult Create()
        {
            ViewBag.MarketId = new SelectList(db.Markets, "Id", "Name");
            return View();
        }

        // POST: Coins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,BaseAsset,QuoteAsset,LastPrice,Volum24h,MarketId,CreatedAt,UpdatedAd,Status")] Coin coin)
        {
            if (ModelState.IsValid)
            {
                db.Coins.Add(coin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MarketId = new SelectList(db.Markets, "Id", "Name", coin.MarketId);
            return View(coin);
        }

        // GET: Coins/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coin coin = db.Coins.Find(id);
            if (coin == null)
            {
                return HttpNotFound();
            }
            ViewBag.MarketId = new SelectList(db.Markets, "Id", "Name", coin.MarketId);
            return View(coin);
        }

        // POST: Coins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,BaseAsset,QuoteAsset,LastPrice,Volum24h,MarketId,CreatedAt,UpdatedAd,Status")] Coin coin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MarketId = new SelectList(db.Markets, "Id", "Name", coin.MarketId);
            return View(coin);
        }

        // GET: Coins/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coin coin = db.Coins.Find(id);
            if (coin == null)
            {
                return HttpNotFound();
            }
            return View(coin);
        }

        // POST: Coins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Coin coin = db.Coins.Find(id);
            db.Coins.Remove(coin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
