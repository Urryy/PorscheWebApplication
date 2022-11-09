using PorscheMarket.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorscheMarket.Domain.Entity
{
    public class User
    {
        public int Id { get; set; }
        //user password.
        public string Password { get; set; }
        //user name.
        public string Name { get; set; }
        //Enum user role(Admin,DefaultUser...).
        public Role Role { get; set; }
        //1 Basket : 1 User.
        public Basket Basket { get; set; }
    }
}
