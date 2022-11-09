using PorscheMarket.Dal.Interfaces;
using PorscheMarket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PorscheMarket.Dal.Repositories
{
    public class UserRepository : IBaseRepository<User>
    {
        //Connect Database Context.
        private readonly ApplicationDbContext _db;
        //Construktor.
        public UserRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }
        //Create Entity User in Database.
        public async Task<bool> Create(User entity)
        {
            await _db.Users.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }
        //Delete Entity User from Database.
        public async Task<bool> Delete(User entity)
        {
            _db.Users.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }
        //Getting all Entities Users from Database.
        public IQueryable<User> SelectAll()
        {
            return _db.Users;
        }
        //Updating Entity User in Database.
        public async Task<bool> Update(User entity)
        {
            _db.Users.Update(entity);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
