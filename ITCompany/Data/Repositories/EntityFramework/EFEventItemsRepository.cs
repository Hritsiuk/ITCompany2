using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCompany.Data.Entities;
using ITCompany.Data.Repositories.Abstract;
using ITCompany.Data;
using Microsoft.EntityFrameworkCore;

namespace ITCompany.Data.Repositories.EntityFramework
{
    public class EFEventItemsRepository : IEventItemsRepository
    {
        private readonly ApplicationDbContext context;

        public EFEventItemsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<EventItem> GetEventItems()
        {
            return context.EventItems;
        }

        public EventItem GetEventItemById(Guid id)
        {
            return context.EventItems.FirstOrDefault(x => x.Id == id);
        }

        public void SaveEventItem(EventItem entity)
        {
            context.Entry(entity).State = EntityState.Added;// объект будет добавлен как новый
            context.SaveChanges();
        }

        public void DeleteEventItem(string id)
        {
            context.EventItems.Remove(new EventItem() { Id = Guid.Parse(id) });
            context.SaveChanges();
        }
    }
}
