using System;
using ITCompany.Data.Entities;
using System.Linq;

namespace ITCompany.Data.Repositories.Abstract
{
    public interface IEventsUsersRepository
    {
        IQueryable<EventUser> GetEventsUsers();
        void SaveEventsUsers(EventUser entity);
        void DeleteEventsUsersByEventGuid(string id);
    }
}
