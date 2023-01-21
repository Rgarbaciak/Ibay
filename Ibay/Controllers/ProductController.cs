using Microsoft.AspNetCore.Mvc;

namespace Ibay.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
