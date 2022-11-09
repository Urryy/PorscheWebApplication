using PorscheMarket.Dal.Interfaces;
using PorscheMarket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PorscheMarket.Dal.Repositories
{
    public class BasketRepository : IBaseRepository<Basket>
    {
        //Connect Database Context.
        private readonly ApplicationDbContext _dbContext;
        //Construktor.
        public BasketRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //Create Entity Basket.
        public async Task<bool> Create(Basket entity)
        {
            await _dbContext.Baskets.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        //Delete Entity Basket.
        public async Task<bool> Delete(Basket entity)
        {
            _dbContext.Baskets.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        //Getting All Entities Baskets From Database.
        public IQueryable<Basket> SelectAll()
        {
            return _dbContext.Baskets;
        }
        //Updating Entity Basket.
        public async Task<bool> Update(Basket entity)
        {
            _dbContext.Baskets.Update(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
