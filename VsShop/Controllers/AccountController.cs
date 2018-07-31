using System.Threading.Tasks;
using VsShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VsShop.Auth;
using System.Security.Claims;

namespace VsShop.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "User name / password not found");
            return View(loginViewModel);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = loginViewModel.UserName
                };
                var result = await _userManager.CreateAsync(user, loginViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(loginViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult GoogleLogin(string returnUrl = null)
        {
            var redirectUrl = Url.Action("GoogleLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(ExternalLoginServiceConstants.GoogleProvider, redirectUrl);
            return Challenge(properties, ExternalLoginServiceConstants.GoogleProvider);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GoogleLoginCallback(string returnUrl=null, string serviceError = null)
        {
            if (serviceError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {serviceError}");
            }

            var infor = await _signInManager.GetExternalLoginInfoAsync();
            if (infor == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var result = await _signInManager.ExternalLoginSignInAsync(infor.LoginProvider, infor.ProviderKey, true);
            if (result.Succeeded)
            {
                if (returnUrl == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(returnUrl);
            }

            var user = new ApplicationUser()
            {
                Email = infor.Principal.FindFirst(ClaimTypes.Email).Value,
                UserName = infor.Principal.FindFirst(ClaimTypes.Email).Value
            };

            var identityResult = await _userManager.CreateAsync(user);

            if (!identityResult.Succeeded) return AccessDenied();

            identityResult = await _userManager.AddLoginAsync(user, infor);

            if (!identityResult.Succeeded) return AccessDenied();

            await _signInManager.SignInAsync(user, false);

            if(returnUrl == null)
            {
                return RedirectToAction("index", "home");
            }

            return Redirect(returnUrl);
        }
    }
}