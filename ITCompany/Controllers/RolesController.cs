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
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> roleManager;
        UserManager<User> userManager;
        public RolesController(RoleManager<IdentityRole> _roleManager, UserManager<User> _userManager)
        {
            roleManager = _roleManager;
            userManager = _userManager;
        }
        public IActionResult Index()
        {
            ViewBag.isAut = HttpContext.User.Identity.IsAuthenticated;
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View(roleManager.Roles.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.isAut = HttpContext.User.Identity.IsAuthenticated;
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            ViewBag.isAut = HttpContext.User.Identity.IsAuthenticated;
            ViewBag.Name = HttpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
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
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            ViewBag.isAut = HttpContext.User.Identity.IsAuthenticated;
            ViewBag.Name = HttpContext.User.Identity.Name;
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }

        public IActionResult UserList()
        {
            ViewBag.isAut = HttpContext.User.Identity.IsAuthenticated;
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View(userManager.Users.ToList());
        }

        public async Task<IActionResult> Edit(string userId)
        {
            ViewBag.isAut = HttpContext.User.Identity.IsAuthenticated;
            ViewBag.Name = HttpContext.User.Identity.Name;
            // получаем пользователя
            User user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await userManager.GetRolesAsync(user);
                var allRoles = roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            ViewBag.isAut = HttpContext.User.Identity.IsAuthenticated;
            ViewBag.Name = HttpContext.User.Identity.Name;
            // получаем пользователя
            User user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await userManager.AddToRolesAsync(user, addedRoles);

                await userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }
    }
}
