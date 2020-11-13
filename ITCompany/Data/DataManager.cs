using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCompany.Data.Repositories.Abstract;

namespace ITCompany.Data
{
    public class DataManager
    {
        public IEventItemsRepository EventItems { get; set; }
        public IEventsUsersRepository EventsUsers { get; set; }

        public DataManager(IEventItemsRepository eventItemsRepository, IEventsUsersRepository eventsUsersRepository)
        {
            EventItems = eventItemsRepository;
            EventsUsers = eventsUsersRepository;
        }
    }
}
