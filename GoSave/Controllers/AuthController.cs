using Microsoft.AspNetCore.Mvc;

namespace GoSave.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
