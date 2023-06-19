using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PFE.Data;
using PFE.Models;

namespace PFE.Controllers
{
    public class ProjetsController : Controller
    {
        private readonly MyDbContext _context;

        public ProjetsController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Projet> objProjetList = _context.Projets;
            return View(objProjetList);
        }



        //La barre de recherche des objectifs
        [HttpGet]
        public async Task<IActionResult> Index(string ProjetSearch)
        {
            ViewData["salam"] = ProjetSearch;
            var ProjetQuery = _context.Projets.AsQueryable(); // Convertir en IQueryable pour pouvoir ajouter des conditions
            ProjetQuery = from x in _context.Projets select x;

            if (!String.IsNullOrEmpty(ProjetSearch))
            {
                ProjetQuery = ProjetQuery.Where(x => x.Nom_Projet.Contains(ProjetSearch) || x.Membre_Equipe.Contains(ProjetSearch));
            }

            return View(await ProjetQuery.AsNoTracking().ToListAsync());
        }

        public IActionResult page1()
        {
            List<ProjetViewModel> projets = new List<ProjetViewModel>();

            var collabProjets = _context.CollabProjets.ToList();
            var projetIDs = collabProjets.Select(cf => cf.Id).Distinct().ToList();

            foreach (var projetId in projetIDs)
            {
                var ProjetName = _context.Projets.FirstOrDefault(f => f.Id == projetId)?.Nom_Projet;
                var collaboratorIds = collabProjets.Where(cf => cf.Id == projetId).Select(cf => cf.id_Collab);

                var collaboratorNames = _context.Collabs
                    .Where(c => collaboratorIds.Contains(c.id_Collab))
                    .Select(c => c.Nom)
                    .ToList();
                var collaboratorPrenom = _context.Collabs
                    .Where(c => collaboratorIds.Contains(c.id_Collab))
                    .Select(c => c.Prenom)
                    .ToList();
                projets.Add(new ProjetViewModel
                {
                    ProjetName = ProjetName,
                    CollaboratorNames = collaboratorNames,
                    CollaboratorPrenom = collaboratorPrenom
                });
            }

            return View(projets);
        }

        //GetActionMethod
        public IActionResult Create()
        {
            return View();
        }

        //Post ActionMethod
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Projet obj)
        {
            if(ModelState.IsValid)
            {
                _context.Projets.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
            
        }




        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projets == null)
            {
                return NotFound();
            }

            var projet = await _context.Projets.FindAsync(id);
            if (projet == null)
            {
                return NotFound();
            }

            return View(projet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom_Projet,Description,Membre_Equipe,Date_Debut")] Projet projet)
        {
            if (id != projet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjetExists(projet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(projet);
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projets == null)
            {
                return NotFound();
            }

            var projet = await _context.Projets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projet == null)
            {
                return NotFound();
            }

            return View(projet);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projets == null)
            {
                return Problem("Entity set 'MyDbContext.Projets' is null.");
            }

            var projet = await _context.Projets.FindAsync(id);
            if (projet != null)
            {
                _context.Projets.Remove(projet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool ProjetExists(int id)
        {
            return (_context.Projets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

