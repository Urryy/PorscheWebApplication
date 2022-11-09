using Microsoft.AspNetCore.Mvc;
using PorscheMarket.Domain.Enum;
using PorscheMarket.Domain.ViewModels;
using PorscheMarket.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PorscheMarket.Controllers
{
    public class UserController : Controller
    {
        //Connect User Service.
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //Getting All Users and Sort.
        public async Task<IActionResult> GetUsers(SortState sortOrder = SortState.IdAsc)
        {
            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["IdSort"] = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            var response = await _userService.GetUsers();
            if (response.StatusCode==Domain.Enum.StatusCode.OK)
            {
                response.Data = sortOrder switch
                {
                    SortState.IdDesc=>response.Data.OrderByDescending(s=>s.Id),
                    SortState.NameAsc => response.Data.OrderBy(s => s.Name),
                    SortState.NameDesc => response.Data.OrderByDescending(s => s.Name),
                    _ => response.Data.OrderBy(s => s.Id),
                };
                return View(response.Data);
            }
            else
            {
                return NotFound();
            }
        }
        //Get User By Id
        public async Task<IActionResult> GetUserById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var response = await _userService.GetUserById(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            else
            {
                return NotFound();
            }
        }
        //Create user Form
        public IActionResult CreateUser() => PartialView();

        //Create user by ViewModel
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.CreateUser(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        //Delete User .
        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var response = await _userService.DeleteUser(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK) 
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return NotFound();
            }
        }
        //Getting all user Roles(Admin,Moderator ...)
        [HttpGet]
        public JsonResult GetRoles()
        {
            var response = _userService.GetRoles();
            return Json(response.Data);
        }
    }
}
