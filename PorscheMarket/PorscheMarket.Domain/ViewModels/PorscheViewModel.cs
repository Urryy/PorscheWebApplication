using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PorscheMarket.Domain.ViewModels
{
    public class PorscheViewModel
    {

        private string fullNameModel;
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Введите модель")]
        [MinLength(2, ErrorMessage = "Минимальная длина должна быть больше двух символов")]
        public string Name { get; set; }
        [Display(Name = "Комплектация")]
        [Required(ErrorMessage = "Комплектация модели")]
        [MinLength(2, ErrorMessage = "Минимальная длина должна быть больше двух символов")]
        public string Model { get; set; }
        public string FullNameModel
        {
            get { return this.fullNameModel; }
            set { this.fullNameModel = Name + Model; }
        }
        [Display(Name = "Описание автомобиля")]
        public string Description { get; set; }
        [Display(Name = "Стоймость автомобиля")]
        [Required(ErrorMessage = "Введите стоймость за автомобиль")]
        public int Price { get; set; }
        [Display(Name = "Скорость автомобиля")]
        [Required(ErrorMessage = "Введите скорость автомобиля")]
        public int MaxSpeed { get; set; }
        public DateTime CreateDate { get; set; }
        [Display(Name = "Тип автомобиля")]
        [Required(ErrorMessage = "Выберите тип")]
        public string BodyType { get; set; }
        public IFormFile ImgPorsche { get; set; }

        public string ImgString { get; set; }
    }
}
