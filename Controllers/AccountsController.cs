using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using StokTakipProjesi2.Models;
using Microsoft.AspNetCore.Authentication;

namespace StokTakipProjesi2.Controllers
{
    public class AccountsController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            //Claim principal sınıfının Userı 
            if (User.Identity?.IsAuthenticated == true)
            {
                var role = User.FindFirstValue(ClaimTypes.Role);

                if (role == "Admin")
                    return RedirectToAction("Dashboard", "Admin");

                if (role == "Store")
                    return RedirectToAction("Dashboard", "Store");

                if (role == "Customer")
                    return RedirectToAction("Dashboard", "Customer");
            }

            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(User signedUser)
        {
            if (Models.User.ValidateUser(signedUser.Username, signedUser.Password))
            {
                var user = Accounts.Users.FirstOrDefault(u => u.Username == signedUser.Username);
                if (user != null)
                {
                    user.isRemembered = signedUser.isRemembered;
                    await AuthenticateUserAsync(user);

                    switch (user.Role)
                    {
                        case "Admin":
                            return RedirectToAction("Dashboard", "Admin");
                        case "Store":
                            return RedirectToAction("Dashboard", "Store");
                        case "Customer":
                            return RedirectToAction("Dashboard", "Customer");
                        default:
                            return View();
                    }
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User registeredUser)
        {
            Accounts.Users.Add(registeredUser);
            return RedirectToAction("Login", "Accounts");
        }








        private async Task<bool> AuthenticateUserAsync(User user)
        {
            List<Claim> claimList = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString())
            };

            var identity = new ClaimsIdentity(claimList, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = user.isRemembered,
                AllowRefresh = true
            };


            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                authProperties
            );

            
            return true;
        }
    }
}
