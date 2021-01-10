using Cliente.Comunication;
using Cliente.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cliente.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            HttpContext.SignOutAsync();
            return View();
        }
 

        public IActionResult Login()
        {
            HttpContext.SignOutAsync();
            return View();
        }


        public async Task<IActionResult> Authentication([Bind("Username,Password")] Login @user)
        {
            try
            {
                if (user == null || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Username))
                    return View("Login");

                ClaimsIdentity identity = null;
                UserLogin tk = null;
                if ((tk = await LoginComm.Authentication(user)) != null)
                {
                    identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name,tk.User.Name),
                        new Claim(ClaimTypes.Role,tk.User.Role.ToString()),
                        new Claim(ClaimTypes.Rsa,tk.Token),
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    return RedirectToAction("Index");
                }
                else return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
