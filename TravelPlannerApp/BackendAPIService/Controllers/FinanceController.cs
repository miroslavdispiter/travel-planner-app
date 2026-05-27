using Microsoft.AspNetCore.Mvc;

namespace BackendAPIService.Controllers
{
    public class FinanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
