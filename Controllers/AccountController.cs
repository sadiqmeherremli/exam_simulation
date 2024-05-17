using CRUD_Identity_Full.ViewModels;
using Database.Entities.Concretes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Identity_Full.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            User user = new User()
            {
                UserName=registerVM.Email,
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                Email = registerVM.Email,
            };
            var result = await _userManager.CreateAsync(user,registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            User user;

            if (loginVM.Email.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(loginVM.Email);
            }
            else
            {
                user = await _userManager.FindByNameAsync(loginVM.Email);
            }

            if (user == null)
            {
                ModelState.AddModelError("", "Username Or Email Incorrect!");
                return View();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginVM.Password, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Try again ");
                return View();
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username Email Incorrect");
                return View();
            }
            await _signInManager.SignInAsync(user, loginVM.RememberMe);

            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]

        public IActionResult Login()
        {
            return View();
        }


        //public async Task<IActionResult> CreateRole()
        //{
        //    foreach (var item in Enum.GetValues(typeof(UserRole)))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole()
        //        {
        //            Name = item.ToString()
        //        });
        //    }
        //    return Ok();
        //}


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
