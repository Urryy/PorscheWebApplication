using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PorscheMarket.Dal.Interfaces;
using PorscheMarket.Domain.Entity;
using PorscheMarket.Domain.Enum;
using PorscheMarket.Domain.Extensions;
using PorscheMarket.Domain.Helpers;
using PorscheMarket.Domain.Interfaces;
using PorscheMarket.Domain.Response;
using PorscheMarket.Domain.ViewModels;
using PorscheMarket.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PorscheMarket.Service.Implements
{
    public class UserService : IUserService
    {
        //Connect user repository and looger.
        private readonly IBaseRepository<User> _baseRepository;
        private readonly ILogger<UserService> _logger;
        //Construktor.
        public UserService(IBaseRepository<User> baseRepository, ILogger<UserService> logger)
        {
            _baseRepository = baseRepository;
            _logger = logger;
        }
        //Create user.
        public async Task<IBaseResponse<bool>> CreateUser(UserViewModel userViewModel)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var user =await _baseRepository.SelectAll().FirstOrDefaultAsync(x=>x.Name==userViewModel.Name);
                if (user !=null)
                {
                    return new BaseResponse<bool>
                    {
                        Description = $"[CreateUser] : The user exists with this name",
                        StatusCode = StatusCode.InternalServerError
                    };
                }

                var User = new User
                {
                    Name=userViewModel.Name,
                    Password = HashPasswordHelper.HashPassword(userViewModel.Password),
                    Role =Enum.Parse<Role>(userViewModel.Role)
                };
                user.Basket = new Basket
                {
                    Id = user.Id,
                    UserId = user.Id,
                    User = user,
                };
                var result = await _baseRepository.Create(User);
                if (result)
                {
                    baseResponse.Data = result;
                    baseResponse.Description = "Success";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                else
                {
                    baseResponse.Data = result;
                    baseResponse.Description = "Failed";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    return baseResponse;
                }
                
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description=$"[CreateUser] : {ex.Message}",
                    StatusCode=Domain.Enum.StatusCode.InternalServerError
                };
            }
        }
        //Delete user.
        public async Task<IBaseResponse<bool>> DeleteUser(int? id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var User = await _baseRepository.SelectAll().FirstOrDefaultAsync(x => x.Id == id);
                if (User != null)
                {
                    var result = await _baseRepository.Delete(User);
                    if (result)
                    {
                        baseResponse.Data = result;
                        baseResponse.Description = "Success";
                        baseResponse.StatusCode = StatusCode.OK;
                        return baseResponse;
                    }
                    else
                    {
                        baseResponse.Data = result;
                        baseResponse.Description = "Failed";
                        baseResponse.StatusCode = StatusCode.InternalServerError;
                        return baseResponse;
                    }
                }
                else
                {
                    return new BaseResponse<bool>
                    {
                        Description = $"[DeleteUser] : Failed",
                        StatusCode = StatusCode.InternalServerError
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description=$"[DeleteUser] : {ex.Message}",
                    StatusCode=StatusCode.InternalServerError
                };
            }
        }
        //Getting all user roles(Admin,DefaultUser...).
        public IBaseResponse<Dictionary<int, string>> GetRoles()
        {
            var baseResponse = new BaseResponse<Dictionary<int, string>>();
            try
            {
                var roles = ((Role[])Enum.GetValues(typeof(Role))).ToDictionary(k => (int)k, t => (string)t.GetDisplayName());
                baseResponse.Data = roles;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                baseResponse.StatusCode = StatusCode.InternalServerError;
                baseResponse.Description = $"[GetRoles] : {ex.Message}";
                return baseResponse;
            }
        }
        //Getting one user by Id.
        public async Task<IBaseResponse<User>> GetUserById(int? id)
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                var user = await _baseRepository.SelectAll().FirstOrDefaultAsync(x=>x.Id==id);
                if (user != null)
                {
                    baseResponse.Data = user;
                    baseResponse.Description = "Success";
                    baseResponse.StatusCode = StatusCode.OK;
                }
                else
                {
                    baseResponse.Description = "Failed";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>
                {
                    Description = $"[GetUserById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        //Getting All Users.
        public async Task<IBaseResponse<IEnumerable<User>>> GetUsers()
        {
            var baseResponse = new BaseResponse<IEnumerable<User>>();
            try
            {
                var users = await _baseRepository.SelectAll().ToListAsync();
                if (users != null)
                {
                    baseResponse.Data = users;
                    baseResponse.Description = "Success";
                    baseResponse.StatusCode = StatusCode.OK;
                }
                else
                {
                    baseResponse.Description = "Failed";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<User>>
                {
                    Description = $"[GetUserById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
