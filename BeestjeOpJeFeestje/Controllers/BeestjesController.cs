using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Repos;
using BeestjeOpJeFeestje.ViewModels;

namespace BeestjeOpJeFeestje.Controllers
{
    public class BeestjesController : Controller
    {
        private readonly IBeestjesRepository beestjesRepository;

        public BeestjesController(IBeestjesRepository repo)
        {
            beestjesRepository = repo;
        }

        // Shows all the animals in the database
        [HttpGet]
        public ActionResult Index()
        {
            List<BeestjeVM> beestList = new List<BeestjeVM>();
            foreach (Beestje b in beestjesRepository.GetBeestjes())
                beestList.Add(new BeestjeVM { Beest = b });
            return View(beestList);
        }

        // Shows the details of an animal
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Error");
            }
            Beestje beestje = beestjesRepository.GetBeestjeById(id);
            if (beestje == null)
            {
                return HttpNotFound();
            }
            BeestjeVM beestjeVM = new BeestjeVM();
            beestjeVM.Beest = beestje;
            beestjeVM.AccessoiresList = beestjesRepository.GetAccessoiresById(beestjeVM.Id);
            beestjeVM.AllBoekingen = beestjesRepository.GetBoekingenFromBeestje(beestjeVM);
            return View(beestjeVM);
        }

        //Shows the page to create an animal
        public ActionResult Create()
        {
            return View();
        }

        //Validate the user input and then create the animal in the database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Type,Price,imagePath")] BeestjeVM beestje)
        {
            if (ModelState.IsValid)
            {
                string[] validTypes = new string[] { "Woestijn", "Boerderij", "Sneeuw", "Jungle" };
                if (!validTypes.Contains(beestje.Type))
                {
                    ViewBag.Error ="Kies uit de types Woestijn, Boerderij, Sneeuw of Jungle.";
                    return View();
                }
                beestjesRepository.AddBeestje(beestje);
                return RedirectToAction("Index", "Beestjes");
            }
            return View(beestje);
        }

        // Get an animal and show a page to edit the animal
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Error");
            }
            Beestje beestje = beestjesRepository.GetBeestjeById(id);
            if (beestje == null)
            {
                return HttpNotFound();
            }
            BeestjeVM beestjeVM = new BeestjeVM();
            beestjeVM.Beest = beestje;
            return View(beestjeVM);
        }

        // Validate the user input and save the changes to the animal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Type,Price,imagePath")] BeestjeVM beestje)
        {
            if (ModelState.IsValid)
            {
                string[] validTypes = new string[] { "Woestijn", "Boerderij", "Sneeuw", "Jungle" };
                if (!validTypes.Contains(beestje.Type))
                {
                    ViewBag.Error = "Kies uit de types Woestijn, Boerderij, Sneeuw of Jungle.";
                    return View();
                }
                beestjesRepository.EditBeestje(beestje);
                return RedirectToAction("Index", "Beestjes");
            }
            return View(beestje);
        }

        //Get the selected animal and show the delete page
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Error");
            }
            Beestje beestje = beestjesRepository.GetBeestjeById(id);
            if (beestje == null)
            {
                return HttpNotFound();
            }
            return View(beestje);
        }

        //delete the selected animal after the user confirms
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Beestje beestje = beestjesRepository.GetBeestjeById(id);
            beestjesRepository.DeleteBeestje(beestje);
            return RedirectToAction("Index", "Beestjes");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                beestjesRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
