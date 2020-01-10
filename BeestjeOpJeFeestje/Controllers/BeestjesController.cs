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
        [HttpGet]
        public ActionResult Index()
        {
            List<BeestjeVM> beestList = new List<BeestjeVM>();
            foreach (Beestje b in beestjesRepository.GetBeestjes())
                beestList.Add(new BeestjeVM { Beest = b });
            return View(beestList);
        }

        // GET: Beestjes/Details/5
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
            return View(beestjeVM);
        }

        // GET: Beestjes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Beestjes/Create
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

        // GET: Beestjes/Edit/5
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

        // POST: Beestjes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Beestjes/Delete/5
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

        // POST: Beestjes/Delete/5
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
