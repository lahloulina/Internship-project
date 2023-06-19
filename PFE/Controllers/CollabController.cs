using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFE.Data;
using PFE.Models;
using System.Linq;

namespace PFE.Controllers
{
    public class CollabController : Controller
    {
        private readonly MyDbContext _db;

        public CollabController(MyDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Collab> objCollabList = _db.Collabs;
            return View(objCollabList);
        }

        //la barre de recherche

        [HttpGet]
        public async Task<IActionResult> Index(string Collabsearch)
        {
            ViewData["salam"] = Collabsearch;
            var CollabQuery = _db.Collabs.AsQueryable(); // Convertir en IQueryable pour pouvoir ajouter des conditions
            CollabQuery = from x in _db.Collabs select x;

            if (!String.IsNullOrEmpty(Collabsearch))
            {
                CollabQuery = CollabQuery.Where(x => x.Nom.Contains(Collabsearch) || x.Prenom.Contains(Collabsearch) || x.Matricule.Contains(Collabsearch));
            }

            return View(await CollabQuery.AsNoTracking().ToListAsync());
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Collab obj)
        {
            if (ModelState.IsValid)
            {
                _db.Collabs.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Collaborateur crée avec succèes";
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
            var CollabFromDatabase = _db.Collabs.Find(id);
            if (CollabFromDatabase == null)
            {
                return NotFound();
            }
            return View(CollabFromDatabase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Collab obj)
        {
            if (ModelState.IsValid)
            {
                _db.Collabs.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Collaborateur modifié avec succèes";
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CollabFromDatabase = _db.Collabs.Find(id);
            if (CollabFromDatabase == null)
            {
                return NotFound();
            }
            return View(CollabFromDatabase);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete_(int? id)
        {
            var CollabFromDatabase = _db.Collabs.Find(id);
            if (CollabFromDatabase == null)
            {
                return NotFound();
            }
            _db.Collabs.Remove(CollabFromDatabase);
            _db.SaveChanges();
            TempData["success"] = "Collaborateur supprimé avec succèes";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AssignProjet(string projetName, List<int> selectedCollaborateurs)
        {
            // Validate the projetName and selectedCollaborateurs
            if (string.IsNullOrEmpty(projetName) || selectedCollaborateurs == null || selectedCollaborateurs.Count == 0)
            {
                return BadRequest("Projet ou collaborateur sélectionnés non trouvés.");
            }

            // Retrieve the projet from the database by name
            var projet = _db.Projets.FirstOrDefault(p => p.Nom_Projet == projetName);
            if (projet == null)
            {
                return NotFound("Projet non trouvé.");
            }

            // Get the existing collaborators for the projet
            var existingCollaborators = _db.CollabProjets.Where(cp => cp.Id == projet.Id).Select(cp => cp.id_Collab).ToList();

            // Remove any collaborators that are already assigned to the projet
            var newCollaborators = selectedCollaborateurs.Where(c => !existingCollaborators.Contains(c)).ToList();

            // Iterate over the new collaborators and save the assignments
            foreach (var collaborateurId in newCollaborators)
            {
                var collaborateur = _db.Collabs.FirstOrDefault(c => c.id_Collab == collaborateurId);
                if (collaborateur != null)
                {
                    // Create a new CollabProjet instance
                    var collabProjet = new CollabProjet
                    {
                        id_Collab = collaborateurId,
                        Id = projet.Id,
                    };

                    // Add the CollabProjet to the database
                    _db.CollabProjets.Add(collabProjet);
                }
            }

            // Save the changes to the database
            _db.SaveChanges();

            // Redirect to the Collaborateurs Index after assigning the projet
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetProjetNames()
        {
            var projetNames = _db.Projets.Select(p => p.Nom_Projet).ToList();
            return Json(projetNames);
        }

    }
}
