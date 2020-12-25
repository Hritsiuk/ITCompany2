using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCompany.Data;
using ITCompany.Data.Entities;
using ITCompany.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITCompany.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        //переменные для оперирования пользователями
        public static CurrentUModel Cr;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly DataManager dataManager;

        public AccountController(UserManager<User> userMgr, SignInManager<User> signInMgr, DataManager data)
        {
            /*Cr = new CurrentUModel();
            if (HomeController.Cr != null)
                Cr = HomeController.Cr;
            if (EventsController.Cr != null)
                Cr = EventsController.Cr;
            if (RolesController.Cr != null)
                Cr = RolesController.Cr;
            if (UsersController.Cr != null)
                Cr = UsersController.Cr;*/
            userManager = userMgr;
            signInManager = signInMgr;
            dataManager = data;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        /*Cr.name = user.UserName;
                        Cr.position = user.Position;
                        Cr.time = DateTime.Now;
                        Cr.hour = Cr.hour - DateTime.Now.Hour;*/
                        HttpContext.Response.Cookies.Append("date", DateTime.Now.ToString());
                        return Redirect(returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(LoginViewModel.UserName), "Неверный логин или пароль");
            }
            
            return View(model);
        }

        public IActionResult Register(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View(new RegisterViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.UserName, Email = model.Email };
                // добавляем пользователя
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await signInManager.SignInAsync(user, false);
                    return Redirect(returnUrl ?? "/");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            
            /*Cr = null;
            HomeController.Cr = null;
            EventsController.Cr = null;
            RolesController.Cr = null;
            UsersController.Cr = null;*/
            string date;
            HttpContext.Request.Cookies.TryGetValue("date", out date);
            if (date != null)
            {
                TimeSpan span = DateTime.Now - DateTime.Parse(date);
                User user = userManager.GetUserAsync(HttpContext.User).Result;
                UserInformation inf = new UserInformation
                {
                    Id = Guid.NewGuid(),
                    Id_user = Guid.Parse(user.Id),
                    Date = DateTime.Now,
                    Hours = Math.Round(span.TotalMinutes, 1)/**/
                };
                dataManager.UsersInformation.SaveUserInformation(inf);
            }
            await signInManager.SignOutAsync();
            
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Manage()
        {
            /*if (Cr != null)
                ViewBag.name = Cr.name + "(" + Cr.position + ")";*/

            return View();
        }

        [Authorize]
        public IActionResult Mypage()
        {
            /*if (Cr != null)
                ViewBag.name = Cr.name + "(" + Cr.position + ")";*/
            User user = userManager.GetUserAsync(HttpContext.User).Result;
            ViewBag.Position = user.Position;
            ViewBag.HoursThisMonth = Math.Round(dataManager.UsersInformation.GetUserInformationByIdAndMonth(Guid.Parse(user.Id), DateTime.Now), 1);
            ViewBag.HoursAllMonth = Math.Round(dataManager.UsersInformation.GetUserInformationByIdAllMonth(Guid.Parse(user.Id)), 1);
            return View();
        }
    }
}
