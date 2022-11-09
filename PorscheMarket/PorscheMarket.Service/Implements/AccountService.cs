using Microsoft.EntityFrameworkCore;
using PorscheMarket.Dal.Interfaces;
using PorscheMarket.Domain.Entity;
using PorscheMarket.Domain.Helpers;
using PorscheMarket.Domain.Interfaces;
using PorscheMarket.Domain.Response;
using PorscheMarket.Domain.ViewModels.Account;
using PorscheMarket.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PorscheMarket.Service.Implements
{
    public class AccountService : IAccountService
    {
        //Connect account repository.
        private readonly IBaseRepository<User> _accountRepository;
        //Constuktor.
        public AccountService(IBaseRepository<User> accountRepository)
        {
            _accountRepository = accountRepository;
        }
        //User ChangePassword.
        public async Task<IBaseResponse<bool>> ChangePassword(ChangePasswordViewModel model)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var user = await _accountRepository.SelectAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                if (user == null)
                {
                    baseResponse.Description = $"User not found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
                }
                else
                {
                    if (user.Password != HashPasswordHelper.HashPassword(model.OldPassword))
                    {
                        baseResponse.Description = $"User not found";
                        baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
                        
                    }
                    else
                    {
                        user.Password =HashPasswordHelper.HashPassword(model.NewPassword);
                        await _accountRepository.Update(user);
                        baseResponse.Description = $"Success";
                        baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                        baseResponse.Data = true;

                    }
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = $"[Change Password] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }
        //User LogIn.
        public async Task<IBaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            var baseResponse = new BaseResponse<ClaimsIdentity>();
            try
            {
                var user = await _accountRepository.SelectAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                if (user == null)
                {
                    baseResponse.Description = $"User not found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
                    return baseResponse;
                }
                if (user.Password != HashPasswordHelper.HashPassword(model.Password))
                {
                    baseResponse.Description = $"User not found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
                    return baseResponse;
                }
                var result = Authenticate(user);
                if (result!=null)
                {
                    baseResponse.Data = result;
                    baseResponse.Description = "Success";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                    return baseResponse;
                }
                else
                {
                    baseResponse.Description = $"User not found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
                    return baseResponse;
                }
                

            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = $"[Login] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }
        //User registration.
        public async Task<IBaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            var baseResponse = new BaseResponse<ClaimsIdentity>();
            try
            {
                var user = await _accountRepository.SelectAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                if (user != null)
                {

                    baseResponse.Description = $"User in exist now";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
                    return baseResponse;
                }
                else
                {
                    user = new User()
                    {
                        Name = model.Name,
                        Password = HashPasswordHelper.HashPassword(model.Password),
                        Role = Domain.Enum.Role.User
                    };
                    user.Basket = new Basket
                    {
                        Id = user.Id,
                        UserId = user.Id,
                        User = user,
                    };
                    await _accountRepository.Create(user);
                    var result = Authenticate(user);
                    if (result != null)
                    {
                        baseResponse.Description = "success";
                        baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                        baseResponse.Data = result;
                        
                    }
                    else
                    {
                        baseResponse.Description = "failed opperation";
                        baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
                        
                    }
                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = $"[Login] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }
        //Authenticate user by Name and Role.
        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
