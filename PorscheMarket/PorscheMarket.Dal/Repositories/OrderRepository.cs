using PorscheMarket.Dal.Interfaces;
using PorscheMarket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PorscheMarket.Dal.Repositories
{
    public class OrderRepository : IBaseRepository<Order>
    {
        //Connect Database Context.
        private readonly ApplicationDbContext _dbContext;

        //Construktor.
        public OrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //Create entity Order in Database.
        public async Task<bool> Create(Order entity)
        {
            _dbContext.Orders.Add(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        //Delete entity Order in Database.
        public async Task<bool> Delete(Order entity)
        {
            _dbContext.Orders.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        //Getting all entities Orders from Database.
        public IQueryable<Order> SelectAll()
        {
            return _dbContext.Orders;
        }
        //Updating entity Order in Database.
        public async Task<bool> Update(Order entity)
        {
            _dbContext.Orders.Update(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
