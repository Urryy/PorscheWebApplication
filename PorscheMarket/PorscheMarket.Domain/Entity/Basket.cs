using System;
using System.Collections.Generic;
using System.Text;

namespace PorscheMarket.Domain.Entity
{
    public class Basket
    {
        public int Id { get; set; }
        //1 User : 1 Basket relationship.
        public User User { get; set; }
        //UserId.
        public int UserId { get; set; }
        //1 Basket : n Order relationship
        public List<Order> Orders { get; set; }
    }
}
