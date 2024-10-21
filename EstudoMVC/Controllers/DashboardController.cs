using EstudoMVC.DataContent;
using EstudoMVC.Interfaces;
using EstudoMVC.Models;
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

        public DashboardController( UserManager<User> userManager, MVC_DbContext context, IReviewService reviewService)
        {
            _userManager = userManager;
            _context = context;
            _reviewService = reviewService;
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
    }
}
