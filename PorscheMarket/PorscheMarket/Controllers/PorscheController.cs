using Microsoft.AspNetCore.Mvc;
using PorscheMarket.Service.Interfaces;
using PorscheMarket.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PorscheMarket.Domain.ViewModels;
using System.IO;
using PorscheMarket.Domain.Helpers;

namespace PorscheMarket.Controllers
{
    public class PorscheController : Controller
    {
        //Connect Porsche Service.
        private readonly IPorscheService _porscheService;
        public PorscheController(IPorscheService porscheService)
        {
            _porscheService = porscheService;
        }
        //Getting all the Porsches
        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            var porsches = await _porscheService.GetCars();
            return View(porsches.Data);
        }
        //Getting one Porsche by Id
        [HttpGet]
        public async Task<IActionResult> GetCarById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var porsche = await _porscheService.GetCar(id);
            if (porsche.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(porsche.Data);
            }
            return NotFound();
        }
        //Getting one Porsche by Name
        [HttpGet]
        public async Task<IActionResult> GetCarByName(string name)
        {
            if (name == null)
            {
                return NotFound();
            }
            var porsche = await _porscheService.GetCarByName(name);
            if (porsche.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(porsche.Data);
            }
            return NotFound();
        }
        //Delete Porsche in BD
        [Authorize(Roles="Admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteCar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var porsche = await _porscheService.DeleteCar(id);
            if (porsche.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetAllCars");
            }
            return NotFound();
        }
        //Create Porsche
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> SaveCar(int? id)
        {
            if (id==null)
            {
                return PartialView();
            }
            var porsche = await _porscheService.GetCar(id);
            if (porsche.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView(porsche.Data);
            }
            ModelState.AddModelError("", porsche.Description);
            return PartialView();
        }
        //Create Porsche
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> SaveCar(PorscheViewModel modelPorsche)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                if (modelPorsche.Id != 0)
                {
                    var result=await _porscheService.EditCar(modelPorsche.Id, modelPorsche);
                    if (result.StatusCode == Domain.Enum.StatusCode.OK)
                    {
                        return RedirectToAction("GetAllCars");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    byte[] imageporsche = ConvertFileHelper.ConvertToByte(modelPorsche.ImgPorsche);
                    string imgString = ConvertFileHelper.ReadAsList(modelPorsche.ImgPorsche);
                    var result=await _porscheService.CreateCar(modelPorsche,imageporsche,imgString);
                    if (result.StatusCode==Domain.Enum.StatusCode.OK)
                    {
                        return RedirectToAction("GetAllCars");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            return View();
        }
        //Getting All types car body
        [HttpGet]
        public JsonResult GetTypesBody()
        {
            var types = _porscheService.getBodyTypes();
            if (types.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return new JsonResult(types);
            }
            else
            {
                return new JsonResult(types.StatusCode);
            }
        }
        //Comparison of two cars 
        public IActionResult GetCompareCar() => PartialView();
        [HttpGet]
        public async Task<ActionResult> CompareCar(int id, bool isJson)
        {
            var response = await _porscheService.GetCar(id);
            if (isJson)
            {
                return Json(response.Data);
            }
            return PartialView("GetCompareCar", response.Data);
        }
        //Comparison of two cars 
        [HttpPost]
        public async Task<IActionResult> CompareCar(string term)
        {
            var data = await _porscheService.GetCarByPattern(term);
            if (data.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Json(data.Data);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
    }
}
