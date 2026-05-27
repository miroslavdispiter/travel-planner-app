using Microsoft.AspNetCore.Mvc;

namespace BackendAPIService.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
