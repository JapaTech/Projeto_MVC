using EstudoMVC.DataContent;
using EstudoMVC.Interfaces;
using EstudoMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace EstudoMVC.Controllers
{
    public class TouristAttractionController : Controller
    {
        private readonly MVC_DbContext _context;
        private readonly ITouristAttractionService _touristAttractionService;

        public TouristAttractionController(MVC_DbContext context, ITouristAttractionService touristAttractionService)
        {
            _context = context;
            _touristAttractionService = touristAttractionService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<TouristAttraction> touristAttractions = await _touristAttractionService.GetAll();
            return View(touristAttractions);
        }

        
        public async Task<IActionResult> Detail(int id)
        {
            TouristAttraction attraction = await _touristAttractionService.GetByIdAsync(id);
            return View(attraction);
        }
    }
}
