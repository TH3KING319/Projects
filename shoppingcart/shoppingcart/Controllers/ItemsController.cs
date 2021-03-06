﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using shoppingcart.Models;
using System.IO;

namespace shoppingcart.Controllers

{
    public class ItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Requests Items and places them as a list
        public ActionResult Index()
        {
            return View(db.Items.ToList());
        }

        // GET: Items/Details/5  //Name of the action
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        // GET: Items/Create  Submits the data
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "Id,Name,Price,MediaUrl,Description,Created,Updated")] Item item, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                //Check the file name to make sure it is an image
                var ext = Path.GetExtension(image.FileName).ToLower();

                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".gif" && ext != ".bmp")
                    ModelState.AddModelError("image", "Invalid Format.");
            }

            if (ModelState.IsValid)
            {

                if (image != null)

                {
                    //relative server path
                    string filePath = "/img/";
                    //path on physical drive on server
                    var absPath = Server.MapPath("~" + filePath);
                    //media url for relative path
                    item.MediaUrl = filePath + image.FileName;
                    //Save image

                    image.SaveAs(Path.Combine(absPath, image.FileName));
                }

                item.Created = DateTimeOffset.Now;
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // GET: Items/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,MediaUrl,Description,Created,Updated")] Item item, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                //Check the file name to make sure it is an image
                var ext = Path.GetExtension(image.FileName).ToLower();

                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".gif" && ext != ".bmp")
                    ModelState.AddModelError("image", "Invalid Format.");
            }

            if (ModelState.IsValid)
            {

                if (image != null)

                {
                    //relative server path
                    string filePath = "/img/";
                    //path on physical drive on server
                    var absPath = Server.MapPath("~" + filePath);
                    //media url for relative path
                    item.MediaUrl = filePath + image.FileName;
                    //Save image

                    image.SaveAs(Path.Combine(absPath, image.FileName));
                }
                item.Updated = DateTimeOffset.Now;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: Items/Delete/5

        public ActionResult Delete(int? id)

        {

            if (id == null)

            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            Item item = db.Items.Find(id);

            if (item == null)

            {

                return HttpNotFound();

            }

            return View(item);

        }

        // POST: Items/Delete/5

        [HttpPost, ActionName("Delete")]

        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)

        {

            Item item = db.Items.Find(id);

            db.Items.Remove(item);

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


