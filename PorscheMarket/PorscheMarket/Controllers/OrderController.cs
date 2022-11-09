using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using PorscheMarket.Domain.ViewModels.Order;
using PorscheMarket.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PorscheMarket.Controllers
{
    public class OrderController : Controller
    {
        //Connect Order Service
        private readonly IOrderService _orderSerivce;
        //Construktor
        public OrderController(IOrderService orderSerivce)
        {
            _orderSerivce = orderSerivce;
        }
        //Add order to Basket
        [Authorize]
        public IActionResult Create(int id)
        {
            var orderModel = new CreateOrderViewModel()
            {
                CarId = id,
                Login = User.Identity.Name,
            };
            return PartialView(orderModel);
        }
        //Add order to Basket
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _orderSerivce.Create(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("GetAllCars", "Porsche");
                }
            }
            return NotFound();
        }
        //Delete order in basket
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var response = await _orderSerivce.Delete(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetAllCars", "Porsche");
            }
            return NotFound();
        }
        //Confirm order and Send Email.
        [HttpPost]
        public bool ConfirmOrder(string email)
        {
            MailAddress fromAddress = new MailAddress("yatsko19791@gmail.com", "Porsche");
            MailAddress toAddress = new MailAddress(email,"text");
            MailMessage message = new MailMessage(fromAddress, toAddress);
            message.Body = "Ваш заказ подтвержден ожидайте подтверждение от Администратора, Спасибо за заказ";
            message.Subject = "Подтверждение заказа";


            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "ketyyzpletmnlvim")
            };

            smtp.Send(message);
            return true;

        }
    }
}