using SamsPizzeria.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamsPizzeria.Services
{
    public interface IUserRolesService
    {
        Task<ICollection<UserRolesModificationModel>> GetUsers();

        Task UpdateUserRoles(UserRolesModificationModel user);
    }
}
