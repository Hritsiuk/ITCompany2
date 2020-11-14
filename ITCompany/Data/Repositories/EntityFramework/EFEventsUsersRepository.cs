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
            //Guid eventId = Guid.Parse(id);
            //context.Database.ExecuteSqlCommand("DELETE FROM EventsUsers WHERE EventId = {0}", id);
            context.Database.ExecuteSqlInterpolated($"DELETE FROM EventsUsers WHERE EventId = {id}");
            //context.EventsUsers.RemoveRange(context.EventsUsers.Where(c => c.EventId == eventId));
            //context.SaveChanges();
        }
    }
}
