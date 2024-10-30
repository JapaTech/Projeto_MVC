using EstudoMVC.DataContent;
using EstudoMVC.DataContent.Enum;
using EstudoMVC.Interfaces;
using EstudoMVC.Models;
using EstudoMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


            var viewModel = touristAttractions.Select(attraction => new AttractionViewModel
            {
                Id = attraction.Id,
                Name = attraction.Name,
                Description = attraction.Description,
                Image = attraction.Image,
                ReviewsAvg = attraction.Reviews.Any() ? attraction.Reviews.Average(r => (float)r.Score) : 0
            }).ToList();

            return View(viewModel);
        }

        
        public async Task<IActionResult> Detail(int id)
        {
            TouristAttraction attraction = await _context.TouristAttractions.
                Include(r => r.Reviews). 
                FirstOrDefaultAsync(t => t.Id == id);

            if (attraction != null && attraction.Reviews != null)
            {
                var userIds = attraction.Reviews.Select(r => r.UserId).Distinct().ToList();
                var users = await _context.Users.Where(u => userIds.Contains(u.Id)).ToDictionaryAsync(u => u.Id);

                foreach (var review in attraction.Reviews)
                {
                    if (review.UserId != null && users.TryGetValue(review.UserId, out var user))
                    {
                        review.User = user;
                    }
                }
            }

            float avgScore = attraction.Reviews.Any() ? attraction.Reviews.Average(r => (float)r.Score) : 0;

            TouristAttractionReviewViewModel touristAttractionReviewVM = new TouristAttractionReviewViewModel
            {
                Attraction = attraction,
                //TouristAttractionId = attraction.Id,
                Reviews = attraction.Reviews.OrderByDescending(r => r.CreationDate).ToList(),
                Review = new ReviewViewModel(),
                ReviesAvg = avgScore
            };
            return View(touristAttractionReviewVM);
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
        public async Task<IActionResult> DeleteReview(int id)
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
