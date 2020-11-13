using ITCompany.Data.Entities;
using System;
using System.Linq;

namespace ITCompany.Data.Repositories.Abstract
{
    public interface IEventItemsRepository
    {
        IQueryable<EventItem> GetEventItems();
        EventItem GetEventItemById(Guid id);
        void SaveEventItem(EventItem entity);
        void DeleteEventItem(string id);
    }
}
