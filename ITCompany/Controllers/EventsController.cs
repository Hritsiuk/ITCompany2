using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCompany.Data;
using ITCompany.Models;
using ITCompany.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITCompany.Controllers
{
    [Authorize(Roles = "admin,moderator")]
    public class EventsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly DataManager dataManager;
        

        public EventsController(UserManager<User> _userManager, DataManager manager)
        {
            userManager = _userManager;
            dataManager = manager;
        }

        public IActionResult Index()
        {
            return View(dataManager.EventItems.GetEventItems());
        }

        public IActionResult Create()
        {
            var users = userManager.Users.ToList();
            List<string> userNames = new List<string>();

            foreach (User user in users)
                userNames.Add(user.UserName);

            CreateEventViewModel model = new CreateEventViewModel
            {
                Users = userNames
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventViewModel model, List<string> names)
        {
            if (ModelState.IsValid)
            {
               
                Guid guid = Guid.NewGuid();
                model.Id = guid;
                dataManager.EventItems.SaveEventItem(new EventItem { Id = model.Id, Name = model.Name, DateStart = model.DateStart, DateEnd = model.DateEnd });
                foreach(string name in names)
                {
                    User user = await userManager.FindByNameAsync(name);
                    EventUser eventUser = new EventUser { Id = Guid.NewGuid(), EventId = model.Id, UserId = user.Id };
                    dataManager.EventsUsers.SaveEventsUsers(eventUser);
                }
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            dataManager.EventItems.DeleteEventItem(id);
            dataManager.EventsUsers.DeleteEventsUsersByEventGuid(id.ToUpper());
            return RedirectToAction("Index");
        }
    }
}
