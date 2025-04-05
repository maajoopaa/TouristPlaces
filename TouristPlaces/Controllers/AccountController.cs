using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TouristPlaces.Application.Services;
using TouristPlaces.Models;

namespace TouristPlaces.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string username, string password)
        {
            var user = await _userService.Get(username, password);

            if (user is not null)
            {
                var isSuccess = await _userService.Authorize(user, HttpContext);

                if (isSuccess)
                {
                    return Redirect("/Home/Index");
                }

                return Redirect("/Account/SignIn");
            }

            return PartialView(new AccountViewModel
            {
                signInModel = new SignInModel
                {
                    Username = username,
                    Password = password
                },

                Error = "Пароль или имя пользователя введены неверно! Попробуйте еще раз."
            });
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return PartialView(new AccountViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(string username, string password)
        {
            var user = await _userService.Create(username, password);

            if (user is not null)
            {
                var isSuccess = await _userService.Authorize(user, HttpContext);

                if (isSuccess)
                {
                    return Redirect("/Home/Index");
                }

                return Redirect("/Account/SignIn");
            }

            return PartialView(new AccountViewModel
            {
                signUpModel = new SignUpModel
                {
                    Username = username,
                    Password = password
                },

                Error = "Это имя пользователя, попробуйте другое!"
            });
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return PartialView(new AccountViewModel());
        }

        [Authorize]
        public async Task<IActionResult> Quit()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/Home/Index");
        }
    }
}
