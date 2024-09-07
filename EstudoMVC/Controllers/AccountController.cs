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

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

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
            var response = new RegisterViewModel();
            return View(response);

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid)
                return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);

            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVM);

            }
            var newUser = new User()
            {
                Email = registerVM.EmailAddress,
                UserName = registerVM.Username,
                Birth  = registerVM.Birth,
                LockoutEnabled = false,
                LockoutEnd = null
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded) 
            { 
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            return RedirectToAction("Index", "TouristAttraction");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "TouristAttraction");
        }
    }
}
