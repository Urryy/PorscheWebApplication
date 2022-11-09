using Microsoft.EntityFrameworkCore;
using PorscheMarket.Dal.Interfaces;
using PorscheMarket.Domain.Entity;
using PorscheMarket.Domain.Extensions;
using PorscheMarket.Domain.Interfaces;
using PorscheMarket.Domain.Response;
using PorscheMarket.Domain.ViewModels.Order;
using PorscheMarket.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PorscheMarket.Service.Implements
{
    public class BasketService : IBasketService
    {
        //Connect basket repository.
        private readonly IBaseRepository<Basket> _basketRepository;
        //Connect porsche repository.
        private readonly IBaseRepository<Porsche> _porscheRepository;
        //Connect user repository.
        private readonly IBaseRepository<User> _userRepository;
        //Construktor.
        public BasketService(IBaseRepository<Basket> basketRepository, IBaseRepository<Porsche> porscheRepository, IBaseRepository<User> userRepository)
        {
            _basketRepository = basketRepository;
            _porscheRepository = porscheRepository;
            _userRepository = userRepository;
        }
        //Getting one order in Basket.
        public async Task<IBaseResponse<OrderViewModel>> GetItem(string userName, int id)
        {
            //user --> basket --> orders.
            var baseResponse = new BaseResponse<OrderViewModel>();
            try
            {
                var user = await _userRepository.SelectAll()
                    .Include(curUser => curUser.Basket)
                    .ThenInclude(curBasket => curBasket.Orders)
                    .FirstOrDefaultAsync(u => u.Name == userName);
                if (user==null)
                {
                    baseResponse.Description = "User not Found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                }
                else
                {
                    var orders = user.Basket?.Orders.Where(order=>order.Id==id).ToList();
                    if(orders==null || orders.Count == 0)
                    {
                        baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
                        baseResponse.Description = "Order not Found";
                    }
                    else
                    {
                        var data = (from order in orders
                            join porsche in _porscheRepository.SelectAll() on order.CarId equals porsche.Id
                            select new OrderViewModel()
                            {
                                Id = order.Id,
                                FirstName = order.FirstName,
                                LastName = order.LastName,
                                MiddleName = order.MiddleName,
                                Address = order.Address,
                                DateCreate = order.DateCreated.ToString(),
                                CarName = porsche.Name,
                                Model = porsche.Model,
                                Speed = porsche.MaxSpeed,
                                email=order.email,
                                TypeCar = porsche.BodyType.GetDisplayName()
                            }).FirstOrDefault();
                        baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                        baseResponse.Data = data;
                    }
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderViewModel>{
                    Description=$"[GetItem] Basket : ${ex.Message}",
                    StatusCode=Domain.Enum.StatusCode.InternalServerError
                };
            }
        }
        //Getting all orders in Basket.
        public async Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetItems(string userName)
        {
            //user --> basket --> orders.
            var baseResponse = new BaseResponse<IEnumerable<OrderViewModel>>();
            try
            {
                var user = await _userRepository.SelectAll()
                    .Include(x => x.Basket)
                    .ThenInclude(y => y.Orders)
                    .FirstOrDefaultAsync(userCur => userCur.Name == userName);
                if (user==null)
                {
                    baseResponse.Description = "Failed";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
                }
                else
                {
                    var orders = user.Basket?.Orders;
                    var data = from order in orders
                        join porsche in _porscheRepository.SelectAll() on order.CarId equals porsche.Id
                        select new OrderViewModel()
                        {
                            Id = order.Id,
                            FirstName = order.FirstName,
                            LastName = order.LastName,
                            MiddleName = order.MiddleName,
                            Address = order.Address,
                            DateCreate = order.DateCreated.ToString(),
                            CarName = porsche.Name,
                            Model = porsche.Model,
                            email=order.email,
                            Speed = porsche.MaxSpeed,
                            TypeCar = porsche.BodyType.GetDisplayName()
                        };
                    baseResponse.Data = data;
                    baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<OrderViewModel>>
                {
                    Description=$"[GetItems] basket : ${ex.Message}",
                    StatusCode=Domain.Enum.StatusCode.InternalServerError
                };
            }
        }
    }
}
