using System;
using System.Linq;
using ITCompany.Data.Entities;
using ITCompany.Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ITCompany.Data.Repositories.EntityFramework
{
    public class EFEventsUsersRepository : IEventsUsersRepository
    {
        private readonly ApplicationDbContext context;

        public EFEventsUsersRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<EventUser> GetEventsUsers()
        {
            return context.EventsUsers;
        }

        public void SaveEventsUsers(EventUser entity)
        {
            context.Entry(entity).State = EntityState.Added;// объект будет добавлен как новый
            context.SaveChanges();
        }

        public void DeleteEventsUsersByEventGuid(string id)
        {
            context.RemoveRange(context.EventsUsers.Where(c => c.EventId == Guid.Parse(id)));
            context.SaveChanges();
        }
    }
}
