using Microsoft.AspNetCore.Mvc;

namespace EstudoMVC.Controllers
{
    public class TouristAttractionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
