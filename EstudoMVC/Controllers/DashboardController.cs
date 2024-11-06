using EstudoMVC.DataContent;
using EstudoMVC.Interfaces;
using EstudoMVC.Models;
using EstudoMVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EstudoMVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly MVC_DbContext _context;
        private readonly IReviewService _reviewService;
        private readonly IDashboardService _dashboardService;

        public DashboardController( UserManager<User> userManager, MVC_DbContext context, IReviewService reviewService,
            IDashboardService dashboardService)
        {
            _userManager = userManager;
            _context = context;
            _reviewService = reviewService;
            _dashboardService = dashboardService;            
        }

        public async Task<IActionResult> Index()
        {
            var currentUserId = _userManager.GetUserId(User);
            IEnumerable<Review> reviews = await _reviewService.GetAllById(currentUserId);
            return View(reviews);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string? id)
        {
            var currentUserId = _userManager.GetUserId(User);

            if (id != null)
            {
                Console.WriteLine("User Id" + id);
            }
            else
            {
                id = currentUserId;
            }


            IEnumerable<Review> reviews = await _reviewService.GetAllById(id);

            if(reviews == null)
            {
                Console.WriteLine("reviews == null");
            }
            return View(reviews); // Passando as reviews para a View, se necessário
        }

        public async Task<IActionResult> Edit(int id)
        {
            Console.WriteLine("review id " + id);

            var review = await _reviewService.GetByIdAsync(id);

            if(review  == null)
            {
                return View("Error");
            }

            var reviewVM = new ReviewViewModel
            {
                Id = review.Id,
                Content = review.Content,
                CreationDate = review.CreationDate,
                MainExperience = review.MainExperience,
                SideExperience = review.SideExperience,
                Score = review.Score,
            };

            return View(reviewVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ReviewViewModel reviewViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", reviewViewModel);
            }

            var currentReview = await _reviewService.GetByIdAsyncNoTracking(id);

            if (currentReview != null) 
            {

                var success = await _reviewService.UpdateReviewAsync(id, reviewViewModel);
                
                if (success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(reviewViewModel);
                }
            }
            else
            {
                return View(reviewViewModel);

            }


        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null) 
            {
                return View("Error");
            }

            var reviewModel = new ReviewViewModel
            {
                Id = review.Id,
                Content = review.Content,
                CreationDate = review.CreationDate,
                MainExperience = review.MainExperience,
                SideExperience = review.SideExperience,
                Score = review.Score
            };

            return View(reviewModel);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeteleReview(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null)
            {
                return View("Error");
            }
            _reviewService.Delete(review);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditUserProfile()
        {
            var curUser = await _userManager.GetUserAsync(User);

            if (curUser == null)
            {
                return View("Error");
            }


            return View();
        }
    }
}
