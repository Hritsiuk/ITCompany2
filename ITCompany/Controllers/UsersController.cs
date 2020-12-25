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
    [Authorize(Roles = "admin,moderator")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;
        public static CurrentUModel Cr;
        public UsersController(UserManager<User> _userManager)
        {
           /* if (AccountController.Cr != null)
                Cr = AccountController.Cr;
            if (EventsController.Cr != null)
                Cr = EventsController.Cr;
            if (HomeController.Cr != null)
                Cr = HomeController.Cr;
            if (RolesController.Cr != null)
                Cr = RolesController.Cr;*/

            userManager = _userManager;
        }

        public IActionResult Index()
        {
           /* if (Cr != null)
                ViewBag.name = Cr.name + "(" + Cr.position + ")";*/
            return View(userManager.Users.ToList());
        }

        public IActionResult Create()
        {
           /* if (Cr != null)
                ViewBag.name = Cr.name + "(" + Cr.position + ")";*/
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
           /* if (Cr != null)
                ViewBag.name = Cr.name + "(" + Cr.position + ")";*/
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.UserName, Position = model.Position, Email = model.Email };
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
            /*if (Cr != null)
                ViewBag.name = Cr.name + "(" + Cr.position + ")";*/
            User user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, UserName = user.UserName, Position = user.Position, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.Position = model.Position;

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
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}
