using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhotoGallery.DAL;
using PhotoGallery.Models;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

namespace PhotoGallery.Controllers
{
    public class PhotosController : Controller
    {
        private GalleryContext db = new GalleryContext();

        // GET: Photos
        public ActionResult Index(IEnumerable<Photo> photos)
        {
            if(photos==null)
                return Index(10, 1);
            return View(photos);
        }
        private int PageNumber = 0;
        //POST: /Index
        [HttpPost]
        public ActionResult Index(int PageSize, int PageNumber)
        {
            var page = 1;
            var photos = db.Photos.ToList();
            if (PageNumber != 0)
                page = PageNumber;
            ViewBag.totalpages = Math.Ceiling((double)photos.Count() / PageSize);
            ViewBag.currentpage = page;
            ViewBag.pagesize = PageSize;
            var photolist = photos.Skip((page - 1) * PageSize).Take(PageSize);
            return Index(photolist);
        }
        public ActionResult Pagination(int PageSize, int PageNumber)
        {
            var page = 1;
            var photos = db.Photos.ToList();
            if (PageNumber != 0)
                page = PageNumber;
            ViewBag.totalpages = Math.Ceiling((double)photos.Count() / PageSize);
            ViewBag.currentpage = page;
            ViewBag.pagesize = PageSize;
            var photolist = photos.Skip((page - 1) * PageSize).Take(PageSize);
            return Index(photolist);
        }

        // GET: Photos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: Photos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Photos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Name,Description,Path")] Photo photo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Photos.Add(photo);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(photo);
        //}

        // GET: Photos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Path")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(photo);
        }

        // GET: Photos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = db.Photos.Find(id);
            db.Photos.Remove(photo);
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
        //post photos/saveImage
        [HttpPost]
        public ActionResult SaveImage(string description,string name)
        {
            try
            {
                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var pic = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
                    HttpPostedFileBase filebase = new HttpPostedFileWrapper(pic);
                   // var fileName = Path.GetFileName(filebase.FileName);
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(filebase.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    filebase.SaveAs(path);
                    //var db = new GalleryContext();
                    db.Photos.Add(new Photo { Name = name, Description = description, Path = path });
                    db.SaveChanges();
                    return Json("Photo was saved.");
                }
                else { return Json("No File Saved."); }
            }
            catch (Exception ex) { return Json("Error While Saving."); }
        }
        // post /PageSize
        //[HttpPost]
        //public ActionResult PageSize(int PageSize,int PageNumber)
        //{
        //    var page = 1;
        //    var photos = db.Photos.ToList();
        //    if (PageNumber != 0)
        //        page = PageNumber;
        //    var TotalPages = Math.Ceiling((double)photos.Count() / PageSize);
        //    var photolist = photos.Skip((page - 1) * PageSize).Take(PageSize);

        //    return View(Index(photolist));
        //}
    }
}
