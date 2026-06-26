using Microsoft.AspNetCore.Mvc;

namespace ShopBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
