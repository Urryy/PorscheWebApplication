using PorscheMarket.Domain.Entity;
using PorscheMarket.Domain.Interfaces;
using PorscheMarket.Domain.Response;
using PorscheMarket.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PorscheMarket.Service.Interfaces
{
    public interface IPorscheService
    {
        //Getting All Porsches from the database.
        Task<IBaseResponse<IEnumerable<PorscheViewModel>>> GetCars();
        //Getting one Porsche from Database, by Id.
        Task<IBaseResponse<PorscheViewModel>> GetCar(int? id);
        //Getting one Porsche from Database, by Pattern.
        Task<IBaseResponse<Dictionary<int,string>>> GetCarByPattern(string term);
        //Getting one Porsche from Database, by Name.
        Task<IBaseResponse<Porsche>> GetCarByName(string name);
        //Delete Porsche from Database.
        Task<IBaseResponse<bool>> DeleteCar(int? id);
        //Create Porsche from Database.
        Task<IBaseResponse<bool>> CreateCar(PorscheViewModel model,byte[] imgData,string imgString);
        //Update Porsche from Database.
        Task<IBaseResponse<bool>> EditCar(int? id,PorscheViewModel model);
        //Get All BodyTypes in Porsche(Universal,Sedan...).
        IBaseResponse<Dictionary<int, string>> getBodyTypes();
    }
}
