using PorscheMarket.Domain.Entity;
using PorscheMarket.Domain.Interfaces;
using PorscheMarket.Domain.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PorscheMarket.Service.Interfaces
{
    public interface IOrderService
    {
        //Create Order.
        Task<IBaseResponse<bool>> Create(CreateOrderViewModel model);
        //Delete Order.
        Task<IBaseResponse<bool>> Delete(int id);
    }
}
