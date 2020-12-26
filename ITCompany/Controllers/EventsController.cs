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
    public class EventsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly DataManager dataManager;
        

        public EventsController(UserManager<User> _userManager, DataManager manager)
        {
            userManager = _userManager;
            dataManager = manager;
        }
        [Authorize]
        public IActionResult Index()
        {
            /*var data = dataManager.EventsUsers.GetEventsUsers();
            Dictionary<Guid, int> keyValues = new Dictionary<Guid, int>();
            int count = 0;
            foreach (EventUser eventUser in data)
            {
                if (count == 0)
                {
                    keyValues.Add(eventUser.EventId, 1);
                }
                else
                {
                    bool isFind = false;
                    for (int i = 0; i < keyValues.Count; i++)
                    {
                        if (keyValues.ContainsKey(eventUser.EventId))
                        {
                            keyValues[eventUser.EventId] += keyValues[eventUser.EventId];
                            isFind = true;
                        }
                    }
                    if (isFind == false)
                        keyValues.Add(eventUser.EventId, 1);
                }
                
            }
            ViewBag.EventCount = keyValues;*/
            return View(dataManager.EventItems.GetEventItems());
        }

        [Authorize(Roles = "admin,moderator")]
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

        [Authorize(Roles = "admin,moderator")]
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

        [Authorize(Roles = "admin,moderator")]
        [HttpPost]
        public IActionResult Delete(string id)
        {
            dataManager.EventItems.DeleteEventItem(id);
            dataManager.EventsUsers.DeleteEventsUsersByEventGuid(id.ToUpper());
            return RedirectToAction("Index");
        }
    }
}
