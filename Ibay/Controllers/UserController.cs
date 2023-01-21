using Microsoft.AspNetCore.Mvc;

namespace Ibay.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
