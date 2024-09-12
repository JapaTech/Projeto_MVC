using EstudoMVC.DataContent;
using EstudoMVC.Interfaces;
using EstudoMVC.Models;
using EstudoMVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EstudoMVC.Controllers
{
    public class ReviewController : Controller
    {
        private readonly MVC_DbContext _context;
        private readonly IReviewService _reviewService;
        private readonly UserManager <User> _userManager; 

        public ReviewController(MVC_DbContext context, IReviewService reviewService, UserManager<User> userManager)
        {
            _context = context;
            _reviewService = reviewService;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Create(TouristAttractionReviewViewModel touristAttractionReviewVM)
        {
            if (!ModelState.IsValid) 
            {
                //touristAttractionReviewVM.Attraction = _context.TouristAttractions.Find(touristAttractionID);
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    // Aqui você pode inspecionar os erros ou registrar em algum lugar
                    Console.WriteLine(error.ErrorMessage);
                }

                // Retorna a view com o modelo para exibir erros
                return View(touristAttractionReviewVM);
            }

            var userId = _userManager.GetUserId(User);

            if (userId == null) 
            {
                return Unauthorized();
            }

            Console.WriteLine("Id: " + touristAttractionReviewVM.Attraction.Id);
            var attraction = await _context.TouristAttractions.FindAsync(touristAttractionReviewVM.Attraction.Id);

            if(attraction == null)
            {
                return NotFound();
            }

            Review review = new Review
            {
                Content = touristAttractionReviewVM.Review.Content,
                CreationDate = DateTime.UtcNow,
                MainExperience = touristAttractionReviewVM.Review.MainExperience,
                SideExperience = touristAttractionReviewVM.Review.SideExperience,
                Score = touristAttractionReviewVM.Review.Score,
                UserId = userId,
                TouristAttractionId = touristAttractionReviewVM.Attraction.Id
            };

            _context.Reviews.Add(review);
            _context.SaveChanges();
            return RedirectToAction("Detail", "TouristAttraction", new { id = touristAttractionReviewVM.Attraction.Id });
        }
    }
}

