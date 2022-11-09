using PorscheMarket.Domain.Interfaces;
using PorscheMarket.Domain.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PorscheMarket.Service.Interfaces
{
    public interface IBasketService
    {
        //Getting all orders in Basket.
        Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetItems(string userName);
        //Getting one order in Basket.
        Task<IBaseResponse<OrderViewModel>> GetItem(string userName, int id);
    }
}
