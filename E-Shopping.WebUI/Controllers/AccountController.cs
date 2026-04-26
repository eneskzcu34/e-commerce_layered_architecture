using E_Shopping.Application.DTOs.AccountDTos;
using E_Shopping.Domain.Interfaces.Repositories;
using E_Shopping.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks;
using Microsoft.Extensions.Options;

namespace WebUI.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IOptions<IdentityOptions> _identityOptions;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<IdentityOptions> identityOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identityOptions = identityOptions;
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [Route("[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTo model)
        {
            var userData = await _userManager.FindByEmailAsync(model.Email);
            if (userData == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı");
                return View(model);
            }
            var userLogin = await _signInManager.PasswordSignInAsync(userData, model.Password, model.RememberMe, true);

            if (userLogin.Succeeded)
            {
                await _userManager.SetLockoutEndDateAsync(userData, null);
                return RedirectToAction("Index", "Home");
            }
            else if (userLogin.IsLockedOut)
            {
                var lockoutEnd = await _userManager.GetLockoutEndDateAsync(userData);
                var lockMinutes = (int)_identityOptions.Value.Lockout.DefaultLockoutTimeSpan.TotalMinutes;

                ModelState.AddModelError("", $"Hesap kilitlendi. {lockMinutes} dakika bekle.Kalan {(lockoutEnd.Value - DateTimeOffset.UtcNow):mm\\:ss}");
            }
            if (userLogin.IsLockedOut)
            {
                ModelState.AddModelError("", "Hesap kilitlendi");
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış");
            }
            return View(model);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [Route("[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTo model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Bu email zaten kayıtlı.");
                return View(model);
            }

            var newUser = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.FirstName + model.LastName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}