
using Microsoft.EntityFrameworkCore;
using PorscheMarket.Dal.Interfaces;
using PorscheMarket.Domain.Entity;
using PorscheMarket.Domain.Enum;
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
    public class OrderService:IOrderService
    {
        //Connect order repository.
        private readonly IBaseRepository<Order> _orderRepository;
        //Connect user repository.
        private readonly IBaseRepository<User> _userRepository;
        //Construktor.
        public OrderService(IBaseRepository<User> userRepository, IBaseRepository<Order> orderRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }
        //Create Order.
        public async Task<IBaseResponse<bool>> Create(CreateOrderViewModel model)
        {
            try
            {
                var user = await _userRepository.SelectAll()
                    .Include(x => x.Basket)
                    .FirstOrDefaultAsync(x => x.Name == model.Login);
                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.InternalServerError
                    };
                }
                

                var order = new Order()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    Address = model.Address,
                    email=model.email,
                    DateCreated = DateTime.Now,
                    BasketId = user.Basket.Id,
                    CarId = model.CarId
                };

                await _orderRepository.Create(order);

                return new BaseResponse<bool>()
                {
                    Description = "Заказ создан",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        //Delete Order.
        public async Task<IBaseResponse<bool>> Delete(int id)
        {
            try
            {
                var order= await _orderRepository.SelectAll().FirstOrDefaultAsync(x => x.Id == id);

                var order1 = await _orderRepository.SelectAll()
                    .Select(x => x.Basket.Orders.FirstOrDefault(y => y.Id == id))
                    .FirstOrDefaultAsync();

                if (order != null)
                {
                    await _orderRepository.Delete(order);
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.OK,
                        Data = true
                    };
                }
                else
                {
                    return new BaseResponse<bool>
                    {
                        Description = "Failed",
                        StatusCode = StatusCode.InternalServerError
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description=$"[DeleteOrder] : ${ex.Message}",
                    StatusCode=StatusCode.OK
                };
            }
            

        }
    }
}
