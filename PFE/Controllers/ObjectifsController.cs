using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFE.Data;
using PFE.Models;

namespace PFE.Controllers
{
    public class ObjectifsController : Controller
    {
        private readonly MyDbContext _db;

        public ObjectifsController(MyDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Objectif> objObjectifList = _db.Objectifs;
            return View(objObjectifList);
        }

        //La barre de recherche des objectifs
        [HttpGet]
        public async Task<IActionResult> Index(string ObjectifSearch)
        {
            ViewData["salam"] = ObjectifSearch;
            var ObjectifQuery = _db.Objectifs.AsQueryable(); // Convertir en IQueryable pour pouvoir ajouter des conditions
            ObjectifQuery = from x in _db.Objectifs select x;

            if (!String.IsNullOrEmpty(ObjectifSearch))
            {
                ObjectifQuery = ObjectifQuery.Where(x => x.Label_Obj.Contains(ObjectifSearch));
            }

            return View(await ObjectifQuery.AsNoTracking().ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Objectif obj)
        {
            if (ModelState.IsValid)
            {
                _db.Objectifs.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Objectif crée avec succèes";
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var ObjFromDatabase = _db.Objectifs.Find(id);
            if (ObjFromDatabase == null)
            {
                return NotFound();
            }
            return View(ObjFromDatabase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Objectif obj)
        {
            if (ModelState.IsValid)
            {
                _db.Objectifs.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Objectif modifié avec succèes";
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var ObjFromDatabase = _db.Objectifs.Find(id);
            if (ObjFromDatabase == null)
            {
                return NotFound();
            }
            return View(ObjFromDatabase);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete_(int? id)
        {
            var ObjFromDatabase = _db.Objectifs.Find(id);
            if (ObjFromDatabase == null)
            {
                return NotFound();
            }
            _db.Objectifs.Remove(ObjFromDatabase);
            _db.SaveChanges();
            TempData["success"] = "Objectif supprimé avec succèes";
            return RedirectToAction("Index");
        }
    }
}
