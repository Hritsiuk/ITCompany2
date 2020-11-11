using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCompany.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITCompany.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;
        public UsersController(UserManager<User> _userManager)
        {
            userManager = _userManager;
        }

        public IActionResult Index()
        {
            ViewBag.isAut = HttpContext.User.Identity.IsAuthenticated;
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View(userManager.Users.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.isAut = HttpContext.User.Identity.IsAuthenticated;
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            ViewBag.isAut = HttpContext.User.Identity.IsAuthenticated;
            ViewBag.Name = HttpContext.User.Identity.Name;
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.UserName, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
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

        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.isAut = HttpContext.User.Identity.IsAuthenticated;
            ViewBag.Name = HttpContext.User.Identity.Name;
            User user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, UserName = user.UserName, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            ViewBag.isAut = HttpContext.User.Identity.IsAuthenticated;
            ViewBag.Name = HttpContext.User.Identity.Name;
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.UserName = model.UserName;
                    user.Email = model.Email;

                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            ViewBag.isAut = HttpContext.User.Identity.IsAuthenticated;
            ViewBag.Name = HttpContext.User.Identity.Name;
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}
