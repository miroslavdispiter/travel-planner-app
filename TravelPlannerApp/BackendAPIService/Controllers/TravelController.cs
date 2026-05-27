using Microsoft.AspNetCore.Mvc;

namespace BackendAPIService.Controllers
{
    public class TravelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
