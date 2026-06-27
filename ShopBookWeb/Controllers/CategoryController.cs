using Microsoft.AspNetCore.Mvc;
using ShopBook.Business.Services.IServices;
using ShopBook.Models;
using ShopBookWeb.Data;



// Espace de noms contenant les contrôleurs de l'application
namespace ShopBookWeb.Controllers
{
    // Contrôleur responsable de la gestion des catégories
    public class CategoryController : Controller
    {
        // Contexte de la base de données (Entity Framework)
        private readonly ICategoryService _categoryService;

        // Constructeur : reçoit le contexte de la base de données
        // grâce à l'injection de dépendances
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Action qui affiche la liste des catégories
        public async Task<IActionResult> Index()
        {
            var categories = _categoryService.GetAllCategoriesAsync();
            return View("Index", categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePOST(Category category)
        {

            if (!String.IsNullOrEmpty(category.Name) && await _categoryService.IsCategoryNameUniqueAsync(category.Name))
            {
                ModelState.AddModelError("", "Category name already exists!");
            }


            if (ModelState.IsValid) 
            {
                await _categoryService.CreateCategoryAsync(category);
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Update(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            var category = _categoryService.GetCategoryByIdAsync(id.Value);
            if (category== null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Update")]
        public async Task<IActionResult> UpdatePOST(Category category)
        {
            if (!String.IsNullOrEmpty(category.Name) &&
               await _categoryService.IsCategoryNameUniqueAsync(category.Name, category.Id))
            {
                ModelState.AddModelError("", "Category name already exists!");
            }


            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategoryAsync(category);
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

            var category = _categoryService.GetCategoryByIdAsync(id.Value);
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
            _categoryService.DeleteCategoryAsync(id);
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}


