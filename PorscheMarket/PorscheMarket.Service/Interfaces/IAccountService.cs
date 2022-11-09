using PorscheMarket.Domain.Interfaces;
using PorscheMarket.Domain.ViewModels.Account;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PorscheMarket.Service.Interfaces
{
    public interface IAccountService
    {
        //User registration.
        Task<IBaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);
        //User LogIn.
        Task<IBaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
        //User ChangePassword.
        Task<IBaseResponse<bool>> ChangePassword(ChangePasswordViewModel model);

    }
}
