    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PorscheMarket.Domain.ViewModels.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string CarName { get; set; }
        public string Model { get; set; }

        public double Speed { get; set; }

        public string TypeCar { get; set; }

        public string Address { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string email { get; set; }

        public string MiddleName { get; set; }

        public string DateCreate { get; set; }
    }
}
