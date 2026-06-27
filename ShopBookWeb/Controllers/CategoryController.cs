using Microsoft.AspNetCore.Mvc;
using ShopBook.Models;
using ShopBookWeb.Data;



// Espace de noms contenant les contrôleurs de l'application
namespace ShopBookWeb.Controllers
{
    // Contrôleur responsable de la gestion des catégories
    public class CategoryController : Controller
    {
        // Contexte de la base de données (Entity Framework)
        private readonly ApplicationDbContext _context;

        // Constructeur : reçoit le contexte de la base de données
        // grâce à l'injection de dépendances
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Action qui affiche la liste des catégories
        public IActionResult Index()
        {
            // Récupère toutes les catégories de la base de données
            var categories = _context.Categories.ToList();

            // Envoie la liste des catégories à la vue "Index"
            return View("Index", categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public IActionResult CreatePOST(Category category)
        {

            if (!String.IsNullOrEmpty(category.Name) && _context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower()))
            {
                ModelState.AddModelError("", "Category name already exists!");
            }


            if (ModelState.IsValid) 
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Update(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            var category = _context.Categories.Find(id);
            if (category== null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Update")]
        public IActionResult UpdatePOST(Category category)
        {
            if (!String.IsNullOrEmpty(category.Name) && 
                _context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower() && c.Id != category.Id))
            {
                ModelState.AddModelError("", "Category name already exists!");
            }


            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeletePOST(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}


