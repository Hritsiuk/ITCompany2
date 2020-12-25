using ITCompany.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITCompany.Data.Repositories.Abstract
{
    public interface IUsersInformationRepository
    {
        double GetUserInformationByIdAndMonth(Guid id, DateTime date);
        double GetUserInformationByIdAllMonth(Guid id);
        void SaveUserInformation(UserInformation entity);
    }
}
