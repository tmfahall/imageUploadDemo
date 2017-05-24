using imageUploadDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace imageUploadDemo.Controllers
{
    public class ImageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Create()
        {
            return View(new ImageViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ImageViewModel model)
        {
            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/jpg",
                "image/png"
            };

            if (model.ImageUpload == null || model.ImageUpload.ContentLength == 0)
            {
                ModelState.AddModelError("ImageUpload", "This field is required");
            }
            else if (!validImageTypes.Contains(model.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Please choose either a GIF JPG or PNG image.");
            }

            if (ModelState.IsValid)
            {
                var image = new Image
                {
                    Title = model.Title,
                    AltText = model.AltText,
                    Caption = model.Caption
                };

                if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                {
                    var uploadDir = "~/uploads";
                    var imagePath = System.IO.Path.Combine(Server.MapPath(uploadDir), model.ImageUpload.FileName);
                    var imageUrl = System.IO.Path.Combine(uploadDir, model.ImageUpload.FileName);

                    model.ImageUpload.SaveAs(imagePath);
                    image.ImageUrl = imageUrl;
                }

                db.Create(image);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);
        }
    }
}