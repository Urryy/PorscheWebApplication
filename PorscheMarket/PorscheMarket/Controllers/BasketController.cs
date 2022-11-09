using Microsoft.AspNetCore.Mvc;
using PorscheMarket.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PorscheMarket.Controllers
{
    public class BasketController : Controller
    {
        //Connect Basket Service
        private readonly IBasketService _basketService;

        //Construktor
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        //Getting all Orders and show in Basket
        public async Task<IActionResult> GetItems()
        {
            var data = await _basketService.GetItems(User.Identity.Name);
            if (data.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(data.Data.ToList());
            }
            return RedirectToAction("Index", "Home");
        }
        //Getting one order .
        public async Task<IActionResult> GetItem(int id)
        {
            var data = await _basketService.GetItem(User.Identity.Name, id);
            if (data.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView(data.Data);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
