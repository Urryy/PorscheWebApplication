using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PorscheMarket.Domain.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Введите имя(логин)")]
        [MaxLength(20,ErrorMessage ="Имя(логин) должен иметь длинну меньше 20 значений")]
        [MinLength(3,ErrorMessage = "Имя(логин) должен иметь длинну больше 6 значений")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
