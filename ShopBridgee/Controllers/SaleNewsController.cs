using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopBridgee.Models;

namespace ShopBridgee.Controllers
{
    public class SaleNewsController : Controller
    {
        private ShopBridge db = new ShopBridge();

        // GET: SaleNews
        public ActionResult Index()
        {
            return View(db.SaleNews.ToList());
        }

        // GET: SaleNews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleNew saleNew = db.SaleNews.Find(id);
            if (saleNew == null)
            {
                return HttpNotFound();
            }
            return View(saleNew);
        }

        // GET: SaleNews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SaleNews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,Image")] SaleNew saleNew)
        {
            if (ModelState.IsValid)
            {
                db.SaleNews.Add(saleNew);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(saleNew);
        }

        // GET: SaleNews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleNew saleNew = db.SaleNews.Find(id);
            if (saleNew == null)
            {
                return HttpNotFound();
            }
            return View(saleNew);
        }

        // POST: SaleNews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price,Image")] SaleNew saleNew)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saleNew).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(saleNew);
        }

        // GET: SaleNews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleNew saleNew = db.SaleNews.Find(id);
            if (saleNew == null)
            {
                return HttpNotFound();
            }
            return View(saleNew);
        }

        // POST: SaleNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SaleNew saleNew = db.SaleNews.Find(id);
            db.SaleNews.Remove(saleNew);
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
