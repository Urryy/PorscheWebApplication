using Microsoft.EntityFrameworkCore;
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
    public class PorscheService : IPorscheService
    {
        private readonly IBaseRepository<Porsche> _porscheRepository;
        public PorscheService(IBaseRepository<Porsche> porscheRepository)
        {
            
            _porscheRepository = porscheRepository;
        }
        //Getting All Porsches from the database
        public async Task<IBaseResponse<IEnumerable<PorscheViewModel>>> GetCars()
        {
            var baseResoponse = new BaseResponse<IEnumerable<PorscheViewModel>>();
            try
            {
                var porsches = await _porscheRepository.SelectAll().Select(porsche=> new PorscheViewModel
                {
                    Id = porsche.Id,
                    Name = porsche.Name,
                    Model = porsche.Model,
                    Description = porsche.Description,
                    Price =porsche.Price,
                    MaxSpeed = porsche.MaxSpeed,
                    CreateDate = porsche.CreateDate,
                    BodyType = porsche.BodyType.GetDisplayName(),
                    ImgString = porsche.ImgString
                }).ToListAsync();
                if (porsches.Count == 0)
                {
                    baseResoponse.Description = $"In list: {porsches.Count} elements";
                    baseResoponse.StatusCode = StatusCode.OK;
                }
                else
                {
                    baseResoponse.Description = $"In List: {porsches.Count} elements";
                    baseResoponse.StatusCode = StatusCode.OK;
                    baseResoponse.Data = porsches;
                }        
                return baseResoponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<PorscheViewModel>>()
                {
                    Description = $"[GetCars] : {e.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        //Getting one Porsche from Database, by Id.
        public async Task<IBaseResponse<PorscheViewModel>> GetCar(int? id)
        {
            int idCur = (int)id;
            var baseResponse = new BaseResponse<PorscheViewModel>();
            try
            {
                var Porsche = await _porscheRepository.SelectAll().FirstOrDefaultAsync(x => x.Id == id);
                var modelPorsche = new PorscheViewModel
                {
                    Id=Porsche.Id,
                    Name=Porsche.Name,
                    Model=Porsche.Model,
                    Description=Porsche.Description,
                    Price=Porsche.Price,
                    MaxSpeed=Porsche.MaxSpeed,
                    CreateDate=Porsche.CreateDate,
                    BodyType=Porsche.BodyType.GetDisplayName(),
                    ImgString=Porsche.ImgString                
                };
                if (Porsche == null)
                {
                    baseResponse.Description = $"Not found object";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                }
                else
                {
                    baseResponse.Data = modelPorsche;
                    baseResponse.Description = $"It's your car";
                    baseResponse.StatusCode = StatusCode.OK;
                }             
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<PorscheViewModel>()
                {
                    Description=$"[GetCar] : {e.Message}",
                    StatusCode=StatusCode.InternalServerError
                };
            }
        }
        //Getting one Porsche from Database ,by Name
        public async Task<IBaseResponse<Porsche>> GetCarByName(string name)
        {
            var baseResponse = new BaseResponse<Porsche>();
            try
            {
                var Porsche = await _porscheRepository.SelectAll().FirstOrDefaultAsync(p => p.Name == name);
                if (Porsche==null)
                {
                    baseResponse.Description = $"Not found object";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                }
                else
                {
                    baseResponse.StatusCode = StatusCode.OK;
                    baseResponse.Description = $"Car: {Porsche.Name}";
                    baseResponse.Data = Porsche;
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Porsche>()
                {
                    Description=$"[GetCarByName] : {ex.Message}",
                    StatusCode=StatusCode.InternalServerError
                };
            }
        }
        //Delete Porsche from Database
        public async Task<IBaseResponse<bool>> DeleteCar(int? id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var responsePorsche = await _porscheRepository.SelectAll().FirstOrDefaultAsync(p=>p.Id==id);
                if (responsePorsche == null)
                {
                    baseResponse.Description = $"Not found object";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                }
                else
                {
                    var result=await _porscheRepository.Delete(responsePorsche);
                    if (result == true)
                    {
                        baseResponse.StatusCode = StatusCode.OK;
                        baseResponse.Description = $"Deleted Car: {responsePorsche.Name}";
                        baseResponse.Data = result;
                    }
                    else
                    {
                        baseResponse.Description = $"Not found object";
                        baseResponse.StatusCode = StatusCode.InternalServerError;
                    }
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteCar] : {ex.Message}",
                    StatusCode=StatusCode.InternalServerError
                };
            }
        }
        //Create Porsche and adding it to the database
        public async Task<IBaseResponse<bool>> CreateCar(PorscheViewModel model, byte[] imageData,string imgString)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var Porsche = new Porsche()
                {
                    Name = model.Name,
                    Model = model.Model,
                    Description = model.Description,
                    Price = model.Price,
                    MaxSpeed = model.MaxSpeed,
                    CreateDate = model.CreateDate,
                    BodyType = (BodyType)Enum.Parse(typeof(BodyType), model.BodyType),
                    ImgPorsche =imageData,
                    ImgString=imgString
                };
                var result=await _porscheRepository.Create(Porsche);
                if (result == true)
                {
                    baseResponse.StatusCode = StatusCode.OK;
                    baseResponse.Description = $"Created Car: {Porsche.Name}";
                    baseResponse.Data = result;
                }
                else
                {
                    baseResponse.Description = $"Not found object";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateCar] : {ex.Message}",
                    StatusCode=StatusCode.InternalServerError
                };
            }
        }
        //Edit Porsche and updating it to the database
        public async Task<IBaseResponse<bool>> EditCar(int? id, PorscheViewModel model)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var porsche = await _porscheRepository.SelectAll().FirstOrDefaultAsync(p=>p.Id==id); ;
                if (porsche == null)
                {
                    baseResponse.Description = $"car not found";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                }
                else
                {
                    porsche.Name = model.Name;
                    porsche.Model = model.Model;
                    porsche.Description = model.Description;
                    porsche.Price = model.Price;
                    porsche.MaxSpeed = model.MaxSpeed;
                    porsche.CreateDate = model.CreateDate;
                    porsche.BodyType = (BodyType)Enum.Parse(typeof(BodyType), model.BodyType); 
                    var result=await _porscheRepository.Update(porsche);
                    if (result)
                    {
                        baseResponse.Description = $"Car by {id} was updated";
                        baseResponse.StatusCode = StatusCode.OK;
                        baseResponse.Data = true;
                        
                    }
                    else
                    {
                        baseResponse.Description = $"car not found";
                        baseResponse.StatusCode = StatusCode.InternalServerError;

                    }
                }
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description=$"[EditCar] : {ex.Message}",
                    StatusCode=StatusCode.InternalServerError
                };
            }
        }
        //Getting all types body Porsche.
        public IBaseResponse<Dictionary<int,string>> getBodyTypes()
        {
            var baseResponse = new BaseResponse<Dictionary<int, string>>();
            try
            {
                var roles = ((BodyType[])Enum.GetValues(typeof(BodyType)))
                    .ToDictionary(k => (int)k, t => (string)t.GetDisplayName());
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
        //Getting Porsche by Pattern
        public async Task<IBaseResponse<Dictionary<int, string>>> GetCarByPattern(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<int, string>>();
            try
            {
                var porsche = await _porscheRepository.SelectAll()
                    .Select(x => new PorscheViewModel
                    {
                        Id = x.Id,
                        MaxSpeed = x.MaxSpeed,
                        Name = x.Name,
                        Description = x.Description,
                        Model = x.Model,
                        CreateDate = x.CreateDate,
                        Price = x.Price,
                        BodyType = x.BodyType.GetDisplayName()
                    })
                    .Where(x => EF.Functions.Like(x.Name, $"%{term}%"))
                    .ToDictionaryAsync(x => x.Id, t => t.Name);
                baseResponse.Data = porsche;
                baseResponse.Description = "Success";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[GetCarByPattern] : {ex.Message}";
                baseResponse.StatusCode = StatusCode.InternalServerError;
                return baseResponse;
            }
        }
    }
}
