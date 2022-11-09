using PorscheMarket.Domain.Entity;
using PorscheMarket.Domain.Interfaces;
using PorscheMarket.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PorscheMarket.Service.Interfaces
{
    public interface IUserService
    {
        //Getting All Users.
        Task<IBaseResponse<IEnumerable<User>>> GetUsers();
        //Getting one user by Id.
        Task<IBaseResponse<User>> GetUserById(int? id);
        //Getting all user roles(Admin,DefaultUser...).
        IBaseResponse<Dictionary<int, string>> GetRoles();
        //Delete user.
        Task<IBaseResponse<bool>> DeleteUser(int? id);
        //Create user.
        Task<IBaseResponse<bool>> CreateUser(UserViewModel userViewModel);
    }
}
