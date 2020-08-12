using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopBridgee.Models;
using System.IO;

namespace ShopBridgee.Controllers
{
    public class SalesController : Controller
    {
        private ShopBridge db = new ShopBridge();

        // GET: Sales
        public async Task<ActionResult> Index()
        {
            return View(await db.Sales.ToListAsync());
        }

        // GET: Sales/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = await db.Sales.FindAsync(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        public bool ValidateFile(HttpPostedFileBase file)
        {
            switch (file.ContentType)
            {
                // Example: return valid = true for following file types:
                case ("image.gif"):
                case ("image/jpg"):
                case ("image/png"):
                    return true;

                // Otherwise if anything else, return false
                default: return false;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,Price")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                var postedFile = Request.Files["fileUpload"];
                var fileIsValid = ValidateFile(postedFile);
                byte[] bytes;
                using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                {
                    bytes = br.ReadBytes(postedFile.ContentLength);
                }
                sale.Image = bytes;
                //byte[] data = new byte[IG.File.ContentLength];
                //entities.tblFiles.Add(new tblFile
                //{
                //    Name = Path.GetFileName(postedFile.FileName),
                //    ContentType = postedFile.ContentType,
                //    Data = bytes
                //});

                db.Sales.Add(sale);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sale);
        }

        // GET: Sales/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = await db.Sales.FindAsync(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,Price")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                var postedFile = Request.Files["fileUpload"];
                var fileIsValid = ValidateFile(postedFile);
                byte[] bytes;
                using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                {
                    bytes = br.ReadBytes(postedFile.ContentLength);
                }
                sale.Image = bytes;
                db.Entry(sale).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sale);
        }

        // GET: Sales/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = await db.Sales.FindAsync(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Sale sale = await db.Sales.FindAsync(id);
            db.Sales.Remove(sale);
            await db.SaveChangesAsync();
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
