using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PorscheMarket.Domain.Enum
{
    public enum BodyType
    {
        [Display(Name= "SportSedan")]
        SportSedan=0,
        [Display(Name = "Cabriolet")]
        Cabriolet = 1,
        [Display(Name = "Coupe")]
        Coupe = 2,
        [Display(Name = "OffRoad")]
        OffRoad = 3,
        [Display(Name = "Universal")]
        Universal = 4,
    }
}
