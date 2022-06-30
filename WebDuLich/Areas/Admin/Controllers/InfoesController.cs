using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebDuLich.Models.Entities;

namespace WebDuLich.Areas.Admin.Controllers
{
    public class InfoesController : Controller
    {
        private WebDuLich_ConText db = new WebDuLich_ConText();

        // GET: Admin/Infoes
        public ActionResult Index()
        {
            var infoes = db.Infoes.Include(i => i.Country);
            return View(infoes.ToList());
        }

        // GET: Admin/Infoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Info info = db.Infoes.Find(id);
            if (info == null)
            {
                return HttpNotFound();
            }
            return View(info);
        }

        // GET: Admin/Infoes/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Admin/Infoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public ActionResult Create( Info info)
        {
            if (info.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(info.ImageUpload.FileName);
                string extension = Path.GetExtension(info.ImageUpload.FileName);
                fileName = fileName + extension;
                info.Image = "/Content/image/" + fileName;
                info.ImageUpload.SaveAs(Server.MapPath("/Content/image/") + fileName);
            }
            db.Infoes.Add(info);
            db.SaveChanges();
            return View(info);
        }

        // GET: Admin/Infoes/Edit/5
        public ActionResult Edit(int? id)
        {
            return View(db.Infoes.Where(s => s.Id == id).FirstOrDefault());
        }
        // POST: Admin/Infoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public ActionResult Edit(/*[Bind(Include = "Id,Name,CountryId,UnitPrice,Image,Description")]*/int id, Info info)
        {


            if (ModelState.IsValid)
            {
                if (info.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(info.ImageUpload.FileName);
                    string extension = Path.GetExtension(info.ImageUpload.FileName);
                    fileName = fileName + extension;
                    info.Image = "/Content/image/" + fileName;
                    info.ImageUpload.SaveAs(Server.MapPath("/Content/image/") + fileName);
                }
                db.Entry(info).State = EntityState.Modified;
                db.SaveChanges();
                return View(info);
            }
            return View(info);

        }

        // GET: Admin/Infoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Info info = db.Infoes.Find(id);
            if (info == null)
            {
                return HttpNotFound();
            }
            return View(info);
        }

        // POST: Admin/Infoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Info info = db.Infoes.Find(id);
            db.Infoes.Remove(info);
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
