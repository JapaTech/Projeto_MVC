using EstudoMVC.DataContent;
using EstudoMVC.Interfaces;
using EstudoMVC.Models;
using EstudoMVC.ViewModels;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TouristAttraction touristAttraction)
        {
            if (!ModelState.IsValid)
            {
                return View(touristAttraction);
            }
            _touristAttractionService.Add(touristAttraction);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var attraction = await _touristAttractionService.GetByIdAsync(id);
            if (attraction == null) return View("Error");
            var attractionVM = new EditAttractionViewModel
            {
                Id = attraction.Id,
                Name = attraction.Name,
                Description = attraction.Description,
                Image = attraction.Image,
            };

            return View (attractionVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditAttractionViewModel editAttractionVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", editAttractionVM);
            }

            var currentAtrraction = await _touristAttractionService.GetByIdAsyncNoTracking(id);

            if(currentAtrraction != null)
            {
                var newAttraction = new TouristAttraction
                {
                    Id = id,
                    Name = editAttractionVM.Name,
                    Description = editAttractionVM.Description,
                    Image = editAttractionVM.Image,
                    ReviewsAvg = editAttractionVM.ReviewsAvg,
                };
                _touristAttractionService.Update(newAttraction);
                return RedirectToAction("Index");
            }
            else
            {
                return View(editAttractionVM);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var touristAttraction = await _touristAttractionService.GetByIdAsync(id);
            if (touristAttraction == null) 
            {
                return View("Error");
            }
            return View(touristAttraction);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteTouristAttraction(int id)
        {
            var touristAttraction = await _touristAttractionService.GetByIdAsync(id);
            if (touristAttraction == null)
            {
                return View("Error");
            }
            _touristAttractionService.Delete(touristAttraction);
            return RedirectToAction("Index");
        }
    }
}
