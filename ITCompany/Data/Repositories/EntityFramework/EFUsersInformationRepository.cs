using ITCompany.Data.Entities;
using ITCompany.Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITCompany.Data.Repositories.EntityFramework
{
    public class EFUsersInformationRepository : IUsersInformationRepository
    {
        private readonly ApplicationDbContext context;

        public EFUsersInformationRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public double GetUserInformationByIdAndMonth(Guid id, DateTime date)
        {
            IQueryable<UserInformation> Users = from i in context.UsersInformation
                                                 where i.Id_user == id && i.Date.Year == date.Year && i.Date.Month == date.Month
                                                 select i;
            double hours = 0;
            foreach (UserInformation user in Users)
                hours += user.Hours;

            return hours;
        }

        public double GetUserInformationByIdAllMonth(Guid id)
        {
            IQueryable<UserInformation> Users = from i in context.UsersInformation
                                                 where i.Id_user == id
                                                 select i;
            double hours = 0;
            foreach (UserInformation user in Users)
                hours += user.Hours;

            return hours;
        }

        public void SaveUserInformation(UserInformation entity)
        {
            context.Entry(entity).State = EntityState.Added;// объект будет добавлен как новый
            context.SaveChanges();
        }
    }
}
