using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PorscheMarket.Domain.ViewModels.Account
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Введите имя(логин)")]
        [MaxLength(20, ErrorMessage = "Имя(логин) должен иметь длинну меньше 20 значений")]
        [MinLength(3, ErrorMessage = "Имя(логин) должен иметь длинну больше 6 значений")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите старый пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Старый Пароль")]
        public string OldPassword { get; set; }


        [Required(ErrorMessage = "Введите новый пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Новый Пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Подтвердите новый пароль")]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmNewPassword { get; set; }
    }
}
