using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Repos;
using BeestjeOpJeFeestje.ViewModels;

namespace BeestjeOpJeFeestje.Controllers
{
    public class AccessoiresController : Controller
    {
        private readonly IAccessoiresRepository accessoiresRepository;

        public AccessoiresController(IAccessoiresRepository repo)
        {
            accessoiresRepository = repo;
        }

        // Shows all the accessoires in the database
        public ActionResult Index()
        {
            List<AccessoireVM> accessoiresList = new List<AccessoireVM>();
            foreach (Accessoires a in accessoiresRepository.GetAccessoires())
                accessoiresList.Add(new AccessoireVM {Accessoire = a});
            return View(accessoiresList);
        }

        // Shows the details of an accessoire
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Error");
            }
            Accessoires accessoires = accessoiresRepository.GetAccessoireById(id);
            if (accessoires == null)
            {
                return HttpNotFound();
            }
            AccessoireVM accessoireVM = new AccessoireVM();
            accessoireVM.Accessoire = accessoires;
            return View(accessoireVM);
        }

        //Shows the page to create an accessoire, get a list of all the animals to select from
        public ActionResult Create()
        {
            var accessoireVM = new AccessoireVM();
            accessoireVM.BeestjesLijst = accessoiresRepository.GetBeestjes();

            return View(accessoireVM);
        }

        //Validate the user input and then create the accessoire in the database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,ImagePath,SelectedBeestjesId")] AccessoireVM accessoires)
        {
            if (ModelState.IsValid)
            {
                accessoiresRepository.CreateAccessoire(accessoires);
                return RedirectToAction("Index", "Accessoires");
            }

            return View(accessoires);
        }

        // Get an accessoire and show a page to edit the accessoire
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Error");
            }
            Accessoires accessoires = accessoiresRepository.GetAccessoireById(id);
            if (accessoires == null)
            {
                return HttpNotFound();
            }
            AccessoireVM accessoireVM = new AccessoireVM();
            accessoireVM.Accessoire = accessoires;
            accessoireVM.BeestjesLijst = accessoiresRepository.GetBeestjes();
            return View(accessoireVM);
        }

        // Validate the user input and save the changes to the accessoire
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,ImagePath,SelectedBeestjesId")] AccessoireVM model)
        {
            if (ModelState.IsValid)
            {
                accessoiresRepository.EditAccessoire(model);
                return RedirectToAction("Index", "Accessoires");
            }
            return View(model);
        }

        //Get the selected accessoire and show the delete page
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Error");
            }
            Accessoires accessoires = accessoiresRepository.GetAccessoireById(id);
            if (accessoires == null)
            {
                return HttpNotFound();
            }
            return View(accessoires);
        }

        //Delete the accessoire after the user confirms this
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accessoires accessoires = accessoiresRepository.GetAccessoireById(id);
            accessoiresRepository.DeleteAccessoire(accessoires);
            return RedirectToAction("Index", "Accessoires");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                accessoiresRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
