using Microsoft.AspNetCore.Mvc;
using ShopBookWeb.Data;
using ShopBookWeb.Models;


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

        // Action appelée lorsqu'on ouvre la page de création
        // Affiche simplement le formulaire
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

       
        [ActionName("Create")]
        public IActionResult CreatePOST(Category category)
        {
            _context.Categories.Add(category);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}


