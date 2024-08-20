using EstudoMVC.DataContent;
using EstudoMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace EstudoMVC.Controllers
{
    public class TouristAttractionController : Controller
    {
        private readonly MVC_DbContext _context;

        public TouristAttractionController(MVC_DbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<TouristAttraction> touristAttractions = _context.TouristAttractions.ToList();
            return View(touristAttractions);
        }
    }
}
