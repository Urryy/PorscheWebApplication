using System;
using System.Collections.Generic;
using System.Text;

namespace PorscheMarket.Domain.Entity
{
    public class Order
    {
        public int Id { get; set; }
        //Porsche Id.
        public int? CarId { get; set; }
        //DataCreated Order.
        public DateTime DateCreated { get; set; }
        //Email User.
        public string email { get; set; }
        //Address User.
        public string Address { get; set; }
        //Firstname User.
        public string FirstName { get; set; }
        //LastName User.
        public string LastName { get; set; }
        //MiddleName User.
        public string MiddleName { get; set; }
        //Basket Id.
        public int? BasketId { get; set; }
        // n Orders : 1 Basker.
        public virtual Basket Basket { get; set; }
    }
}
