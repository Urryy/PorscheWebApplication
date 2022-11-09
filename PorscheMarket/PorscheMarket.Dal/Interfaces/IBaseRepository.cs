using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PorscheMarket.Dal.Interfaces
{
    public interface IBaseRepository<T>
    {

        //Create entity to Database.
        Task<bool> Create(T entity);

        //Getting all entities from Database.
        IQueryable<T> SelectAll();

        //Delete entity from Database.
        Task<bool> Delete(T entity);

        //Updating entity in Database.
        Task<bool> Update(T entity);
    }
}
