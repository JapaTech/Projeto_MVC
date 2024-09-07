using EstudoMVC.DataContent;
using EstudoMVC.Models;
using EstudoMVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EstudoMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly MVC_DbContext _context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            MVC_DbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddres);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "TouristAttraction");
                    }
                }

                TempData["Error"] = "Wrong credentials, Please, try agin";
                return View(loginVM);

            }
            TempData["Error"] = "Wrong credentials, Please, try agin";
            return View(loginVM);
        }

        public IActionResult Register()
        {
            var response = new LoginViewModel();
            return View(response);

        }
    }
}
