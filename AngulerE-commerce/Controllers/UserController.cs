using Microsoft.AspNetCore.Mvc;

namespace AngulerE_commerce.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
