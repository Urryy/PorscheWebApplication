using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PorscheMarket.Domain.ViewModels.Order
{
    public class CreateOrderViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Адрес")]
        [Required(ErrorMessage = "Укажите адрес")]
        [MinLength(5, ErrorMessage = "Адрес должен быть больше 5 символов")]
        [MaxLength(200, ErrorMessage = "Адрес должен быть меньше 200 символов")]
        public string Address { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Укажите имя")]
        [MaxLength(20, ErrorMessage = "Имя должно иметь длину меньше 20 символов")]
        [MinLength(3, ErrorMessage = "Имя должно иметь длину больше 3 символов")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [MaxLength(50, ErrorMessage = "Фамилия должно иметь длину меньше 50 символов")]
        [MinLength(2, ErrorMessage = "Фамилия должно иметь длину больше 2 символов")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        [MaxLength(50, ErrorMessage = "Отчество должно иметь длину меньше 50 символов")]
        [MinLength(2, ErrorMessage = "Отчество должно иметь длину больше 2 символов")]
        public string MiddleName { get; set; }
        [Display(Name ="E-mail")]
        [MaxLength(50,ErrorMessage ="Емейл должен быть меньше 50 символов")]
        [MinLength(10,ErrorMessage ="Емейл должен быть больше 10 символов")]
        public string email { get; set; }

        public int CarId { get; set; }

        public string Login { get; set; }
    }
}
